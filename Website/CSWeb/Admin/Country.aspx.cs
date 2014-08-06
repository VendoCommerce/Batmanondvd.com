using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness;
using CSBusiness.Cache;

namespace CSWeb.Admin
{
    public partial class Country : BasePage  
    {
        public List<CSBusiness.Country> displayValues;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BaseLoad();
            if (!Page.IsPostBack)
            {
                lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
                lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
                //Response.Write(Session.SessionID);
                displayValues = new List<CSBusiness.Country>();
                BindCounty();
               
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["Items"] = displayValues;
        }

        protected void BindCounty()
        {
            List<CSBusiness.Country> MasterList = CountryManager.GetAllMasterCountries();
               CountryCache cache = new CountryCache(this.Context);
               List<CSBusiness.Country> list = (List<CSBusiness.Country>) cache.Value;
               foreach (CSBusiness.Country item in list)
               {
                   CSBusiness.Country existItem = MasterList.FirstOrDefault(x => x.CountryId == item.CountryId);
                   if (existItem != null)
                   {
                       MasterList.Remove(existItem);
                   }
               }

                ddlProducts.DataSource = MasterList;                             
                ddlProducts.DataTextField = "Name";
                ddlProducts.DataValueField = "CountryId";
                ddlProducts.DataBind();
                ddlProducts.Items.Insert(0, new ListItem("Select", ""));
             
        }



        protected void btn_AddCountry(object sender, EventArgs e)
        {
            displayValues = (List<CSBusiness.Country>)ViewState["Items"];
            int countryId = Convert.ToInt32(ddlProducts.SelectedItem.Value);
            string countryName = ddlProducts.SelectedItem.Text;

            CSBusiness.Country item = displayValues.FirstOrDefault(x => x.CountryId == countryId);
            if (item ==  null)
            {
                displayValues.Add(new  CSBusiness.Country(countryId, countryName) );
            }

            BindItem();

        }

        protected void dlrepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
           
            switch (e.CommandName)
            {
                case "Delete":
              
                    displayValues = (List<CSBusiness.Country>)ViewState["Items"];
                    CSBusiness.Country item = displayValues.FirstOrDefault(x => x.CountryId == Convert.ToInt32(e.CommandArgument));
                    displayValues.Remove(item);
                    BindItem();
                    break;
               
            }
        }

        protected void BindItem()
        {
            rptItems.DataSource = displayValues;
            rptItems.DataBind();
        }

        protected void btnSave_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                displayValues = (List<CSBusiness.Country>)ViewState["Items"];
                if (displayValues.Count > 0)
                {
                    List<CSBusiness.Country> MasterList = CountryManager.GetAllMasterCountries();
                    foreach (CSBusiness.Country item in displayValues)
                    {
                        CSBusiness.Country existItem = MasterList.FirstOrDefault(x => x.CountryId == item.CountryId);
                        if (existItem != null)
                        {
                            item.Code = existItem.Code;
                            item.OrderNo = existItem.OrderNo;
                        }
                    }

                    CountryManager.CreateCountry(displayValues);

                    CountryManager.ResetCountryCache();
                }

                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            else
            {
                Response.Redirect("CountryList.aspx");
                
            }

            
            
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
           
            //Show ModalPopup 
         

            //mpeThePopup.Hide();
        } 


    }
}