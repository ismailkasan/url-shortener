using Moq;
using Microsoft.Extensions.Logging;
using URLShortener.Data;

namespace URLShortener.Test
{
    /// <summary>
    /// Test class for LogRepository with moq.
    /// </summary>
    public class LogRepositoryTest
    {
        public readonly IBaseRepository<Log> _mockRepository;
        public LogRepositoryTest()
        {
            // Mock log list 
            var logList = new List<Log>();

            // Create Mock repository 
            var mockRepository = new Mock<IBaseRepository<Log>>();

            // Configure repository methods 

            mockRepository
                .Setup(mr => mr.AddAsync(It.IsAny<Log>()))
                .Callback((Log target) => { logList.Add(target); });

            // Set Mock object.
            this._mockRepository = mockRepository.Object;
        }

        /// <summary>
        /// Log should be added.
        /// </summary>
        [TestMethod]
        public void Log_Should_Be_Added_Test_Method()
        {
            string methodName = Helpers.CreateGuid(8);
            var model = new Log()
            {
                CreatedDate = DateTime.Now,
                LogLevel = LogLevel.Error,
                Message = "Test log",
                StackTrace = "Test stack trace",
                MethodName = methodName,
                Url = "Test url"
            };

            var added = this._mockRepository.AddAsync(model).Result;

            // log is not null.
            Assert.IsNotNull(added.Data);

            // Checks for succes
            Assert.IsTrue(added.ResultType == ResultType.Success);
        }
    }
}
