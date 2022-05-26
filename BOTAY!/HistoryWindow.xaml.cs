using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace BOTAY
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MemoryInteraction.Update();
            ToBotayHistoryList.ItemsSource = MemoryInteraction.HistoryTasks.ListOfTasks;
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (ToBotayHistoryList.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранное задание?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Task path = ToBotayHistoryList.SelectedItem as Task;
                    MemoryInteraction.HistoryTasks.deleteTaskFromList(path);
                    ToBotayHistoryList.UnselectAll();
                    DataGridUpdate();
                }
            }
        }

        private void DataGridUpdate()
        {
            ToBotayHistoryList.Items.Refresh();
        }
    }
}
