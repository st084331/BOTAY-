
using BOTAY_browser;
using NUnit.Framework;
namespace BOTAY_Tests
{
    public class TaskTests
    {
        [Test]
        public void NameSet1()
        {
            Task task = new Task();
            task.Name = "";
            Assert.AreEqual("NoName", task.Name);
        }

        [Test]
        public void NameSet2()
        {
            Task task = new Task();
            task.Name = "        ";
            Assert.AreEqual("NoName", task.Name);
        }

        [Test]
        public void NameSet3()
        {
            Task task = new Task();
            task.Name = string.Empty;
            Assert.AreEqual("NoName", task.Name);
        }

        [Test]
        public void NameSet4()
        {
            Task task = new Task();
            task.Name = "Name";
            Assert.AreEqual("Name", task.Name);
        }

        [Test]
        public void FullNameSet1()
        {
            Task task = new Task();
            task.FullName = "";
            Assert.AreEqual(string.Empty, task.FullName);
        }

        [Test]
        public void FullNameSet2()
        {
            Task task = new Task();
            task.FullName = "        ";
            Assert.AreEqual(string.Empty, task.FullName);
        }

        [Test]
        public void FullNameSet3()
        {
            Task task = new Task();
            task.FullName = string.Empty;
            Assert.AreEqual(string.Empty, task.FullName);
        }

        [Test]
        public void FullNameSet4()
        {
            Task task = new Task();
            task.FullName = "FullName";
            Assert.AreEqual("FullName", task.FullName);
        }

        [Test]
        public void UrlSet1()
        {
            Task task = new Task();
            task.Url = "";
            Assert.AreEqual(string.Empty, task.Url);
        }

        [Test]
        public void UrlSet2()
        {
            Task task = new Task();
            task.Url = "notURL";
            Assert.AreEqual(string.Empty, task.Url);
        }

        [Test]
        public void UrlSet3()
        {
            Task task = new Task();
            task.Url = "https://www.google.com/";
            Assert.AreEqual("https://www.google.com/", task.Url);
        }

        [Test]
        public void IsReadySet1()
        {
            Task task = new Task();
            task.IsReady = true;
            Assert.AreEqual(true, task.IsReady);
        }

        [Test]
        public void IsReadySet2()
        {
            Task task = new Task();
            task.IsReady = false;
            Assert.AreEqual(false, task.IsReady);
        }

        [Test]
        public void DeadlineSet1()
        {
            Task task = new Task();
            task.Deadline = "";
            Assert.AreEqual("NoDeadline", task.Deadline);
        }

        [Test]
        public void DeadlineSet2()
        {
            Task task = new Task();
            task.Deadline = "NotDeadline";
            Assert.AreEqual("NoDeadline", task.Deadline);
        }

        [Test]
        public void DeadlineSet3()
        {
            Task task = new Task();
            task.Deadline = "20.12.2000";
            Assert.AreEqual("NoDeadline", task.Deadline);
        }

        [Test]
        public void DeadlineSet4()
        {
            Task task = new Task();
            task.Deadline = "01.01.2500";
            Assert.AreEqual("01.01.2500", task.Deadline);
        }
    }
}