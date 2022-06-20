using Emarket.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Domain.Entities
{
     public class Category : AuditableProperties
     {
         public int CategoryId { get; set; }
         public string CategoryName { get; set; }
         public string Description { get; set; }

         public ICollection<Advertisement> Advertisements { get; set; }
     }

}
