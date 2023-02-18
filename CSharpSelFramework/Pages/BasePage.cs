using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharpNetCore;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System;

namespace CSharpSelFramework.Pages
{
    public class BasePage
    {
        private IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
        public void EnterText(IWebElement webElement, string value) => webElement.SendKeys(value);

        public bool IsDisplayed(IWebElement webElement) => webElement.Displayed;

        public void Click(IWebElement webElement) => webElement.Click();

        public void SelectByValue(IWebElement webElement, string value)
        {
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByValue(value);
        }

        public void SelectByText(IWebElement webElement, string text)
        {
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByText(text);
        }

        public void ScrollToElemet(IWebElement webElement)
        {
            var element = webElement;
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void WaitUntilVisibleByXpath(string xpathValue)

        {

            var wait = new WebDriverWait(Driver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathValue)));
        }

        public void WaitAndClickOnElement(string id)
        {

            var wait = new WebDriverWait(Driver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(id)));
            clickableElement.Click();
        }

        public void WaitUntilPresentById(string xpathValue)
        {

            var wait = new WebDriverWait(Driver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementExists(By.Id(xpathValue)));
        }

    }
}
