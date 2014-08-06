<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSWeb.Root.Store.index" EnableSessionState="True" %>
<%@ Register Src="UserControls/TrackingPixels.ascx" TagName="TrackingPixels" TagPrefix="uc" %>

<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=640px, initial-scale=.5, maximum-scale=.5" />
<title>Wise Emergency Foods - Special Offers</title>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script type="text/javascript" src="/Scripts/fancybox/jquery.fancybox.pack.js"></script>
<link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/jwplayer/jwplayer.js"></script>
<script type="text/javascript" src="/Scripts/global.js"></script>
<link href="/styles/global_mobile.css" rel="stylesheet" type="text/css" />

</head>
 
<body>
 <form runat="server" id="fm1">
<div class="container">
<!--#include file="popups.html"-->
<!--#include file="header_upsell.html"-->

<div class="content">

<!-- additional 1p -->
<div class="page_upsell">

  <h2>Get an Additional Month's <br>
    Food Supply <strong>and save BIG!</strong></h2>
  <p><img src="../Content/Images/mobile/upsell_additional_1p.jpg" width="280" height="328" class="fright" />You're wise enough to prepare for emergencies with a 1 month's supply, keep in mind that is 1 month per person so if you have more members in your family you can add another month's food at a deep discount of 20%. Or simply get a second month's supply for yourself and save. Get your additional 1-month food supply for a special price of $79.99 plus <strong class="red">FREE SHIPPING &amp; HANDLING</strong> when you add it to your purchase today!</p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>

<span class="ask">Would you like to take <br>
advantage of this special offer? <span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>

  
  
  
<!-- additional 3p -->
<div class="page_upsell">


  <h2>Get an Additional Month's <br>
    Food Supply <strong>and save BIG!</strong></h2>
  <p><img src="../Content/Images/mobile/upsell_additional_1p.jpg" width="280" height="328" class="fright" />You're wise enough to prepare for emergencies with a 1 month's supply, keep in mind that is <strong>1 MONTH PER PERSON</strong> so if you have more members in your family you can add another month's food at a deep discount of 20%. Or simply get a second month's supply for yourself and save. Get your additional 1-month food supply with 3 easy payments of only $26.66 plus $19.99 
    Shipping and handling when you add it to your purchase today!</p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>

  


<!-- fruit 1p -->
<div class="page_upsell">


  <h2><strong>Enjoy Flavorful, Delicious Fruits, Even in An Emergency!</strong></h2>
  <p><img src="../Content/Images/mobile/upsell_fruit_1p.jpg" /><br>
<a class="green fruitmobile scored" href="/content/images/mobile/fruit_pop.png"><strong>See What You'll Get!</strong></a></p>
  <p class="pad6">Just because your normal pace of life (and your food supply) has been disrupted by an emergency doesn't mean you shouldn't have tasty, flavorful fruits to enjoy. Add the fruit kit to your purchase today for a special price of only $89.99 plus you'll get <strong><span class="red">FREE SHIPPING &amp; HANDLING!</span> Get 88 tasty servings of fruits by adding the fruit kit today! </strong></p>
    <ul class="bold" style="margin-left: 26px">
    <li class="pad6"><span class="red">88 Total Servings</span></li>
    <li class="pad6"><span class="red">20 Year Shelf Life</span></li>
    <li class="pad6"><span class="red">Just Add Water to Rehydrate</span></li>
  </ul>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  





<!-- fruit 3p -->
<div class="page_upsell">


  <h2><strong>Enjoy Flavorful, Delicious Fruits, Even in An Emergency!</strong></h2>
  <p><img src="../Content/Images/mobile/upsell_fruit_3p.jpg" /><br><a class="green fruitmobile scored" href="/content/images/mobile/fruit_pop.png"><strong>See What You'll Get!</strong></a></p>
  <p class="pad6">Just because your normal pace of life (and your food supply) has been disrupted by an emergency doesn't mean you shouldn't have tasty, flavorful fruits to enjoy. Add the fruit kit to your purchase today with three easy payments of only $29.99 plus $5.99 shipping and handling. <strong>Get 88 tasty servings of fruits by adding the fruit kit today! </strong></p>
      <ul class="bold" style="margin-left: 26px">
    <li class="pad6"><span class="red">88 Total Servings</span></li>
    <li class="pad6"><span class="red">20 Year Shelf Life</span></li>
    <li class="pad6"><span class="red">Just Add Water to Rehydrate</span></li>
  </ul>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  



