using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectManager.ViewModels
{
    class ProjectResourcesCreateViewModel : ViewModel
    {
        private NavigationService navigationService;
        private Projects currentProject;

        private int resourceType = -1, salaryType = -1, valuteType = -1;

        private Visibility workingResourceVisibility = Visibility.Collapsed;
        private Visibility materialResourceVisibility = Visibility.Collapsed;
        private Visibility addButtonVisibility = Visibility.Collapsed;

        private bool
            isMondayWorking = false,
            isTuesdayWorking = false,
            isWednesdayWorking = false,
            isThursdayWorking = false,
            isFridayWorking = false,
            isSaturdayWorking = false,
            isSundayWorking = false;

        private string
            name = "",

            salary = "",
            workingDayStart = "",
            workingDayEnd = "",

            count = "",
            cost = "";

        private List<SalaryTypes> salaryTypes;
        private List<CostTypes> valuteTypes;
        private Repository<SalaryTypes> salaryTypesRepository = new Repository<SalaryTypes>();
        private Repository<CostTypes> valuteTypesRepository = new Repository<CostTypes>();

        private SolidColorBrush standartColor = new SolidColorBrush(Color.FromRgb(250, 250, 250));
        private SolidColorBrush redColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        private SolidColorBrush
            nameColor,

            salaryColor,
            salaryTypeColor,
            workingDayStartColor,
            workingDayEndColor,
            workingDayColor,

            materialCountColor,
            materialCostColor,
            materialValuteColor;

        public ICommand AddResource { get; set; }
        public ICommand Back { get; set; }

        public ProjectResourcesCreateViewModel(NavigationService navigationService, Projects currentProject)
        {
            this.navigationService = navigationService;
            this.currentProject = currentProject;
            salaryTypes = salaryTypesRepository.Items.ToList();
            valuteTypes = valuteTypesRepository.Items.ToList();
            AddResource = new LambdaCommand(GoAddResource);
            Back = new LambdaCommand(GoBack);
            nameColor = standartColor;

            salaryColor = standartColor;
            salaryTypeColor = standartColor;
            workingDayStartColor = standartColor;
            workingDayEndColor = standartColor;
            workingDayColor = standartColor;

            materialCountColor = standartColor;
            materialCostColor = standartColor;
            materialValuteColor = standartColor;
        }

        #region VisibilityProperties
        public int ResourceType
        {

            get => resourceType;
            set
            {
                Set(ref resourceType, ref value);
                if (resourceType == 0)
                {
                    WorkingResourceVisibility = Visibility.Visible;
                    MaterialResourceVisibility = Visibility.Collapsed;
                    AddButtonVisibility = Visibility.Visible;
                }
                else if (resourceType == 1)
                {
                    WorkingResourceVisibility = Visibility.Collapsed;
                    MaterialResourceVisibility = Visibility.Visible;
                    AddButtonVisibility = Visibility.Visible;
                }
                else
                {
                    WorkingResourceVisibility = Visibility.Collapsed;
                    MaterialResourceVisibility = Visibility.Collapsed;
                    AddButtonVisibility = Visibility.Collapsed;
                }
            }
        }
        public Visibility WorkingResourceVisibility
        {
            get => workingResourceVisibility;
            set => Set(ref workingResourceVisibility, ref value);
        }
        public Visibility MaterialResourceVisibility
        {
            get => materialResourceVisibility;
            set => Set(ref materialResourceVisibility, ref value);
        }
        public Visibility AddButtonVisibility
        {
            get => addButtonVisibility;
            set => Set(ref addButtonVisibility, ref value);
        }
        #endregion

        #region WorkingDaysProperties
        public bool IsMondayWorking
        {
            get => isMondayWorking;
            set => Set(ref isMondayWorking, ref value);
        }

        public bool IsTuesdayWorking
        {
            get => isTuesdayWorking;
            set => Set(ref isTuesdayWorking, ref value);
        }

        public bool IsWednesdayWorking
        {
            get => isWednesdayWorking;
            set => Set(ref isWednesdayWorking, ref value);
        }

        public bool IsThursdayWorking
        {
            get => isThursdayWorking;
            set => Set(ref isThursdayWorking, ref value);
        }

        public bool IsFridayWorking
        {
            get => isFridayWorking;
            set => Set(ref isFridayWorking, ref value);
        }

        public bool IsSaturdayWorking
        {
            get => isSaturdayWorking;
            set => Set(ref isSaturdayWorking, ref value);
        }

        public bool IsSundayWorking
        {
            get => isSundayWorking;
            set => Set(ref isSundayWorking, ref value);
        }
        #endregion

        #region ComboboxProperties
        public int SalaryType
        {
            get => salaryType;
            set => Set(ref salaryType, ref value);
        }

        public int ValuteType
        {
            get => valuteType;
            set => Set(ref valuteType, ref value);
        }
        public List<SalaryTypes> SalaryTypes
        {
            get => salaryTypes;
        }

        public List<CostTypes> ValuteTypes
        {
            get => valuteTypes;
        }
        #endregion

        #region StringProperties
        public string Name
        {
            get => name;
            set => Set(ref name, ref value);
        }

        public string Salary
        {
            get => salary;
            set 
            {
                if (int.TryParse(value, out int num))
                    salary = value;
                else
                {
                    salary = "";
                }
            }
        }
        public string WorkingDayStart
        {
            get => workingDayStart;
            set
            {
                if (Regex.IsMatch(value, "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
                    workingDayStart = value;
                else
                    workingDayStart = "";
            }
        }
        public string WorkingDayEnd
        {
            get => workingDayEnd;
            set
            {
                if (Regex.IsMatch(value, "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
                    workingDayEnd = value;
                else
                    workingDayEnd = "";
            }
        }

        public string Count
        {
            get => count;
            set 
            {
                if (int.TryParse(value, out int num))
                    count = value;
                else
                {
                    count = "";
                }
            }
        }
        public string Cost
        {
            get => cost;
            set
            {
                if (int.TryParse(value, out int num))
                    cost = value;
                else
                {
                    cost = "";
                }
            }
        }
        #endregion

        #region BorderBrushes
        public SolidColorBrush NameColor
        {
            get => nameColor;
            set => Set(ref nameColor, ref value);
        }

        public SolidColorBrush SalaryColor
        {
            get => salaryColor;
            set => Set(ref salaryColor, ref value);
        }
        public SolidColorBrush SalaryTypeColor
        {
            get => salaryTypeColor;
            set => Set(ref salaryTypeColor, ref value);
        }
        public SolidColorBrush WorkingDayStartColor
        {
            get => workingDayStartColor;
            set => Set(ref workingDayStartColor, ref value);
        }
        public SolidColorBrush WorkingDayEndColor
        {
            get => workingDayEndColor;
            set => Set(ref workingDayEndColor, ref value);
        }
        public SolidColorBrush WorkingDayColor
        {
            get => workingDayColor;
            set => Set(ref workingDayColor, ref value);
        }

        public SolidColorBrush MaterialCountColor
        {
            get => materialCountColor;
            set => Set(ref materialCountColor, ref value);
        }
        public SolidColorBrush MaterialCostColor
        {
            get => materialCostColor;
            set => Set(ref materialCostColor, ref value);
        }
        public SolidColorBrush MaterialValuteColor
        {
            get => materialValuteColor;
            set => Set(ref materialValuteColor, ref value);
        }
        #endregion

        private void GoAddResource(object obj)
        {
            if (resourceType == 0)
                AddWorkingResource();
            else
                AddMaterialResource();
        }

        private void GoBack(object obj)
        {
            navigationService.CurrentViewModel = new ProjectResourcesViewModel(navigationService, currentProject);
        }

        private void AddWorkingResource()
        {
            if (name.Length == 0 || salary.Length == 0 || salaryType == -1 || workingDayStart.Length == 0 || workingDayEnd.Length == 0 || !HasWorkingDays())
            {
                NameColor = FieldColorCheck(Name);
                SalaryColor = FieldColorCheck(Salary);
                SalaryTypeColor = FieldColorCheck(salaryType);
                WorkingDayStartColor = FieldColorCheck(WorkingDayStart);
                WorkingDayEndColor = FieldColorCheck(WorkingDayEnd);
                WorkingDayColor = FieldColorCheck();
                return;
            }
            //Добавление календаря
            Repository<WorkingCalendars> workingCalendars = new Repository<WorkingCalendars>();
            WorkingCalendars newWorkingCalendar = new WorkingCalendars();
            TimeSpan startTime = TimeSpan.Parse(workingDayStart);
            TimeSpan endTime = TimeSpan.Parse(workingDayEnd);
            newWorkingCalendar.StartTime = startTime;
            newWorkingCalendar.EndTime = endTime;
            if(startTime < endTime)
                newWorkingCalendar.Duration = TimeSpan.FromMinutes(endTime.TotalMinutes - startTime.TotalMinutes);
            else
                newWorkingCalendar.Duration = TimeSpan.FromMinutes(1440 + endTime.TotalMinutes - startTime.TotalMinutes);
            var newCalendarId = workingCalendars.Add(newWorkingCalendar).Id;

            Repository<WorkingCalendarWeekends> workingCalendarsWeekends = new Repository<WorkingCalendarWeekends>();
            workingCalendarsWeekends.Add(new WorkingCalendarWeekends() { WeekDay = 1, WorkingCalendar = newCalendarId, IsWeekend = Convert.ToInt32(isMondayWorking) });
            workingCalendarsWeekends.Add(new WorkingCalendarWeekends() { WeekDay = 2, WorkingCalendar = newCalendarId, IsWeekend = Convert.ToInt32(isTuesdayWorking) });
            workingCalendarsWeekends.Add(new WorkingCalendarWeekends() { WeekDay = 3, WorkingCalendar = newCalendarId, IsWeekend = Convert.ToInt32(isWednesdayWorking) });
            workingCalendarsWeekends.Add(new WorkingCalendarWeekends() { WeekDay = 4, WorkingCalendar = newCalendarId, IsWeekend = Convert.ToInt32(isThursdayWorking) });
            workingCalendarsWeekends.Add(new WorkingCalendarWeekends() { WeekDay = 5, WorkingCalendar = newCalendarId, IsWeekend = Convert.ToInt32(isFridayWorking) });
            workingCalendarsWeekends.Add(new WorkingCalendarWeekends() { WeekDay = 6, WorkingCalendar = newCalendarId, IsWeekend = Convert.ToInt32(isSaturdayWorking) });
            workingCalendarsWeekends.Add(new WorkingCalendarWeekends() { WeekDay = 7, WorkingCalendar = newCalendarId, IsWeekend = Convert.ToInt32(isSundayWorking) });

            //Добавление ресурса и связи с проектом
            Repository<Resources> resourcesRepository = new Repository<Resources>();
            Repository<WorkingResources> workingResourcesRepository = new Repository<WorkingResources>();
            Repository<ProjectResources> projectResourcesRepository = new Repository<ProjectResources>();

            var newResourceId = resourcesRepository.Add(new Resources() { Name = name }).Id;
            projectResourcesRepository.Add(new ProjectResources() { Project = currentProject.Id, Resource = newResourceId });

            WorkingResources newWorkingResource = new WorkingResources();
            newWorkingResource.Id = newResourceId;
            newWorkingResource.Salary = Convert.ToInt32(salary);
            newWorkingResource.SalaryType = salaryTypes[salaryType].Id;
            newWorkingResource.WorkingCalendar = newCalendarId;
            workingResourcesRepository.Add(newWorkingResource);

            navigationService.CurrentViewModel = new ProjectResourcesViewModel(navigationService, currentProject);
        }

        private void AddMaterialResource()
        {
            if (name.Length == 0 || count.Length == 0 || cost.Length == 0 || valuteType != -1)
            {
                NameColor = FieldColorCheck(Name);
                MaterialCountColor = FieldColorCheck(Count);
                MaterialCostColor = FieldColorCheck(Cost);
                MaterialValuteColor = FieldColorCheck(valuteType);
                return;
            }

            //Добавление ресурса и связи с проектом
            Repository<Resources> resourcesRepository = new Repository<Resources>();
            Repository<MaterialResoucres> materialResoucresRepository = new Repository<MaterialResoucres>();
            Repository<ProjectResources> projectResourcesRepository = new Repository<ProjectResources>();

            var newResourceId = resourcesRepository.Add(new Resources() { Name = name }).Id;
            projectResourcesRepository.Add(new ProjectResources() { Project = currentProject.Id, Resource = newResourceId });
            materialResoucresRepository.Add(new MaterialResoucres() { Id = newResourceId, Count = Convert.ToInt32(count), Cost = Convert.ToInt32(cost), CostType = valuteTypes[valuteType].Id });

            navigationService.CurrentViewModel = new ProjectResourcesViewModel(navigationService, currentProject);
        }

        private bool HasWorkingDays()
        {
            if (!(isMondayWorking || isTuesdayWorking || isWednesdayWorking || isThursdayWorking || isFridayWorking || isSaturdayWorking || isSundayWorking))
                return false;
            else
                return true;
        }

        #region ColorCheck
        private SolidColorBrush FieldColorCheck()
        {
            if (!HasWorkingDays())
                return redColor;
            else
                return standartColor;
        }

        private SolidColorBrush FieldColorCheck(string property)
        {
            if (property.Length == 0)
                return redColor;
            else
                return standartColor;
        }

        private SolidColorBrush FieldColorCheck(int selectedIndex)
        {
            if (selectedIndex == -1)
                return redColor;
            else
                return standartColor;
        }
        #endregion
    }
}
