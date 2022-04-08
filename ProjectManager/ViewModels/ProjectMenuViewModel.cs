using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;

namespace ProjectManager.ViewModels
{
    class ProjectMenuViewModel : ViewModel
    {
        public ProjectMenuViewModel(NavigationService navigationService, Projects project, Users user)
        {
            CurrentProject = project;
            CurrentUser = user;

            MainNavigationService = navigationService;

            ProjectMenuNavigationService = new NavigationService();
            ProjectMenuNavigationService.CurrentViewModel = new ProjectTasksViewModel(ProjectMenuNavigationService, project);

            ToProjectTasksPageCommand = new LambdaCommand(OnToProjectTasksPageCommandExecute, CanToProjectTasksPageCommandExecuted);
            ToProjectResourcePageCommand = new LambdaCommand(OnToProjectResourcePageCommandExecute, CanToProjectResourcePageCommandExecuted);
            ToProjectReportPageCommand = new LambdaCommand(OnToProjectReportPageCommandExecute, CanToProjectReportPageCommandExecuted);
            ToProjectPassportPageCommand = new LambdaCommand(CreatePassport);
            ToAdminPageCommand = new LambdaCommand(OnToAdminPageCommandExecute);
            ToAuthorizationPageCommand = new LambdaCommand(OnToAuthorizationPageCommandExecute);
        }

        #region Fields


        #endregion

        #region Properties

        public Projects CurrentProject { get; }
        public Users CurrentUser { get; }

        public NavigationService ProjectMenuNavigationService { get; set; }
        public NavigationService MainNavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand ToProjectTasksPageCommand { get; set; }
        public ICommand ToProjectResourcePageCommand { get; set; }
        public ICommand ToProjectReportPageCommand { get; set; }
        public ICommand ToProjectPassportPageCommand { get; set; }
        public ICommand ToAdminPageCommand { get; set; }
        public ICommand ToAuthorizationPageCommand { get; set; }

        #endregion

        #region Private Methods

        private void OnToProjectTasksPageCommandExecute(object parameter)
        {
            ProjectMenuNavigationService.CurrentViewModel = new ProjectTasksViewModel(ProjectMenuNavigationService, null);
        }
        private void OnToProjectResourcePageCommandExecute(object parameter)
        {
            ProjectMenuNavigationService.CurrentViewModel = new ProjectResourcesViewModel(ProjectMenuNavigationService, CurrentProject);
        }
        private void OnToProjectReportPageCommandExecute(object parameter)
        {
            ProjectMenuNavigationService.CurrentViewModel = new ProjectReportViewModel(ProjectMenuNavigationService, null);
        }
        private void OnToAdminPageCommandExecute(object parameter)
        {
            MessageBox.Show("Это еще не добавили!", "Упс...", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void OnToAuthorizationPageCommandExecute(object parameter)
        {
            var dialogResult = MessageBox.Show("Вы точно хотите выйти из аккаунта?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Yes)
            {
                MainNavigationService.CurrentViewModel = new AuthorizationViewModel(MainNavigationService);
            }
        }
        private bool CanToProjectTasksPageCommandExecuted(object parameter)
        {
            return ProjectMenuNavigationService.CurrentViewModel.GetType() != typeof(ProjectTasksViewModel);
        }
        private bool CanToProjectResourcePageCommandExecuted(object parameter)
        {
            return ProjectMenuNavigationService.CurrentViewModel.GetType() != typeof(ProjectResourcesViewModel);
        }
        private bool CanToProjectReportPageCommandExecuted(object parameter)
        {
            return ProjectMenuNavigationService.CurrentViewModel.GetType() != typeof(ProjectReportViewModel);
        }
        #endregion

        #region PassportMethod

        private void CreatePassport(object parameter) //создание паспорта
        {
            System.Windows.Forms.FolderBrowserDialog FBD = new System.Windows.Forms.FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = FBD.SelectedPath;

                Word.Application app = new Word.Application();
                Word.Document doc = app.Documents.Add();

                Word.Paragraph pp = doc.Paragraphs.Add();
                CreateRange(ref pp, "\n\n\n\n\n\nПАСПОРТ ПРОЕКТА",1, 30,false);
                CreateRange(ref pp, "\"" + CurrentProject.Name + "\"", 1, 20, false);
                doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                Word.Paragraph pInroduction = doc.Paragraphs.Add(); //введение
                CreateRange(ref pInroduction, "1. Введение", 2, 16, false);
                CreateRange(ref pInroduction, "Назначение данного документа – документально зафиксировать Паспорт Проекта, который:", 0, 14, false);
                CreateRange(ref pInroduction, "Формально авторизует Проект \""+CurrentProject.Name+"\";\n" +
                    "Определяет цели Проекта;\n" +
                    "Определяет границы(область действия) Проекта;\n" +
                    "Определяет начальные исходные данные, в том числе допущения и ограничения, с учетом которых планируется Проект;\n" +
                    "Определяет организационную структуру проекта.", 0, 14, true);
                CreateRange(ref pInroduction, "Паспорт Проекта обеспечивает основу для общего понимания Проекта, " +
                    "включая определение того, что входит в рамки Проекта, а что оставлено за его рамками, " +
                    "позволяет всем участникам Проекта сформировать общее понимание ожидаемых результатов Проекта", 0, 14,false);
                
                doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                Word.Paragraph pDescription = doc.Paragraphs.Add();
                CreateRange(ref pDescription, "2. Описание проекта", 2, 16, false);
                CreateRange(ref pDescription, "Название проекта: ", 2, 14, false);
                
                CreateRange(ref pDescription, "\"" + CurrentProject.Name + "\"", 0, 14, false);
                pDescription = doc.Paragraphs.Add();



                app.Visible = true;
                doc.SaveAs2(path+"\\Паспорт_"+CurrentProject.Name+".docx");
            }

        }

        private void CreateRange(ref Word.Paragraph pp, string text, short title, short size, bool marker)
        {
            Word.Range pr = pp.Range;
            pr.Text = marker? text + "\n": text; //если маркированный список, то нужен перенос через \n, чтобы не было маркера на пустой строке
            switch(title)
            {
                case 1: //заголовок
                    SetStyle(ref pr, size, Word.WdParagraphAlignment.wdAlignParagraphCenter, 1);
                    break;
                case 2: //обычный жирный текст
                    SetStyle(ref pr, size, Word.WdParagraphAlignment.wdAlignParagraphLeft, 1);
                    break;
                default: //обычный текст
                    SetStyle(ref pr, size, Word.WdParagraphAlignment.wdAlignParagraphLeft, 0);
                    break;
            }
            if (marker)
            {
                pr.ListFormat.ApplyBulletDefault();
            }
            else
            {
                pr.InsertParagraphAfter();
            }
            
        }

        private void SetStyle(ref Word.Range pr, short size, Word.WdParagraphAlignment al, short bold)
        {
            pr.Font.Size = size;
            pr.Font.Name = "Times New Roman";
            pr.ParagraphFormat.Alignment = al;
            pr.Font.Bold = bold;
        }


        #endregion
    }
}
