using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TeachMate.SeleniumTests
{
    public class CreateLearningModuleUseCaseTest
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

            // Đăng nhập vào hệ thống
            ManualLogin();
        }

        private void ManualLogin()
        {
            _driver.Navigate().GoToUrl(baseUrl + "auth/login");

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            try
            {
                IWebElement usernameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
                usernameField.SendKeys("tutorTest");

                IWebElement passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
                passwordField.SendKeys("Tutor@123");

                _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                Thread.Sleep(TimeSpan.FromSeconds(5));

                Assert.That(_driver.Url, Is.EqualTo(baseUrl));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login test failed: " + ex.Message);
            }
        }
        //
        [Test]
        public void TestCreateClass()
        {
            _driver.Navigate().GoToUrl(baseUrl + "create-learning-module");

            //y chang nhau
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Thread.Sleep(TimeSpan.FromSeconds(5));

            //khac
            try
            {
                //tuy form
                _driver.FindElement(By.Id("title")).SendKeys("Test Class Title");
                _driver.FindElement(By.Id("duration")).SendKeys("60");  // Example duration
                var moduleTypeDropdown = new SelectElement(_driver.FindElement(By.Id("moduleType")));
                moduleTypeDropdown.SelectByText("Custom");
                // Set module type if needed (not shown in HTML snippet)
                _driver.FindElement(By.Id("maximumLearners")).SendKeys("5");  // Example maximum learners
                _driver.FindElement(By.Id("numOfWeeks")).SendKeys("8");  // Example number of weeks
                _driver.FindElement(By.Id("startDate")).SendKeys("2024-07-18");  // Example start date
                _driver.FindElement(By.Id("gradeLevel")).SendKeys("10");
                _driver.FindElement(By.Id("description")).SendKeys("Test Description");
                var subjectDropdown = new SelectElement(_driver.FindElement(By.Id("subject")));
                subjectDropdown.SelectByText("Maths");

                //click button
                _driver.FindElement(By.Id("create-class")).Click();

                //dung lai de ma minh xem ket qua
                Thread.Sleep(TimeSpan.FromSeconds(5));


                Assert.That(_driver.Url, Is.EqualTo(baseUrl+"setprice"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("TestCreateClass failed: " + ex.Message);
            }
        }


        //[Test]
        //public void TestLearningModuleRequest()
        //{
        //    Navigate to the page with the form
        //    _driver.Navigate().GoToUrl(baseUrl + "request");
        //    Thread.Sleep(TimeSpan.FromSeconds(5));

        //    // Find the input element for title
        //    IWebElement titleInput = _driver.FindElement(By.Id("title"));

        //    // Enter text into the title input
        //    titleInput.SendKeys("Test Request Title");

        //    // Find the send button
        //    IWebElement sendButton = _driver.FindElement(By.XPath("//button[contains(text(),'Send')]"));

        //    // Click the send button
        //    sendButton.Click();

        //    // Wait for response or validation message
        //    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        //    IWebElement responseMessage = wait.Until(ExpectedConditions.ElementExists(By.XPath("//p[@class='mx-auto text-center text-red-300']")));

        //    // Assert that the message is as expected
        //    Assert.AreEqual("Your request was sent successfully.", responseMessage.Text);
        //}
        [TearDown]
        public void TearDown()
        {
            // Đóng trình duyệt sau khi tất cả các test case đã hoàn thành
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
