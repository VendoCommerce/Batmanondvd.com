$(function() {
if (window['optimizely']!= null)
{
	if(window['optimizely'].data.state.activeExperiments.length != 0)
	{
		var versionName = window['optimizely'].data.state.variationNamesMap[window['optimizely'].data.state.activeExperiments];
		var versionId = window['optimizely'].data.state.variationIdsMap[window['optimizely'].data.state.activeExperiments];
		var experimentId = window['optimizely'].data.state.activeExperiments;
		var domain  = $(location).attr('host').replace('www','');
		$.cookie("csexperience", versionName,{domain: domain});
		$.cookie("csinfo", experimentId+';'+versionId,{domain: domain});
	}
}
}
);
function GetClientVersionName(siteVersion)
{
		var versionName = '';	
	if($.cookie('csexperience')!=null)
		{versionName = $.cookie('csexperience');}
	else if (window['optimizely']== null)
		{versionName = siteVersion}
	else if (window['optimizely']!= null)
		{
			if(window['optimizely'].data.state.activeExperiments.length == 0)
			{
				versionName = siteVersion;
			}
			else
			{	
				versionName = window['optimizely'].data.state.variationNamesMap[window			['optimizely'].data.state.activeExperiments];
			}
		}
	return versionName;
}

