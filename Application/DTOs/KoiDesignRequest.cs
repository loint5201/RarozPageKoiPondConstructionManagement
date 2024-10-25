using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs
{
    public class KoiDesignRequest
    {
        public int DesignId { get; set; }

        public string? DesignName { get; set; } = null!;

        public string? DesignImage { get; set; } = null!;

        public string? Description { get; set; }

        public string? CostEstimate { get; set; }

        public int? Status { get; set; }

        [NotMapped]
        public IFormFile? Avatar { get; set; }
    }
}
