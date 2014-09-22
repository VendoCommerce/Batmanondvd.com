﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upsells.aspx.cs" Inherits="CSWeb.index" EnableSessionState="True" %>

<!doctype html>
<html>
<head runat="server">
<meta charset="utf-8">    
<title>BATMAN</title>
<meta name="description" content=""/>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Scripts/fancybox/jquery.fancybox.css">
<script src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<script type="text/javascript" src="/scripts/jwplayer/jwplayer.js"></script>
<script src="/Scripts/jquery.cycle.js"></script>
<script type="text/javascript">jwplayer.key = "JEtVDryJGkO9Q215yroU+Wz4oLeTJGMccGU/Wb3Kv9s=";</script>
<script src="/Scripts/global.js"></script>
<link href="../styles/global.css" rel="stylesheet" type="text/css" />
</head>
<body id="upsells">
<form id="form1" runat="server">

    <div id="mainContainer">
<!--#include file="header_upsell.html"-->

        <!-- UPSELL - complete collection - single pay -->
        <div class="page_upsell" style="background: #fff url(//d1kg9stb0ddjcv.cloudfront.net/images/upsell_full_collection.jpg) no-repeat 469px 127px;">
            <h2>Upgrade to the Full 120 Episode Collection!</h2>
            <h3>Just one season of Batman will leave you needing more! Upgrade to the HD full collection today and receive <br />120 Episodes from all three seasons!</h3>
            <ul>
                <li>120 Original Broadcast Episodes Fully Remastered In HD</li>
                <li>Bonus DVD containing 3 Hours Of ALL NEW Extras like:                    <ul>
                        <li>Hanging with Batman – A true slice of life in the words of Adam West</li>
                        <li>Holy Memorabilia Batman! – A journey into the most sought <br />        after collectibles</li>
                        <li>Batmania Born! – Explore the art and design behind the fiction.</li>
                    </ul></li>
                <li>Na Na Na Batman! — Hollywood favorites stars recount their <br />favorite Batman memories</li>
                <li>Highly Collectible Premiums: Hot Wheels® Replica Batmobile & <br />
                    The Adam West Scrapbook 44 Vintage Trading Cards</li>
                <li>Episode Guide</li>
                <li>Adam West Naked DVD</li>
                <li>The 1966 Batman Movie on DVD</li>
                <li>Episode Script (first Joker episode) with personalized letter from Adam West!</li>
            </ul>

            <p class="ask f14">Would you like to take advantage of this special offer?</p>

            <script type="text/javascript">function updateUpsellSku(fld) { document.getElementById('addsku').value = fld.value; $("#upsell_error").html(""); }</script>
            <input type="hidden" name="addsku" id="addsku" required="true" error="Please select an item." value="one" />
            <input type="radio" id="radio1" name="skuitem" value="one" onclick="updateUpsellSku(this)" checked="" /> <label for="radio1"><strong style="display: inline-block; position: relative; top: -2px; left: 5px;">DVD Version for only $149.95</strong></label>
            <br /><br />
            <div style="padding-bottom: 20px;"><input type="radio" id="radio2" name="skuitem" value="two" onclick="updateUpsellSku(this)" /> <label for="radio2"><strong style="display: inline-block; position: relative; top: -2px; left: 5px;">Blu-Ray HD version for only $174.95</strong></label></div>

            <div class="btns"><a href="javascript:void(0);" bind="no"><img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
        </div>



        <br /><br /><br /><br /><br /><br /><br /><br />

        <!-- UPSELL - complete collection - multi pay -->
        <div class="page_upsell" style="background: #fff url(//d1kg9stb0ddjcv.cloudfront.net/images/upsell_full_collection.jpg) no-repeat 469px 127px;">
            <h2>Upgrade to the Full 120 Episode Collection!</h2>
            <h3>Just one season of Batman will leave you needing more! Upgrade to the HD full collection today and receive <br />120 Episodes from all three seasons!</h3>
            <ul>
                <li>120 Original Broadcast Episodes Fully Remastered In HD</li>
                <li>Bonus DVD containing 3 Hours Of ALL NEW Extras like:                    <ul>
                        <li>Hanging with Batman – A true slice of life in the words of Adam West</li>
                        <li>Holy Memorabilia Batman! – A journey into the most sought <br />        after collectibles</li>
                        <li>Batmania Born! – Explore the art and design behind the fiction.</li>
                    </ul></li>
                <li>Na Na Na Batman! — Hollywood favorites stars recount their <br />favorite Batman memories</li>
                <li>Highly Collectible Premiums: Hot Wheels® Replica Batmobile & <br />
                    The Adam West Scrapbook 44 Vintage Trading Cards</li>
                <li>Episode Guide</li>
                <li>Adam West Naked DVD</li>
                <li>The 1966 Batman Movie on DVD</li>
                <li>Episode Script (first Joker episode) with personalized letter from Adam West!</li>
            </ul>

            <p class="ask f14">Would you like to take advantage of this special offer?</p>

            <script type="text/javascript">function updateUpsellSku(fld) { document.getElementById('addsku').value = fld.value; $("#upsell_error").html(""); }</script>
            <input type="hidden" name="addsku" id="addsku" required="true" error="Please select an item." value="one" />
            <input type="radio" id="radio1" name="skuitem" value="one" onclick="updateUpsellSku(this)" checked="" /> <label for="radio1"><strong style="display: inline-block; position: relative; top: -2px; left: 5px;">DVD Version for only 5 payments of $29.99</strong></label>
            <br /><br />
            <div style="padding-bottom: 20px;"><input type="radio" id="radio2" name="skuitem" value="two" onclick="updateUpsellSku(this)" /> <label for="radio2"><strong style="display: inline-block; position: relative; top: -2px; left: 5px;">Blu-Ray HD version for only 5 payments of $34.99</strong></label></div>

            <div class="btns"><a href="javascript:void(0);" bind="no"><img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
        </div>



        




        <br /><br /><br /><br /><br /><br /><br /><br />
        <!-- UPSELL - additional -->
        <div class="page_upsell">
            
            <div class="fleft" style="width: 450px; margin-right: 10px;">
                <h2 class="pad20">Don’t be a Joker, Share the<br />
                    Bat-Love and Save!</h2>

                <p class="f16 lh20">Who wouldn’t want to enjoy the complete Batman & Robin collection now digitally remastered and perfectly pixelated! So do yourself a favor, Get Two!</p>
                <p class="f16 lh20">Save 30%, get free shipping and become a Holiday Hero when you buy a second set today!</p>
                <p class="f16 lh20">Now you and your loved ones can reminisce together over the hilarious hijinks, formidable foes and campy costumes that make Batman and Robin a venerable classic. Save 30% today!</p>

                <p class="ask">Would you like to take advantage of this special offer?</p>


                <div class="btns"><a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
            </div>

            <div class="fleft" style="width: 369px; top: -35px;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/upsell_additional.jpg" width="369" height="450" alt="" /></div>
            <div class="clear"></div>
        </div>

        
        



        <br /><br /><br /><br /><br /><br /><br /><br />

        <!-- UPSELL - gift wrap -->
        <div class="page_upsell">
            
            <div class="fleft" style="width: 445px; margin-right: 55px;">
                <h2 class="pad20">Give the Gift of <br />
                    Endless Entertainment!</h2>

                <p class="f16 lh20">Cliffhangers, bat-gadgets, alter egos and more! A perfect gift for the new or die-hard Bat-Fan!</p>
                <p class="f16 lh20">Have your order specially gift wrapped for only $5.95 per item. Even the pickiest comic book fan will be thrilled to receive this exclusive gift!</p>

                <p class="ask">Would you like to have your order gift wrapped with our custom Batman gift wrapping?</p>

                
                <div class="btns"><a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
            </div>

            <div class="fleft" style="width: 303px; top: -10px;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/upsell_giftwrap.jpg" width="303" height="405" alt="" /></div>
            <div class="clear"></div>
        </div>

        



        <br /><br /><br /><br /><br /><br /><br /><br />

        <!-- UPSELL - rush shipping -->
        <div class="page_upsell">
            
            <div class="fleft" style="width: 465px; margin-right:20px;">
                <h2 class="pad20">Can’t wait for the Campy <br />                    Comic Fun!<br />                    Get Rush Shipping now!</h2>

                <p class="f16 lh20">Choose Rush Shipping today to start reliving your favorite moments now. The sooner it arrives, the sooner you can watch Julie Newmar in her Purrfect Cat Suit, or Adam West play out his iconic role. Simply choose rush shipping and be one step closer to digitally remastered Batman Perfection. </p>

                <p class="ask">Would you like to receive your order faster?</p>

                
                <div class="btns"><a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
            </div>

            <div class="fleft" style="width: 310px;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/upsell_rush.png" width="310" height="320" alt="" /></div>
            <div class="clear"></div>
        </div>


        



        <br /><br /><br /><br /><br /><br /><br /><br />

        <!-- UPSELL - rush shipping with radio buttons -->
        <div class="page_upsell">
            
            <div class="fleft" style="width: 465px; margin-right:20px;">
                <h2 class="pad20">Can’t wait for the Campy <br />                    Comic Fun!<br />                    Get Rush Shipping now!</h2>

                <p class="f16 lh20">Choose Rush Shipping today to start reliving your favorite moments now. The sooner it arrives, the sooner you can watch Julie Newmar in her Purrfect Cat Suit, or Adam West play out his iconic role. Simply choose rush shipping and be one step closer to digitally remastered Batman Perfection. </p>

                <p class="ask">Would you like to receive your order faster?</p>

                <script type="text/javascript">function updateUpsellSku(fld) { document.getElementById('addsku').value = fld.value; $("#upsell_error").html(""); }</script>
                <input type="hidden" name="addsku" id="addsku" required="true" error="Please select an item." value="one" />
                <input type="radio" name="skuitem" id="radio1" value="one" onclick="updateUpsellSku(this)" checked="" /> <label for="radio1" style="display: inline-block; position: relative; top: -2px; left: 5px;"><strong style="display: inline-block; width: 188px;">2-day Air $9.95</strong></label>
                <br /><br />
                <div style="padding-bottom: 16px;"><input type="radio" id="radio2" name="skuitem" value="two" onclick="updateUpsellSku(this)" /> <label for="radio2" style="display: inline-block; position: relative; top: -2px; left: 5px;"><strong style="display: inline-block; width: 188px;">Overnight $14.95</strong></label></div>


                <div class="btns"><a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
            </div>

            <div class="fleft" style="width: 310px;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/upsell_rush.png" width="310" height="320" alt="" /></div>
            <div class="clear"></div>
        </div>








        </div>

</form>
</body>
</html>
