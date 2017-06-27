namespace PollingLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Threading;
    using static Polling;

    [TestClass]
    public class Tests
    {
        private const string Filename = "test.txt";

        private const string Content = "Hello World!";

        private readonly Func<string> ReadFile = () => File.Exists(Filename) ? File.ReadAllText(Filename) : null;

        private Timer Timer(int ms)
        {
            return new Timer(_ => File.WriteAllText(Filename, Content), null, ms, Timeout.Infinite);
        }

        [TestInitialize]
        public void Initialize()
        {
            if (File.Exists(Filename))
                File.Delete(Filename);
        }

        [TestMethod]
        public void Vanilla()
        {
            using (var timer = Timer(500))            {
                Assert.AreEqual(Content, Poll(ReadFile));
            }
        }

        [TestMethod]
        public void CustomCheck()
        {
            using (var timer = Timer(500))
            {
                Poll(
                    ReadFile,
                    content =>
                    {
                        if (content == null)
                        {
                            return false;
                        }
                        else
                        {
                            Assert.AreEqual(Content, content);
                            return true;
                        }
                    }
                );
            }
        }

        [TestMethod, ExpectedException(typeof(PollingTimeoutException))]
        public void ExceptionTimeout()
        {
            Poll(ReadFile, 500);
        }

        [TestMethod, ExpectedException(typeof(PollingTimeoutException))]
        public void SafeTimeout()
        {
            Assert.AreEqual(null, Poll(ReadFile, 500));
        }
    }
}
