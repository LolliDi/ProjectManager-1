using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly DbContext dbContext;
        public static DbContext DbContext => dbContext;

        static App()
        {
            dbContext = new ProjectManagerContext();
        }
    }
}
