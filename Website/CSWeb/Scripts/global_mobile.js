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
$('#season1').show();
$('#selectField').change(function () {
    $('.episodebox').hide();
    $('#' + $(this).val()).show();
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