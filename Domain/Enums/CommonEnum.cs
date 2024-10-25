namespace Domain.Enums
{
    internal class CommonEnum
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DesignStatus
    {
        Show = 1, // Hiển thị
        Hide = 2, // Ẩn
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PaymentMethod
    {
        Cash = 1, // Tiền mặt
        BankTransfer = 2, // Chuyển khoản
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PaymentStatus
    {
        Unpaid = 1, // Chưa thanh toán
        Paid = 2, // Đã thanh toán
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ConstructionRequestsStatus
    {
        Pending = 1, // Chờ xử lý
        Approved = 2, // Đã duyệt
        InProgress = 3, // Đang thi công
        Completed = 4, // Đã hoàn thành
        Rejected = 5, // Từ chối
        Cancelled = 6, // Đã hủy
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ConstructionProcessStatus
    {
        All = -1, // Tất cả
        Pending = 0, // Chờ xử lý
        InProgress = 1, // Đang thi công
        Completed = 2, // Đã hoàn thành
    }
}
