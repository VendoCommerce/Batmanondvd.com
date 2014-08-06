using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Web;

namespace CSWeb.Admin
{
    public partial class CacheUtility : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BaseLoad();

                lbltext.Text = this.Context.Cache.Count.ToString();

                foreach (DictionaryEntry item in this.Context.Cache)
                {

                    ddlList.Items.Add(new ListItem(item.Key as string, item.Key as string));

                }

                ddlList.Items.Insert(0, new ListItem("Select", "Select"));
                ddlList.DataBind();
            }

        }

        protected void btnAction_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string selectedItem = ddlList.SelectedItem.Value;
            object cacheData = this.Context.Cache[selectedItem];
            if (cacheData != null)
            {
                this.Context.Cache.Remove(selectedItem);
            }

            lbltext.Text = this.Context.Cache.Count.ToString();

            PopulateCacheContents();
        }

        protected void lbViewCache_Click(object sender, EventArgs e)
        {
            PopulateCacheContents();
        }

        private void PopulateCacheContents()
        {
            try
            {
                StringBuilder cacheContent = new StringBuilder();
                                
                foreach (DictionaryEntry item in this.Context.Cache)
                {
                    cacheContent.Append(string.Format("<p><strong>*** {0}</strong>:<br/>", (string)item.Key));
                    cacheContent.Append(HttpUtility.HtmlEncode(CSCore.Serializer.Serialize(this.Context.Cache[(string)item.Key])));
                }

                lblCacheContent.Text = cacheContent.Length > 0 ? cacheContent.ToString() + "</p>" : "Cache is empty.";
            }
            catch (Exception ex)
            {
                lblCacheContent.Text = string.Format("Error encountered: {0}; {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
            }
        }
    }
}