using System;

namespace TestAutoGlass.Domain.Requests
{
    public class ProductsRequest
    {
        public bool? Status { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCNPJ { get; set; }
    }
}
