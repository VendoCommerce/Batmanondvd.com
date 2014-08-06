// JavaScript Document

$(document).ready(function () {
	
$(window).scrollTop(0);
	
	

	
//faq toggle stuff 
$('.togglefaq').click(function(e) {
e.preventDefault();
var notthis = $('.active').not(this);
notthis.find('.icon-minus').addClass('icon-plus').removeClass('icon-minus');
notthis.toggleClass('active').next('.faqanswer').slideToggle(300);
 $(this).toggleClass('active').next().slideToggle("fast");
$(this).children().toggleClass('icon-plus icon-minus');
});

//fancybox popups
    $(".fancybox").fancybox();


   $(".included").fancybox({
        closeBtn: false,
        fitToView: false,
        wrapCSS: 'nowrapper',
        padding: 0,
        width: 710,
        height: 461,
        autoSize: false,
        closeClick: false,
        scrolling: 'no',
        helpers: {
            overlay: {
				locked: false,
				css : {
					'background' : 'rgba(0,0,0,.8)'
					}
            }
        }
    });

    $(".guarantee").fancybox({
        closeBtn: false,
        fitToView: false,
        wrapCSS: 'nowrapper',
        padding: 0,
        width: 373,
        height: 469,
        autoSize: false,
        closeClick: false,
        scrolling: 'no',
        helpers: {
            overlay: {
				locked: false,
				css : {
					'background' : 'rgba(0,0,0,.8)'
					}
            }
        }
    });
	
	    $(".cvv").fancybox({
        closeBtn: false,
        fitToView: false,
        wrapCSS: 'nowrapper',
        padding: 0,
        width: 500,
        height: 685,
        autoSize: false,
        closeClick: true,
        scrolling: 'no',
        helpers: {
            overlay: {
				locked: false,
				css : {
					'background' : 'rgba(255,255,255,.8)'
					}
            }
        }
    });
	
/* home videos */


$(".hometest1").bind("click touch", function(e){
	$("#homet1").show();
	$("#homet2").hide();
	$("#homet3").hide();
			
  jwplayer('hometest1').setup({
	file: '/content/video/jennifer.mp4',
	autostart: true,
    primary: "flash",   
	image: '/content/images/poster_jennifer.jpg',
	controls: true,
    width: 413, height: 232,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events:{
	onPlay: function() {
   	jwplayer('ctavideo').stop();	
   	jwplayer('hometest2').stop();
   	jwplayer('hometest3').stop();
	}
	}
	});
	e.preventDefault();
});

$(".hometest2").bind("click touch", function(e){
	$("#homet2").show();
	$("#homet1").hide();
	$("#homet3").hide();
			
  jwplayer('hometest2').setup({
	file: '/content/video/stephanie.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
	image: '/content/images/poster_stephanie.jpg',
    width: 413, height: 232,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events:{
	onPlay: function() {
   	jwplayer('ctavideo').stop();	
   	jwplayer('hometest1').stop();
   	jwplayer('hometest3').stop();
	}
	}
	});
	e.preventDefault();
});

$(".hometest3").bind("click touch", function(e){
	$("#homet3").show();
	$("#homet2").hide();
	$("#homet1").hide();
			
  jwplayer('hometest3').setup({
	file: '/content/video/ashley.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
	image: '/content/images/poster_ashley.jpg',
    width: 413, height: 232,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events:{
	onPlay: function() {
   	jwplayer('ctavideo').stop();	
   	jwplayer('hometest1').stop();
   	jwplayer('hometest2').stop();
	}
	}
	});
	e.preventDefault();
});



	
$(".test1").bind("click touch", function(e){
	$("#test1").show();
	$("#test2").hide();
	$("#test3").hide();
	$("#test4").hide();
	$("#test5").hide();
	$("#test6").hide();
			
  jwplayer('videotest1').setup({
	file: '/content/video/ashley.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
    width: 499, height: 281,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events: {
onComplete: function() { 
$('.test2').trigger('click');
}
}
  });
	jwplayer('videotest2').stop();
	jwplayer('videotest3').stop();
	jwplayer('videotest4').stop();
	jwplayer('videotest5').stop();
	jwplayer('videotest6').stop();
	e.preventDefault();
});

$(".test2").bind("click touch", function(e){
	$("#test1").hide();
	$("#test2").show();
	$("#test3").hide();
	$("#test4").hide();
	$("#test5").hide();
	$("#test6").hide();
		
	jwplayer('videotest1').stop();
  jwplayer('videotest2').setup({
	file: '/content/video/stephanie.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
    width: 499, height: 281,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events: {
onComplete: function() { 
$('.test3').trigger('click');
}
}
  });
	jwplayer('videotest3').stop();
	jwplayer('videotest4').stop();
	jwplayer('videotest5').stop();
	jwplayer('videotest6').stop();

		e.preventDefault();
});

$(".test3").bind("click touch", function(e){
	$("#test1").hide();
	$("#test2").hide();
	$("#test3").show();
	$("#test4").hide();
	$("#test5").hide();
	$("#test6").hide();
	
	jwplayer('videotest1').stop();
	jwplayer('videotest2').stop();
  jwplayer('videotest3').setup({
	file: '/content/video/megan.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
    width: 499, height: 281,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events: {
onComplete: function() { 
$('.test4').trigger('click');
}
}
  });
	jwplayer('videotest4').stop();
	jwplayer('videotest5').stop();
	jwplayer('videotest6').stop();

		e.preventDefault();
});

$(".test4").bind("click touch", function(e){
	$("#test1").hide();
	$("#test2").hide();
	$("#test3").hide();
	$("#test4").show();
	$("#test5").hide();
	$("#test6").hide();
		
	jwplayer('videotest1').stop();
	jwplayer('videotest2').stop();
	jwplayer('videotest3').stop();
	jwplayer('videotest5').stop();
	jwplayer('videotest6').stop();

  jwplayer('videotest4').setup({
	file: '/content/video/theresa.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
    width: 499, height: 281,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events: {
onComplete: function() { 
$('.test5').trigger('click');
}
}
  });
		
		e.preventDefault();
});

$(".test5").bind("click touch", function(e){
	$("#test1").hide();
	$("#test2").hide();
	$("#test3").hide();
	$("#test4").hide();
	$("#test5").show();
	$("#test6").hide();
		
	jwplayer('videotest1').stop();
	jwplayer('videotest2').stop();
	jwplayer('videotest3').stop();
	jwplayer('videotest4').stop();
	jwplayer('videotest6').stop();

  jwplayer('videotest5').setup({
	file: '/content/video/jennifer.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
    width: 499, height: 281,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events: {
onComplete: function() { 
$('.test6').trigger('click');
}
}
  });
		
		e.preventDefault();
});


$(".test6").bind("click touch", function(e){
	$("#test1").hide();
	$("#test2").hide();
	$("#test3").hide();
	$("#test4").hide();
	$("#test5").hide();
	$("#test6").show();
		
	jwplayer('videotest1').stop();
	jwplayer('videotest2').stop();
	jwplayer('videotest3').stop();
	jwplayer('videotest4').stop();
	jwplayer('videotest5').stop();

  jwplayer('videotest6').setup({
	file: '/content/video/carly.mp4',
	autostart: true,
    primary: "flash",   
	controls: true,
    width: 499, height: 281,
	stretching: 'exactfit',
	skin: '/scripts/jwplayer/five.xml',
	events: {
onComplete: function() { 
$('.test1').trigger('click');
}
}
  });
		
		e.preventDefault();
});



});

function pageLoad() //use to resolve postback issues
{
//put jquery code that doesn't work with document ready in here	
}
function MM_showHideLayers() { //v9.0    
    window.scrollTo(0, 0);
    var i, p, v, obj, args = MM_showHideLayers.arguments;
    for (i = 0; i < (args.length - 2); i += 3)
        with (document) if (getElementById && ((obj = getElementById(args[i])) != null)) {
            v = args[i + 2];
            if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
            obj.visibility = v;
        }
}