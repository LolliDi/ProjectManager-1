//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkingCalendarWeekends
    {
        public int Id { get; set; }
        public Nullable<int> WorkingCalendar { get; set; }
        public Nullable<int> WeekDay { get; set; }
        public Nullable<int> IsWeekend { get; set; }
    
        public virtual WeekDays WeekDays { get; set; }
        public virtual WorkingCalendars WorkingCalendars { get; set; }
    }
}
