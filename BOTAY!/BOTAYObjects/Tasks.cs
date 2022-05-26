using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BOTAY
{
    public class Tasks
    {
        private List<Task> _ListOfTasks = new List<Task>();

        public Tasks()
        {
            _ListOfTasks = new List<Task>();
        }

        public Tasks(string filename)
        {
            var tasksParams = File.ReadAllLines(filename);
            foreach (var taskParamsOneLine in tasksParams)
            {
                try
                {
                    var taskParams = taskParamsOneLine.Split(',');
                    Task task = new Task();
                    task.Name = taskParams[0];
                    task.FullName = taskParams[1];
                    task.Url = taskParams[2];
                    task.Deadline = taskParams[3].Replace('/', '.').Replace(',', '.');
                    task.IsReady = Convert.ToBoolean(taskParams[4]);
                    this.addToList(task);
                }
                catch { }
            }
        }

        public List<Task> ListOfTasks { get { return _ListOfTasks; } }

        public void SortTasksListByDeadline()
        {
            var culture = CultureInfo.GetCultureInfo("ru");
            Task temp;
            for (int i = 0; i < _ListOfTasks.Count; i++)
            {
                for (int j = i + 1; j < _ListOfTasks.Count; j++)
                {
                    if (_ListOfTasks[i].Deadline == "NoDeadline")
                    {
                        temp = _ListOfTasks[i];
                        _ListOfTasks[i] = _ListOfTasks[j];
                        _ListOfTasks[j] = temp;
                    }
                    else if (_ListOfTasks[j].Deadline == "NoDeadline")
                    {

                    }
                    else if (DateTime.Compare(DateTime.Parse(_ListOfTasks[i].Deadline, culture), DateTime.Parse(_ListOfTasks[j].Deadline, culture)) > 0)
                    {
                        temp = _ListOfTasks[i];
                        _ListOfTasks[i] = _ListOfTasks[j];
                        _ListOfTasks[j] = temp;
                    }
                }
            }
        }

        public void addToList(Task task)
        {
            if (task != null && task != default) _ListOfTasks.Add(task);
        }

        public void deleteTaskFromList(Task task)
        {
            _ListOfTasks.Remove(task);
        }
        public void deleteTaskFromList(int taskIndex)
        {
            _ListOfTasks.RemoveAt(taskIndex);
        }

        public void ListToCsv(string filename)
        {
            string res = "";
            for (int i = 0; i < _ListOfTasks.Count - 1; i++)
            {
                Task task = _ListOfTasks[i];
                res += task.Name + "," + task.FullName + "," + task.Url + "," + task.Deadline + "," + task.IsReady.ToString() + "\n";
            }
            if (_ListOfTasks.Count > 0)
            {
                Task task = _ListOfTasks[_ListOfTasks.Count - 1];
                res += task.Name + "," + task.FullName + "," + task.Url + "," + task.Deadline + "," + task.IsReady.ToString();
            }
            File.WriteAllText(filename, res);
        }

        public void leaveTwentyLast()
        {
            if (ListOfTasks.Count > 20)
            {
                for(int i = 0; i < ListOfTasks.Count - 20; i++)
                {
                    _ListOfTasks.RemoveAt(0);                
                }
            }
        }

        public void printList()
        {
            if (_ListOfTasks.Count > 0)
            {
                foreach (Task task in _ListOfTasks)
                {
                    Console.WriteLine(task.Name + " , " + task.FullName + " , " + task.Url + " , " + task.Deadline + " , " + task.IsReady.ToString());
                }
            }
        }
    }
}
