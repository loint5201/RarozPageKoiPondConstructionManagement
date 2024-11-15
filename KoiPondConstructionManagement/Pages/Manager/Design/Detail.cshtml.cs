using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using KoiPondConstructionManagement.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiPondConstructionManagement.Pages.Manager.Design
{
    [AuthorizeRole(Domain.Enums.AppRoles.Design_Staff)]
    public class DetailModel : PageModel
    {
        private KoiPondConstructionManagementContext _context;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public DetailModel(KoiPondConstructionManagementContext context, IImageService imageService, IMapper mapper)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
        }

        public KoiDesign KoiDesign { get; set; }

        [BindProperty]
        public KoiDesignRequest KoiDesignRequest { get; set; }

        public async Task OnGetAsync(int id)
        {
            KoiDesign = await _context.KoiDesigns.FindAsync(id);
            KoiDesignRequest = _mapper.Map<KoiDesignRequest>(KoiDesign);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var koiDesign = new KoiDesign();
                if (KoiDesignRequest.DesignId != 0)
                {
                    koiDesign = await _context.KoiDesigns.FirstOrDefaultAsync(x => x.DesignId == KoiDesignRequest.DesignId);
                }

                if (KoiDesignRequest.Avatar != null && KoiDesignRequest.DesignId != 0)
                {
                    if (!string.IsNullOrEmpty(koiDesign.DesignImage))
                    {
                        await _imageService.DeleteImageAsync($"images/design/{koiDesign.DesignImage}");
                    }

                    KoiDesignRequest.DesignImage = await _imageService.UploadImageAsync(KoiDesignRequest.Avatar, $"images/design");
                }
                else
                {
                    KoiDesignRequest.DesignImage = koiDesign.DesignImage;
                }

                _mapper.Map(KoiDesignRequest, koiDesign);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }

            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            try
            {
                var koiDesign = await _context.KoiDesigns.FindAsync(id);
                if (koiDesign != null)
                {
                    await _imageService.DeleteImageAsync($"images/design/{koiDesign.DesignImage}");
                    _context.KoiDesigns.Remove(koiDesign);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }

            return RedirectToPage("/Manager/Design/List");
        }
    }
}
