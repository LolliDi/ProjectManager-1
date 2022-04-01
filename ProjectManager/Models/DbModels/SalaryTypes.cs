namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SalaryTypes : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryTypes()
        {
            WorkingResources = new HashSet<WorkingResources>();
        }

        public int Id { get; set; }

        public int? CostType { get; set; }

        public int? DateType { get; set; }

        public virtual CostTypes CostTypes { get; set; }

        public virtual DateTypes DateTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkingResources> WorkingResources { get; set; }
    }
}
