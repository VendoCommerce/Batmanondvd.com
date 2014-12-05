<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upsells.aspx.cs" Inherits="CSWeb.BIG3.upsells" EnableSessionState="True" %>

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
<link href="/styles/global_big3.css" rel="stylesheet" type="text/css" />
</head>
<body id="upsells">
<form id="form1" runat="server">

    <div id="mainContainer">
<!--#include file="header_upsell.html"-->

        <!-- UPSELL - complete collection - single pay -->
        <div class="page_upsell" style="background: #fff url(//d1kg9stb0ddjcv.cloudfront.net/images/upsell_full_collection.jpg) no-repeat 469px 148px;">
            <h2 class="f28">Upgrade to the Full 120 Episode Collection in a Beautiful Collector’s Box!</h2>
            <h3 class="f20">Just half of the episodes of Batman will leave you needing more! Upgrade to the full collection today and receive all <br />
120 Episodes from all Three Seasons!  Get it on DVD or stunning high definition Blu-ray.</h3>
            <ul>
                <li>120 Original Broadcast Episodes Fully Remastered In HD</li>
                <li>OVER 3 Hours Of ALL NEW Extras like:
                    <ul>
                        <li><strong>Hanging with Batman</strong> – A true slice of life in the words of Adam West</li>
                        <li><strong>Holy Memorabilia Batman!</strong> – A journey into the most sought <br />
        after collectibles through the eyes of 3 extraordinary collectors</li>
                        <li><strong>Batmania Born!</strong> – Building the World of Batman - Explore the art and <br />design behind the fiction.</li>
                        <li><strong>Bats of the Round Table</strong>  – A candid conversation with Adam West and <br />
        his celebrity friends, chatting all things Bat ’66.</li>
                        <li><strong>Inventing Batman in the words of Adam West</strong> (episode 1 &2) – A rare <br />
        treat for the fans as Adam discusses his script notes on bringing Batman to life  <br />
        in the first and second episodes </li>
                        <li><strong>Na Na Na Batman!</strong> Hollywood favorites stars and producers recount their <br />
                            favorite Batman memories</li>
                        <li><strong>BATRARITIES! STRAIGHT FROM THE VAULT</strong> – 
                            <strong>Batgirl Pilot</strong> –
                            This is the pilot <br />
                            that sold the executives on the idea of a Dynamic Trio.  Batgirl had gravitas and sex appeal.  <br />
                            She kept her cool, she fought the evil denizens of Gotham!   POW! ZAP! BAM!
                        </li>
                    </ul></li>
                <li>Also Get Adam West Naked on DVD, The Original 1966 Batman, The Movie   <br />
  DVD, an Extensive Episode Guide, an Episode Script, a Personalized Letter <br />
  from Adam West <strong>PLUS</strong> your own Classic Batman Collector’s Box</li>
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
                <li>Bonus DVD containing 3 Hours Of ALL NEW Extras like:
                    <ul>
                        <li>Hanging with Batman – A true slice of life in the words of Adam West</li>
                        <li>Holy Memorabilia Batman! – A journey into the most sought <br />
        after collectibles</li>
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

                <p class="f16 lh20">Who wouldn’t want to enjoy the Classic Batman & Robin collection now digitally remastered and perfectly pixelated! So do yourself and a friend or a foe a favor, Get Two!</p>
                <p class="f16 lh20">Save 30%, get FREE shipping and become a Holiday Hero when you buy a second set today!</p>
                <p class="f16 lh20">Now you can share and reminisce together over the hilarious hijinks, formidable foes and campy costumes that make the 1960’s Batman series a venerable classic. Save 30% today!</p>

                <p style="padding-bottom: 30px;">
                    <span class="webfont1bold f18">Qty</span> &nbsp;
                    <select id="secondset_qty" class="select_qty">
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                </p>

                <p class="ask">Would you like to take advantage of this special offer?</p>


                <div class="btns"><a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
            </div>

            <div class="fleft" style="width: 369px; top: -35px;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/upsell_additional.jpg" width="369" height="450" alt="" /></div>
            <div class="clear"></div>
        </div>







        <br /><br /><br /><br /><br /><br /><br /><br />
        <!-- UPSELL - additional complete -->
        <div class="page_upsell">
            
            <div class="fleft" style="width: 450px; margin-right: 10px;">
                <h2 class="pad20">Don’t be a Joker, Share the<br />
                    Bat-Love and Save!</h2>

                <p class="f16 lh20">Who wouldn’t want to enjoy the Complete Classic Batman & Robin collection now digitally remastered and perfectly pixelated! So do yourself and a friend or a foe a favor, Get Two!</p>
                <p class="f16 lh20">Save 30%, get FREE shipping and become a Holiday Hero when you buy a second set today!</p>
                <p class="f16 lh20">Now you can share and reminisce together over the hilarious hijinks, formidable foes and campy costumes that make the 1960’s Batman series a venerable classic. Save 30% today!</p>


                <p style="padding-bottom: 30px;">
                    <span class="webfont1bold f18">Qty</span> &nbsp;
                    <select id="secondset_qty" class="select_qty">
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                </p>

                <p class="ask">Would you like to take advantage of this special offer?</p>


                <div class="btns"><a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
            </div>

            <div class="fleft" style="width: 369px; padding: 45px 0 0 10px;"><img src="//d1kg9stb0ddjcv.cloudfront.net/images/upsell_additional_complete.jpg" width="335" height="245" alt="" /></div>
            <div class="clear"></div>
        </div>


        




        <br /><br /><br /><br /><br /><br /><br /><br />
        <!-- UPSELL - superman -->
        <div class="page_upsell" style="background: #fff url(//d1kg9stb0ddjcv.cloudfront.net/images/upsell_superman.jpg) no-repeat 456px 115px; padding-bottom: 16px;">

            <h2 class="pad20">It's a bird! It's a plane! No, it's the <br />
                first six seasons of the legendary <br />
                Adventures of Superman!
            </h2>

            <p class="f16 lh22">Faster than a speeding bullet! More powerful than <br />
