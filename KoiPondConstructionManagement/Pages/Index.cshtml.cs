using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiPondConstructionManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMaintenanceService _maintenanceService;
        public IEnumerable<MaintenanceService> MaintenanceService { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMaintenanceService maintenanceService)
        {
            _logger = logger;
            _maintenanceService = maintenanceService;
        }

        public async Task OnGetAsync()
        {
            MaintenanceService = await _maintenanceService.GetByCondition(orderBy: x => x.OrderBy(y => y.Order));
        }
    }
}
