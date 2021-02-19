using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Forex.Entities.Models
{
    [Index(nameof(SyncId), nameof(Currency), IsUnique = true)]
    public partial class ExchangeRate : Audit
    {
        public int Id { get; set; }        
        public int SyncId { get; set; }
        public string Currency { get; set; }
        public decimal Rate { get; set; }

        [ForeignKey("SyncId")]
        public virtual ExchangeRateSyncBase ExchangeRateSyncBase { get; set; }
    }
}
