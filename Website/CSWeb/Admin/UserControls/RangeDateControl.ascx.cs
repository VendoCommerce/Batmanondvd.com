using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSCore;


namespace CSWeb.Admin.UserControls
{
    public partial class RangeDateControl : UserControl
    {
        public enum MaximumDateRange
        {
            None = 0,
            OneWeek = 1
        }

        #region Private Variables
        private bool m_displayDropDown = false;
        private string m_labelStyle = string.Empty;
        private string m_postbackFunction = string.Empty;
        #endregion

        protected DateControl dateControlStart;
        protected DateControl dateControlEnd;

        #region Properties
        public string GetStartDateControlID
        {
            get { return dateControlStart.TextID; }
        }

        public string GetEndDateControlID
        {
            get { return dateControlEnd.TextID; }
        }

        public MaximumDateRange MaxDateRange
        {
            get
            {
                if (ViewState["MaxDateRange"] != null)
                    return (MaximumDateRange)ViewState["MaxDateRange"];
                else
                    return MaximumDateRange.None;
            }
            set { ViewState["MaxDateRange"] = value; }
        }

        public string DefaultSelection
        {
            get
            {
                return (ViewState["DefaultSelection"] != null) ? ViewState["DefaultSelection"].ToString() : "Select";
            }
            set { ViewState["DefaultSelection"] = value; }
        }

        public string DropDownSelectionSuffix
        {
            get { return labelDropDownSelectionSuffix.Text; }
            set { labelDropDownSelectionSuffix.Text = value; }
        }

        public string LabelStartText
        {
            get { return labelStart.Text; }
            set { labelStart.Text = value; }
        }

        public string LabelEndText
        {
            get { return labelEnd.Text; }
            set { labelEnd.Text = value; }
        }

        public string ValidationGroup
        {
            get { return compareValidatorDateFields.ValidationGroup; }
            set { compareValidatorDateFields.ValidationGroup = value; }
        }

        public bool DateRequired
        {
            get
            {
                if (ViewState["DateRequired"] != null)
                    return (bool)ViewState["DateRequired"];
                else
                    return false;
            }
            set { ViewState["DateRequired"] = value; }
        }

        public Unit StartDateWidth
        {
            get { return ((DateControl)dateControlStart).TextboxWidth; }
            set { ((DateControl)dateControlStart).TextboxWidth = value; }
        }

        public Unit EndDateWidth
        {
            get { return ((DateControl)dateControlEnd).TextboxWidth; }
            set { ((DateControl)dateControlEnd).TextboxWidth = value; }
        }

        public bool DisplayDropDown
        {
            get { return m_displayDropDown; }
            set { m_displayDropDown = value; }
        }

        public string DateRangeValue
        {
            get { return (dropDownListDate.SelectedItem != null) ? dropDownListDate.SelectedItem.Value : String.Empty; }
        }

        public DateTime? StartDateValueLocal
        {
            get { return ((DateControl)dateControlStart).ValueLocal; }
            set { ((DateControl)dateControlStart).ValueLocal = value; }
        }


        public DateTime? EndDateValueLocal
        {
            get { return ((DateControl)dateControlEnd).ValueLocal; }
            set { ((DateControl)dateControlEnd).ValueLocal = value; }
        }



        public string LabelStyle
        {
            get { return m_labelStyle; }
            set { m_labelStyle = value; }
        }
        public string PostbackFunction
        {
            get { return m_postbackFunction; }
            set { m_postbackFunction = value; }
        }

        #endregion








        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            compareValidatorDateFields.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            labelStart.CssClass = m_labelStyle;
            labelEnd.CssClass = m_labelStyle;

