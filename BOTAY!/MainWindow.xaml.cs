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
        private int currentTasks = 0;
        public MainWindow()
        {
            MemoryInteraction.InitializeMemorySystem();
            MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
            InitializeComponent();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateLeftTaks();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            MemoryInteraction.UpdateCsv();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateLeftTaks();
            if (ToBotayList.SelectedItem != null)
            {
                Task path = ToBotayList.SelectedItem as Task;
                EditWindow editWindow = new EditWindow(path);
                ToBotayList.UnselectAll();
                if ((bool)editWindow.ShowDialog())
                {
                    MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
                    DataGridUpdate();
                }
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            UpdateLeftTaks();
            if (ToBotayList.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранное задание?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Task path = ToBotayList.SelectedItem as Task;
                    MemoryInteraction.CurrentTasks.deleteTaskFromList(path);
                    ToBotayList.UnselectAll();
                    DataGridUpdate();
                    UpdateLeftTaks();
                }
            }
         
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            UpdateLeftTaks();
            AddWindow addWindow = new AddWindow();
            if ((bool)addWindow.ShowDialog()) {
                MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
                DataGridUpdate();
                UpdateLeftTaks();
            }
        }


        private void Button_Click_History(object sender, RoutedEventArgs e)
        {
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            DataGridUpdate();
            UpdateLeftTaks();
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            DataGridUpdate();
            UpdateLeftTaks();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
            UpdateLeftTaks();
            ToBotayList.ItemsSource = MemoryInteraction.CurrentTasks.ListOfTasks;
        }

        private void DataGridUpdate()
        {
            ToBotayList.Items.Refresh();
        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)e.OriginalSource;
            Process.Start(link.NavigateUri.AbsoluteUri);
        }

        private int ReadyCurrentTasks()
        {
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

        private void UpdateLeftTaks()
        {
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
