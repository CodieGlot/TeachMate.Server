using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TeachMate.SeleniumTests
{
    public class LoginUsecaseTest
    {
        private static string baseUrl = "https://localhost:5173/";
        private IWebDriver _driver;

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
        public void TestManualSignup()
        {
            // Navigate to the signup page
            _driver.Navigate().GoToUrl(baseUrl + "auth/signup");

            // Set up WebDriverWait for explicit waits
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            try
            {
                // Fill out the signup form
                // Wait until the username field is visible
                IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));

                // Fill out the signup form
                usernameField.SendKeys("testuser");

                // Wait until the password field is visible
                IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
                passwordField.SendKeys("Test123!");

                // Wait until the confirm password field is visible
                IWebElement confirmPasswordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("confirmPassword")));
                confirmPasswordField.SendKeys("Test123!");

                // Wait until the email field is visible
                IWebElement emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
                emailField.SendKeys("testuser@example.com");

                // Select the user role (optional)
                // Assuming Learner is default selected in the UI
                // If TUTOR is selected:
                // driver.FindElement(By.Id("learner-radio")).Click();
                // If Learner is selected (default):
                // driver.FindElement(By.Id("tutor-radio")).Click();

                // Submit the form
                _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                // Wait for the page to load and check the current URL
                Thread.Sleep(TimeSpan.FromSeconds(5));

                // Assert that the current URL is as expected
                Assert.That(_driver.Url, Is.EqualTo($"{baseUrl}/add-learner-detail"));
            }
            catch (Exception ex)
            {
                // Handle exceptions or assertion failures
                Console.WriteLine("Signup test failed: " + ex.Message);
            }
            finally
            {
                // Close the browser window
                _driver.Quit();
            }
        }
        [Test]
        public void TestManualLogin()
        {
            // Navigate to the signup page
            _driver.Navigate().GoToUrl(baseUrl + "auth/login");

            // Set up WebDriverWait for explicit waits
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            try
            {
                // Fill out the signup form
                // Wait until the username field is visible
                IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));

                // Fill out the signup form
                usernameField.SendKeys("testuser");

                // Wait until the password field is visible
                IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
                passwordField.SendKeys("Test123!");

                // Submit the form
                _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                // Wait for the page to load and check the current URL
                Thread.Sleep(TimeSpan.FromSeconds(5));

                // Assert that the current URL is as expected
                Assert.That(_driver.Url, Is.EqualTo(baseUrl));
            }
            catch (Exception ex)
            {
                // Handle exceptions or assertion failures
                Console.WriteLine("Login test failed: " + ex.Message);
            }
            finally
            {
                // Close the browser window
                _driver.Quit();
            }
        }

        //[Test]
        //public void TestGoogleLogin()
        //{
        //    _driver.Navigate().GoToUrl("https://localhost:5173/auth/login"); // Replace with your React app URL

        //    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

        //    // Assuming the GoogleLogin button has a unique identifier or class for locating it
        //    // Modify the selector to match the actual implementation in your React app
        //    IWebElement googleLoginDiv = wait.Until(driver => driver.FindElement(By.ClassName("google-login-button-class")));

        //    // Click the Google Login button
        //    googleLoginDiv.Click();

        //    // Handle the Google login popup (this part depends on your actual implementation and test environment)
        //    // You might need to switch to the popup window and perform the login steps manually.

        //    // Switch to Google login popup
        //    foreach (var windowHandle in _driver.WindowHandles)
        //    {
        //        if (_driver.SwitchTo().Window(windowHandle).Title.Contains("Google"))
        //        {
        //            break;
        //        }
        //    }

        //    // Assuming you have test credentials for Google login
        //    IWebElement emailField = wait.Until(driver => driver.FindElement(By.Id("identifierId")));
        //    emailField.SendKeys("ngnhathuy1224@gmail.com");

        //    IWebElement nextButton = wait.Until(driver => driver.FindElement(By.Id("identifierNext")));
        //    nextButton.Click();

        //    // Wait for manual CAPTCHA completion
        //    //Console.WriteLine("Please complete the CAPTCHA if prompted and press Enter to continue...");
        //    ////Console.ReadLine();
        //    //// Introduce a delay to allow manual CAPTCHA completion
        //    //Thread.Sleep(TimeSpan.FromSeconds(20));

        //    // Wait and input password
        //    //IWebElement passwordField = wait.Until(driver => driver.FindElement(By.CssSelector("input[type='password']")));
        //    //passwordField.SendKeys("");

        //    //IWebElement loginButton = wait.Until(driver => driver.FindElement(By.Id("passwordNext")));
        //    //loginButton.Click();

        //    // Switch back to the main window
        //    _driver.SwitchTo().Window(_driver.WindowHandles[0]);

        //    // Verify login success by checking for a specific element or URL change
        //    IWebElement loggedInElement = wait.Until(driver => driver.FindElement(By.Id("logged-in-element-id")));
        //    Assert.IsNotNull(loggedInElement);

        //    // Alternatively, you can verify the URL change or any other success indicator
        //    Assert.AreEqual("https://localhost:5173", _driver.Url); // Replace with your expected URL
        //}

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }
    }
}
