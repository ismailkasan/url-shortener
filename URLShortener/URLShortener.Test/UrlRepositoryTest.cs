using Moq;
using URLShortener.Data;

namespace URLShortener.Test
{
    /// <summary>
    /// Test class fro UrlRepository with moq.
    /// </summary>
    [TestClass]
    public class UrlRepositoryTest
    {
        public readonly IUrlRepository<UrlModel> _mockRepository;
        public UrlRepositoryTest()
        {
            // Mock url list 
            var urlList = new List<UrlModel>
            {
                new UrlModel("http://demo-url.com/long-url-1","segment-1") {Id=1,ShortUrl="http://demo-url.com/segment-1" },
                new UrlModel("http://demo-url.com/long-url-2","segment-2") {Id=2,ShortUrl="http://demo-url.com/segment-2" },
                new UrlModel("http://demo-url.com/long-url-3","segment-3") {Id=3,ShortUrl="http://demo-url.com/segment-3" },
            };

            // Create Mock repository 
            var mockRepository = new Mock<IUrlRepository<UrlModel>>();

            // Configure repository methods 
            mockRepository
                .Setup(c => c.GetAsync(It.IsAny<string>()))
                .Returns(async (string segment) => await Task.FromResult(new ServiceResult<UrlModel>(urlList.Find(a => a.Segment == segment))));

            mockRepository
                .Setup(c => c.AnyAsync(It.IsAny<string>()))
                .Returns(async (string segment) => await Task.FromResult(new ServiceResult<bool>(urlList.Any(a => a.Segment == segment))));

            mockRepository
                .Setup(mr => mr.AddAsync(It.IsAny<UrlModel>()))
                .Callback((UrlModel target) => { urlList.Add(target); });

            // Set Mock object.
            this._mockRepository = mockRepository.Object;
        }

        /// <summary>
        /// Test URL with "segment-1" in repository.
        /// </summary>
        [TestMethod]
        public void Url_Should_Not_Empty_With_Segment_1_Test_Method()
        {
            var expected = this._mockRepository.GetAsync("segment-1").Result;

            // Url is not null.
            Assert.IsNotNull(expected.Data);

            // LongUrl is not null.
            Assert.IsNotNull(expected.Data.LongUrl);

            // Url is not deleted.
            Assert.IsTrue(!expected.Data.IsDeleted);
        }

        /// <summary>
        /// Test URL exist or not  with "segment-2" in repository
        /// </summary>
        [TestMethod]
        public void Url_Should_Exist_With_Segment_2_Test_Method()
        {
            var expected = this._mockRepository.AnyAsync("segment-2").Result;

            // Url is not null.
            Assert.IsNotNull(expected.Data);

            // Url is exist.
            Assert.IsTrue(expected.Data);
        }

        /// <summary>
        /// Add new Url with "segment-4".Then,checks it was added or not.
        /// </summary>
        [TestMethod]
        public void Url_Should_Be_Added_With_Segment_4_Test_Method()
        {
            var model = new UrlModel("http://demo-url.com/long-url-4", "segment-4")
            {
                Id = 4,
                ShortUrl = "http://demo-url.com/segment-4"
            };

            var added = this._mockRepository.AddAsync(model).Result;
            var expected = this._mockRepository.GetAsync("segment-4").Result;

            // Url is not null.
            Assert.IsNotNull(expected.Data);

            // Check for "segment-4"
            Assert.IsTrue(expected.Data.Segment == "segment-4");
        }
    }
}