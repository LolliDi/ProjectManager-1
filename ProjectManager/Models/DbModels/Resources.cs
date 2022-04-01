namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Resources : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resources()
        {
            ProjectResources = new HashSet<ProjectResources>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public virtual MaterialResoucres MaterialResoucres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectResources> ProjectResources { get; set; }

        public virtual WorkingResources WorkingResources { get; set; }
    }
}
