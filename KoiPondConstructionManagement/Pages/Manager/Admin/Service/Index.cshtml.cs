using Domain.ResponseData;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Admin.Service
{
    [AuthorizeRole(Domain.Enums.AppRoles.Manager)]
    public class IndexModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public List<Domain.Entities.MaintenanceService> MaintenanceServices;
        public IndexModel(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            MaintenanceServices = await _context.MaintenanceServices
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public async Task<IActionResult> OnDeleteDeleteServiceAsync(int serviceId)
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var service = await _context.MaintenanceServices
                .FirstOrDefaultAsync(x => x.ServiceId == serviceId);

                if (service == null)
                {
                    serviceResponse.OnError("Không tìm thấy dịch vụ");
                    return new JsonResult(serviceResponse);
                }

                var request = _context.ConstructionRequests.FirstOrDefault(x => x.MaintenanceServiceId == serviceId);
                if (request != null)
                {
                    serviceResponse.OnError("Dịch vụ đang được sử dụng");
                    return new JsonResult(serviceResponse);
                }

                _context.MaintenanceServices.Remove(service);
                await _context.SaveChangesAsync();
                serviceResponse.OnSuccess("Xóa dịch vụ thành công");
            }
            catch (Exception e)
            {
                serviceResponse.OnError(e.Message);
                return new JsonResult(serviceResponse);
            }

            return new JsonResult(serviceResponse);
        }
    }
}
