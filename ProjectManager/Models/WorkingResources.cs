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
    
    public partial class WorkingResources
    {
        public int Id { get; set; }
        public Nullable<int> Salary { get; set; }
        public Nullable<int> SalaryType { get; set; }
        public Nullable<int> WorkingCalendar { get; set; }
    
        public virtual Resources Resources { get; set; }
        public virtual SalaryTypes SalaryTypes { get; set; }
        public virtual WorkingCalendars WorkingCalendars { get; set; }
    }
}
