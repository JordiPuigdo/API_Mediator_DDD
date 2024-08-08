using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public string CompanyCode {  get; set; }
        public string Tags { get; set; }
        public int Stock {  get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal CostValue { get; set; }
        public decimal SellsValue { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public decimal PercentageVAT { get; set; }
    }
}
