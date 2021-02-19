using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.Entities.Models
{
    public abstract class Audit
    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
