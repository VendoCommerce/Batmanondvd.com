<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrackingPixels.ascx.cs" Inherits="CSWeb.PS_A1.UserControls.TrackingPixels" %>
<!-- All Pixels Here -->
<asp:Panel ID="pnlHomePage" runat="server" Visible="false">
    

</asp:Panel>

<asp:Panel ID="pnlAllPages" runat="server" Visible="false">

        <!--
Start of DoubleClick Floodlight Tag: Please do not remove
Activity name of this tag: Batman on DVD - Landing Page
URL of the webpage where the tag is expected to be placed: http://www.batmanondvd.com/
This tag must be placed between the <body> and </body> tags, as close as possible to the opening tag.
Creation Date: 10/02/2014
-->
<iframe src="https://4523247.fls.doubleclick.net/activityi;src=4523247;type=Onlin0;cat=Batma0;qty=1;cost=[Revenue];ord=<%= GetRandomNumber() %>?" width="1" height="1" frameborder="0" style="display:none"></iframe>
<!-- End of DoubleClick Floodlight Tag: Please do not remove -->

    <script type="text/javascript">
        var newPageName = '/' +
        <%=versionNameClientFunction %> + window.location.pathname +
    window.location.search;    
    </script>
    <!-- HitsLink.com tracking script -->
    <script type="text/javascript"
            id="wa_u" defer></script><script type="text/javascript" async>                                         //<![CDATA[
                                         var wa_pageName=location.pathname;    // customize the page name here;
                                         wa_account="9D9E8B929E9190919B899B"; wa_location=31;
                                         wa_MultivariateKey = <%=versionNameClientFunction %>;    //  Set this variable to perform multivariate testing
                                         var wa_c=new RegExp('__wa_v=([^;]+)').exec(document.cookie),wa_tz=new Date(),
                                             wa_rf=document.referrer,wa_sr=location.search,wa_hp='http'+(location.protocol=='https:'?'s':'');
                                         if(top!==self){wa_rf=top.document.referrer;wa_sr=top.location.search}
                                         if(wa_c!=null){wa_c=wa_c[1]}else{wa_c=wa_tz.getTime();
                                             document.cookie='__wa_v='+wa_c+';path=/;expires=1/1/'+(wa_tz.getUTCFullYear()+2);}wa_img=new Image();
                                         wa_img.src=wa_hp+'://counter.hitslink.com/statistics.asp?v=1&s=31&eacct='+wa_account+'&an='+
                                             escape(navigator.appName)+'&sr='+escape(wa_sr)+'&rf='+escape(wa_rf)+
                                             '&mvk='+escape(wa_MultivariateKey)+
                                             '&sl='+escape(navigator.systemLanguage)+'&l='+escape(navigator.language)+
                                             '&pf='+escape(navigator.platform)+'&pg='+escape(wa_pageName)+'&cd='+screen.
                                             colorDepth+'&rs='+escape(screen.width+' x '+screen.height)+'&je='+navigator.javaEnabled()+'&c='+wa_c+'&tks='+wa_tz.getTime();
                                         document.getElementById('wa_u').src=wa_hp+'://counter.hitslink.com/track.js';//]]>
                                     </script>

    
    <!-- GA pixel -->
    <script>
        (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new
                    Date(); a = s.createElement(o),

                    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(
                    a, m)

            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga')
            ;
        ga('create', 'UA-52993620-1', 'auto');
        ga('require', 'displayfeatures');
        ga('send', 'pageview', { 'page': newPageName });
    </script>  

 <!-- Google Code for Remarketing Tag -->
<script type="text/javascript">
    /* <![CDATA[ */
    var google_conversion_id = 962540852;
    var google_custom_params = window.google_tag_params;
    var google_remarketing_only = true;
    /* ]]> */
</script>
<script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/962540852/?value=0&amp;guid=ON&amp;script=0"/>
</div>
</noscript>


    <script type="text/javascript">
        (function(a,e,c,f,g,b,d){var
        h={ak:"962540852",cl:"8HiFCN75nFcQtOr8ygM"};a[c]=a[c]||
        function(){(a[c].q=a[c].q||[]).push(arguments)};a[f]||
        (a[f]=h.ak);b=e.createElement(g);b.async=1;b.src="//www.gstatic.com/wcm/loader.js";d=e.getElementsByTagName(g)[0];d.parentNode.insertBefore(b,d);a._googWcmGet=function(b,d,e){a[c](2,b,h,d,null,new
        Date,e)}})(window,document,"_googWcmImpl","_googWcmAk","script");
</script>

</asp:Panel>
<asp:Panel ID="pnlHomeAndSubPages" runat="server" Visible="false">

</asp:Panel>
<asp:Panel ID="pnlCartPages" runat="server" Visible="false">
    
  
</asp:Panel>

<asp:Panel ID="pnlReceiptPage" runat="server" Visible="false">
    

        <!-- Reciept Page Pixel
        Start of DoubleClick Floodlight Tag: Please do not remove
        Activity name of this tag: Batman on DVD - Purchase Confirmation
        URL of the webpage where the tag is expected to be placed: http://www.batmanondvd.com/thankyou
        This tag must be placed between the <body> and </body> tags, as close as possible to the opening tag.
        Creation Date: 10/02/2014
        -->
        <iframe src="https://4523247.fls.doubleclick.net/activityi;src=4523247;type=Onlin0;cat=Batma00;qty=1;cost=<%=cartTotal.ToString() %>;ord=<%= CartContext.OrderId %>?" width="1" height="1" frameborder="0" style="display:none"></iframe>
        <!-- End of DoubleClick Floodlight Tag: Please do not remove -->    
    

      <!-- Google Code for Purchase Conversion Page --> <script type="text/javascript">
        /* <![CDATA[ */
        var google_conversion_id = 962540852;
        var google_conversion_language = "en";
        var google_conversion_format = "2";
        var google_conversion_color = "ffffff";
        var google_conversion_label = "mCovCMfF-FYQtOr8ygM"; var google_conversion_value = <%=cartTotal.ToString() %>; var google_conversion_currency = "USD"; var google_remarketing_only = false;
        /* ]]> */
        </script>
        <script type="text/javascript"  
        src="//www.googleadservices.com/pagead/conversion.js">
        </script>
        <noscript>
        <div style="display:inline;">
        <img height="1" width="1" style="border-style:none;" alt=""  
        src="//www.googleadservices.com/pagead/conversion/962540852/?value=<%=cartTotal.ToString() %>&amp;currency_code=USD&amp;label=mCovCMfF-FYQtOr8ygM&amp;guid=ON&amp;script=0"/>
        </div>
        </noscript>


        <!-- Bing Code for Purchase Conversion Page --> 
            <script type="text/javascript"> if (!window.mstag) mstag = {loadTag : function(){},time : (new Date()).getTime()};</script> <script id="mstag_tops" type="text/javascript" src="//flex.msn.com/mstag/site/818483c7-c4a9-4546-8395-99eb37baf5aa/mstag.js"></script> <script type="text/javascript">            mstag.loadTag("analytics", {dedup:"1",domainId:"1263508",type:"1",revenue:"<%=cartTotal.ToString() %>",actionid:"36417"})</script> <noscript> <iframe src="//flex.msn.com/mstag/tag/818483c7-c4a9-4546-8395-99eb37baf5aa/analytics.html?dedup=1&domainId=1263508&type=1&revenue=<%=cartTotal.ToString() %>&actionid=36417" frameborder="0" scrolling="no" width="1" height="1" style="visibility:hidden;display:none"> </iframe> </noscript>  
        <!-- End Of Bing Code for Purchase Conversion Page --> 

    <asp:Literal ID="litMdgConfirm" runat="server" />

    <asp:Literal ID="litGAReceiptPixel2" runat="server" />

    <!-- GA pixel -->
<script>
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new
        Date(); a = s.createElement(o),

        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(
        a, m)

    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga')
    ;

    ga('create', 'UA-52993620-1', 'auto');
    ga('require', 'displayfeatures');
    ga('send', 'pageview');
    ga('require', 'ecommerce', 'ecommerce.js');
    <asp:Literal ID="litGAReceiptPixel" runat="server" />
    ga('ecommerce:send');

</script>  
 
</asp:Panel>
