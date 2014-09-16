using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSData;
using System.Xml.Linq;
using System.Web;
using System.Configuration;
using CSCore;

namespace CSBusiness.PostSale
{
    public class PathManager
    {
        #region Paths
        public List<Path> GetAllPaths(int versionId, bool active)
        {
            List<Path> PathList = new List<Path>();
            using (SqlDataReader reader = PostSaleDAL.GetAllPaths(versionId,active))
            {
                while (reader.Read())
                {
                    Path item = new Path();
                    item.PathId = Convert.ToInt32(reader["PathId"]);
                    item.Title = reader["Title"].ToString();
                    item.Weight = Convert.ToDecimal(reader["Weight"]);
                    item.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    item.Active = Convert.ToBoolean(reader["Active"]);
                    PathList.Add(item);

                }

            }
            return PathList;
        }

        public List<Pair<int, int>> GetPathOrderProcess()
        {
            List<Pair<int, int>> PathList = new List<Pair<int, int>>();
            using (SqlDataReader reader = PostSaleDAL.GetPathOrderProcess())
            {
                while (reader.Read())
                {
                    Pair<int, int>  item = new Pair<int, int> ();
                    item.Item1 = Convert.ToInt32(reader["pathId"]);
                    item.Item2 = Convert.ToInt32(reader["pathCount"]);
                  
                    PathList.Add(item);

                }

            }
            return PathList;
        }

        public Path  GetUpSalePath(int pathId, bool expired)
        {
            Path PathItem = new Path();
            using (SqlDataReader reader = PostSaleDAL.GetUpSalePath(pathId, expired))
            {
              
                while (reader.Read())
                {

                    PathItem.PathId = Convert.ToInt32(reader["PathId"]);
                    PathItem.Title = reader["Title"].ToString();
                    PathItem.Weight = Convert.ToDecimal(reader["Weight"]);
                    PathItem.CreateDate = Convert.ToDateTime(reader["CreateDate"]);

                }

                PathItem.Templates = new List<Template>();
                reader.NextResult();
                while (reader.Read())
                {
                    Template templateItem = new Template();
                    templateItem.TemplateId = Convert.ToInt32(reader["TemplateId"]);
                    templateItem.OrderNo = Convert.ToInt32(reader["Order"]);
                    PathItem.Templates.Add(templateItem);
                }

                PathItem.Versions = new List<Int32>();
                reader.NextResult();
                while (reader.Read())
                {
                    PathItem.Versions.Add(Convert.ToInt32(reader["VersionId"]));
                }                

            }
            return PathItem;
        }

        public void RemovePath(int pathId)
        {
            PostSaleDAL.RemovePath(pathId);
        }

        public void SaveUpsalePath(Path pathItem)
        {
            
            XElement rootNode = new XElement("root");
            XElement rootNodeVersions = new XElement("root");
            foreach (Template item in pathItem.Templates)
            {
                XElement xElem = new XElement("item",
                                                new XAttribute("Id", item.TemplateId.ToString()),
                                                new XAttribute("orderno", item.OrderNo.ToString())
                                              );
                rootNode.Add(xElem);
            }
            foreach (Int32 item in pathItem.Versions)
            {
                XElement xElem = new XElement("item",
                                                new XAttribute("Id", item.ToString())
                                              );
                rootNodeVersions.Add(xElem);
            }

            PostSaleDAL.SaveUpsalePath(pathItem.PathId, pathItem.Title, pathItem.Code, pathItem.Weight, rootNode.ToString(), rootNodeVersions.ToString());
        }

        public void SavePath(List<Path> Values)
        {
            XElement rootNode = new XElement("root");
            foreach (Path item in Values)
            {
                XElement xElem = new XElement("item",
                                                new XAttribute("Id", item.PathId.ToString()),
                                                new XAttribute("weight", item.Weight),
                                                new XAttribute("active", item.Active)
                                              );
                rootNode.Add(xElem);
            }

            PostSaleDAL.SavePath(rootNode.ToString());

        } 

		public Path GetPath(HttpContext context, int VersionId)
		{
            
             //Path Caluclation:
             //Check Cookie exist or not
             //Check any override setting defined at webconfig
             //pull from DB Setting and determine single path or random path calculation             

			var cookie = context.Request.Cookies["CS-PathId"];
			int pathID = -1;
			if (cookie != null && int.TryParse(cookie.Value, out pathID))
			{
				return GetUpSalePath(pathID, false); 
			}
			else if (ConfigurationManager.AppSettings["PathSetting"] != null && int.TryParse(ConfigurationManager.AppSettings["PathSetting"], out pathID))
			{
				return GetUpSalePath(pathID, false);
			}
			else
			{
                return GetRandomPath(VersionId);
			}
		}

