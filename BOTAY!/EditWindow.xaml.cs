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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Task WindowTask;
        public EditWindow(Task task)
        {
            InitializeComponent();
            WindowTask = task;
            URLBox.Text = task.Url;
            NameBox.Text = task.Name;
            FullNameBox.Text = task.FullName;
            DeadlineBox.Text = task.Deadline;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowTask.Url = URLBox.Text;
            WindowTask.Name = NameBox.Text;
            WindowTask.Deadline = DeadlineBox.Text;
            WindowTask.FullName = FullNameBox.Text;
            DialogResult = true;
        }
    }
}
