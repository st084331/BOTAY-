using System;
using System.Collections.Generic;
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

namespace BOTAY_
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            task.Url = URLBox.Text;
            task.Name = NameBox.Text;
            task.Deadline = DeadlineBox.Text;
            task.FullName = FullNameBox.Text;
            MemoryInteraction.CurrentTasks.addToList(task);
            DialogResult = true;
        }
    }
}