a locomotive! Able to leap tall buildings at a single <br />
bound!  Don’t miss out on the legendary 1950’s  <br />
Adventures of Superman series starring George Reeves!   <br />
Your Superman DVD Collection will contain 104 episodes  <br />
on 20 DVDs for only $49.95!
            </p>

            <p class="ask">Would you like to take advantage of this special offer?</p>


            <div class="btns">
                <a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" />
            </div>
            <p class="f12 pad0" style="padding: 30px 50px 0 0; line-height: 14px;">ADVENTURES OF SUPERMAN &copy; DC Comics. All Rights Reserved. SUPERMAN and all related characters and elements are trademarks of and &copy; DC Comics.</p>


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
                <p style="padding-bottom: 30px;">                    <span class="webfont1bold f18">Qty</span> &nbsp;                  <select name="qty" required="true" error="* Please Select Quantity" class="select_qty">         <option value="1">1</option>          <option value="2">2</option>          <option value="3">3</option>          <option value="4">4</option>        <option value="5">5</option>        <option value="6">6</option>        <option value="7">7</option>        <option value="8">8</option>       <option value="9">9</option>         </select>            </p>   
                <p class="ask">Would you like to have your order gift wrapped with our custom Batman gift wrapping?</p>

                
                <div class="btns"><a href="javascript:void(0);" bind="no">
                    <img width="86" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_no.png" /></a><a href="javascript:void(0);" bind="yes"><img width="262" height="60" src="//d1kg9stb0ddjcv.cloudfront.net/images/btn_yes.png" /></a><img width="72" height="63" src="//d1kg9stb0ddjcv.cloudfront.net/images/arrow_upsell.png" class="ask_arrow" /></div>
            </div>

            <div class="fleft" style="width: 303px; top: -10px;"><img width="292" height="430" src="//d1kg9stb0ddjcv.cloudfront.net/images/upsell_giftwrap_bag.jpg" alt="" /></div>   
            <div class="clear"></div>
        </div>

        



        <br /><br /><br /><br /><br /><br /><br /><br />

        <!-- UPSELL - rush shipping -->
        <div class="page_upsell">
            
            <div class="fleft" style="width: 465px; margin-right:20px;">
                <h2 class="pad20">Can’t wait for the Campy <br />
                    Comic Fun!<br />
                    Get Rush Shipping now!</h2>

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
                <h2 class="pad20">Can’t wait for the Campy <br />
                    Comic Fun!<br />
                    Get Rush Shipping now!</h2>

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
