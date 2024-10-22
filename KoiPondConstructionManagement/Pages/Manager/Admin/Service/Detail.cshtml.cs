using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ResponseData;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Admin.Service
{
    public class DetailModel : PageModel
    {
        private readonly KoiPondConstructionManagementContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public Domain.Entities.MaintenanceService? MaintenanceService { get; set; }

        [BindProperty]
        public MaintenanceServiceDTO MaintenanceServiceDTO { get; set; }

        public DetailModel(KoiPondConstructionManagementContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task OnGetAsync(int id)
        {
            MaintenanceService = await _context.MaintenanceServices
                .FirstOrDefaultAsync(x => x.ServiceId == id);

            MaintenanceService ??= new Domain.Entities.MaintenanceService();
            if (MaintenanceService.ServiceId == 0)
            {
                MaintenanceService.Order = await _context.MaintenanceServices.MaxAsync(x => x.Order);
                MaintenanceService.Order++;
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            if (id != 0)
            {
                MaintenanceServiceDTO.ServiceId = id;
                var service = await _context.MaintenanceServices
                    .FirstOrDefaultAsync(x => x.ServiceId == id);
                if (service == null)
                {
                    serviceResponse.OnError("Không tìm thấy dịch vụ");
                    return new JsonResult(serviceResponse);
                }

                if (MaintenanceServiceDTO.ServiceImage != null)
                {
                    if (!string.IsNullOrEmpty(service.ServiceName))
                    {
                        await _imageService.DeleteImageAsync($"images/services/{service.ServiceName}");
                    }

                    service.ServiceImage = await _imageService.UploadImageAsync(MaintenanceServiceDTO.ServiceImage, $"images/services");
                }
                _mapper.Map(MaintenanceServiceDTO, service);
                await _context.SaveChangesAsync();
            }
            else
            {
                var service = _mapper.Map<MaintenanceService>(MaintenanceServiceDTO);
                if (MaintenanceServiceDTO.ServiceImage != null)
                {
                    service.ServiceImage = await _imageService.UploadImageAsync(MaintenanceServiceDTO.ServiceImage, $"images/services");
                }
                _context.MaintenanceServices.Add(service);
                await _context.SaveChangesAsync();
                serviceResponse.Data = service.ServiceId;
            }
            return new JsonResult(serviceResponse);
        }
    }
}
