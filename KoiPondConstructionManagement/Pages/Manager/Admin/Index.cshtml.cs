using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiPondConstructionManagement.Pages.Manager.Admin
{
    [AuthorizeRole(Domain.Enums.AppRoles.Manager)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
