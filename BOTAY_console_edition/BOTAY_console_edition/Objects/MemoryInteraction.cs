using System;
using System.Collections.Generic;
using System.IO;

namespace BOTAY_console_edition;

public static class MemoryInteraction
{
    private static string _documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private static string _currentFilename = Path.Combine(_documents, "BotayData.csv");
    private static string _historyFilename = Path.Combine(_documents, "BotayHistoryData.csv");
    private static Tasks _CurrentTasks;
    private static Tasks _HistoryTasks;

    public static Tasks CurrentTasks
    {
        get { return _CurrentTasks; }
        set { _CurrentTasks = value; }
    }

    public static Tasks HistoryTasks
    {
        get { return _HistoryTasks; }
        set { _HistoryTasks = value; }
    }

    public static string currentFilename
    {
        get { return _currentFilename; }
    }

    public static string historyFilename
    {
        get { return _historyFilename; }
    }

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

    public static void Update(Task task)
    {
        if (task.IsReady)
        {
            MemoryInteraction.CurrentTasks.deleteTaskFromList(task);
            MemoryInteraction.HistoryTasks.addToList(task);
        }
        else
        {
            MemoryInteraction.HistoryTasks.deleteTaskFromList(task);
            MemoryInteraction.CurrentTasks.addToList(task);
        }
    }
}