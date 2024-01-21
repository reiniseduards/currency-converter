// HomeControllerTests.cs
using System.Web;
using System.Web.Mvc;
using CurrencyConverter.Controllers;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Models;
using CurrencyConverter.Tests.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CurrencyConverter.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void TryConvert_ValidInput_ReturnsJsonResult()
        {
            string fakeJsonResponse = @"{
                ""result"": ""success"",
                ""documentation"": ""https://www.exchangerate-api.com/docs"",
                ""terms_of_use"": ""https://www.exchangerate-api.com/terms"",
                ""time_last_update_unix"": 1585267200,
                ""time_last_update_utc"": ""Fri, 27 Mar 2020 00:00:00 +0000"",
                ""time_next_update_unix"": 1585270800,
                ""time_next_update_utc"": ""Sat, 28 Mar 2020 01:00:00 +0000"",
                ""base_code"": ""EUR"",
                ""target_code"": ""GBP"",
                ""conversion_rate"": 0.8412,
                ""conversion_result"": 5.8884
            }";
            // Arrange
            var webClientMock = new Mock<IWebClientWrapper>();
            webClientMock.Setup(x => x.DownloadString(It.IsAny<string>()))
                .Returns(fakeJsonResponse);

            var configManagerMock = new Mock<IConfigurationManagerWrapper>();
            configManagerMock.Setup(x => x.GetAppSetting("ExchangeRateApiUrl")).Returns("https://fakeapi.com");

            var controller = new HomeController(webClientMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                HttpContext = Mock.Of<HttpContextBase>(c => c.Request == Mock.Of<HttpRequestBase>())
            };

            // Act
            var result = controller.TryConvert(100m, "USD", "EUR") as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(Response));
            Assert.AreEqual("success", ((Response)result.Data).result);
            Assert.AreEqual("5.8884", ((Response)result.Data).conversion_result);
            Assert.AreEqual("0.8412", ((Response)result.Data).conversion_rate);
        }

        [TestMethod]
        public void TryConvert_InvalidInput_ReturnsJsonResultWithError()
        {
            // Arrange
            var webClientMock = new Mock<IWebClientWrapper>();
            var configManagerMock = new Mock<IConfigurationManagerWrapper>();
            configManagerMock.Setup(x => x.GetAppSetting("ExchangeRateApiUrl")).Returns("https://fakeapi.com");

            var controller = new HomeController(webClientMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                HttpContext = Mock.Of<HttpContextBase>(c => c.Request == Mock.Of<HttpRequestBase>())
            };

            // Act
            var result = controller.TryConvert(-1m, "USD", "EUR") as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(Response));
            Assert.AreEqual("error", ((Response)result.Data).result);
        }
    }
}
