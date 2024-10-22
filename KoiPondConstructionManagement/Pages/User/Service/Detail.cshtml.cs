using Domain.Entities;
using Domain.Enums;
using Domain.Extension;
using Domain.ResponseData;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.User.Service
{
    public class DetailModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public ConstructionRequest? ConstructionRequest { get; set; }

        public List<Domain.Entities.MaintenanceService>? MaintenanceServices { get; set; }

        public List<ConstructionProcess> LstConstructionProcess;

        public CustomerOrderHistory? CustomerOrderHistory;
        public Feedback? Feedback;
        public DetailModel(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Chi tiết dịch vụ đã yêu cầu của người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnGetAsync(int id)
        {
            MaintenanceServices = await _context.MaintenanceServices
               .OrderBy(x => x.Order)
               .ToListAsync();

            ConstructionRequest = await _context.ConstructionRequests
                .Include(x => x.MaintenanceService)
                .Include(x => x.Design)
                .FirstOrDefaultAsync(x => x.RequestId == id && x.CustomerId == User.GetUserId());

            if (ConstructionRequest != null)
            {
                LstConstructionProcess = await _context.ConstructionProcesses
                                            .Include(x => x.AssignedStaff)
                                            .Where(x => x.RequestId == ConstructionRequest.RequestId)
                                            .ToListAsync();

                CustomerOrderHistory = await _context.CustomerOrderHistories
                    .FirstOrDefaultAsync(x => x.RequestId == ConstructionRequest.RequestId);

                Feedback = await _context.Feedbacks
                    .FirstOrDefaultAsync(x => x.RequestId == ConstructionRequest.RequestId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPutUpdateService(ConstructionRequestDTO request, int id)
        {
            ServiceResponse response = new();
            var constructionRequests = await _context.ConstructionRequests.FirstOrDefaultAsync(x => x.RequestId == id && x.CustomerId == User.GetUserId());
            if (constructionRequests == null)
            {
                response.OnError("Request not found.");
                return new JsonResult(response);
            }

            var maintainceService = await _context.MaintenanceServices.FirstOrDefaultAsync(x => x.ServiceId == constructionRequests.MaintenanceServiceId);
            if (maintainceService == null)
            {
                response.OnError("Service not found.");
                return new JsonResult(response);
            }

            if (constructionRequests.Status != (int)ConstructionRequestsStatus.Pending)
            {
                response.OnError("Request already processed.");
                return new JsonResult(response);
            }

            constructionRequests.MaintenanceServiceId = request.MaintenanceServiceId;
            constructionRequests.CustomDesignDescription = request.CustomDesignDescription;
            constructionRequests.CostEstimate = maintainceService.Price;
            constructionRequests.Status = (int)ConstructionRequestsStatus.Pending;
            constructionRequests.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostCreateFeedbackAsync(int requestId, Feedback feedbackDTO)
        {
            ServiceResponse response = new();
            try
            {
                var constructionRequests = await _context.ConstructionRequests.FirstOrDefaultAsync(x => x.RequestId == requestId && x.CustomerId == User.GetUserId());
                if (constructionRequests == null)
                {
                    response.OnError("Request not found.");
                    return new JsonResult(response);
                }

                var feedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.RequestId == requestId);
                if (feedback != null)
                {
                    response.OnError("Feedback already exists.");
                    return new JsonResult(response);
                }

                feedback = new Feedback
                {
                    RequestId = requestId,
                    CustomerId = constructionRequests.CustomerId,
                    Rating = feedbackDTO.Rating,
                    Comment = feedbackDTO.Comment,
                    CreatedAt = DateTime.Now,
                };

                await _context.Feedbacks.AddAsync(feedback);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                response.OnError(ex.Message);
            }

            return new JsonResult(response);
        }
    }
}
