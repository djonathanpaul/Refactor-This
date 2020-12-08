namespace refactor_this.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        public Guid Id { get; set; }

        public double? Amount { get; set; }

        public DateTime? Date { get; set; }

        public Guid? AccountId { get; set; }

    }
}
