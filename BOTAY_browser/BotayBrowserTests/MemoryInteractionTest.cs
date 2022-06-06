
using BOTAY_browser;
using NUnit.Framework;
using System.IO;

namespace BOTAY_Tests
{
    public class MemoryInteractionTest
    {
        [Test]
        public void InitializeMemorySystemTest()
        {
            MemoryInteraction.InitializeMemorySystem();
            Assert.IsTrue(File.Exists(MemoryInteraction.currentFilename));
            Assert.AreNotEqual(null, MemoryInteraction.CurrentTasks);
        }

        [Test]
        public void UpdateCsvTest()
        {
            MemoryInteraction.InitializeMemorySystem();
            Task task1 = new Task();
            string OldCurrentCsvText = File.ReadAllText(MemoryInteraction.currentFilename);
            MemoryInteraction.CurrentTasks.addToList(task1);
            MemoryInteraction.UpdateCurrentCsv();
            if (OldCurrentCsvText != string.Empty)
            {
                Assert.AreEqual(OldCurrentCsvText + "\nNoName,,,NoDeadline,False", File.ReadAllText(MemoryInteraction.currentFilename));
            }
            else
            {
                Assert.AreEqual(OldCurrentCsvText + "NoName,,,NoDeadline,False", File.ReadAllText(MemoryInteraction.currentFilename));
            }
            MemoryInteraction.CurrentTasks.deleteTaskFromList(task1);
            MemoryInteraction.UpdateCurrentCsv();
            Assert.AreEqual(OldCurrentCsvText, File.ReadAllText(MemoryInteraction.currentFilename));
        }

        [Test]
        public void UpdateTest()
        {
            MemoryInteraction.InitializeMemorySystem();
            Task task1 = new Task();
            Task task2 = new Task();
            task1.IsReady = true;
            MemoryInteraction.CurrentTasks.addToList(task1);
            MemoryInteraction.CurrentTasks.addToList(task2);
            MemoryInteraction.UpdateCurrent();
            Assert.IsFalse(MemoryInteraction.CurrentTasks.ListOfTasks.Contains(task1));
            Assert.IsTrue(MemoryInteraction.CurrentTasks.ListOfTasks.Contains(task2));
            MemoryInteraction.CurrentTasks.deleteTaskFromList(task2);
        }
    }
}
