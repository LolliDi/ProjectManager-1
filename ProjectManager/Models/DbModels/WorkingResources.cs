namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkingResources : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Salary { get; set; }

        public int? SalaryType { get; set; }

        public int? WorkingCalendar { get; set; }

        public virtual Resources Resources { get; set; }

        public virtual SalaryTypes SalaryTypes { get; set; }

        public virtual WorkingCalendars WorkingCalendars { get; set; }
    }
}