		private Path GetRandomPath(int versionId)
		{
            List<Path> allActivePaths = GetAllPaths(versionId, true).Where(p => p.Active).OrderBy(p => p.PathId).ToList();
			if (allActivePaths.Count > 1)
			{

                /// <summary>
                /// Sri Comment: This information only pulls more than one active path. 
                /// Pull latest pathCount from DB. Eventually we can move to cache latter
                /// </summary>
            
                List<Pair<int, int>> ItemList = GetPathOrderProcess();
                foreach (Pair<int, int> item in ItemList)
                {
                    Path pathItem = allActivePaths.FirstOrDefault(p => p.PathId == item.Item1);
                    if (pathItem != null)
                    {
                        pathItem.ProcessedCount = item.Item2;
                    }
                }

				decimal totalExpectedWeight = allActivePaths.Sum(p => p.Weight);
				decimal totalActualWeight = allActivePaths.Sum(p => p.ProcessedCount);

				var weightedPaths = allActivePaths.Select(p => new
				{
							Path = p,
							Expected = totalExpectedWeight != 0 ? p.Weight / totalExpectedWeight : 0, 
							Actual = totalActualWeight != 0 ? p.ProcessedCount / totalActualWeight : 0
				});

				List<Path> availablePaths = weightedPaths.Where(p => p.Actual <= p.Expected).Select(p => p.Path).ToList();
				Random rnd = new Random();
				int nexRandom = rnd.Next(0, availablePaths.Count - 1);

                return GetUpSalePath(availablePaths[nexRandom].PathId, false);
			}
			else if (allActivePaths.Count == 1)
			{
				return GetUpSalePath(allActivePaths[0].PathId, false);
			}
			else
			{
				return null;
			}
		}

        private Path GetRandomPathOld(int versionId)
        {
            List<Path> allActivePaths = GetAllPaths(versionId, true).OrderBy(p => p.PathId).ToList();


            if (allActivePaths.Count == 1)
            {
                return GetUpSalePath(allActivePaths[0].PathId, false);
               
            }
            else if (allActivePaths.Count > 1)  //Single Path Setup
            {
                Random rnd = new Random();
                decimal nexRandom = (decimal)rnd.NextDouble();

                decimal totalWeight = allActivePaths.Sum(p => p.Weight);
                decimal currentWeight = 0M;
                for (int i = 0; i < allActivePaths.Count; i++)
                {
                    currentWeight += allActivePaths[i].Weight;
                    if ((currentWeight / totalWeight) > nexRandom)
                    {
                        return GetUpSalePath(allActivePaths[i].PathId, false);
                    }
                }

                //We should never get here, but just in case
                //let's just return the last item
                return GetUpSalePath(allActivePaths[allActivePaths.Count - 1].PathId, false);
            }
            else
            {
                return null;
            }
        }
		#endregion Paths   

        #region Templates
           public List<Template> GetAllTemplates(bool includeExpired)
        {
            List<Template> PathList = new List<Template>();
            using (SqlDataReader reader = PostSaleDAL.GetAllTemplates(includeExpired))
            {
                while (reader.Read())
                {
                    Template item = new Template();
                    item.TemplateId = Convert.ToInt32(reader["TemplateId"]);
                    item.Title = reader["Name"].ToString();
                    item.ExpireDate = Convert.ToDateTime(reader["ExpireDate"]);
                    item.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    item.HideRemove = Convert.ToBoolean(reader["flag"]);
                    PathList.Add(item);

                }

            }
            return PathList;
        }

       public void RemoveTemplate(int templateId)
        {
            PostSaleDAL.RemoveTemplate(templateId);
        }

           public Template GetTemplate(int templateId)
           {
               Template templateItem = new Template();
               using (SqlDataReader reader = PostSaleDAL.GetTemplate(templateId))
               {
                   while (reader.Read())
                   {

                       templateItem.TemplateId = templateId;
                       templateItem.Title = reader["Name"].ToString();
                       templateItem.Body = reader["Body"].ToString();
                       templateItem.Script = reader["Script"].ToString();
                       templateItem.Tag = reader["Tag"].ToString();
                       templateItem.UriLabel = reader["URILabel"].ToString();
                       if( reader["ExpireDate"] !=null)
                           templateItem.ExpireDate = Convert.ToDateTime(reader["ExpireDate"]);

                   }

                   templateItem.Items = new List<TemplateSku>();
                   reader.NextResult();

                   while (reader.Read())
                   {

                       templateItem.Items.Add(new TemplateSku { SkuId = Convert.ToInt32(reader["SkuId"]), TypeId = (TemplateItemTypeEnum) Convert.ToInt32(reader["TypeId"]) });

                   }

                   templateItem.ControlItems = new List<TemplateControl>();
                   reader.NextResult();

                   while (reader.Read())
                   {
                       templateItem.ControlItems.Add(new TemplateControl() { StateId = (int?)reader["StateId"], DisableTemplate = (bool?)reader["DisableTemplate"] });
                   }

               }
               return templateItem;
           }

           public void SaveTemplate(Template templateItem)
           {

               XElement rootNode = new XElement("root");
               foreach (TemplateSku item in templateItem.Items)
               {
                   XElement xElem = new XElement("item",
                                                   new XAttribute("skuId", item.SkuId.ToString()),
                                                   new XAttribute("typeId", ((int)item.TypeId).ToString())
                                                 );
                   rootNode.Add(xElem);
               }

               foreach (TemplateControl item in templateItem.ControlItems)
               {
                   XElement xElem = new XElement("controlItem",
                                                   new XAttribute("stateId", item.StateId.Value.ToString()),
                                                   new XAttribute("disableTemplate", item.DisableTemplate.HasValue ? (item.DisableTemplate.Value ? "1" : "0") : "")
                                                 );
                   rootNode.Add(xElem);
               }

               PostSaleDAL.SaveTemplate(templateItem.TemplateId, templateItem.Title, templateItem.Body, templateItem.Script, templateItem.Tag, templateItem.UriLabel, 
                  templateItem.ExpireDate, rootNode.ToString());
           }

           public void CopyTemplate(int templateId)
           {
               PostSaleDAL.CopyTemplate(templateId);
           }
        #endregion Templates
    }
}
