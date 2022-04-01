namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkingCalendarWeekends : IEntity
    {
        public int Id { get; set; }

        public int? WorkingCalendar { get; set; }

        public int? WeekDay { get; set; }

        public int? IsWeekend { get; set; }

        public virtual WeekDays WeekDays { get; set; }

        public virtual WorkingCalendars WorkingCalendars { get; set; }
    }
}
