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

namespace BOTAY
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Task WindowTask;
        public EditWindow(Task task)
        {
            WindowTask = task;
            InitializeComponent();
            InitializeTextFields();
        }

        private void InitializeTextFields()
        {
            URLBox.Text = WindowTask.Url;
            NameBox.Text = WindowTask.Name;
            FullNameBox.Text = WindowTask.FullName;
            DeadlineBox.Text = WindowTask.Deadline;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateTaskFields();
            DialogResult = true;
        }

        private void UpdateTaskFields()
        {
            WindowTask.Url = URLBox.Text;
            WindowTask.Name = NameBox.Text;
            WindowTask.Deadline = DeadlineBox.Text;
            WindowTask.FullName = FullNameBox.Text;
        }
    }
}
