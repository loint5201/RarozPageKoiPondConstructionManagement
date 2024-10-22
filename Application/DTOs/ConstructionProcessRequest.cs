namespace Application.DTOs
{
    public class ConstructionProcessRequest
    {
        public int ProcessId { get; set; }

        public int? RequestId { get; set; }

        public int? Step { get; set; } = null!;

        public string? StepInfo { get; set; } = null!;

        public string? Note { get; set; } = null!;

        public int? Status { get; set; } = null!;

        public int? AssignedStaffId { get; set; }
    }

    public class ConstructionProcessResponse
    {
        public int ProcessId { get; set; }

        public int? RequestId { get; set; }

        public int? Step { get; set; } = null!;

        public string? StepInfo { get; set; } = null!;

        public string? Note { get; set; } = null!;

        public int? Status { get; set; } = null!;

        public int? AssignedStaffId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
