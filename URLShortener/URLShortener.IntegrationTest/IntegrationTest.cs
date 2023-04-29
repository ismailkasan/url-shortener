using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace URLShortener.IntegrationTest
{

    /// <summary>
    /// ApiApplication Host class
    /// </summary>
    internal class ApiApplication : WebApplicationFactory<Program>
    {
        /// <summary>
        /// Create a host to actual builder.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
    }

    /// <summary>
    /// Test class is responsable for integration tests of api endpoints.
    /// </summary>
    public class IntegrationTest
    {
        /// <summary>
        /// HttpClient object
        /// </summary>
        private readonly HttpClient _client;


        /*
         * Apply singleton design to get only one application instance.
         */
        private static ApiApplication? _apiInstance;
        private static readonly object lockObject = new object();
        private static ApiApplication ApiApplicationInstance
        {
            get
            {
                lock (lockObject) // lock thread to guraantee only one intance created.
                {
                    if (_apiInstance == null)
                    {
                        _apiInstance = new ApiApplication();
                    }
                    return _apiInstance;

                }
            }
        }

        /// <summary>
        /// Constructor of IntegrationTest creaate HttpCliet object from application server.
        /// </summary>
        public IntegrationTest()
        {
            _client = ApiApplicationInstance.Server.CreateClient();
        }

        /// <summary>
        /// Should return OK with ServiceResult object that has a ResultType error when tries to insert custom url with actuals parameters.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Return_OK_With_ServiceResult_ResultType_Error_When_Tries_To_Insert_Custom_Url()
        {
            var expectedResultType = ResultType.Warning;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange
            var request = new UrlRequestDto("", "test")
            {
                IsCustomUrl = true,
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var response = await _client.PostAsync("/api/v1/Url/CreateShortUrl", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResult<UrlResponseDto>>(actualResult);

            // Assert
            Assert.Equal(expectedResultType, result?.ResultType);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }


        /// <summary>
        /// Should return OK with ServiceResult object that has a ResultType error when tries to insert custom url with custom url segment.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Return_OK_With_ServiceResult_ResultType_Error_When_Tries_To_Insert_Custom_Url_with_Custom_Url_Segment()
        {
            var expectedResultType = ResultType.Warning;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange
            var request = new UrlRequestDto("http://demo-url.com/long-url-6", "custom-segment")
            {
                IsCustomUrl = true,
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var response = await _client.PostAsync("/api/v1/Url/CreateShortUrl", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResult<UrlResponseDto>>(actualResult);

            // Assert
            Assert.Equal(expectedResultType, result?.ResultType);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        /// <summary>
        ///  Should return OK with ServiceResult object that has a ResultType success when tries to create url.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Return_OK_With_ServiceResult_ResultType_Success_When_Tries_To_Create()
        {
            var expectedResultType = ResultType.Success;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange
            var request = new UrlRequestDto("http://demo-url.com/long-url-5", "")
            {
                IsCustomUrl = false,
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var response = await _client.PostAsync("/api/v1/Url/CreateShortUrl", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResult<UrlResponseDto>>(actualResult);

            // Assert
            Assert.Equal(expectedResultType, result?.ResultType);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
    }
}