            if (!Page.IsPostBack)
            {
                SetSelection();
            }
        }

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "RangeDateControl"))
            { Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RangeDateControl", ClientScriptGenerator()); }

            ((DateControl)dateControlStart).IsRequired = DateRequired;
            ((DateControl)dateControlEnd).IsRequired = DateRequired;
            dateControlStart.DataBind();
            dateControlEnd.DataBind();
            labelStart.DataBind();
            labelEnd.DataBind();
            labelDropDownSelectionSuffix.DataBind();

            if (!Page.IsPostBack)
            {
                if (m_displayDropDown)
                {
                    placeHolderDropDown.Visible = true;

                    dropDownListDate.Attributes.Add("onchange", "DropDownListDateOnChange(this.options[this.selectedIndex].value, \"" + ((DateControl)dateControlStart).TextID + "\", \"" + ((DateControl)dateControlEnd).TextID + "\", \"" + ((DateControl)dateControlStart).ExtenderID + "\", \"" + ((DateControl)dateControlEnd).ExtenderID + "\")");
                    dropDownListDate.DataSource = DateSelection();
                    dropDownListDate.DataTextField = "Text";
                    dropDownListDate.DataValueField = "Value";
                    dropDownListDate.DataBind();
                }
                else
                { placeHolderDropDown.Visible = false; }
            }
        }
        #endregion

        #region Private Functions
        private ListItemCollection DateSelection()
        {
            ListItemCollection listBoxData = new ListItemCollection();

            switch (MaxDateRange)
            {
                case MaximumDateRange.None:
                    listBoxData.Add(new ListItem("-Date Range-", ""));
                    listBoxData.Add(new ListItem("Today", "Today"));
                    listBoxData.Add(new ListItem("Yesterday", "Yesterday"));
                    listBoxData.Add(new ListItem("This Week", "ThisWeek"));
                    listBoxData.Add(new ListItem("This Month", "ThisMonth"));
                    listBoxData.Add(new ListItem("This Year", "ThisYear"));
                    break;
                case MaximumDateRange.OneWeek:
                    listBoxData.Add(new ListItem("-Date Range-", ""));
                    listBoxData.Add(new ListItem("Yesterday", "Yesterday"));
                    listBoxData.Add(new ListItem("Today", "Today"));
                    listBoxData.Add(new ListItem("Tomorrow", "Tomorrow"));
                    listBoxData.Add(new ListItem("Last Week", "LastWeek"));
                    listBoxData.Add(new ListItem("Next Week", "NextWeek"));
                    listBoxData.Add(new ListItem("Last 7 Days", "Last7"));
                    listBoxData.Add(new ListItem("Next 7 Days", "Next7"));
                    break;
            }

            return listBoxData;
        }

        private void SetSelection()
        {
            DateTime defaultStartDate;
            DateTime defaultEndDate;

            switch (DefaultSelection)
            {

                default:
                    defaultStartDate = DateTime.UtcNow.Date;
                    defaultEndDate = DateTime.UtcNow.Date;
                    break;
            }

            if (DefaultSelection != "Select")
            {
                ((DateControl)dateControlStart).ValueLocal = new DateTime(defaultStartDate.Year, defaultStartDate.Month, defaultStartDate.Day, 0, 0, 0);
                ((DateControl)dateControlEnd).ValueLocal = new DateTime(defaultEndDate.Year, defaultEndDate.Month, defaultEndDate.Day, 23, 59, 59);
                dropDownListDate.SelectedValue = DefaultSelection;
            }
        }

        private string ClientScriptGenerator()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<script language='javascript'>");
            sb.AppendLine("function DropDownListDateOnChange(selectedValue, startDateControl, endDateControl, startDateExtender, endDateExtender)");
            sb.AppendLine("{");
            sb.Append(" if (selectedValue == '') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.MonthFirstDay(DateTime.Now.Month, DateTime.Now.Year).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTime.Now.ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'Yesterday') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.PriorDate(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.PriorDate(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'Today') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTime.Now.ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTime.Now.ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'Last7') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.LastDays(DateTime.Now, 6).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTime.Now.ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'Last30') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.LastDays(DateTime.Now, 30).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTime.Now.ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'Last60') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.LastDays(DateTime.Now, 60).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTime.Now.ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'Last120') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.LastDays(DateTime.Now, 120).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTime.Now.ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'ThisMonth') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.MonthFirstDay(DateTime.Now.Month, DateTime.Now.Year).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.MonthLastDay(DateTime.Now.Month, DateTime.Now.Year).ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'LastMonth') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.PriorMonthFirstDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.PriorMonthLastDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'ThisWeek') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.WeekFirstDay(DateTime.Now).AddDays(1).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.WeekLastDay(DateTime.Now).AddDays(1).ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'LastWeek') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.PriorWeekFirstDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.PriorWeekLastDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'ThisQuarter') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.QuarterFirstDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.QuarterLastDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'LastQuarter') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.PriorQuarterFirstDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.PriorQuarterLastDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" } if (selectedValue == 'ThisYear') { \n");
            sb.Append(" document.getElementById(startDateControl).value = '" + DateTimeUtil.YearFirstDay(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" document.getElementById(endDateControl).value = '" + DateTimeUtil.LastDayOfYear(DateTime.Now).ToShortDateString() + "'; \n");
            sb.Append(" } \n");
            //sb.Append(" $find(startDateExtender).raiseDateSelectionChanged();\n");
            //sb.Append(" $find(endDateExtender).raiseDateSelectionChanged();\n");
            sb.Append(" document.getElementById(startDateControl).focus(); \n");
            sb.Append(" document.getElementById(startDateControl).blur(); \n");
            sb.Append(" document.getElementById(endDateControl).focus(); \n");
            sb.Append(" document.getElementById(endDateControl).blur(); \n");
            if (PostbackFunction != string.Empty && PostbackFunction.Length > 0)
            {
                sb.Append(" __doPostBack('" + PostbackFunction + "') \n");
            }

            sb.Append(" }</script> \n");
            return sb.ToString();
        }
        #endregion
    }
}