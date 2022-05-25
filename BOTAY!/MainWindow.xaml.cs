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

namespace BOTAY_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            MemoryInteraction.InitializeMemorySystem();
            MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
            InitializeComponent();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MemoryInteraction.Update();
            MemoryInteraction.UpdateHistoryCsv();
            MemoryInteraction.UpdateCurrentCsv();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
            if (ToBotayList.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранное задание?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Task path = ToBotayList.SelectedItem as Task;
                    MemoryInteraction.CurrentTasks.deleteTaskFromList(path);
                    ToBotayList.UnselectAll();
                    DataGridUpdate();
                }
            }
         
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            if ((bool)addWindow.ShowDialog()) {
                MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
                DataGridUpdate();
            }
        }


        private void Button_Click_History(object sender, RoutedEventArgs e)
        {
            MemoryInteraction.HistoryTasks.leaveTwentyLast();

            MemoryInteraction.Update();
            DataGridUpdate();

            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();

            MemoryInteraction.Update();
            DataGridUpdate();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MemoryInteraction.HistoryTasks.leaveTwentyLast();
            MemoryInteraction.Update();
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
    }
}
