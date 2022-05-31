using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

namespace BOTAY
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Считаем текущие заданий
        private int currentTasks = 0;
        public MainWindow()
        {
            //Инициализируем файловую систему и наборы заданий
            MemoryInteraction.InitializeMemorySystem();
            //Сортируем текущие задания по дедлайну
            MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
            InitializeComponent();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Обновляем число оставшихся заданий по клику
            UpdateLeftTasks();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            //Обновляем файлы памяти перед закрытием
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            MemoryInteraction.UpdateCsv();
        }


        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateLeftTasks();
            if (ToBotayList.SelectedItem != null)
            {
                //Редактируем слот при двойном клике
                Task path = ToBotayList.SelectedItem as Task;
                EditWindow editWindow = new EditWindow(path);
                ToBotayList.UnselectAll();
                if ((bool)editWindow.ShowDialog())
                {
                    MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
                    DataGridUpdate();
                }
            }
            UpdateLeftTasks();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            UpdateLeftTasks();
            if (ToBotayList.SelectedItem != null)
            {
                //Подтвержаем удаление
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранное задание?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Удаляем
                    Task path = ToBotayList.SelectedItem as Task;
                    MemoryInteraction.CurrentTasks.deleteTaskFromList(path);
                    ToBotayList.UnselectAll();
                    DataGridUpdate();
                    UpdateLeftTasks();
                }
            }
         
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            //Добавляем новое задание
            UpdateLeftTasks();
            AddWindow addWindow = new AddWindow();
            if ((bool)addWindow.ShowDialog()) {
                MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
                DataGridUpdate();
                UpdateLeftTasks();
            }
        }


        private void Button_Click_History(object sender, RoutedEventArgs e)
        {
            //Обновляем историю и текущие
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            DataGridUpdate();
            UpdateLeftTasks();
            //Открываем окно истории
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
            //Обновляем историю и текущие
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            DataGridUpdate();
            UpdateLeftTasks();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //Запускаем таблицу
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            UpdateLeftTasks();
            ToBotayList.ItemsSource = MemoryInteraction.CurrentTasks.ListOfTasks;
        }

        private void DataGridUpdate()
        {
            //Обновляем таблицу
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
