using System;
using System.Collections.Generic;
using System.IO;

namespace BOTAY
{
    public static class MemoryInteraction
    {
        private static string _documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string _currentFilename = Path.Combine(_documents, "BotayData.csv");
        private static string _historyFilename = Path.Combine(_documents, "BotayHistoryData.csv");
        private static Tasks _CurrentTasks;
        private static Tasks _HistoryTasks;

        public static Tasks CurrentTasks { get { return _CurrentTasks; } set { _CurrentTasks = value; } }
        public static Tasks HistoryTasks { get { return _HistoryTasks; } set { _HistoryTasks = value; } }
        public static string currentFilename { get { return _currentFilename; } }
        public static string historyFilename { get { return _historyFilename; } }

        private static void InitializeCurrentTasks()
        {
            _CurrentTasks = new Tasks(_currentFilename);
        }

        private static void InitializeHistoryTasks()
        {
            _HistoryTasks = new Tasks(_historyFilename);
        }

        private static void InitializeTasks()
        {
            InitializeCurrentTasks();
            InitializeHistoryTasks();
        }
        private static void InitializeFileSystem()
        {
            InitializeFile(_currentFilename);
            InitializeFile(_historyFilename);
        }

        public static void InitializeMemorySystem()
        {
            InitializeFileSystem();
            InitializeTasks();
        }

        private static void InitializeFile(string filename)
        {
            if (!File.Exists(filename))
            {
                FileStream fs = File.Create(filename);
                fs.Close();
            }
        }

        private static void UpdateCurrentCsv()
        {
            _CurrentTasks.ListToCsv(_currentFilename);
        }

        private static void UpdateHistoryCsv()
        {
            _HistoryTasks.ListToCsv(_historyFilename);
        }

        public static void UpdateCsv()
        {
            UpdateCurrentCsv();
            UpdateHistoryCsv();
        }

        public static void Update()
        {
            _HistoryTasks.leaveTwentyLast();
            UpdateCurrent();
            UpdateHistory();
        }

        private static void UpdateCurrent()
        {
            List<Task> DeleteList = new List<Task>();

            foreach (Task task in _CurrentTasks.ListOfTasks)
            {
                if (task.IsReady)
                {
                    _HistoryTasks.addToList(task);
                    DeleteList.Add(task);
                }
            }

            foreach (Task task in DeleteList)
            {
                _CurrentTasks.deleteTaskFromList(task);
            }
        }

        private static void UpdateHistory()
        {
            List<Task> DeleteList = new List<Task>();

            foreach (Task task in _HistoryTasks.ListOfTasks)
            {
                if (!task.IsReady)
                {
                    _CurrentTasks.addToList(task);
                    DeleteList.Add(task);
                }
            }

            foreach (Task task in DeleteList)
            {
                _HistoryTasks.deleteTaskFromList(task);
            }
        }
    }
}
