using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiPondConstructionManagement.Pages.Manager.Construction
{
    [AuthorizeRole(Domain.Enums.AppRoles.Construction_Staff)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
