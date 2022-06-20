using Emarket.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Domain.Entities
{
    public class Advertisement : AuditableProperties
    {
        public int AdvertisementId { get; set; }
        public string ProductName { get; set; }
        public string FirstImage { get; set; }
        public string? SecondImage { get; set; }
        public string? ThirdImage { get; set; }
        public string? FourthImage { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }

}
