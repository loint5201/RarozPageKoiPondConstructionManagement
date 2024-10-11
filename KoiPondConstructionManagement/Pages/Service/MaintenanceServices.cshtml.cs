using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiPondConstructionManagement.Pages.Service
{
    public class MaintenanceServicesModel : PageModel
    {
        private readonly IMaintenanceService _maintenanceService;
        public IEnumerable<MaintenanceService> MaintenanceService { get; set; }
        public int Id { get; set; }
        public MaintenanceService MaintenanceServiceSelected { get; set; }
        public MaintenanceServicesModel(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        public async Task OnGetAsync(int id)
        {
            Id = id;
            MaintenanceService = await _maintenanceService.GetAllAsync();

            if (MaintenanceService != null)
            {
                if (Id == 0) Id = MaintenanceService.FirstOrDefault().ServiceId;

                MaintenanceServiceSelected = MaintenanceService.FirstOrDefault(x => x.ServiceId == Id);
            }
        }
    }
}
