using System.Text.RegularExpressions;

namespace BOTAY_console_edition;

public class MainWin
{
    private bool _pooling = true;

    public void MainWinPolling()
    {
        //Инициализируем файловую систему и наборы заданий
        MemoryInteraction.InitializeMemorySystem();
        //Сортируем текущие задания по дедлайну
        MemoryInteraction.CurrentTasks.SortTasksListByDeadline();

        Console.WriteLine("Hi! Welcome to BOTAY! To close the application type end");
        printCurrent();
        string currentLine = String.Empty;
        while (_pooling)
        {
            currentLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (!String.IsNullOrEmpty(currentLine) && !String.IsNullOrWhiteSpace(currentLine))
            {
                ProcessLine(currentLine);
            }
        }

        //Обновляем файлы памяти перед закрытием
        MemoryInteraction.HistoryTasks.leaveTwentyLast();
        MemoryInteraction.UpdateCsv();
    }

    private void deleteCommand()
    {
        if (MemoryInteraction.CurrentTasks.ListOfTasks.Count > 0)
        {
            printCurrent();
            Console.WriteLine("Enter the task number from the list above that you want to delete.");
            string indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (indexStr == "cancel")
            {
                Console.Clear();
                printCurrent();
                return;
            }

            int index = -1;
            Int32.TryParse(indexStr, out index);
            while (index < 0 || index >= MemoryInteraction.CurrentTasks.ListOfTasks.Count ||
                   String.IsNullOrEmpty(indexStr) || String.IsNullOrWhiteSpace(indexStr))
            {
                Console.WriteLine("Incorrect index, try again.");
                indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
                if (indexStr == "cancel")
                {
                    Console.Clear();
                    printCurrent();
                    return;
                }

                Int32.TryParse(indexStr, out index);
            }

            MemoryInteraction.CurrentTasks.deleteTaskFromList(index);
            Console.Clear();
            printCurrent();
        }
    }

    private void addCommand()
    {
        Task newTask = new Task();
        Console.WriteLine("Enter name of the task.");
        string readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
        if (readLine == "cancel")
        {
            Console.Clear();
            printCurrent();
            return;
        }

        newTask.Name = readLine;
        Console.WriteLine("Enter full name of the task.");
        readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
        if (readLine == "cancel")
        {
            Console.Clear();
            printCurrent();
            return;
        }

        newTask.FullName = readLine;
        Console.WriteLine("Enter URL of the task.");
        readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
        if (readLine == "cancel")
        {
            Console.Clear();
            printCurrent();
            return;
        }

        newTask.Url = readLine;
        Console.WriteLine("Enter deadline of the task.");
        readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
        if (readLine == "cancel")
        {
            Console.Clear();
            printCurrent();
            return;
        }

        newTask.Deadline = readLine;
        newTask.IsReady = false;
        MemoryInteraction.CurrentTasks.addToList(newTask);
        MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
        Console.Clear();
        Console.WriteLine("New task added.");

        printCurrent();
    }

    private void printCurrent()
    {
        Console.WriteLine();
        Console.WriteLine("Current tasks:");
        Console.WriteLine(MemoryInteraction.CurrentTasks.printList());
    }

    private void editCommand()
    {
        if (MemoryInteraction.CurrentTasks.ListOfTasks.Count > 0)
        {
            printCurrent();
            Console.WriteLine("Enter the task number from the list above that you want to edit.");
            string indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (indexStr == "cancel")
            {
                Console.Clear();
                printCurrent();
                return;
            }

            int index = -1;
            Int32.TryParse(indexStr, out index);
            while (index < 0 || index >= MemoryInteraction.CurrentTasks.ListOfTasks.Count ||
                   String.IsNullOrEmpty(indexStr) || String.IsNullOrWhiteSpace(indexStr))
            {
                Console.WriteLine("Incorrect index, try again.");
                indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
                if (indexStr == "cancel")
                {
                    Console.Clear();
                    printCurrent();
                    return;
                }

                Int32.TryParse(indexStr, out index);
            }

            Task task = MemoryInteraction.CurrentTasks.ListOfTasks[index];
            string[] taskParams = new string[4];
            Console.WriteLine("Enter name of the task.");
            string readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printCurrent();
                return;
            }

            taskParams[0] = readLine;
            Console.WriteLine("Enter full name of the task.");
            readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printCurrent();
                return;
            }

            taskParams[1] = readLine;
            Console.WriteLine("Enter URL of the task.");
            readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printCurrent();
                return;
            }

            taskParams[2] = readLine;
            Console.WriteLine("Enter deadline of the task.");
            readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printCurrent();
                return;
            }

            taskParams[3] = readLine;
            task.Name = taskParams[0];
            task.FullName = taskParams[1];
            task.Url = taskParams[2];
            task.Deadline = taskParams[3];
            MemoryInteraction.CurrentTasks.SortTasksListByDeadline();
            Console.Clear();
            Console.WriteLine("Task edited.");

            printCurrent();
        }
    }

    private void HistoryWinCommand()
    {
        Console.Clear();
        HistoryWin historyWin = new HistoryWin();
        historyWin.HistoryWinPolling();
        Console.Clear();
        printCurrent();
    }

    private void doneCommand()
    {
        if (MemoryInteraction.CurrentTasks.ListOfTasks.Count > 0)
        {
            printCurrent();
            Console.WriteLine("Enter the task number from the list above that you want to finish.");
            string indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (indexStr == "cancel")
            {
                Console.Clear();
                printCurrent();
                return;
            }

            int index = -1;
            Int32.TryParse(indexStr, out index);
            while (index < 0 || index >= MemoryInteraction.CurrentTasks.ListOfTasks.Count ||
                   String.IsNullOrEmpty(indexStr) || String.IsNullOrWhiteSpace(indexStr))
            {
                Console.WriteLine("Incorrect index, try again.");
                indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
                if (indexStr == "cancel")
                {
                    Console.Clear();
                    printCurrent();
                    return;
                }

                Int32.TryParse(indexStr, out index);
            }

            Task task = MemoryInteraction.CurrentTasks.ListOfTasks[index];
            task.IsReady = true;
            MemoryInteraction.Update(task);
            Console.Clear();
            printCurrent();
        }
    }
    
    private void ProcessLine(string line)
    {
        switch (line)
        {
            case "delete":
                deleteCommand();
                break;
            case "add":
                addCommand();
                break;
            case "edit":
                editCommand();
                break;
            case "history":
                HistoryWinCommand();
                break;
            case "done":
                doneCommand();
                break;
            case "end":
                _pooling = false;
                break;
            default:
                Console.WriteLine("No such command.");
                break;
        }
    }
}