using Application.Interfaces;
using AutoMapper;
using Domain.Extension;
using Domain.ResponseData;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.User.Profile
{
    public class IndexModel : PageModel
    {
        private readonly KoiPondConstructionManagementContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public Domain.Entities.User? CurrentUser { get; set; }

        [BindProperty]
        public RegisterRequest UserModel { get; set; }

        public List<Domain.Entities.Role>? Roles { get; set; }

        public IndexModel(KoiPondConstructionManagementContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task OnGetAsync()
        {
            CurrentUser = await _context.Users.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == User.GetUserId());

            Roles =
            [
                new() { RoleId = 2, RoleName = "Customer", RoleNameVietnam = "Khách hàng" },
                new() { RoleId = 3, RoleName = "Consulting Staff", RoleNameVietnam = "Nhân viên tư vấn" },
                new() { RoleId = 4, RoleName = "Design Staff", RoleNameVietnam = "Nhân viên thiết kế" },
                new() { RoleId = 5, RoleName = "Construction Staff", RoleNameVietnam = "Nhân viên thi công" },
                new() { RoleId = 6, RoleName = "Manager", RoleNameVietnam = "Quản lý" }
            ];
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var user = await _context.Users.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == User.GetUserId());

                if (user == null)
                {
                    serviceResponse.OnError("Không tìm thấy người dùng");
                    return new JsonResult(serviceResponse);
                }

                if (await _context.Users.AnyAsync(x => x.Email == UserModel.Email && x.UserId != user.UserId))
                {
                    serviceResponse.OnError("Email đã tồn tại");
                    return new JsonResult(serviceResponse);
                }

                if (UserModel.Avatar != null)
                {
                    if (!string.IsNullOrEmpty(user.Avatar))
                    {
                        await _imageService.DeleteImageAsync($"images/users/{User.GetUserId()}/avatar/{user.Avatar}");
                    }

                    user.Avatar = await _imageService.UploadImageAsync(UserModel.Avatar, $"images/users/{User.GetUserId()}/avatar");
                }

                _mapper.Map(UserModel, user);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.OnException(ex);
            }

            return new JsonResult(serviceResponse);
        }
    }
}
