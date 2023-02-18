using CSharpSelFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumCSharpNetCore.Pages
{
    class PlanMyJourney
    {
        private IWebDriver Driver;
        BasePage basePage;

        public PlanMyJourney(IWebDriver driver)
        {
            Driver = driver;
        }

        #region "From, To Locations"
        public IWebElement fromLocation => Driver.FindElement(By.Id("InputFrom"));
        public IWebElement toLocation => Driver.FindElement(By.Id("InputTo"));

        IWebElement dropDown => Driver.FindElement(By.XPath("//span[@class='source-header']"));
        #endregion
        IWebElement btnplanAJourney => Driver.FindElement(By.Id("plan-journey-button"));

        public string inputFormError => Driver.FindElement(By.XPath("//span[@id='InputFrom-error']")).Text;
        public string inputToError => Driver.FindElement(By.XPath("//span[@id='InputTo-error']")).Text;

        public string recentTab => Driver.FindElement(By.XPath("//a[@data-journey-type='recent']")).Text;

        public IWebElement suggestion => Driver.FindElement(By.XPath("//span[@id= 'stop-points-search-suggestion-0']"));
        public void ClickPlanAJourneyButton() => btnplanAJourney.Click();

        public void EnterFromAndToLocation(string from, string to)
        {
            fromLocation.SendKeys(from);
            toLocation.SendKeys(to);

        }
        public void EnterFromLocation(string from)
        {
            foreach (char fromText in from)
            {
                fromLocation.SendKeys(fromText.ToString());
            }
            Assert.True(dropDown.Displayed);
            fromLocation.SendKeys(Keys.ArrowDown);
            fromLocation.SendKeys(Keys.Tab);

        }
        public void EnterToLocation(string to)
        {
            foreach (char toText in to)
            {
                toLocation.SendKeys(toText.ToString());
            }
            Assert.True(dropDown.Displayed);
            toLocation.SendKeys(Keys.ArrowDown);
            toLocation.SendKeys(Keys.Tab);

        }

        public void SelectValueFromRecentSearch()
        {
            fromLocation.Click();
            Driver.FindElement(By.XPath("//span[@data-id='stops-recent-magic-searches-suggestion-1']")).Click();
            toLocation.Click();
            Driver.FindElement(By.XPath("//span[@data-id='stops-recent-magic-searches-suggestion-2']")).Click();

        }
        public void EnterFromValueUsingLocationCode(string from, string dataId)
        {
            fromLocation.SendKeys(from);
            Driver.FindElement(By.XPath("//span[@data-id='" + dataId + "']")).Click();
        }

        public void EnterToValueUsingLocationCode(string to, string dataId)
        {
            toLocation.SendKeys(to);
            Driver.FindElement(By.XPath("//span[@data-id='" + dataId + "']/strong")).Click();
        }

    }
}
