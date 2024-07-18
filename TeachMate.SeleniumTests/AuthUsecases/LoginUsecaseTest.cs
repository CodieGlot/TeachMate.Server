using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TeachMate.SeleniumTests.AuthUsecases;

public class LoginUsecaseTest
{
    private IWebDriver _driver;

    private string baseUrl = "https://localhost:5173/";
    private (string username, string password) _validInput = ("testuser", "Test123!");
    private (string username, string password) _invalidInput = ("test", "test");

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
    public void TestLoginValidInput()
    {
        _driver.Navigate().GoToUrl(baseUrl + "auth/login");

        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        try
        {
            IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));

            usernameField.SendKeys(_validInput.username);

            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            passwordField.SendKeys(_validInput.password);

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(driver => driver.Url == baseUrl);

            Assert.That(_driver.Url, Is.EqualTo(baseUrl));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Login test failed: " + ex.Message);
            Assert.Fail("Login test failed: " + ex.Message);
        }
        finally
        {
            _driver.Quit();
        }
    }
    [Test]
    public void TestLoginInvalidInput()
    {
        _driver.Navigate().GoToUrl(baseUrl + "auth/login");

        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        try
        {
            IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));

            usernameField.SendKeys(_invalidInput.username);

            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            passwordField.SendKeys(_invalidInput.password);

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            IWebElement usernameError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username-error")));
            IWebElement passwordError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password-error")));

            Assert.That(usernameError.Text, Is.EqualTo("Username must be at least 5 characters"));
            Assert.That(passwordError.Text, Is.EqualTo("Password must be at least 6 characters"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Login test failed: " + ex.Message);
            Assert.Fail("Login test failed: " + ex.Message);
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
