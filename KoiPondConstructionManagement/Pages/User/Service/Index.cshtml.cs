using Domain.Entities;
using Domain.Enums;
using Domain.Extension;
using Domain.ResponseData;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.User.Service
{
    /// <summary>
    /// 
    /// </summary>
    [AuthorizeRole(Domain.Enums.AppRoles.Customer)]
    public class IndexModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public List<ConstructionRequest> ConstructionRequests { get; set; }
        public IndexModel(KoiPondConstructionManagementContext context)
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
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostCancelService(int requestId)
        {
            ServiceResponse response = new();

            var constructionRequests = _context.ConstructionRequests.FirstOrDefault(x => x.RequestId == requestId);
            if (constructionRequests == null)
            {
                response.OnError("Request not found.");
                return new JsonResult(response);
            }

            if (constructionRequests.Status == (int)ConstructionRequestsStatus.Cancelled)
            {
                response.OnError("Request already cancelled.");
                return new JsonResult(response);
            }

            if (constructionRequests.Status != (int)ConstructionRequestsStatus.Pending)
            {
                response.OnError("Request can't be cancelled.");
                return new JsonResult(response);
            }

            constructionRequests.Status = (int)ConstructionRequestsStatus.Cancelled;
            _context.ConstructionRequests.Update(constructionRequests);
            await _context.SaveChangesAsync();

            return new JsonResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnGetDetail(int id)
        {
            return Page();
        }
    }
}
