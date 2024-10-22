using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.ResponseData;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KoiPondConstructionManagement.Pages.Manager.Admin.Service
{
    public class RequestDetailModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public ConstructionRequest? ConstructionRequest;
        private IMapper _mapper;
        public string UserSystem { get; set; }
        public string ConstructionProcesses { get; set; }
        public RequestDetailModel(KoiPondConstructionManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

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
                (int)AppRoles.Consulting_Staff,
                (int)AppRoles.Manager,
            };

            var systemUsers = await _context.Users.Where(x => systemRoles.Contains(x.RoleId.Value)).ToListAsync();
            var systemUsersResponse = _mapper.Map<List<UserResponse>>(systemUsers);
            UserSystem = JsonConvert.SerializeObject(systemUsersResponse);

            var lstProcess = await _context.ConstructionProcesses.Where(x => x.RequestId == id).ToListAsync();
            var processResponse = _mapper.Map<List<ConstructionProcessResponse>>(lstProcess);
            ConstructionProcesses = JsonConvert.SerializeObject(processResponse);
        }

        public async Task<IActionResult> OnPostUpdateProcessAsync(int requestId, List<ConstructionProcessRequest> process)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var request = await _context.ConstructionRequests.FirstOrDefaultAsync(x => x.RequestId == requestId);
                if (request == null)
                {
                    return NotFound();
                }

                var lstProcess = await _context.ConstructionProcesses.Where(x => x.RequestId == requestId).ToListAsync();
                if (lstProcess.Count > 0)
                {
                    var processIds = process.Select(x => x.ProcessId).ToList();
                    var deleteProcess = lstProcess.Where(x => !processIds.Contains(x.ProcessId)).ToList();
                    if (deleteProcess.Count > 0)
                    {
                        _context.ConstructionProcesses.RemoveRange(deleteProcess);
                    }
                }

                foreach (var item in process)
                {
                    var processEntity = lstProcess.FirstOrDefault(x => x.ProcessId == item.ProcessId);
                    if (processEntity == null)
                    {
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
                        continue;
                    }

                    processEntity.Status = item.Status;
                    processEntity.Note = item.Note;
                    processEntity.StepInfo = item.StepInfo;
                    processEntity.AssignedStaffId = item.AssignedStaffId;
                    processEntity.UpdatedAt = DateTime.Now;
                    _context.ConstructionProcesses.Update(processEntity);
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
    }
}
