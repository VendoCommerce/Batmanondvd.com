// JavaScript Document

WebFontConfig = {
    google: { families: ['Open+Sans+Condensed:700,300:latin'] }
};
(function () {
    var wf = document.createElement('script');
    wf.src = ('https:' == document.location.protocol ? 'https' : 'http') +
      '://ajax.googleapis.com/ajax/libs/webfont/1/webfont.js';
    wf.type = 'text/javascript';
    wf.async = 'true';
    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(wf, s);
})();

$(document).ready(function () {
	
$(this).scrollTop(0);
	
	
$('.episodebox').hide();
$('#classic_collection').show();
$('#selectField').change(function () {
    $('.episodebox').fadeOut();
    $('#' + $(this).val()).fadeIn();
});


	
    //cvv overlay
$(".cvv").fancybox({
    fitToView: false,
    wrapCSS: 'nowrapper',
    closeBtn: false,
    padding: 0,
    width: 614,
    height: 733,
    autoSize: false,
    closeClick: true,
    scrolling: 'no',
    helpers: {
        overlay: {
            opacity: 0.6,
            locked: false
        }
    }
});

$(".guarantee").fancybox({
    closeBtn: false,
    fitToView: false,
    wrapCSS: 'nowrapper',
    padding: 0,
    width: 606,
    height: 580,
    autoSize: false,
    scrolling: 'no',
    helpers: {
        overlay: {
            locked: false,
            css: {
                'background': 'rgba(0,0,0,.8)'
            }
        }
    }
});

$(".included").fancybox({
    closeBtn: false,
    fitToView: false,
    wrapCSS: 'nowrapper',
    padding: 0,
    margin: 0,
    width: 628,
    height: 2262,
    autoSize: false,
    scrolling: 'no',
    helpers: {
        overlay: {
            locked: false,
            css: {
                'background': 'rgba(0,0,0,.8)'
            }
        }
    }
});

$(".included_complete").fancybox({
    closeBtn: false,
    fitToView: false,
    wrapCSS: 'nowrapper',
    padding: 0,
    margin: 0,
    width: 628,
    height: 2462,
    autoSize: false,
    scrolling: 'no',
    helpers: {
        overlay: {
            locked: false,
            css: {
                'background': 'rgba(0,0,0,.8)'
            }
        }
    }
});

$('.togglefaq').click(function (e) {
    e.preventDefault();
    var notthis = $('.active').not(this);
    notthis.find('.icon-minus').addClass('icon-plus').removeClass('icon-minus');
    notthis.toggleClass('active').next('.faqanswer').slideToggle(300);
    $(this).toggleClass('active').next().slideToggle("fast");
    $(this).children('i').toggleClass('icon-plus icon-minus');
});




$('.toggleNav').click(function () {
    $('#headernav').slideToggle();
});


$('.opendetails').click(function () {
    $(this).parent().next().slideDown(200);
    $(this).parent().slideUp(200);

});

$('.closedetails').click(function () {
    $(this).parent().parent().slideUp(200);
    $(this).parent().parent().prev().slideDown(200);

});



/* Video Clips */

$(".test1").bind("click touch", function (e) {
    $("#test1").show();
    $("#test2").hide();
    $("#test3").hide();
    $("#test4").hide();
    $("#test5").hide();
    $("#test6").hide();
    $("#test7").hide();

    jwplayer('videotest1').setup({
        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Aunt_Harriet.mp4',
        autostart: true,
        controls: true,
        width: 468, height: 284,
        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_1.png',
        stretching: 'exactfit',
        skin: '/scripts/jwplayer/bekle.xml',
        events: {
            onComplete: function () {
                $('.test2').trigger('click');
            }
        }
    });
    jwplayer('videotest2').stop();
    jwplayer('videotest3').stop();
    jwplayer('videotest4').stop();
    jwplayer('videotest5').stop();
    jwplayer('videotest6').stop();
    jwplayer('videotest7').stop();
    e.preventDefault();
});

$(".test2").bind("click touch", function (e) {
    $("#test1").hide();
    $("#test2").show();
    $("#test3").hide();
    $("#test4").hide();
    $("#test5").hide();
    $("#test6").hide();
    $("#test7").hide();

    jwplayer('videotest1').stop();
    jwplayer('videotest2').setup({
        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/RiddlerFight.mp4',
        autostart: true,
        controls: true,
        width: 468, height: 284,
        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_2.png',
        stretching: 'exactfit',
        skin: '/scripts/jwplayer/bekle.xml',
        events: {
            onComplete: function () {
                $('.test3').trigger('click');
            }
        }
    });
    jwplayer('videotest3').stop();
    jwplayer('videotest4').stop();
    jwplayer('videotest5').stop();
    jwplayer('videotest6').stop();
    jwplayer('videotest7').stop();

    e.preventDefault();
});

$(".test3").bind("click touch", function (e) {
    $("#test1").hide();
    $("#test2").hide();
    $("#test3").show();
    $("#test4").hide();
    $("#test5").hide();
    $("#test6").hide();
    $("#test7").hide();

    jwplayer('videotest1').stop();
    jwplayer('videotest2').stop();
    jwplayer('videotest3').setup({
        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Flying_Blind.mp4',
        autostart: true,
        controls: true,
        width: 468, height: 284,
        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_3.png',
        stretching: 'exactfit',
        skin: '/scripts/jwplayer/bekle.xml',
        events: {
            onComplete: function () {
                $('.test4').trigger('click');
            }
        }
    });
    jwplayer('videotest4').stop();
    jwplayer('videotest5').stop();
    jwplayer('videotest6').stop();
    jwplayer('videotest7').stop();

    e.preventDefault();
});

$(".test4").bind("click touch", function (e) {
    $("#test1").hide();
    $("#test2").hide();
    $("#test3").hide();
    $("#test4").show();
    $("#test5").hide();
    $("#test6").hide();
    $("#test7").hide();

    jwplayer('videotest1').stop();
    jwplayer('videotest2').stop();
    jwplayer('videotest3').stop();
    jwplayer('videotest5').stop();
    jwplayer('videotest6').stop();
    jwplayer('videotest7').stop();

    jwplayer('videotest4').setup({
        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/Cloud_Mens_Minds.mp4',
        autostart: true,
        controls: true,
        width: 468, height: 284,
        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_4.png',
        stretching: 'exactfit',
        skin: '/scripts/jwplayer/bekle.xml',
        events: {
            onComplete: function () {
                $('.test5').trigger('click');
            }
        }
    });

    e.preventDefault();
});

$(".test5").bind("click touch", function (e) {
    $("#test1").hide();
    $("#test2").hide();
    $("#test3").hide();
    $("#test4").hide();
    $("#test5").show();
    $("#test6").hide();
    $("#test7").hide();

    jwplayer('videotest1').stop();
    jwplayer('videotest2').stop();
    jwplayer('videotest3').stop();
    jwplayer('videotest4').stop();
    jwplayer('videotest6').stop();
    jwplayer('videotest7').stop();

    jwplayer('videotest5').setup({
        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/CatFight.mp4',
        autostart: true,
        controls: true,
        width: 468, height: 284,
        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_5.png',
        stretching: 'exactfit',
        skin: '/scripts/jwplayer/bekle.xml',
        events: {
            onComplete: function () {
                $('.test6').trigger('click');
            }
        }
    });

    e.preventDefault();
});


$(".test6").bind("click touch", function (e) {
    $("#test1").hide();
    $("#test2").hide();
    $("#test3").hide();
    $("#test4").hide();
    $("#test5").hide();
    $("#test6").show();
    $("#test7").hide();

    jwplayer('videotest1').stop();
    jwplayer('videotest2').stop();
    jwplayer('videotest3').stop();
    jwplayer('videotest4').stop();
    jwplayer('videotest5').stop();
    jwplayer('videotest7').stop();

    jwplayer('videotest6').setup({
        file: 'https://d1kg9stb0ddjcv.cloudfront.net/video/MinstrelCliffHanger.mp4',
        autostart: true,
        controls: true,
        width: 468, height: 284,
        image: 'https://d1kg9stb0ddjcv.cloudfront.net/images/mobile/m_thumb_6.png',
        stretching: 'exactfit',
        skin: '/scripts/jwplayer/bekle.xml',
        events: {
            onComplete: function () {
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
    //window.scrollTo(0, 0);
    var i, p, v, obj, args = MM_showHideLayers.arguments;
    for (i = 0; i < (args.length - 2); i += 3)
        with (document) if (getElementById && ((obj = getElementById(args[i])) != null)) {
            v = args[i + 2];
            if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
            obj.visibility = v;
        }
}