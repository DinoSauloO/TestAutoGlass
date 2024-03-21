using System;

namespace TestAutoGlass.Domain.Requests.Update
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCNPJ { get; set; }
    }
}
