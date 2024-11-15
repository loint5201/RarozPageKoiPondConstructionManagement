using Domain.Entities;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Design
{
    [AuthorizeRole(Domain.Enums.AppRoles.Design_Staff)]
    public class ListModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public List<KoiDesign> KoiDesigns { get; set; }
        public ListModel(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            KoiDesigns = await _context.KoiDesigns.ToListAsync();
        }
    }
}
