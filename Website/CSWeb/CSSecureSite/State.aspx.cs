using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBusiness.Cache;
using CSBusiness;

namespace CSWeb.Admin
{
    public partial class State : BasePage
    {

        public int countryId;
        protected void Page_Load(object sender, EventArgs e)
        {
           if(Request.Params["Id"] != null)
              countryId = Convert.ToInt32(Request.Params["Id"]);
           else
               countryId = 0;

            if (!Page.IsPostBack)
            {
                this.BaseLoad();
                lblSuccess.Text = ResourceHelper.GetResoureValue("LabelSuccess");
                lblCancel.Text = ResourceHelper.GetResoureValue("LabelCancel");
                BindStates();
            }
        }


        public void BindStates()
        {
            //pull the list from Master location

            List<StateProvince> MasterList =  StateManager.GetAllMasterStates(countryId);
            StateCache cache = new StateCache(this.Context);
            List<StateProvince> list = ((List<StateProvince>)cache.Value).FindAll(x => x.CountryId == countryId);
            foreach (StateProvince item in list)
            {
                StateProvince existItem = MasterList.FirstOrDefault(x => x.StateProvinceId == item.StateProvinceId);
                if (existItem != null)
                {
                    existItem.Visible = true;
                }
            }

            dlStateList.DataSource = MasterList;
            dlStateList.DataKeyField = "StateProvinceId";
            dlStateList.DataBind();
        }

        protected void dlCountryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
             
                CheckBox cbVisible = e.Item.FindControl("cbVisible") as CheckBox;
                cbVisible.Attributes.Add("onclick", "CheckAll(this,'" + cbVisible.ClientID + "');");


            }
        }


        protected void btnSave_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                List<StateProvince> States = new List<StateProvince>();
                foreach (DataListItem lst in dlStateList.Items)
                {
                    if ((lst.ItemType == ListItemType.Item) || (lst.ItemType == ListItemType.AlternatingItem))
                    {

                        int stateId = (int)dlStateList.DataKeys[lst.ItemIndex];
                        Label lblCode = (Label)lst.FindControl("lblCode");
                        Label lblTitle = (Label)lst.FindControl("lblTitle");
                        TextBox txtOrderNo = (TextBox)lst.FindControl("txtOrderNo");
                        CheckBox cbVisible = (CheckBox)lst.FindControl("cbVisible");
                        bool active = cbVisible.Checked;
                        int orderNo = 0;
                        if (txtOrderNo.Text.Length > 0)
                            orderNo = Convert.ToInt32(txtOrderNo.Text);
                        StateProvince StateItem = new StateProvince { StateProvinceId = stateId, Name = lblTitle.Text, Abbreviation = lblCode.Text, CountryId = countryId, DisplayOrder = orderNo, Visible = active };
                        States.Add(StateItem);
                    }
                }

                StateManager.SaveStates(States);
                StateManager.ResetStateCache();
                lblSuccess.Visible = true;
                lblCancel.Visible = false;
            }
            else
            {
                Response.Redirect("CountryList.aspx");

            }
            
        }


    }
}