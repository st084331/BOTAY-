using BOTAY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task = BOTAY.Task;

namespace BOTAY_browser
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        //Считаем текущие заданий
        private int currentTasks = 0;
        public MainPage()
        {
            //Инициализируем файловую систему и наборы заданий
            MemoryInteraction.InitializeMemorySystem();
            //Сортируем текущие задания по дедлайну
            MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
            InitializeComponent();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            DataGridUpdate();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //Запускаем таблицу
            MemoryInteraction.UpdateCurrent();
            UpdateLeftTasks();
            ToBotayList.ItemsSource = MemoryInteraction.CurrentTasks.ListOfTasks;
        }

        private void DataGridUpdate()
        {
            //Обновляем таблицу
            MemoryInteraction.UpdateCurrent();
            MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
            MemoryInteraction.UpdateCurrentCsv();
            UpdateLeftTasks();
            ToBotayList.Items.Refresh();
        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //Открываем ссылку
            Hyperlink link = (Hyperlink)e.OriginalSource;
            Process.Start(link.NavigateUri.AbsoluteUri);
        }

        private int ReadyCurrentTasks()
        {
            //Считаем число заданий
            int currentTasks = 0;
            foreach (var task in MemoryInteraction.CurrentTasks.ListOfTasks)
            {
                if (!task.IsReady)
                {
                    currentTasks++;
                }
            }
            return currentTasks;
        }

        private void UpdateLeftTasks()
        {
            //Обновляем текстовый блок, если требуется
            int updateCurrentTasks = ReadyCurrentTasks();
            if (currentTasks != updateCurrentTasks)
            {
                currentTasks = updateCurrentTasks;
                if (currentTasks > 0)
                {
                    TaskLeft.Content = "Осталось заданий: " + currentTasks.ToString();
                }
                else
                {
                    TaskLeft.Content = "Все готово!";
                }
            }
        }
    }
}
