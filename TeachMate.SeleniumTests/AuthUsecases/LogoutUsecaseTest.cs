using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TeachMate.SeleniumTests.AuthUsecases
{
    public class LogoutUsecaseTest
    {
        private IWebDriver _driver;

        private string baseUrl = "https://localhost:5173/";
        private (string username, string password) _validCredential = ("testuser", "Test123!");

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--remote-debugging-port=9222");
            _driver = new ChromeDriver(options);
        }

        [Test]
        public void TestShowUserCardAndLogoutButton()
        {
            // Wait for the elements to be visible
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            LoginManually(wait);

            _driver.Navigate().GoToUrl(baseUrl);

            try
            {
                IWebElement userHeaderCard = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("user-header-card")));
                IWebElement logoutButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("logout-button")));

                // Assert that the elements are displayed
                Assert.That(userHeaderCard.Displayed, Is.True, "User header card is not displayed.");
                Assert.That(logoutButton.Displayed, Is.True, "Logout button is not displayed.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Test failed: Timed out waiting for elements to be visible.");
                Assert.Fail("Timed out waiting for elements to be visible.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test failed: " + ex.Message);
                Assert.Fail("Test failed: " + ex.Message);
            }
        }
        [Test]
        public void TestLogoutAndCheckLoginButton()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            LoginManually(wait);

            _driver.Navigate().GoToUrl(baseUrl);

            try
            {
                // Wait for the logout button to be clickable using JavaScript
                IWebElement logoutButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("logout-button")));
                logoutButton.Click();

                // Wait for the logout button to be stale, indicating the page has reloaded
                wait.Until(ExpectedConditions.StalenessOf(logoutButton));

                // Wait for the login button to be visible
                IWebElement loginButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login-button")));

                // Assert that the login button is displayed
                Assert.That(loginButton.Displayed, Is.True, "Login button is not displayed.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Test failed: Timed out waiting for elements to be visible.");
                Assert.Fail("Timed out waiting for elements to be visible.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test failed: " + ex.Message);
                Assert.Fail("Test failed: " + ex.Message);
            }
        }


        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }
        private void LoginManually(WebDriverWait wait)
        {
            _driver.Navigate().GoToUrl(baseUrl + "auth/login");

            try
            {
                IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));

                usernameField.SendKeys(_validCredential.username);

                IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
                passwordField.SendKeys(_validCredential.password);

                _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                wait.Until(driver => driver.Url == baseUrl);

                Assert.That(_driver.Url, Is.EqualTo(baseUrl));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login failed: " + ex.Message);
                Assert.Fail("Login failed: " + ex.Message);
            }
        }
    }
}
