namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectTasks : IEntity
    {
        public int Id { get; set; }

        public int? Project { get; set; }

        public int? Task { get; set; }

        public virtual Projects Projects { get; set; }

        public virtual Tasks Tasks { get; set; }
    }
}
