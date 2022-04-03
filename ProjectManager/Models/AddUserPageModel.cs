using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectManager.Models
{
    public static class AddUserPageModel
    {

        public static string name = ""; //данные
        public static string lastName = "";
        public static string patronymic = "";
        public static string email = "";
        public static string login = "";
        public static string password = "";
        public static int age = 0;
        public static string selectedRole = "";


        public static SolidColorBrush standartColor = new SolidColorBrush(Color.FromRgb(250, 250, 250)); //цвета границ
        public static SolidColorBrush redColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        public static SolidColorBrush loginBorderColor = standartColor;
        public static SolidColorBrush passwordBorderColor = standartColor;
        public static SolidColorBrush roleBorderColor = standartColor;
    }
}
