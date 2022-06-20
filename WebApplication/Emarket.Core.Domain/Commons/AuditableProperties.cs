using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Domain.Commons
{
    public class AuditableProperties
    {

        //public virtual int Identier { get; set; } //or Id but im not needed
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }

    }
}