<!-- veggies 1p -->
<div class="page_upsell">


  <h2><strong>Essential Vegetables </strong>That <br>
    Are There When You Need Them!</h2>
    <p><img src="../Content/Images/mobile/upsell_veggie_1p.jpg" /><br><a class="green veggiemobile scored" href="/content/images/mobile/veggie_pop.png"><strong>See What You'll Get!</strong></a></p>
  <p class="pad6">In an emergency, it's more important than ever that you get the nutrition that you need, and as we all know, vegetables are a great source of vitamins and minerals! Add the vegetable kit and add even more variety to your food supply. Pay with a single payment of only $89.99 plus <strong><span class="red">FREE SHIPPING &amp; HANDLING!</span> That's 128 servings of vegetables – add the vegetable kit today!</strong></p>
    <ul class="bold" style="margin-left: 20px">
    <li class="pad6"><span class="red">128 Total Servings</span></li>
    <li class="pad6"><span class="red">25 Year Shelf Life</span></li>
    <li class="pad6"><span class="red">Just Add Water to Rehydrate</span></li>
  </ul>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  





<!-- veggies 3p -->
<div class="page_upsell">


  <h2><strong>Essential Vegetables </strong>That <br>
    Are There When You Need Them!</h2>
        <p><img src="../Content/Images/mobile/upsell_veggie_3p.jpg" /><br><a class="green veggiemobile scored" href="/content/images/mobile/veggie_pop.png"><strong>See What You'll Get!</strong></a></p>
  <p class="pad6">In an emergency, it's more important than ever that you get the nutrition that you need, and as we all know, vegetables are a great source of vitamins and minerals! Add the vegetable kit and add even more variety to your food supply. Pay with three easy payments of only $29.99 plus $5.99 shipping and handling. <strong>That's 128 servings of vegetables – add the vegetable kit today!</strong></p>
      <ul class="bold" style="margin-left: 20px">
    <li class="pad6"><span class="red">128 Total Servings</span></li>
    <li class="pad6"><span class="red">25 Year Shelf Life</span></li>
    <li class="pad6"><span class="red">Just Add Water to Rehydrate</span></li>
  </ul>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  





<!-- protein 1p -->
<div class="page_upsell">


  <h2><strong>Keep Your Strength Up in <br>
    the Times When You'll Need It!</strong></h2>
  <p class="pad6"><a href="/content/images/mobile/protein_pop.png" class="proteinmobile"><img src="../Content/Images/mobile/upsell_protein_1p.jpg" class="fright" /></a>Protein is the building block of life, and of muscle. To keep yourself strong in an emergency, you want to make sure you're getting enough proteins. That's why for just a single payment of $89.99 today, you can add a protein kit plus get <strong class="red">FREE SHIPPING &amp; HANDLING! </strong>Keep your strength up – add a protein kit today!</p>
        <ul class="bold" style="margin-left: 20px">
    <li class="pad6"><span class="red">42 Total Servings</span></li>
    <li class="pad6"><span class="red">15 Year Shelf Life</span></li>
    <li class="pad6"><span class="red">Just Add Water to Rehydrate</span></li>
  </ul>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> 
  </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  




<!-- protein 3p -->
<div class="page_upsell">

  <h2><strong>Keep Your Strength Up in <br>
    the Times When You'll Need It!</strong></h2>
  <p class="pad6"><a href="/content/images/mobile/protein_pop.png" class="proteinmobile"><img src="../Content/Images/mobile/upsell_protein_3p.jpg" class="fright" /></a>Protein is the building block of life, and of muscle. To keep yourself strong in an emergency, you want to make sure you're getting enough proteins. That's why for three easy payments of $29.99 
    plus $5.99 shipping and handling you can add a protein kit to your purchase today! Keep your strength up – add a protein kit today!</p>
            <ul class="bold" style="margin-left: 20px">
    <li class="pad6"><span class="red">42 Total Servings</span></li>
    <li class="pad6"><span class="red">15 Year Shelf Life</span></li>
    <li class="pad6"><span class="red">Just Add Water to Rehydrate</span></li>
  </ul>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  





<!-- water 1p -->
<div class="page_upsell">


  <h2>Protect Your Water Supply Too! <br>
    <strong>Get a Filtration Water Bottle!</strong></h2>
  <p><img src="../Content/Images/mobile/upsell_water_1p.jpg" width="230" height="408" class="fright" />In an emergency, water supplies can easily become tainted, requiring you to boil water before you drink it which takes time, precious energy, and even more precious fuel. But you can avoid all of that with a water filtration bottle for a special price of just $29.99 plus <strong class="red">FREE SHIPPING &amp; HANDLING! </strong>Secure your water supply in an emergency! Get a water filtration bottle now!</p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  



<!-- water 3p -->
<div class="page_upsell">


  <h2>Protect Your Water Supply Too! <br>
    <strong>Get a Filtration Water Bottle!</strong></h2>
  <p><img src="../Content/Images/mobile/upsell_water_3p.jpg" width="200" height="392" class="fright" />In an emergency, water supplies can easily become tainted, requiring you to boil water before you drink it which takes time, precious energy, and even more precious fuel. But you can avoid all of that with a water filtration bottle, get it today with three easy payments of just $9.99 plus $5.99 shipping and handling  Secure your water supply in an emergency! Get a water filtration bottle now!</p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  




