﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSBusiness.Web.Scripts {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ScriptsResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ScriptsResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CSBusiness.Web.Scripts.ScriptsResource", typeof(ScriptsResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to var cs = new Object();
        ///cs.writeSysFields = function () {
        ///    document.write(&quot;&lt;input type=&apos;hidden&apos; id=&apos;&lt;TNT_C_ID&gt;&apos; name=&apos;&lt;TNT_C_ID&gt;&apos; /&gt;&lt;input type=&apos;hidden&apos; id=&apos;&lt;TNT_E_ID&gt;&apos; name=&apos;&lt;TNT_E_ID&gt;&apos; /&gt;&quot;);
        ///}
        ///cs.getCompleteVersionId = &lt;VERSION_COMBINE_FUNC&gt;
        ///
        ///cs.writeSysFields();.
        /// </summary>
        internal static string ConversionSystemsBase {
            get {
                return ResourceManager.GetString("ConversionSystemsBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string CSTrackingPixel {
            get {
                return ResourceManager.GetString("CSTrackingPixel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to cs.processTnTVars = function (cmpId,expId) {
        ///
        ///    var url = &quot;&lt;TNT_POST_URL&gt;&quot;;
        ///
        ///    jQuery.post(url, { &quot;token&quot;: &quot;&lt;TOKEN&gt;&quot;, &quot;tntCId&quot;: cmpId, &quot;tntEId&quot;: expId },
        ///        function (resp) { }
        ///        , &quot;json&quot;);
        ///
        ///}.
        /// </summary>
        internal static string Prop_ProcessTnTVars {
            get {
                return ResourceManager.GetString("Prop_ProcessTnTVars", resourceCulture);
            }
        }
    }
}