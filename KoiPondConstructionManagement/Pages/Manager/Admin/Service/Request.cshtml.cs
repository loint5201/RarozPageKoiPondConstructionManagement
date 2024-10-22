using Domain.Entities;
using Domain.Enums;
using Domain.ResponseData;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Admin.Service
{
    public class RequestModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public List<ConstructionRequest> ConstructionRequests;
        public RequestModel(KoiPondConstructionManagementContext context)
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

        public async Task<IActionResult> OnPostChangeStatusAsync(int requestId, UpdateConstructionRequest request)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var constructionRequest = await _context.ConstructionRequests
                    .FirstOrDefaultAsync(x => x.RequestId == requestId);

                if (constructionRequest == null)
                {
                    serviceResponse.OnError("Dịch vụ yêu cầu không tồn tại");
                    return new JsonResult(serviceResponse);
                }


                //if ((constructionRequest.Status != (int)ConstructionRequestsStatus.Pending &&
                //    constructionRequest.Status == (int)ConstructionRequestsStatus.Cancelled)
                //    && request.Status == (int)ConstructionRequestsStatus.Approved)
                //{
                //    serviceResponse.OnError("Không thể chấp nhận yêu cầu này");
                //    return new JsonResult(serviceResponse);
                //}

                //if ((constructionRequest.Status == (int)ConstructionRequestsStatus.Completed ||
                //    constructionRequest.Status == (int)ConstructionRequestsStatus.Cancelled)
                //    && request.Status == (int)ConstructionRequestsStatus.Cancelled)
                //{
                //    serviceResponse.OnError("Không thể thay đổi trạng thái yêu cầu này");
                //    return new JsonResult(serviceResponse);
                //}

                constructionRequest.Status = request.Status;
                constructionRequest.RejectReason = request.RejectReason;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return new JsonResult(serviceResponse);
        }
    }
}
