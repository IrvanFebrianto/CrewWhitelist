//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MyGlobalMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MyGlobalMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.MyGlobalMessage", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        ///   Looks up a localized string similar to {0} berhasil disimpan..
        /// </summary>
        internal static string CreateSuccess {
            get {
                return ResourceManager.GetString("CreateSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Anda yakin menghapus data?.
        /// </summary>
        internal static string DeleteConfirmation {
            get {
                return ResourceManager.GetString("DeleteConfirmation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data gagal dihapus..
        /// </summary>
        internal static string DeleteFailed {
            get {
                return ResourceManager.GetString("DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data gagal dihapus. {0}.
        /// </summary>
        internal static string DeleteFailedWithMessage {
            get {
                return ResourceManager.GetString("DeleteFailedWithMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data berhasil dihapus..
        /// </summary>
        internal static string DeleteSuccess {
            get {
                return ResourceManager.GetString("DeleteSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} berhasil dihapus..
        /// </summary>
        internal static string DeleteSuccessWithParam {
            get {
                return ResourceManager.GetString("DeleteSuccessWithParam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} berhasil diubah..
        /// </summary>
        internal static string EditSuccess {
            get {
                return ResourceManager.GetString("EditSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File yang diperbolehkan hanya berupa jpg/jpeg, png, bmp..
        /// </summary>
        internal static string FileUploadImageTypeError {
            get {
                return ResourceManager.GetString("FileUploadImageTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ukuran file gambar tidak boleh yang melebihi {0}..
        /// </summary>
        internal static string FileUploadSizeError {
            get {
                return ResourceManager.GetString("FileUploadSizeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data {0} berhasil di-import..
        /// </summary>
        internal static string ImportSuccess {
            get {
                return ResourceManager.GetString("ImportSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File berhasil disimpan..
        /// </summary>
        internal static string UploadSuccess {
            get {
                return ResourceManager.GetString("UploadSuccess", resourceCulture);
            }
        }
    }
}
