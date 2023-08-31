using Q1.Models;

namespace Q1.DTO
{
    public class OrderDTO
    {
        public int orderId { get; set; }
        public string? customerId { get; set; }
        public string? customerName { get; set; }
        public int? employeeId { get; set; }
        public string? employeeName { get; set; }
        public int? employeeDepartmentId { get; set; }
        public string? employeeDepartmentName { get; set; }
        public DateTime? orderDate { get; set; }
        public DateTime? requiredDate { get; set; }
        public DateTime? shippedDate { get; set; }
        public decimal? freight { get; set; }
        public string? shipName { get; set; }
        public string? shipAddress { get; set; }
        public string? shipCity { get; set; }
        public string? shipRegion { get; set; }
        public string? shipPostalCode { get; set; }
        public string? shipCountry { get; set; }
    } 
}
