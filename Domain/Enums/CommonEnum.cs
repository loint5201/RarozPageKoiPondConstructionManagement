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
        Pending = 1,
        Approved = 2,
        InProgress = 3,
        Completed = 4,
        Rejected = 5,
        Showroom = 6
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
}