<!-- bundle 1p -->
<div class="page_upsell">
<div style="position:absolute; right: 0; top: 0"><img src="../Content/Images/mobile/upsell_bestvaluecall.png" width="209" height="118" /></div>

  <h2><strong>SAVE BIG </strong>    With The     <br>
    Wise Choice Bundle!</h2>
  <p><a href="#bundle" class="bundlemobile"><img src="../Content/Images/mobile/upsell_bundle_1p.jpg" class="fright" /></a>Take advantage of our <strong>BEST VALUE </strong>available only with your purchase today. Our Wise Choice Bundle complements your Wise Company Emergency Food to bring you nutritious rations of flavorful fruits, essential vegetables and protein. Get all three food packs at the special bundle price of $199 (a $269.97 value).  <strong class="red">That is a 26% SAVINGS plus YOU GET FREE SHIPPING!</strong></p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  






<!-- bundle 3p -->
<div class="page_upsell">
<div style="position:absolute; right: 0; top: 0"><img src="../Content/Images/mobile/upsell_bestvaluecall.png" width="209" height="118" /></div>

  <h2><strong>SAVE BIG </strong> With The <br>
Wise Choice Bundle!</h2>
  <p><a href="#bundle" class="bundlemobile"><img src="../Content/Images/mobile/upsell_bundle_3p.jpg" class="fright" /></a>Take advantage of our <strong>BEST VALUE</strong> available only with your purchase today. Our Wise Choice Bundle complements your Wise Company Emergency Food to bring you nutritious rations of flavorful fruits, essential vegetables and protein. Add all three packs to you purchase today with 3 easy payments of $66.66 plus $5.99 shipping and handling a super low total bundle price of $205.97 (a $269.97 value).  <strong class="red">That is a 24% SAVINGS!</strong></p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  




<!-- backpack 1p -->
<div class="page_upsell">


  <h2><strong>When crisis hits, you've got <br>
    to be ready, fast!<br>
    <span class="black f28">Get Your Survival BackPack Today!</span></strong></h2>
  <p><a href="/content/images/mobile/backpack_pop.png" class="backpackmobile"><img src="../Content/Images/mobile/upsell_backpack.jpg" width="323" height="280" class="fright" /></a>Wise Company is now offering you a 5-Day Survival Backpack in eye-catching red and white, to help you weather just about any storm. Add this to your purchase today for only $69.99 plus shipping and handling. Each pack is well balanced, portable and stocked with everything you need for five whole days, including food, water, first aid and warmth!</p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  




<!-- backpack 3p -->
<div class="page_upsell">


  <h2><strong>When crisis hits, you've got <br>
    to be ready, fast!<br>
    <span class="black f28">Get Your Survival BackPack Today!</span></strong></h2>
  <p><a href="/content/images/mobile/backpack_pop.png" class="backpackmobile"><img src="../Content/Images/mobile/upsell_backpack.jpg" width="323" height="280" class="fright" /></a>Wise Company is now offering you a 5-Day Survival Backpack in eye-catching red and white, to help you weather just about any storm. Add this to your purchase today for only 3 easy payments of $23.33 plus shipping and handling. Each pack is well balanced, portable and stocked with everything you need for five whole days, including food, water, first aid and warmth!</p>
  <p class="text-right"><label for="qty">How Many? </label>
  <select name="qty" required error=" * Please Select Quantity">
  <option value="">- Select -</option>
  <option value="1">1</option> 
  <option value="2">2</option> 
  <option value="3">3</option>
  <option value="4">4</option>
  <option value="5">5</option> </select>
<span class="block text-right f25" style="padding-top: 6px">Limit 5 per customer</span>
  </p>
<span class="ask">Would you like to take <br>
advantage of this special offer?
<span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>
</div>  





<!-- single pay -->
<div class="page_upsell">
<div style="position: absolute; right: 23px; top: 0"><img src="../Content/Images/mobile/upsell_freeshipcall.jpg" width="185" height="170" /></div>

 
    <h2>Get <strong>FREE SHIPPING</strong><br>
      by Choosing One Easy <br>
      Payment!</h2>
  <p>Save yourself the hassles of monthly payments. If you choose to pay in full today, we'll pay for your shipping, that is a $19.99 SAVINGS for you!</p>
  <p><strong>We'll drop the Shipping &amp; Handling: <span class="red" style="text-decoration: line-through"><span class="gray">$19.99</span></span></strong></p>

<span class="ask"><span class="f26">With 1-PAY, you'll only pay <span class="red" style="text-decoration: line-through"><span class="gray">$119.98</span></span>  $99.99</span>
<br>
Would you like to take <br>
advantage of this special offer? <span class="btns"><a href="javascript:void(0)" bind="no"><img src="/content/images/mobile/nothanks_btn.jpg" /></a><a href="javascript:void(0)" bind="yes"><img src="/content/images/mobile/yes_btn.jpg" /></a><div class="ask_arrow"></div></span>
</span>
<div class="clear"></div>

  
 </div>
 
 </div>
<!--#include file="footer_upsell.html"-->
</div>

  <uc:TrackingPixels ID="TrackingPixels" runat="server" />


 </form>
</body>
</html>
