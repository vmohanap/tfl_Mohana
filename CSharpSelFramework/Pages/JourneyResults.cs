using OpenQA.Selenium;
using System.Collections.Generic;

namespace SeleniumCSharpNetCore.Pages
{
    public class JourneyResults
    {
        private IWebDriver Driver;

        public JourneyResults(IWebDriver driver)
        {
            Driver = driver;
        }


        IWebElement planAJourneyLink => Driver.FindElement(By.XPath("(//a[text()='Plan a journey'])[3]"));

        #region "Journey Result section"
        public IWebElement busOnly => Driver.FindElement(By.XPath("//h2[text()='Bus only']"));
        public IWebElement fastestBypublicTransport => Driver.FindElement(By.XPath("//h2[text()='Fastest by public transport']"));
        public IWebElement cyclingAndOtherOPtions => Driver.FindElement(By.XPath("//h2[text()='Cycling and other options']"));
        #endregion

        #region "Edit Journey"
        IWebElement editJourney => Driver.FindElement(By.CssSelector("a.edit-journey"));
        IWebElement editToLocation => Driver.FindElement(By.CssSelector("input.jpTo"));
        IWebElement clearToLocation => Driver.FindElement(By.LinkText("Clear To location"));
        IWebElement updateJourney => Driver.FindElement(By.CssSelector("input.plan-journey-button"));

        #endregion

        #region "Journey Results Text"
        public string fromLocationText => Driver.FindElement(By.XPath("(//span[@class='notranslate']/strong)[1]")).Text;
        public string toLocationText => Driver.FindElement(By.XPath("(//span[@class='notranslate']/strong)[2]")).Text;
        IReadOnlyCollection<IWebElement> journeyResults => Driver.FindElements(By.CssSelector("div.journey-result-summary strong"));
        public string validationError => Driver.FindElement(By.XPath("//li[@class='field-validation-error']")).Text;
        #endregion
        public void ClickEditJourney() => editJourney.Click();
        public void ClickUpdateJourney() => updateJourney.Click();
        public void CLickPlanAJourneyLink() => planAJourneyLink.Click();

        public void EditToLocation(string toLocation)
        {
            clearToLocation.Click();
            editToLocation.SendKeys(toLocation);
        }


    }
}
