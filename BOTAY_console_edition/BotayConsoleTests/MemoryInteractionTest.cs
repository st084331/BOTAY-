using System.IO;
using NUnit.Framework;
using BOTAY_console_edition;

namespace BotayConsoleTests;

public class MemoryInteractionTest
{
    [Test]
        public void InitializeMemorySystemTest()
        {
            MemoryInteraction.InitializeMemorySystem();
            Assert.IsTrue(File.Exists(MemoryInteraction.currentFilename));
            Assert.IsTrue(File.Exists(MemoryInteraction.historyFilename));
            Assert.AreNotEqual(null, MemoryInteraction.CurrentTasks);
            Assert.AreNotEqual(null, MemoryInteraction.HistoryTasks);
        }

        [Test]
        public void UpdateCsvTest()
        {
            MemoryInteraction.InitializeMemorySystem();
            Task task1 = new Task();
            Task task2 = new Task();
            string OldCurrentCsvText = File.ReadAllText(MemoryInteraction.currentFilename);
            string OldHistoryCsvText = File.ReadAllText(MemoryInteraction.historyFilename);
            MemoryInteraction.CurrentTasks.addToList(task1);
            MemoryInteraction.HistoryTasks.addToList(task2);
            MemoryInteraction.UpdateCsv();
            if (OldCurrentCsvText != string.Empty)
            {
                Assert.AreEqual(OldCurrentCsvText + "\nNoName,,,NoDeadline,False", File.ReadAllText(MemoryInteraction.currentFilename));
            }
            else
            {
                Assert.AreEqual(OldCurrentCsvText + "NoName,,,NoDeadline,False", File.ReadAllText(MemoryInteraction.currentFilename));
            }
            if(OldHistoryCsvText != string.Empty)
            {
                Assert.AreEqual(OldHistoryCsvText + "\nNoName,,,NoDeadline,False", File.ReadAllText(MemoryInteraction.historyFilename));
            }
            else
            {
                Assert.AreEqual(OldHistoryCsvText + "NoName,,,NoDeadline,False", File.ReadAllText(MemoryInteraction.historyFilename));
            }
            MemoryInteraction.CurrentTasks.deleteTaskFromList(task1);
            MemoryInteraction.HistoryTasks.deleteTaskFromList(task2);
            MemoryInteraction.UpdateCsv();
            Assert.AreEqual(OldCurrentCsvText, File.ReadAllText(MemoryInteraction.currentFilename));
            Assert.AreEqual(OldHistoryCsvText, File.ReadAllText(MemoryInteraction.historyFilename));
        }

        [Test]
        public void UpdateTest()
        {
            MemoryInteraction.InitializeMemorySystem();
            Task task1 = new Task();
            Task task2 = new Task();
            task1.IsReady = true;
            
            MemoryInteraction.CurrentTasks.addToList(task1);
            MemoryInteraction.Update(task1);
            Assert.IsFalse(MemoryInteraction.CurrentTasks.ListOfTasks.Contains(task1));
            Assert.IsTrue(MemoryInteraction.HistoryTasks.ListOfTasks.Contains(task1));
            
            MemoryInteraction.HistoryTasks.addToList(task2);
            MemoryInteraction.Update(task2);
            Assert.IsTrue(MemoryInteraction.CurrentTasks.ListOfTasks.Contains(task2));
            Assert.IsFalse(MemoryInteraction.HistoryTasks.ListOfTasks.Contains(task2));
            
            MemoryInteraction.CurrentTasks.deleteTaskFromList(task2);
            MemoryInteraction.HistoryTasks.deleteTaskFromList(task1);
        }
}