using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly KoiPondConstructionManagementContext _context;
        public List<KoiDesign> KoiDesigns { get; set; }
        public IndexModel(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            KoiDesigns = await _context.KoiDesigns.ToListAsync();
        }
    }
}
