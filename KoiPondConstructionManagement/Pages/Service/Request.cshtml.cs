using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Extension;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Service
{
    [AuthorizeRole(Domain.Enums.AppRoles.Customer)]
    public class RequestModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public Domain.Entities.User? CurrentUser { get; set; }
        public List<Domain.Entities.MaintenanceService>? MaintenanceServices { get; set; }
        private readonly IMapper _mapper;

        [BindProperty]
        public ConstructionRequestDTO Request { get; set; }  // Model chứa thông tin từ form

        public RequestModel(KoiPondConstructionManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task OnGetAsync()
        {
            CurrentUser = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == User.GetUserId());

            MaintenanceServices = await _context.MaintenanceServices
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Request != null)
            {
                ConstructionRequest request = _mapper.Map<ConstructionRequest>(Request);
                request.CustomerId = User.GetUserId();
                request.CreatedAt = DateTime.Now;
                request.UpdatedAt = DateTime.Now;
                request.Status = (int)ConstructionRequestsStatus.Pending;

                await _context.ConstructionRequests.AddAsync(request);
                await _context.SaveChangesAsync();

                return RedirectToPage("/User/MyService");
            }

            return Page();
        }
    }
}
