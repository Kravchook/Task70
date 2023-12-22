using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task70
{
    public class Tests
    {
        const string Email1 = "testerautomation601@gmail.com";
        const string Password1 = "myINVULNERABLEpass";

        public WebDriver Driver { get; set; }

        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            //Add implicit waiter for WebDriver
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            Driver.Navigate().GoToUrl("https://accounts.google.com/");
        }

        [Test]
        public void PageObjectLoginTest()
        {
            LoginPage loginPage = new LoginPage(Driver);
            HomePage homePage = loginPage.LoginAs(Email1, Password1);

            Assert.That(homePage.IsWelcomeLabelDisplayed());

            Screenshot screenshot = (Driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile("screenshot.png");
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Dispose();
        }
    }
}