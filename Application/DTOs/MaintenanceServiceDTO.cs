using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class MaintenanceServiceDTO
    {
        public int ServiceId { get; set; }

        public string? ServiceName { get; set; } = null!;

        public IFormFile? ServiceImage { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int Order { get; set; } = 0;
    }
}
