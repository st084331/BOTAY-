using System.Text.RegularExpressions;

namespace BOTAY_console_edition;

public class HistoryWin
{
    private bool _pooling = true;

    public void HistoryWinPolling()
    {
        //Сортируем текущие задания по дедлайну
        MemoryInteraction.HistoryTasks.leaveTwentyLast();

        Console.WriteLine("Type end to close History");
        printHistory();
        string currentLine = String.Empty;
        while (_pooling)
        {
            currentLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (!String.IsNullOrEmpty(currentLine) && !String.IsNullOrWhiteSpace(currentLine))
            {
                ProcessLine(currentLine);
            }
        }
    }

    private void deleteCommand()
    {
        if (MemoryInteraction.HistoryTasks.ListOfTasks.Count > 0)
        {
            printHistory();
            Console.WriteLine("Enter the task number from the list above that you want to delete.");
            string indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (indexStr == "cancel")
            {
                Console.Clear();
                printHistory();
                return;
            }

            int index = -1;
            Int32.TryParse(indexStr, out index);
            while (index < 0 || index >= MemoryInteraction.HistoryTasks.ListOfTasks.Count ||
                   String.IsNullOrEmpty(indexStr) || String.IsNullOrWhiteSpace(indexStr))
            {
                Console.WriteLine("Incorrect index, try again.");
                indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
                if (indexStr == "cancel")
                {
                    Console.Clear();
                    printHistory();
                    return;
                }

                Int32.TryParse(indexStr, out index);
            }

            MemoryInteraction.HistoryTasks.deleteTaskFromList(index);
            Console.Clear();
            printHistory();
        }
    }

    private void printHistory()
    {
        Console.WriteLine();
        Console.WriteLine("History tasks:");
        Console.WriteLine(MemoryInteraction.HistoryTasks.printList());
    }

    private void editCommand()
    {
        if (MemoryInteraction.HistoryTasks.ListOfTasks.Count > 0)
        {
            printHistory();
            Console.WriteLine("Enter the task number from the list above that you want to edit.");
            string indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (indexStr == "cancel")
            {
                Console.Clear();
                printHistory();
                return;
            }

            int index = -1;
            Int32.TryParse(indexStr, out index);
            while (index < 0 || index >= MemoryInteraction.HistoryTasks.ListOfTasks.Count ||
                   String.IsNullOrEmpty(indexStr) || String.IsNullOrWhiteSpace(indexStr))
            {
                Console.WriteLine("Incorrect index, try again.");
                indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
                if (indexStr == "cancel")
                {
                    Console.Clear();
                    printHistory();
                    return;
                }

                Int32.TryParse(indexStr, out index);
            }

            Task task = MemoryInteraction.HistoryTasks.ListOfTasks[index];
            string[] taskParams = new string[4];
            Console.WriteLine("Enter name of the task.");
            string readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printHistory();
                return;
            }

            taskParams[0] = readLine;
            Console.WriteLine("Enter full name of the task.");
            readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printHistory();
                return;
            }

            taskParams[1] = readLine;
            Console.WriteLine("Enter URL of the task.");
            readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printHistory();
                return;
            }

            taskParams[2] = readLine;
            Console.WriteLine("Enter deadline of the task.");
            readLine = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (readLine == "cancel")
            {
                Console.Clear();
                printHistory();
                return;
            }

            taskParams[3] = readLine;
            task.Name = taskParams[0];
            task.FullName = taskParams[1];
            task.Url = taskParams[2];
            task.Deadline = taskParams[3];
            MemoryInteraction.HistoryTasks.SortTasksListByDeadline();
            Console.Clear();
            Console.WriteLine("Task edited.");

            printHistory();
        }
    }

    private void undoneCommand()
    {
        if (MemoryInteraction.HistoryTasks.ListOfTasks.Count > 0)
        {
            printHistory();
            Console.WriteLine("Enter the task number from the list above that you want to return.");
            string indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
            if (indexStr == "cancel")
            {
                Console.Clear();
                printHistory();
                return;
            }

            int index = -1;
            Int32.TryParse(indexStr, out index);
            while (index < 0 || index >= MemoryInteraction.HistoryTasks.ListOfTasks.Count ||
                   String.IsNullOrEmpty(indexStr) || String.IsNullOrWhiteSpace(indexStr))
            {
                Console.WriteLine("Incorrect index, try again.");
                indexStr = Regex.Replace(Console.ReadLine(), " {2,}", " ").Trim();
                if (indexStr == "cancel")
                {
                    Console.Clear();
                    printHistory();
                    return;
                }

                Int32.TryParse(indexStr, out index);
            }

            Task task = MemoryInteraction.HistoryTasks.ListOfTasks[index];
            task.IsReady = false;
            MemoryInteraction.Update(task);
            Console.Clear();
            printHistory();
        }
    }
    
    private void ProcessLine(string line)
    {
        switch (line)
        {
            case "delete":
                deleteCommand();
                break;
            case "edit":
                editCommand();
                break;
            case "undone":
                undoneCommand();
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