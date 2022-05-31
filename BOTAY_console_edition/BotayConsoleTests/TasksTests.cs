using System.IO;
using NUnit.Framework;
using BOTAY_console_edition;

namespace BotayConsoleTests;

public class TasksTests
{
    private string TestFile = Path.Combine(Directory.GetCurrentDirectory(), "TasksTest.csv");

    [Test]
    public void AddToListTest1()
    {
        File.WriteAllText(TestFile,
            "Name1,FullName1,https://www.google.com/,01.01.2500,false\nName2,FullName2,https://www.google.com/,01.01.2500,false");
        Tasks tasks = new Tasks(TestFile);
        Task task = new Task();
        tasks.addToList(task);
        Assert.AreEqual(task, tasks.ListOfTasks[2]);
    }

    [Test]
    public void AddToListTest2()
    {
        File.WriteAllText(TestFile,
            "Name1,FullName1,https://www.google.com/,01.01.2500,false\nName2,FullName2,https://www.google.com/,01.01.2500,false");
        Tasks tasks = new Tasks(TestFile);
        Task task = default;
        tasks.addToList(task);
        Assert.AreEqual(2, tasks.ListOfTasks.Count);
    }

    [Test]
    public void AddToListTest3()
    {
        File.WriteAllText(TestFile,
            "Name1,FullName1,https://www.google.com/,01.01.2500,false\nName2,FullName2,https://www.google.com/,01.01.2500,false");
        Tasks tasks = new Tasks(TestFile);
        Task task = null;
        tasks.addToList(task);
        Assert.AreEqual(2, tasks.ListOfTasks.Count);
    }

    [Test]
    public void AddToListTest4()
    {
        Tasks tasks = new Tasks();
        Task task = new Task();
        tasks.addToList(task);
        Assert.AreEqual(task, tasks.ListOfTasks[0]);
    }

    [Test]
    public void DeleteTaskFromListTest1()
    {
        File.WriteAllText(TestFile,
            "Name1,FullName1,https://www.google.com/,01.01.2500,false\nName2,FullName2,https://www.google.com/,01.01.2500,false");
        Tasks tasks = new Tasks(TestFile);
        Task task = tasks.ListOfTasks[1];
        tasks.deleteTaskFromList(0);
        Assert.AreEqual(task, tasks.ListOfTasks[0]);
    }

    [Test]
    public void DeleteTaskFromListTest2()
    {
        File.WriteAllText(TestFile,
            "Name1,FullName1,https://www.google.com/,01.01.2500,false\nName2,FullName2,https://www.google.com/,01.01.2500,false");
        Tasks tasks = new Tasks(TestFile);
        Task task = tasks.ListOfTasks[1];
        tasks.deleteTaskFromList(tasks.ListOfTasks[0]);
        Assert.AreEqual(task, tasks.ListOfTasks[0]);
    }

    [Test]
    public void LeaveTwentyLastTest1()
    {
        Tasks tasks = new Tasks();
        Tasks controlTasks = new Tasks();
        for (int i = 0; i < 25; i++)
        {
            Task task = new Task();
            tasks.addToList(task);
            if (i > 4)
            {
                controlTasks.addToList(task);
            }
        }

        tasks.leaveTwentyLast();
        Assert.AreEqual(controlTasks.ListOfTasks, tasks.ListOfTasks);
    }

    [Test]
    public void LeaveTwentyLastTest2()
    {
        Tasks tasks = new Tasks();
        for (int i = 0; i < 20; i++)
        {
            tasks.addToList(new Task());
        }

        tasks.leaveTwentyLast();
        Assert.AreEqual(20, tasks.ListOfTasks.Count);
    }

    [Test]
    public void LeaveTwentyLastTest3()
    {
        Tasks tasks = new Tasks();
        for (int i = 0; i < 10; i++)
        {
            tasks.addToList(new Task());
        }

        tasks.leaveTwentyLast();
        Assert.AreEqual(10, tasks.ListOfTasks.Count);
    }

    [Test]
    public void ListToCsvTest1()
    {
        Tasks tasks = new Tasks();
        string NoNameTaskString = "NoName,,,NoDeadline,False";
        tasks.addToList(new Task());
        tasks.ListToCsv(TestFile);
        Assert.AreEqual(NoNameTaskString, File.ReadAllText(TestFile));
    }

    [Test]
    public void ListToCsvTest2()
    {
        Tasks tasks = new Tasks();
        string NoNameTaskString = "NoName,,,NoDeadline,False\nNoName,,,NoDeadline,False";
        tasks.addToList(new Task());
        tasks.addToList(new Task());
        tasks.ListToCsv(TestFile);
        Assert.AreEqual(NoNameTaskString, File.ReadAllText(TestFile));
    }

    [Test]
    public void SortTasksListByDeadlineTest1()
    {
        File.WriteAllText(TestFile,
            "Name1,FullName1,https://www.google.com/,01.01.2500,false\nName2,FullName2,https://www.google.com/,01.01.2300,false");
        Tasks tasks = new Tasks(TestFile);
        Assert.AreEqual("01.01.2500", tasks.ListOfTasks[0].Deadline);
        Assert.AreEqual("01.01.2300", tasks.ListOfTasks[1].Deadline);
        tasks.SortTasksListByDeadline();
        Assert.AreEqual("01.01.2500", tasks.ListOfTasks[1].Deadline);
        Assert.AreEqual("01.01.2300", tasks.ListOfTasks[0].Deadline);
    }

    [Test]
    public void SortTasksListByDeadlineTest2()
    {
        File.WriteAllText(TestFile,
            "NoName,,,NoDeadline,false\nName1,FullName1,https://www.google.com/,01.01.2500,false\nName2,FullName2,https://www.google.com/,01.01.2300,false");
        Tasks tasks = new Tasks(TestFile);
        Assert.AreEqual("NoDeadline", tasks.ListOfTasks[0].Deadline);
        Assert.AreEqual("01.01.2500", tasks.ListOfTasks[1].Deadline);
        Assert.AreEqual("01.01.2300", tasks.ListOfTasks[2].Deadline);
        tasks.SortTasksListByDeadline();
        Assert.AreEqual("01.01.2500", tasks.ListOfTasks[1].Deadline);
        Assert.AreEqual("01.01.2300", tasks.ListOfTasks[0].Deadline);
        Assert.AreEqual("NoDeadline", tasks.ListOfTasks[2].Deadline);
    }
}