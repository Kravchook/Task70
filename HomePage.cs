using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Task70
{
    public class HomePage : BasePage
    {
        private const string WelcomeText = "Welcome, Automation Tester";
        private By messageBy = By.XPath($"//*[contains(text(), '{WelcomeText}')]");

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
            TakeScreenshot(messageBy);
            
            if (!Driver.Title.Equals("Google Account"))
            {
                throw new InvalidElementStateException("This is not Home Page of logged in user," +
                                                       " current page is: " + Driver.Title);
            }
        }

        public bool IsWelcomeLabelDisplayed()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(7))
            {
                PollingInterval = TimeSpan.FromMilliseconds(1000)
            };

            var isWelcomeLabelDisplayed = wait.Until(condition =>
            {
                try
                {
                    var welcomeLabel = Driver.FindElement(messageBy);
                    return welcomeLabel.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            return isWelcomeLabelDisplayed;
        }

        private void TakeScreenshot(By elementLocator)
        {
            var element = Driver.FindElement(elementLocator);
            var usernameScreenshot = (element as ITakesScreenshot).GetScreenshot();
            usernameScreenshot.SaveAsFile("screenshotOfHomePageElement.png");
        }
    }
}