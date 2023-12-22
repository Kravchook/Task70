using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Task70
{
    public class LoginPage
    {
        private readonly WebDriver driver;
        By usernameLocator = By.XPath("//input[@id='identifierId']");
        By passwordLocator = By.XPath("//input[@name='Passwd']");
        By nextButtonLocator = By.XPath("//span[contains(text(), 'Next')]");

        public LoginPage(WebDriver driver)
        {
            this.driver = driver;
            
            if (!driver.Title.Equals("Sign in - Google Accounts"))
            {
                throw new InvalidElementStateException("This is not the login page");
            }
        }

        public LoginPage TypeUsername(string username)
        {
            driver.FindElement(usernameLocator).SendKeys(username);
            TakeScreenshot(usernameLocator);
            return this;
        }
        
        public LoginPage TypePassword(String password)
        {
            driver.FindElement(passwordLocator).SendKeys(password);
            return this;
        }
        
        public HomePage Login()
        {
            if (IsNextButtonDisplayed())
            {
                driver.FindElement(nextButtonLocator).Click();
            }
            
            return new HomePage(driver);
        }

        public LoginPage ClickNextButton()
        {
            if (IsNextButtonDisplayed())
            {
                driver.FindElement(nextButtonLocator).Click();
            }
            return new LoginPage(driver);
        }

        private bool IsNextButtonDisplayed()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));

            var isNextButtonDisplayed = wait.Until(condition =>
            {
                try
                {
                    var nextButton = driver.FindElement(nextButtonLocator);
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
            var element = driver.FindElement(elementLocator);
            var usernameScreenshot = (element as ITakesScreenshot).GetScreenshot();
            usernameScreenshot.SaveAsFile("screenshotOfLoginPageElement.png");
        }
    }
}
