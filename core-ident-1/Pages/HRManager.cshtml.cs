using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace core_ident_1.Pages
{
    [Authorize(MyGlobals.PolicyHRManager)]
    public class HRManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
