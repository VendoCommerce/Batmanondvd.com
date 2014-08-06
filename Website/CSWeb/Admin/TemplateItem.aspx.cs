 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness.Resolver;
using CSBusiness;
using CSBusiness.PostSale;
using CSCore.Utils;


namespace CSWeb.Admin
{
    public partial class TemplateItem : System.Web.UI.Page
    {
        public int templateId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["templateId"] != null)
              templateId = Convert.ToInt32(Request["templateId"].ToString());


            if (!Page.IsPostBack)
            {
                lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
                lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
                BindSkus();
                PopulateStates();
                if (templateId > 0)
                {
                    LoadTemplate(templateId);
                }

                
            }

        }

        protected void BindSkus()
        {
            List<Sku> skuItems = CSResolve.Resolve<ISkuService>().GetAllSkus();
            lstSku.DataSource = skuItems;
            lstSku.DataTextField = "SkuTitleCode";
            lstSku.DataBind();

            lstTriggerItem.DataSource = skuItems;
            lstTriggerItem.DataTextField = "SkuTitleCode"; 
            lstTriggerItem.DataBind();

            lstSuppressItem.DataSource = skuItems;
            lstSuppressItem.DataTextField = "SkuTitleCode";
            lstSuppressItem.DataBind();

            lstRemoveItems.DataSource = skuItems;
            lstRemoveItems.DataTextField = "SkuTitleCode";
            lstRemoveItems.DataBind();

            lstAddItems.DataSource = skuItems;
            lstAddItems.DataTextField = "SkuTitleCode";
            lstAddItems.DataBind();
        }

        protected void PopulateStates()
        {
            List<ListItem> items = new List<ListItem>();
            Dictionary<int, string> countryNames = new Dictionary<int, string>();

            foreach (StateProvince state in StateManager.GetCacheStates())
            {
                if (!countryNames.ContainsKey(state.CountryId))
                    countryNames.Add(state.CountryId, CountryManager.GetCacheCountry().First(x => { return x.CountryId == state.CountryId; }).Name);
                
                items.Add(new ListItem(string.Format("{0} - {1}", countryNames[state.CountryId], state.Name), Convert.ToString(state.StateProvinceId)));
            }

            lstDisableStates.DataTextField = "Text";
            lstDisableStates.DataValueField = "Value";
            lstDisableStates.DataSource = items;
            lstDisableStates.DataBind();            
        }

        protected void LoadTemplate(int templateId)
        {
            Template templateItem = new PathManager().GetTemplate(templateId);
            txtName.Text = templateItem.Title;
            ftbShortDesc.Content = templateItem.Body;
            txtScript.Text = templateItem.Script;
			//ftbShortDesc.EnableHtmlMode = true;
            txtTag.Text = templateItem.Tag;
            txtURILabel.Text = templateItem.UriLabel;
            if(templateItem.ExpireDate.Value.Year != 2079)
                  dateControlStart.ValueLocal = templateItem.ExpireDate;

            List<TemplateSku> selectedItems = templateItem.Items.FindAll(x => x.TypeId == TemplateItemTypeEnum.ListItems);
            foreach (TemplateSku item in selectedItems)
            {
                ListItem listItem = lstSku.Items.FindByValue(item.SkuId.ToString());
                listItem.Selected = true;
            }

            List<TemplateSku> triggerItems = templateItem.Items.FindAll(x => x.TypeId == TemplateItemTypeEnum.Triggers);
            foreach (TemplateSku item in triggerItems)
            {
                ListItem listItem = lstTriggerItem.Items.FindByValue(item.SkuId.ToString());
                listItem.Selected = true;
            }

            List<TemplateSku> supressItems = templateItem.Items.FindAll(x => x.TypeId == TemplateItemTypeEnum.Supress);
            foreach (TemplateSku item in supressItems)
            {
                ListItem listItem = lstSuppressItem.Items.FindByValue(item.SkuId.ToString());
                listItem.Selected = true;
            }

            List<TemplateSku> addItems = templateItem.Items.FindAll(x => x.TypeId == TemplateItemTypeEnum.Add);
            foreach (TemplateSku item in addItems)
            {
                ListItem listItem = lstAddItems.Items.FindByValue(item.SkuId.ToString());
                listItem.Selected = true;
            }

            //remove items bind
            List<TemplateSku> removeItems = templateItem.Items.FindAll(x => x.TypeId == TemplateItemTypeEnum.Remove);
            foreach (TemplateSku item in removeItems)
            {
                ListItem listItem = lstRemoveItems.Items.FindByValue(item.SkuId.ToString());
                listItem.Selected = true;
            }

            foreach (CSBusiness.PostSale.TemplateControl item in templateItem.ControlItems)
            {
                if (item.StateId.HasValue && item.DisableTemplate.HasValue && item.DisableTemplate.Value)
                {
                    lstDisableStates.Items.FindByValue(item.StateId.Value.ToString()).Selected = true;
                }
            }
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                if (Page.IsValid)
                {
                    Template item = new Template();
                    item.TemplateId = templateId;
                    item.Title = CommonHelper.fixquotesAccents(txtName.Text);
                    item.Body = ftbShortDesc.Content;
                    item.Script = txtScript.Text;
                    item.Tag = CommonHelper.fixquotesAccents(txtTag.Text);
                    item.UriLabel = CommonHelper.fixquotesAccents(txtURILabel.Text);
                    if (dateControlStart.ValueLocal.HasValue)
                        item.ExpireDate = Convert.ToDateTime(dateControlStart.ValueLocal.ToString());
                    else
                        item.ExpireDate = Convert.ToDateTime("1/1/2079");

                    item.Items = new List<TemplateSku>();
                    item.ControlItems = new List<CSBusiness.PostSale.TemplateControl>();

                    foreach (ListItem x in lstSku.Items)
                    {
                        if (x.Selected)
                            item.Items.Add(new TemplateSku
                                {
                                    SkuId = Int32.Parse(x.Value),
                                    TypeId = TemplateItemTypeEnum.ListItems
                                });
                    }

                    foreach (ListItem x in lstTriggerItem.Items)
                    {
                        if (x.Selected)
                            item.Items.Add(new TemplateSku
                                {
                                    SkuId = Int32.Parse(x.Value),
                                    TypeId = TemplateItemTypeEnum.Triggers
                                });
                    }

                    foreach (ListItem x in lstSuppressItem.Items)
                    {
                        if (x.Selected)
                            item.Items.Add(new TemplateSku
                                {
                                    SkuId = Int32.Parse(x.Value),
                                    TypeId = TemplateItemTypeEnum.Supress
                                });
                    }

                    foreach (ListItem x in lstAddItems.Items)
                    {
                        if (x.Selected)
                            item.Items.Add(new TemplateSku
                                {
                                    SkuId = Int32.Parse(x.Value),
                                    TypeId = TemplateItemTypeEnum.Add
                                });
                    }

                    foreach (ListItem x in lstRemoveItems.Items)
                    {
                        if (x.Selected)
                            item.Items.Add(new TemplateSku
                                {
                                    SkuId = Int32.Parse(x.Value),
                                    TypeId = TemplateItemTypeEnum.Remove
                                });
                    }

                    foreach (ListItem x in lstDisableStates.Items)
                    {
                        if (x.Selected)
                            item.ControlItems.Add(new CSBusiness.PostSale.TemplateControl()
                                {
                                    DisableTemplate = true,
                                    StateId = Convert.ToInt32(x.Value)
                                });
                    }

                    new PathManager().SaveTemplate(item);
                    lblCancel.Visible = false;
                    lblSuccess.Visible = true;
                }
            }
            else
            {
                Response.Redirect("TemplateList.aspx");
            }

            
        }
    }
}