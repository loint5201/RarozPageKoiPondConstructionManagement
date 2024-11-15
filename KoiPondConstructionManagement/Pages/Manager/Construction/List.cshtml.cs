﻿using Domain.Entities;
using Domain.Enums;
using Domain.Extension;
using Domain.ResponseData;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Construction
{
    public class ListModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        public ListModel(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        public List<IGrouping<ConstructionRequest?, ConstructionProcess>> ConstructionGroups;

        [BindProperty(SupportsGet = true)]
        public int Status { get; set; } = (int)ConstructionProcessStatus.All;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {

            var query = _context.ConstructionProcesses
                .Where(cp => cp.AssignedStaffId == User.GetUserId());

            if (Status != (int)ConstructionProcessStatus.All)
            {
                query = query.Where(cp => cp.Status == Status);
            }

            ConstructionGroups = await query
                .Include(x => x.Request)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.Request)
                    .ThenInclude(x => x.MaintenanceService)
                .OrderBy(x => x.CreatedAt)
                .GroupBy(x => x.Request)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostUpdateProcessStatusAsync(List<ConstructionProcessRequest> processes)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var processIds = processes.Select(p => p.ProcessId).ToList();
                var exists = await _context.ConstructionProcesses
                    .Where(x => processIds.Contains(x.ProcessId) && x.AssignedStaffId == User.GetUserId())
                    .ToListAsync();

                bool hasChange = false;
                if (exists.Count > 0)
                {
                    var requestId = exists.FirstOrDefault().RequestId;
                    var request = _context.ConstructionRequests.FirstOrDefault(x => x.RequestId == requestId);
                    if (request != null && request.Status == (int)ConstructionRequestsStatus.Completed)
                    {
                        serviceResponse.OnError("Không thể cập nhật");
                        return new JsonResult(serviceResponse);
                    }
                }

                foreach (var process in processes)
                {
                    var exist = exists.FirstOrDefault(x => x.ProcessId == process.ProcessId);
                    if (exist != null)
                    {
                        hasChange = true;
                        if (exist.Status != process.Status)
                        {
                            exist.Status = process.Status;
                            exist.UpdatedAt = DateTime.Now;
                        }
                        exist.Note = process.Note;
                    }
                }

                if (hasChange)
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.OnException(ex);
            }
            return new JsonResult(serviceResponse);
        }
    }
}