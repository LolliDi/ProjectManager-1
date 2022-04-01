namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MaterialResoucres : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Count { get; set; }

        public int? Cost { get; set; }

        public int? CostType { get; set; }

        public virtual CostTypes CostTypes { get; set; }

        public virtual Resources Resources { get; set; }
    }
}
