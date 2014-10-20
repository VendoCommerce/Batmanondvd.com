using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSWeb.Admin.UserControls
{
    [ValidationProperty("ValueLocalTest")]
    public partial class DateControl : UserControl
    {
        protected RangeValidator rangeValidatorMinMax;

        protected static readonly DateTime MinDate = new DateTime(1900, 1, 2);
        protected static readonly DateTime MaxDate = new DateTime(2079, 5, 6);

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

        }

        public string RequiredValidatorId
        {
            get { return valRequired.ClientID; }
        }

        public string RangeValidatorId
        {
            get { return rangeValidatorMinMax.ClientID; }
        }

        public string TypeCheckValidatorId
        {
            get { return valValidDate.ClientID; }
        }




        #region Properties
        public Unit TextboxWidth
        {
            get { return textboxDate.Width; }
            set { textboxDate.Width = value; }
        }

        public string CssClass
        {
            get { return textboxDate.CssClass; }
            set { textboxDate.CssClass = value; }
        }


        public string ToolTip
        {
            get { return (string)ViewState["ToolTip"] ?? String.Empty; }
            set { ViewState["ToolTip"] = value; }
        }

        public bool DisplayCalenderImage
        {
            get
            {
                if (ViewState["ControlEnabled"] == null)
                    return false;
                else
                    return (bool)ViewState["ControlEnabled"];
            }
            set
            {
                ViewState["ControlEnabled"] = value;
            }
        }

        /// <summary>
        /// Gets the date as local time
        /// </summary>
        public DateTime? ValueLocal
        {
            get
            {
                DateTime selectedValue;
                if (DateTime.TryParse(textboxDate.Text, out selectedValue))
                    return selectedValue;
                else
                    return null;
            }
            set
            {
                textboxDate.Text = value.HasValue ? value.Value.ToShortDateString() : string.Empty;
            }
        }



        /// <summary>
        /// When true, should disable all sorts of processing for the control in events, processing that 
        /// requires the Session State and so on.
        /// </summary>
        public bool Enabled
        {
            get
            {
                if (ViewState["ControlEnabled"] == null)
                    return true;
                else
                    return (bool)ViewState["ControlEnabled"];
            }
            set
            {
                ViewState["ControlEnabled"] = value;
            }
        }





        /// <summary>
        /// Used in conjunction with external validators
        /// </summary>
        public string ValueLocalTest
        {
            get
            { return textboxDate.Text; }
        }

        /// <summary>
        /// Gets or sets whether the date is required
        /// </summary>
        public bool IsRequired
        {
            get
            { return valRequired.Enabled; }
            set
            { valRequired.Enabled = value; }
        }

        /// <summary>
        /// Will use this as title in the validation
        /// </summary>
        public string Title
        {
            get { return (string)ViewState["Title"]; }
            set { ViewState["Title"] = value; }
        }

        /// <summary>
        /// The range type used for validation
        /// </summary>
        public DataRangeValidationType RangeType
        {
            get
            {
                if (ViewState["DataRangeValidationType"] == null)
                { return DataRangeValidationType.Any; }
                else
                { return (DataRangeValidationType)ViewState["DataRangeValidationType"]; }
            }
            set { ViewState["DataRangeValidationType"] = value; }
        }

        public DateTime? CutoffDate
        {
            get
            {
                if (ViewState["CutoffDate"] != null)
                { return (DateTime)ViewState["CutoffDate"]; }
                else
                { return null; }
            }
            set
            {
                if (value == null)
                { ViewState["CutoffDate"] = null; }
                else
                { ViewState["CutoffDate"] = value; }
            }
        }

        public bool ValidationEnabled
        {
            get { return ViewState["ValidationEnabled"] == null ? true : (bool)ViewState["ValidationEnabled"]; }
            set
            {
                ViewState["ValidationEnabled"] = value;
                rangeValidatorMinMax.Enabled = value;
                rangeVal.Enabled = value;
                valRequired.Enabled = value && IsRequired;
                valValidDate.Enabled = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (ViewState["ErrorMessage"] == null)
                {
                    if (Title == null)
                        return "* This field is required";
                    else
                        return string.Format("* {0} is required", Title);
                }
                else
                {
                    return ViewState["ErrorMessage"].ToString();
                }

            }
            set { ViewState["ErrorMessage"] = value; }
        }

        public string TextBoxCssClass
        {
            get { return textboxDate.CssClass; }
            set { textboxDate.CssClass = value; }
        }


        public string RequiredErrorMessage
        {
            get { return (string)ViewState["RequiredErrorMessage"]; }
            set { ViewState["RequiredErrorMessage"] = value; }

        }

        public string ValidationGroup
        {
            get { return (string)ViewState["ValidationGroup"]; }
            set { ViewState["ValidationGroup"] = value; }
        }

        public string TextID
        {
            get { return textboxDate.ClientID; }
        }

        public string ExtenderID
        {
            get { return calendarExtenderDate.ClientID; }
        }

        public bool ReadOnly
        {
            get { return textboxDate.ReadOnly; }
            set { textboxDate.ReadOnly = value; }
        }
        #endregion

        #region Event Handlers
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            valRequired.DataBind();
            valValidDate.DataBind();
            rangeValidatorMinMax.MaximumValue = MaxDate.ToShortDateString();
            rangeValidatorMinMax.MinimumValue = MinDate.ToShortDateString();
            rangeValidatorMinMax.ErrorMessage = "* Date must be between 1/2/1900 and 6/5/2079";
            //calendericon.Visible = DisplayCalenderImage;

            //string style = "#ffffff url(" + ResolveUrl("~/images/icon/icon_calendar.gif") + ") no-repeat right center";
            //textboxDate.Style.Add("background", style);
        }

        protected void rangeVal_Validate(object sender, ServerValidateEventArgs e)
        {

            DateTime today = DateTime.Now;
            if (ValueLocal.HasValue)
            {
                if (RangeType == DataRangeValidationType.Any)
                {
                    e.IsValid = true;
                }
                else if (RangeType == DataRangeValidationType.Future)
                {
                    if (CutoffDate.HasValue)
                        e.IsValid = (ValueLocal.Value.Date > today) && (ValueLocal.Value.Date < CutoffDate.Value.Date);
                    else
                        e.IsValid = (ValueLocal.Value.Date > today);
                    rangeVal.ErrorMessage = "* Date must be in the future.";
                }
                else if (RangeType == DataRangeValidationType.FutureIncludingToday)
                {
                    if (CutoffDate.HasValue)
                        e.IsValid = (ValueLocal.Value.Date >= today) && (ValueLocal.Value.Date < CutoffDate.Value.Date);
                    else
                        e.IsValid = ValueLocal.Value.Date >= today;
                    rangeVal.ErrorMessage = "* Date must be today or in the future.";
                }
                else if (RangeType == DataRangeValidationType.Past)
                {
                    if (CutoffDate.HasValue)
                        e.IsValid = ValueLocal.Value.Date < today && ValueLocal.Value.Date > CutoffDate.Value.Date;
                    else
                        e.IsValid = ValueLocal.Value.Date < today;
                    rangeVal.ErrorMessage = "* Date must be in the past.";
                }
                else if (RangeType == DataRangeValidationType.PastIncludingToday)
                {
                    if (CutoffDate.HasValue)
                        e.IsValid = ValueLocal.Value.Date <= today && ValueLocal.Value.Date > CutoffDate.Value.Date;
                    else
                        e.IsValid = ValueLocal.Value.Date <= today;
                    rangeVal.ErrorMessage = "* Date must be today or in the past.";
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //if (Enabled && Visible)
            if (Visible)
            {
                valRequired.ValidationGroup = ValidationGroup;
                rangeVal.ValidationGroup = ValidationGroup;
                rangeValidatorMinMax.ValidationGroup = ValidationGroup;
                valValidDate.ValidationGroup = ValidationGroup;
                valRequired.ErrorMessage = string.IsNullOrEmpty(RequiredErrorMessage) ? ErrorMessage : RequiredErrorMessage;
                valRequired.Visible = IsRequired;
            }


            rangeValidatorMinMax.Enabled = ValidationEnabled;
            rangeVal.Enabled = ValidationEnabled;
            valRequired.Enabled = ValidationEnabled && IsRequired;
            valValidDate.Enabled = ValidationEnabled;
            if (!ValidationEnabled)
            {
                rangeValidatorMinMax.IsValid = true;
                rangeVal.IsValid = true;
                valRequired.IsValid = true;
                valValidDate.IsValid = true;
            }
            if (ReadOnly)
            {
                textboxDate.Enabled = false;
                calendarExtenderDate.Enabled = false;
            }
        }
        #endregion

        public bool IsValid
        {
            get
            {
                if (ValidationEnabled)
                    return rangeValidatorMinMax.IsValid && rangeVal.IsValid && valRequired.IsValid && valValidDate.IsValid;
                else
                    return true;
            }
        }


    }

    public enum DataRangeValidationType
    {
        Any,
        Past,
        PastIncludingToday,
        Future,
        FutureIncludingToday
    }
}
