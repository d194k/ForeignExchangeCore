using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.Entities.Models
{
    [Index(nameof(SyncDate), nameof(BaseCurrency), IsUnique = true)]
    public partial class ExchangeRateSyncBase : Audit
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public DateTime SyncDate { get; set; }
        public int Timestamp { get; set; }
        public virtual ICollection<ExchangeRate> ExchangeRate { get; set; }
    }
}
