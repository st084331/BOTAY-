    using System;
using System.Collections.Generic;
using System.IO;

namespace BOTAY_browser
{
    public static class MemoryInteraction
    {
        private static string _documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string _currentFilename = Path.Combine(_documents, "BotayData.csv");
        private static Tasks _CurrentTasks;

        public static Tasks CurrentTasks { get { return _CurrentTasks; } set { _CurrentTasks = value; } }
        public static string currentFilename { get { return _currentFilename; } }

        private static void InitializeCurrentTasks()
        {
            _CurrentTasks = new Tasks(_currentFilename);
        }
        private static void InitializeFileSystem()
        {
            InitializeFile(_currentFilename);
        }

        public static void InitializeMemorySystem()
        {
            InitializeFileSystem();
            InitializeCurrentTasks();
        }

        private static void InitializeFile(string filename)
        {
            if (!File.Exists(filename))
            {
                FileStream fs = File.Create(filename);
                fs.Close();
            }
        }

        public static void UpdateCurrentCsv()
        {
            _CurrentTasks.ListToCsv(_currentFilename);
        }

        public  static void UpdateCurrent()
        {
            List<Task> DeleteList = new List<Task>();

            foreach (Task task in _CurrentTasks.ListOfTasks)
            {
                if (task.IsReady)
                {
                    DeleteList.Add(task);
                }
            }

            foreach (Task task in DeleteList)
            {
                _CurrentTasks.deleteTaskFromList(task);
            }
        }
    }
}
