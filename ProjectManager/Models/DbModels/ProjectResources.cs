namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectResources : IEntity
    {
        public int Id { get; set; }

        public int? Project { get; set; }

        public int? Resource { get; set; }

        public virtual Projects Projects { get; set; }

        public virtual Resources Resources { get; set; }
    }
}
