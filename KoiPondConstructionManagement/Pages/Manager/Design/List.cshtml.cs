using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Design
{
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
