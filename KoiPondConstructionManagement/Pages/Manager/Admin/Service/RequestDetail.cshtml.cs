using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.ResponseData;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KoiPondConstructionManagement.Pages.Manager.Admin.Service
{
    [AuthorizeRole(Domain.Enums.AppRoles.Manager)]
    public class RequestDetailModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public ConstructionRequest? ConstructionRequest;
        private IMapper _mapper;

        public List<ConstructionProcess> LstConstructionProcess;

        public CustomerOrderHistory? CustomerOrderHistory;
        public string UserSystem { get; set; }
        public string ConstructionProcesses { get; set; }
        public RequestDetailModel(KoiPondConstructionManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy chi tiết dịch vụ khách hàng yêu cầu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnGetAsync(int id)
        {
            ConstructionRequest = await _context.ConstructionRequests
                .Include(x => x.MaintenanceService)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.RequestId == id);

            var systemRoles = new List<int>
            {
                (int)AppRoles.Construction_Staff,
                (int)AppRoles.Design_Staff,
                //(int)AppRoles.Consulting_Staff,
                //(int)AppRoles.Manager,
            };

            var systemUsers = await _context.Users.Where(x => systemRoles.Contains(x.RoleId.Value)).ToListAsync();
            var systemUsersResponse = _mapper.Map<List<UserResponse>>(systemUsers);
            UserSystem = JsonConvert.SerializeObject(systemUsersResponse);

            LstConstructionProcess = await _context.ConstructionProcesses
                                            .Include(x => x.AssignedStaff)
                                            .Where(x => x.RequestId == id)
                                            .ToListAsync();

            var processResponse = _mapper.Map<List<ConstructionProcessResponse>>(LstConstructionProcess);
            ConstructionProcesses = JsonConvert.SerializeObject(processResponse);

            CustomerOrderHistory = await _context.CustomerOrderHistories
                    .FirstOrDefaultAsync(x => x.RequestId == id);
        }

        /// <summary>
        /// Cập nhật lại quy trình
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostUpdateProcessAsync(int requestId, List<ConstructionProcessRequest> process)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                // Lấy request cần xử lý
                var request = await _context.ConstructionRequests
                    .FirstOrDefaultAsync(x => x.RequestId == requestId);

                if (request == null)
                {
                    return NotFound();
                }

                // Nếu request đã duyệt, cập nhật trạng thái sang "In Progress"
                if (request.Status == (int)ConstructionRequestsStatus.Approved)
                {
                    request.Status = (int)ConstructionRequestsStatus.InProgress;
                }

                // Lấy danh sách quy trình hiện tại từ database
                var existingProcesses = await _context.ConstructionProcesses
                    .Where(x => x.RequestId == requestId)
                    .ToListAsync();

                var processIds = process.Select(x => x.ProcessId).ToHashSet();

                // Xóa các process không còn tồn tại trong danh sách mới
                var processesToDelete = existingProcesses
                    .Where(x => !processIds.Contains(x.ProcessId))
                    .ToList();

                if (processesToDelete.Any())
                {
                    _context.ConstructionProcesses.RemoveRange(processesToDelete);
                }

                // Cập nhật hoặc thêm mới các process
                foreach (var item in process)
                {
                    var processEntity = existingProcesses.FirstOrDefault(x => x.ProcessId == item.ProcessId);
                    if (processEntity == null)
                    {
                        // Tạo mới process
                        processEntity = new ConstructionProcess
                        {
                            RequestId = requestId,
                            Step = item.Step,
                            Status = item.Status,
                            Note = item.Note,
                            StepInfo = item.StepInfo,
                            AssignedStaffId = item.AssignedStaffId,
                            CreatedAt = DateTime.Now,
                        };
                        _context.ConstructionProcesses.Add(processEntity);
                    }
                    else
                    {
                        // Cập nhật process hiện có
                        if (processEntity.Status != item.Status)
                        {
                            processEntity.UpdatedAt = DateTime.Now;
                        }

                        processEntity.Status = item.Status;
                        processEntity.Note = item.Note;
                        processEntity.StepInfo = item.StepInfo;
                        processEntity.AssignedStaffId = item.AssignedStaffId;
                        _context.ConstructionProcesses.Update(processEntity);
                    }
                }

                // Lưu thay đổi tất cả vào database
                await _context.SaveChangesAsync();

                // Kiểm tra nếu tất cả các quy trình đã hoàn thành
                var allCompleted = await _context.ConstructionProcesses
                    .Where(x => x.RequestId == requestId)
                    .AllAsync(x => x.Status == (int)ConstructionProcessStatus.Completed);

                if (allCompleted)
                {
                    request.Status = (int)ConstructionRequestsStatus.Completed;
                    await _context.SaveChangesAsync();
                }

                serviceResponse.OnSuccess();
            }
            catch (Exception ex)
            {
                serviceResponse.OnException(ex);
            }

            return new JsonResult(serviceResponse);
        }


        /// <summary>
        /// Khởi tạo đơn thanh toán
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostCreateOrderAsync(int requestId, CustomerOrderHistory order)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var request = await _context.ConstructionRequests.FirstOrDefaultAsync(x => x.RequestId == requestId);
                if (request == null)
                {
                    return NotFound();
                }

                var orderEntity = await _context.CustomerOrderHistories.FirstOrDefaultAsync(x => x.RequestId == requestId);
                if (orderEntity == null)
                {
                    order.RequestId = requestId;
                    order.CustomerId = request.CustomerId;
                    order.CreatedAt = DateTime.Now;
                    _context.CustomerOrderHistories.Add(order);
                }
                else
                {
                    orderEntity.PaymentMethod = order.PaymentMethod;
                    orderEntity.PaymentStatus = order.PaymentStatus;
                    orderEntity.ActualCost = order.ActualCost;
                    _context.CustomerOrderHistories.Update(orderEntity);
                }

                await _context.SaveChangesAsync();
                serviceResponse.OnSuccess();
            }
            catch (Exception ex)
            {
                serviceResponse.OnException(ex);
            }

            return new JsonResult(serviceResponse);
        }

        /// <summary>
        /// Xử lý upload file
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostUploadFile(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                // Đường dẫn lưu file
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temps");
                Directory.CreateDirectory(uploadsPath); // Tạo thư mục nếu chưa tồn tại

                var filePath = Path.Combine(uploadsPath, upload.FileName);

                // Lưu file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }

                // Trả về JSON với đường dẫn file đã upload
                return new JsonResult(new
                {
                    uploaded = 1,
                    url = $"/uploads/temps/{upload.FileName}" // URL để truy cập file
                });
            }

            return BadRequest();
        }
    }
}
