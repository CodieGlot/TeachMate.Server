using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TeachMate.SeleniumTests.AuthUsecases;
internal class RegisterUsecaseTest
{
    private IWebDriver _driver;

    private string baseUrl = "https://localhost:5173/";
    private (string username, string password, string confirmPassword, string email) _validInput = ("testuser", "Test123!", "Test123!", "testemail@gmail.com");
    private (string username, string password, string confirmPassword, string email) _invalidInput = ("test", "test", "Test123!", "testemail");

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
    public void TestSignupValidInput()
    {
        _driver.Navigate().GoToUrl(baseUrl + "auth/signup");

        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        try
        {
            IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
            usernameField.SendKeys(_validInput.username);

            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            passwordField.SendKeys(_validInput.password);

            IWebElement confirmPasswordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("confirmPassword")));
            confirmPasswordField.SendKeys(_validInput.confirmPassword);

            IWebElement emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            emailField.SendKeys(_validInput.email);

            // Select the user role (optional)
            // Assuming Learner is default selected in the UI
            // If TUTOR is selected:
            // driver.FindElement(By.Id("learner-radio")).Click();
            // If Learner is selected (default):
            // driver.FindElement(By.Id("tutor-radio")).Click();

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(driver => driver.Url == $"{baseUrl}add-learner-detail");

            Assert.That(_driver.Url, Is.EqualTo($"{baseUrl}add-learner-detail"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Signup test failed: " + ex.Message);
            Assert.Fail("Signup test failed: " + ex.Message);
        }
        finally
        {
            _driver.Quit();
        }
    }
    [Test]
    public void TestSignupInvalidInput()
    {
        _driver.Navigate().GoToUrl(baseUrl + "auth/signup");

        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        try
        {
            IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
            usernameField.SendKeys(_invalidInput.username);

            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            passwordField.SendKeys(_invalidInput.password);

            IWebElement confirmPasswordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("confirmPassword")));
            confirmPasswordField.SendKeys(_invalidInput.confirmPassword);

            IWebElement emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            emailField.SendKeys(_invalidInput.email);

            // Select the user role (optional)
            // Assuming Learner is default selected in the UI
            // If TUTOR is selected:
            // driver.FindElement(By.Id("learner-radio")).Click();
            // If Learner is selected (default):
            // driver.FindElement(By.Id("tutor-radio")).Click();

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Wait for the error messages to be visible
            IWebElement usernameError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username-error")));
            IWebElement passwordError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password-error")));
            IWebElement confirmPasswordError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("confirmPassword-error")));
            IWebElement emailError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email-error")));

            // Verify the error messages
            Assert.That(usernameError.Text, Is.EqualTo("Username must be at least 5 characters"));
            Assert.That(passwordError.Text, Is.EqualTo("Password must be at least 6 characters"));
            Assert.That(confirmPasswordError.Text, Is.EqualTo("Passwords must match"));
            Assert.That(emailError.Text, Is.EqualTo("Invalid email format"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Signup test failed: " + ex.Message);
            Assert.Fail("Signup test failed: " + ex.Message);
        }
        finally
        {
            _driver.Quit();
        }
    }
    [TearDown]
    public void TearDown()
    {
        _driver?.Dispose();
    }
}
