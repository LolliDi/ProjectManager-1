using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectManager.CustomControls
{
    class GanttChart : Control
    {
        static GanttChart()
        {
            TasksCollectionProperty = DependencyProperty.Register(nameof(TasksCollection), typeof(ICollection<Tasks>), typeof(GanttChart),
                new FrameworkPropertyMetadata(new ObservableCollection<Tasks>(), OnTasksCollectionChanged));
            DayColumnWidthProperty = DependencyProperty.Register(nameof(DayColumnWidth), typeof(double), typeof(GanttChart), new PropertyMetadata(20d));
            DayTemplateProperty = DependencyProperty.Register(nameof(DayTemplateProperty), typeof(DataTemplate), typeof(GanttChart), new PropertyMetadata(null));
            WeekTemplateProperty = DependencyProperty.Register(nameof(WeekTemplateProperty), typeof(DataTemplate), typeof(GanttChart), new PropertyMetadata(null));
            TaskTemplateProperty = DependencyProperty.Register(nameof(TaskTemplateProperty), typeof(DataTemplate), typeof(GanttChart), new PropertyMetadata(null));
        }

        public ICollection<Tasks> TasksCollection
        {
            get { return (ICollection<Tasks>)GetValue(TasksCollectionProperty); }
            set { SetValue(TasksCollectionProperty, value); }
        }
        public double DayColumnWidth
        {
            get { return (double)GetValue(DayColumnWidthProperty); }
            set { SetValue(DayColumnWidthProperty, value); }
        }
        public DataTemplate DayTemplate
        {
            get { return (DataTemplate)GetValue(DayTemplateProperty); }
            set { SetValue(DayTemplateProperty, value); }
        }
        public DataTemplate WeekTemplate
        {
            get { return (DataTemplate)GetValue(WeekTemplateProperty); }
            set { SetValue(WeekTemplateProperty, value); }
        }
        public DataTemplate TaskTemplate
        {
            get { return (DataTemplate)GetValue(TaskTemplateProperty); }
            set { SetValue(TaskTemplateProperty, value); }
        }

        public static readonly DependencyProperty TasksCollectionProperty;
        public static readonly DependencyProperty DayColumnWidthProperty;
        public static readonly DependencyProperty DayTemplateProperty;
        public static readonly DependencyProperty WeekTemplateProperty;
        public static readonly DependencyProperty TaskTemplateProperty;

        private Grid PART_Grid;
        private ObservableCollection<GanttChartWeek> weekControls;
        private ObservableCollection<GanttChartDay> daysOfTheWeekControls;
        private ObservableCollection<GanttChartTask> taskControls;
        private List<Tasks> sortedTasksCollection;
        private int initRowsCount;


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Grid = (Grid)GetTemplateChild(nameof(PART_Grid));
            initRowsCount = PART_Grid.RowDefinitions.Count;

            weekControls = new ObservableCollection<GanttChartWeek>();
            daysOfTheWeekControls = new ObservableCollection<GanttChartDay>();
            taskControls = new ObservableCollection<GanttChartTask>();

            InitGanttChart();
        }
        private void InitGanttChart()
        {
            Binding ColumnDefinitionBinding = new Binding() { Path = new PropertyPath(nameof(DayColumnWidth)), Source = this };

            var pastDaysOffset = 14;
            var diff = (7 + DateTime.Now.DayOfWeek - DayOfWeek.Monday) % 7;
            var currentWeekDate = DateTime.Now.Date - TimeSpan.FromDays(pastDaysOffset);
            currentWeekDate = currentWeekDate.AddDays(-1 * diff);
            var dayDate = currentWeekDate;

            var minDate = TasksCollection.Min(item => item.StartDate);
            var maxDate = TasksCollection.Max(item => item.EndDate);

            var totalProjectDuration = (maxDate - minDate).GetValueOrDefault() == TimeSpan.MinValue ? (maxDate - minDate).GetValueOrDefault().Days : 50;

            for (int i = 0; i < totalProjectDuration; i++)
            {
                var columnDefinition = new ColumnDefinition();
                columnDefinition.SetBinding(WidthProperty, ColumnDefinitionBinding);
                PART_Grid.ColumnDefinitions.Add(columnDefinition);

                //DaysOfWeek
                var currentDayOfWeek = "?";
                switch (dayDate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        currentDayOfWeek = "ПН";
                        break;
                    case DayOfWeek.Tuesday:
                        currentDayOfWeek = "ВТ";
                        break;
                    case DayOfWeek.Wednesday:
                        currentDayOfWeek = "СР";
                        break;
                    case DayOfWeek.Thursday:
                        currentDayOfWeek = "ЧТ";
                        break;
                    case DayOfWeek.Friday:
                        currentDayOfWeek = "ПТ";
                        break;
                    case DayOfWeek.Saturday:
                        currentDayOfWeek = "СБ";
                        break;
                    case DayOfWeek.Sunday:
                        currentDayOfWeek = "ВС";
                        break;
                    default:
                        break;
                }

                var DayContent = new Border() { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.LightGray, };
                DayContent.Child = new StackPanel();
                ((StackPanel)DayContent.Child).Children.Add(new TextBlock()
                {
                    Text = currentDayOfWeek,
                    Background = Brushes.WhiteSmoke,
                    Padding = new Thickness(2, 0, 2, 0)
                });

                var DayControl = new ContentControl();
                DayControl.Content = DayContent;
                Grid.SetRow(DayControl, 1);
                Grid.SetColumn(DayControl, i);

                daysOfTheWeekControls.Add(new GanttChartDay()
                {
                    ContentControl = DayControl,
                    Date = dayDate
                });
                dayDate += TimeSpan.FromDays(1);

                //Weeks
                if (i % 7 == 0)
                {
                    var previousWeekDate = currentWeekDate;
                    currentWeekDate += TimeSpan.FromDays(7);

                    var WeekContent = new Border() { BorderThickness = new Thickness(1, 1, 0, 1), BorderBrush = Brushes.LightGray, };
                    WeekContent.Child = new StackPanel();
                    ((StackPanel)WeekContent.Child).Children.Add(new TextBlock()
                    {
                        Text = $"{previousWeekDate.ToShortDateString()} - {currentWeekDate.ToShortDateString()}",
                        Background = Brushes.GhostWhite,
                        TextAlignment = TextAlignment.Center,
                        FontSize = 9
                    });

                    var weekControl = new ContentControl();
                    weekControl.Content = WeekContent;
                    Grid.SetRow(weekControl, 0);
                    Grid.SetColumn(weekControl, i);
                    Grid.SetColumnSpan(weekControl, 7);

                    weekControls.Add(new GanttChartWeek()
                    {
                        StartWeekDate = previousWeekDate,
                        EndWeekDate = currentWeekDate,
                        ContentControl = weekControl
                    });
                }
            }

            foreach (var item in weekControls)
            {
                PART_Grid.Children.Add(item.ContentControl);
            }
            foreach (var item in daysOfTheWeekControls)
            {
                PART_Grid.Children.Add(item.ContentControl);
            }
        }

        private static void OnTasksCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sortedTasksCollection = ((GanttChart)d).TasksCollection.OrderBy(item => item.Id).ThenBy(item => item.TaskGroups.FirstOrDefault()?.Child).ThenBy(item => item.TaskGroups1.FirstOrDefault()?.Child).ToList();
            ((GanttChart)d).sortedTasksCollection = sortedTasksCollection;
            ((GanttChart)d).ClearTaskControls();
            ((GanttChart)d).InitTaskControls();
        }
        private void InitTaskControls()
        {
            if (TasksCollection != null)
            {
                for (int i = 0; i < sortedTasksCollection.Count; i++)
                {
                    //Tasks
                    var task = sortedTasksCollection.ElementAt(i);

                    //Создание контрола и стиля
                    var taskContent = new Border()
                    {
                        CornerRadius = new CornerRadius(2),
                        Background = Brushes.DeepSkyBlue,
                        Height = 20,
                        Margin = new Thickness(5)
                    };
                    var taskControl = new ContentControl() { Content = taskContent };

                    PART_Grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    Grid.SetRow(taskControl, i + initRowsCount);

                    var daysOfTheWeekControl = daysOfTheWeekControls.FirstOrDefault(item => item.Date.Date == task.StartDate.GetValueOrDefault().Date);
                    var taskColumn = Grid.GetColumn(daysOfTheWeekControl.ContentControl);
                    Grid.SetColumn(taskControl, taskColumn);

                    var taskDuration = task.EndDate - task.StartDate;
                    if (task.Duration.GetValueOrDefault() > 0)
                    {
                        Grid.SetColumnSpan(taskControl, task.Duration.GetValueOrDefault());
                    }

                    taskControls.Add(new GanttChartTask()
                    {
                        Task = task,
                        ContentControl = taskControl
                    });
                    PART_Grid.Children.Add(taskControl);

                    //TaskConnections
                    var taskConnections = task.TaskConnections1;
                    foreach (var taskConnection in taskConnections)
                    {
                        var arrow = new Polygon()
                        {
                            Points = new PointCollection()
                            {
                                new Point(0, 0),
                                new Point(10, 0),
                                new Point(5, 10),
                            }
                        };
                        var line = new Polyline()
                        {
                            Points = new PointCollection()
                            {
                                new Point(-25, -25),
                                new Point(0, -25),
                                new Point(0, 25),
                            }
                        };
                        //var taskConnectionControl = ;
                    }
                }
            }
        }
        private void ClearTaskControls()
        {
            if (TasksCollection != null)
            {
                for (int i = 0; i < PART_Grid.Children.Count; i++)
                {
                    var item = PART_Grid.Children[i];
                    if (taskControls.Select(control => control.ContentControl).Contains(item))
                    {
                        PART_Grid.Children.Remove(item);
                    }
                }

                PART_Grid.RowDefinitions.Clear();
                PART_Grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                PART_Grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
        }

        private void AddTaskControl(Tasks task)
        {
            var taskContent = new Border()
            {
                CornerRadius = new CornerRadius(2),
                Background = Brushes.DeepSkyBlue,
                Height = 20,
                Margin = new Thickness(5)
            };
            var taskControl = new ContentControl() { Content = taskContent };

            PART_Grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Grid.SetRow(taskControl, PART_Grid.RowDefinitions.Count - 1);

            var daysOfTheWeekControl = daysOfTheWeekControls.FirstOrDefault(item => item.Date.Date == task.StartDate.GetValueOrDefault().Date);
            var taskColumn = Grid.GetColumn(daysOfTheWeekControl.ContentControl);
            Grid.SetColumn(taskControl, taskColumn);

            var taskDuration = task.EndDate - task.StartDate;
            if (task.Duration.GetValueOrDefault() > 0)
            {
                Grid.SetColumnSpan(taskControl, task.Duration.GetValueOrDefault());
            }

            taskControls.Add(new GanttChartTask()
            {
                Task = task,
                ContentControl = taskControl
            });
            PART_Grid.Children.Add(taskControl);

        }

        private void UpdateTaskControl(Tasks task)
        {
            var taskControl = taskControls.FirstOrDefault(item => item.Task == task);
            var startColumn = GetColumn(daysOfTheWeekControls, task.StartDate.GetValueOrDefault().Date);
            var endColumn = GetColumn(daysOfTheWeekControls, task.EndDate.GetValueOrDefault().Date);
            Grid.SetColumn(taskControl.ContentControl, startColumn);
            Grid.SetColumnSpan(taskControl.ContentControl, endColumn - startColumn);
        }

        private void RemoveTaskControl(Tasks task)
        {
            var taskControl = taskControls.FirstOrDefault(item => item.Task == task);
            var taskControlRow = Grid.GetRow(taskControl.ContentControl);
            var rowsToUpdate = PART_Grid.RowDefinitions.Count - initRowsCount - taskControlRow;
            PART_Grid.RowDefinitions.RemoveAt(taskControlRow);
            PART_Grid.Children.Remove(taskControl.ContentControl);
            foreach (var item in taskControls)
            {
                Grid.SetRow(item.ContentControl, taskControlRow - 1);
                taskControlRow++;
            }
        }

        private int GetColumn(IEnumerable<GanttChartDay> daysOfTheWeekControls, DateTime dateTime)
        {
            var daysOfTheWeekControl = daysOfTheWeekControls.FirstOrDefault(item => item.Date.Date == dateTime);
            return Grid.GetColumn(daysOfTheWeekControl.ContentControl);
        }
    }
}
