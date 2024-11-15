using Domain.Entities;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Consulting
{
    [AuthorizeRole(Domain.Enums.AppRoles.Consulting_Staff)]
    public class IndexModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public List<ConstructionRequest> ConstructionRequests;
        public IndexModel(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ConstructionRequests = await _context.ConstructionRequests
                .Include(x => x.MaintenanceService)
                .Include(x => x.Customer)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
