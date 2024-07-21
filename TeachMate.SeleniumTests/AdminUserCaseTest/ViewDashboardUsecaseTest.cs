using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TeachMate.SeleniumTests.AuthUsecases;

public class ViewDashboardUsecaseTest
{
    private string baseUrl = "https://localhost:5173/";
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
        
        ManualLogin();
    }
    private void ManualLogin()
    {
        _driver.Navigate().GoToUrl(baseUrl + "auth/login");

        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        try
        {
            IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
            usernameField.SendKeys("admin");

            IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            passwordField.SendKeys("123456");

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Assert.That(_driver.Url, Is.EqualTo(baseUrl+"admin"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Login test failed: " + ex.Message);
        }
    }

    [Test]
    public void TestUpdateStatus()
    {
        /*_driver.Navigate().GoToUrl(baseUrl + "admin");*/

        //y chang nhau
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        Thread.Sleep(TimeSpan.FromSeconds(5));

        //khac
        try
        {
            //_driver.FindElement(By.Id("report-system")).Click();
            
            IWebElement totalTutorDiv = _driver.FindElement(By.Id("TotalTutor"));

            // Assert that the div exists
            Assert.That(totalTutorDiv, Is.Not.Null);
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Fail("The element with class 'font-bold' and text 'Total Tutor' was not found within the time limit.");
        }
        catch (Exception ex)
        {
            Assert.Fail("An unexpected error occurred: " + ex.Message);
        }
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}
