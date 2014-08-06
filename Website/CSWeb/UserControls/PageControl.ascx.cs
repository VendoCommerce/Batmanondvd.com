using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace CSWeb.Root.UserControls
{
    /// <summary>
    ///		Summary description for PageControl.
    /// </summary>

    // PageChange events args
    public class PageChangeArguments : EventArgs
    {
        private int pageNum;
        public PageChangeArguments(int PageNum)
        {
            this.pageNum = PageNum;
        }
        public int PageNum
        {
            get
            {
                return pageNum;
            }
        }
    }

    public enum PageControlDiplayMode
    {
        Dropdown = 0,
        Links = 1
    }

    // Event delegate definition
    public delegate void PageChangeEventHandler(Object sender, PageChangeArguments e);


    public class PageControl : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.LinkButton Prev;
        protected System.Web.UI.WebControls.DropDownList dl;
        protected System.Web.UI.WebControls.LinkButton Next;
        protected System.Web.UI.WebControls.LinkButton firstPageLink;
        protected System.Web.UI.WebControls.LinkButton previousPageLink;
        protected System.Web.UI.WebControls.LinkButton nextPageLink;
        protected System.Web.UI.WebControls.LinkButton lastPageLink;
                
        protected PlaceHolder holderDropdownMode, holderLinksMode, holderPrewLinks, holderNextLinks;
        protected Repeater pagingLinksRepeater;
        protected Label lblResults;
        public event PageChangeEventHandler PageChanged;
        protected PlaceHolder holderHideOnOnePage;
        public PageControlDiplayMode Mode
        {
            get { return ViewState["Mode"] == null ? PageControlDiplayMode.Dropdown : (PageControlDiplayMode)ViewState["Mode"]; }
            set { ViewState["Mode"] = value; }
        }

        public bool CausesValidation
        {
            get { return ViewState["CausesValidation"] == null ? false : Convert.ToBoolean(ViewState["CausesValidation"]); }
            set { ViewState["CausesValidation"] = value; }
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                Prev.Text = @"&#171;&#160; Previous";
                Next.Text = @"Next &#160;&#187;";
                firstPageLink.DataBind();
                previousPageLink.DataBind();
                nextPageLink.DataBind();
                lastPageLink.DataBind();
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            holderDropdownMode.Visible = (Mode == PageControlDiplayMode.Dropdown);
            holderLinksMode.Visible = (Mode == PageControlDiplayMode.Links);
            base.OnPreRender(e);
        }

        protected void pagingLinksRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton pageSelector = (LinkButton)e.Item.FindControl("pageSelector");
                Label lblCurrentPage = (Label)e.Item.FindControl("lblCurrentPage");
                if (Convert.ToInt32(e.Item.DataItem) == CurrentPage)
                {
                    lblCurrentPage.Visible = true;
                    pageSelector.Visible = false;
                    lblCurrentPage.Text = (Convert.ToInt32(e.Item.DataItem) < 10) ? ("&nbsp;" + e.Item.DataItem.ToString() + "&nbsp;") : e.Item.DataItem.ToString();
                }
                else
                {
                    pageSelector.Text = (Convert.ToInt32(e.Item.DataItem) < 10) ? ("&nbsp;" + e.Item.DataItem.ToString() + "&nbsp;") : e.Item.DataItem.ToString();
                    pageSelector.CommandArgument = e.Item.DataItem.ToString();
                }
            }
        }

        protected void pagingLinksRepeater_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            SetPageNum(Convert.ToInt32(e.CommandArgument));
            FirePageChangeEvent(Convert.ToInt32(e.CommandArgument) - 1);
        }

        protected void OnNavigationClick(object sender, EventArgs e)
        {
            switch (((LinkButton)sender).CommandName)
            {
                case "Firts":
                    _currentPage = 1;
                    break;
                case "Previous":
                    _currentPage--;
                    break;
                case "Next":
                    _currentPage++;
                    break;
                case "Last":
                    _currentPage = TotalPages;
                    break;
            }
            SetUpLinksMode();
            FirePageChangeEvent(CurrentPage - 1);
        }

        public int _currentPage
        {
            get { return ViewState["_currentPage"] == null ? 1 : Convert.ToInt32(ViewState["_currentPage"]); }
            set { ViewState["_currentPage"] = (value <= 0 ? 1 : value); }
        }
        /// <summary>
        /// Current selected page of page.
        /// </summary>
        public int CurrentPage { get { return _currentPage; } }

        public int _totalResults
        {
            get
            {
                return ViewState["_totalResults"] == null ? 0 : Convert.ToInt32(ViewState["_totalResults"]);
            }
            set
            {
                ViewState["_totalResults"] = value;
                lblResults.Text = string.Format("({0} {1})&nbsp;&nbsp;", value, "Results");
            }
        }

        public int TotalResults { get { return _totalResults; } }

        private int _resultsPerPage
        {
            get
            {
                return ViewState["_resultsPerPage"] == null ? 10 : Convert.ToInt32(ViewState["_resultsPerPage"]);
            }
            set
            {
                ViewState["_resultsPerPage"] = (value <= 0 ? 10 : value);
            }
        }
        public int ResultsPerPage { get { return _resultsPerPage; } }

        public int TotalPages { get { return TotalResults / ResultsPerPage + ((TotalResults % ResultsPerPage == 0) ? 0 : 1); } }
        public int FirstRecord { get { return (CurrentPage - 1) * ResultsPerPage + 1; } }
        public int LastRecord { get { return (FirstRecord + ResultsPerPage - 1) < TotalResults ? TotalResults : (FirstRecord + ResultsPerPage - 1); } }

        /// <summary>
        /// Get/set number of clickable lables displayed at once
        /// </summary>
        public int TotalPageLabels
        {
            get { return ViewState["TotalPageLabels"] == null ? 5 : Convert.ToInt32(ViewState["TotalPageLabels"]); }
            set { ViewState["TotalPageLabels"] = value; }
        }

        /// <summary>
        /// Default False, Shows "(100 Results)" when it is true
        /// </summary>
        public bool ShowResultsCount
        {
            get
            {
                if (ViewState["ShowResultsCount"] == null)
                    ViewState["ShowResultsCount"] = true;
                return (bool)ViewState["ShowResultsCount"];
            }
            set
            {
                ViewState["ShowResultsCount"] = value;
            }
        }

        /// <summary>
        /// Page #  Returns a value greater than or equal to 1
        /// </summary>
        /// 
        [Obsolete("Use CurrentPage instead.")]
        public int PageNum
        {
            get
            {
                if (Mode == PageControlDiplayMode.Dropdown)
                {
                    // When there are no selections in the dropdown, we should default
                    // the value to Page #1.  Because, even if there are no results, we should
                    // technically be on the first page, not on the Page #0.
                    if (dl.SelectedIndex == -1)
                        return 1;
                    else
                        return dl.SelectedIndex + 1;
                }
                else if (Mode == PageControlDiplayMode.Links)
                    return CurrentPage;
                else
                    return 0;
            }
        }

        #region Paging Events & methods
        // Set PAge
        public void Reset()
        {
            if (dl.Items.Count > 0)
            {
                dl.SelectedIndex = 0;
                SetNextPrev(0);
            }
        }


        // Create list items
        public void SetUp(int Total)
        {
            SetUp(Total, 20);
        }


        public void SetUpArgs(int Total, int argsPageNum)
        {
            SetUpArgs(Total, 20, argsPageNum);
        }

        public void SetUpArgs(int Total, int PageSize, int argsPageNum)
        {
            SetUp(Total, PageSize);
            SetPageNum(argsPageNum);
            FirePageChangeEvent(argsPageNum - 1);
        }

        public int Total()
        {
            string text = dl.Items[0].Text;
            int lastSpaceIndex = text.LastIndexOf(' ');
            return Convert.ToInt32(text.Substring(lastSpaceIndex));
        }

        public void SetUp(int Total, int PageSize)
        {
            _totalResults = Total;
            _resultsPerPage = PageSize;
            _currentPage = 1;

            switch (Mode)
            {
                case PageControlDiplayMode.Dropdown:
                    SetUpDropDownMode();
                    break;
                case PageControlDiplayMode.Links:
                    SetUpLinksMode();
                    break;
            }

        }

        protected void SetUpLinksMode()
        {
            if (TotalPages > 1)
            {
                holderHideOnOnePage.Visible = true;
                int itemsFromLeft = (TotalPageLabels - 1) % 2 == 0 ? (TotalPageLabels - 1) / 2 : (TotalPageLabels - 2) / 2;
                int itemsFromRight = (TotalPageLabels - 1) % 2 == 0 ? (TotalPageLabels - 1) / 2 : TotalPageLabels / 2;
                List<int> pagesList = new List<int>();
                pagesList.Add(CurrentPage);

                for (int i = 1; i <= itemsFromLeft; i++)
                    if (CurrentPage - i > 0)
                        pagesList.Add(CurrentPage - i);
                    else
                        itemsFromRight++;

                itemsFromLeft = TotalPageLabels - 1 - itemsFromRight;

                for (int i = 1; i <= itemsFromRight; i++)
                    if (CurrentPage + i <= TotalPages)
                        pagesList.Add(CurrentPage + i);
                    else if (!pagesList.Contains(TotalPages - itemsFromLeft - i) && TotalPages - itemsFromLeft - i > 0)
                        pagesList.Add(TotalPages - itemsFromLeft - i);

                pagesList.Sort();
                pagingLinksRepeater.DataSource = pagesList.ToArray();
                pagingLinksRepeater.DataBind();
                holderPrewLinks.Visible = CurrentPage != 1;
                holderNextLinks.Visible = CurrentPage != TotalPages;
            }
            else
                holderHideOnOnePage.Visible = false;

            if (this.ShowResultsCount)
            {
                lblResults.Visible = true;
                lblResults.Text = string.Format("({0} {1})&nbsp;&nbsp;", TotalResults, "Results");
            }
        }

        protected void SetUpDropDownMode()
        {
            // Generate list items
            int start;
            int end;
            Prev.Enabled = false;
            Next.Enabled = true;
            dl.Items.Clear();
            if (_totalResults > 0)
            {
                if (TotalPages == 1)
                    Next.Enabled = false;
                for (int i = 0; i < TotalPages; i++)
                {
                    start = i * _resultsPerPage + 1;
                    end = (start + _resultsPerPage - 1) > _totalResults ? _totalResults : (start + _resultsPerPage - 1);

                    end = (start + _resultsPerPage - 1) > _totalResults ? _totalResults : (start + _resultsPerPage - 1);
                    if (_totalResults > 1)
                        dl.Items.Add(start.ToString() + "-" + end.ToString() + " of " + _totalResults.ToString());
                    else
                        dl.Items.Add(start.ToString() + "-" + end.ToString() + " of " + _totalResults.ToString());
                }
                if (dl.Items.Count > 0)
                    SetPageNum(1);
            }
            else
            {
                dl.Items.Add("0 - 0 " + "of" + " 0");
                Next.Enabled = false;
            }

            if (Next.Enabled == false)
                Next.Style.Add("cursor", "default;");
            else
                Next.Style.Remove("cursor");
            if (Prev.Enabled == false)
                Prev.Style.Add("cursor", "default;");
            else
                Prev.Style.Remove("cursor");
        }


        // LinkButton is clicked
        public void OnCommand(Object s, CommandEventArgs e)
        {
            int indx;
            if (e.CommandName == "Prev")
            {
                indx = dl.SelectedIndex - 1;
            }
            else
            {
                indx = dl.SelectedIndex + 1;
            }
            if (indx >= 0 & indx < dl.Items.Count)
            {
                SetPageNum(indx + 1);
                FirePageChangeEvent(indx);
            }
        }


        // Fire PageChange event
        public void FirePageChangeEvent(int PageNum)
        {
            PageChangeArguments args = new PageChangeArguments(PageNum + 1);
            if (PageChanged != null)
                PageChanged(this, args);

            SetNextPrev(PageNum);
        }


        //This method MUST be called AFTER SetUp is called
        public void SetPageNum(int PageNum)
        {
            _currentPage = PageNum;
            if (Mode == PageControlDiplayMode.Dropdown)
            {
                dl.SelectedIndex = PageNum - 1;
                SetNextPrev(PageNum - 1);
            }
            else if (Mode == PageControlDiplayMode.Links)
            {
                SetUpLinksMode();
            }
        }

        private void SetNextPrev(int PageNum)
        {
            Prev.Enabled = true;
            Next.Enabled = true;
            if (PageNum == 0)
            {
                Prev.Enabled = false;
            }
            if (PageNum == (dl.Items.Count - 1))
            {
                Next.Enabled = false;
            }

            if (Next.Enabled == false)
                Next.Style.Add("cursor", "default;");
            else
                Next.Style.Remove("cursor");
            if (Prev.Enabled == false)
                Prev.Style.Add("cursor", "default;");
            else
                Prev.Style.Remove("cursor");
        }


        public void OnSelectedIndexChanged(Object s, EventArgs e)
        {
            if (_currentPage != dl.SelectedIndex + 1)
            {
                _currentPage = dl.SelectedIndex + 1;
                FirePageChangeEvent(dl.SelectedIndex);
            }
        }
        #endregion Paging Events & methods

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
