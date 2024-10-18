using Domain.Entities;
using Domain.Extension;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KoiPondConstructionManagement.Pages.User
{
    /// <summary>
    /// 
    /// </summary>
    [AuthorizeRole(Domain.Enums.AppRoles.Customer)]
    public class MyServiceModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public List<ConstructionRequest> ConstructionRequests { get; set; }
        public MyServiceModel(KoiPondConstructionManagementContext context)
        {
            _context = context;
            ConstructionRequests = [];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            ConstructionRequests = await _context.ConstructionRequests
                .Include(x => x.MaintenanceService)
                .Include(x => x.Design)
                .Where(x => x.CustomerId == User.GetUserId())
                .ToListAsync();
        }
    }
}
