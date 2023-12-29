using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Task70
{
    public class LoginPage : BasePage
    {
        By usernameLocator = By.XPath("//input[@id='identifierId']");
        By passwordLocator = By.XPath("//input[@name='Passwd']");
        By nextButtonLocator = By.XPath("//span[contains(text(), 'Next')]");

        public LoginPage(IWebDriver driver)
        {
            Driver = driver;
            if (!Driver.Title.Equals("Sign in - Google Accounts"))
            {
                throw new InvalidElementStateException("This is not the login page");
            }
        }

        public LoginPage TypeUsername(string username)
        {
            Driver.FindElement(usernameLocator).SendKeys(username);
            TakeScreenshot(usernameLocator);
            return this;
        }
        
        public LoginPage TypePassword(String password)
        {
            Driver.FindElement(passwordLocator).SendKeys(password);
            return this;
        }
        
        public HomePage Login()
        {
            if (IsNextButtonDisplayed())
            {
                Driver.FindElement(nextButtonLocator).Click();
            }
            
            return new HomePage(Driver);
        }

        public LoginPage ClickNextButton()
        {
            if (IsNextButtonDisplayed())
            {
                Driver.FindElement(nextButtonLocator).Click();
            }
            return new LoginPage(Driver);
        }

        private bool IsNextButtonDisplayed()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(7));

            var isNextButtonDisplayed = wait.Until(condition =>
            {
                try
                {
                    var nextButton = Driver.FindElement(nextButtonLocator);
                    return nextButton.Displayed;
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

            return isNextButtonDisplayed;
        }

        public HomePage LoginAs(string username, string password)
        {
            TypeUsername(username);
            ClickNextButton();
            TypePassword(password);

            return Login();
        }

        private void TakeScreenshot(By elementLocator)
        {
            var element = Driver.FindElement(elementLocator);
            var usernameScreenshot = (element as ITakesScreenshot).GetScreenshot();
            usernameScreenshot.SaveAsFile("screenshotOfLoginPageElement.png");
        }
    }
}
