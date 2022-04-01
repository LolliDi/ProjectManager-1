namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskGroups : IEntity
    {
        public int Id { get; set; }

        public int Parent { get; set; }

        public int Child { get; set; }

        public int Depth { get; set; }

        public virtual Tasks Tasks { get; set; }

        public virtual Tasks Tasks1 { get; set; }
    }
}
