using Volo.Abp.Bundling;

namespace HIS.Blazor;

/* Add your global styles/scripts here.
 * See https://docs.abp.io/en/abp/latest/UI/Blazor/Global-Scripts-Styles to learn how to use it
 */
public class HISBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        //context.Add("jquery.js");
        //context.Add("libs/bootstrap/js/bootstrap.bundle.min.js", true);
        //context.Add("libs/toastr/toastr.min.js", true);
        //context.Add("common.js");
    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
        //context.Add("libs/toastr/toastr.min.css",true);
    }
}
