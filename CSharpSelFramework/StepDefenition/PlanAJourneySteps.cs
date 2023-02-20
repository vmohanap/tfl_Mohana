using AventStack.ExtentReports.Gherkin.Model;
using CSharpSelFramework.Pages;
using CSharpSelFramework.utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCSharpNetCore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;


namespace SeleniumCSharpNetCore.Steps
{

    [Binding]
    public class PlanAJourneySteps
    {

        private DriverHelper _driverHelper;
        PlanMyJourney planMyJourney;
        JourneyResults jonrneyResults;
        BasePage basePage;

        public PlanAJourneySteps(DriverHelper driverHelper)
        {
            _driverHelper = driverHelper;
            planMyJourney = new PlanMyJourney(_driverHelper.Driver);
            jonrneyResults = new JourneyResults(_driverHelper.Driver);
            basePage = new BasePage(_driverHelper.Driver);
        }

        //[Parallelizable(ParallelScope.All)]
        [Given(@"I navigate to the tfl plan a journey page")]
        public void iNavigateToTheTflPlanAJourneyPage()
        {
            _driverHelper.Driver.Url = "https://tfl.gov.uk/plan-a-journey/";
            basePage.WaitAndClickOnElement("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");

        }

        [When(@"I navigate back to plan a journey page")]
        public void WhenINavigateBackToPlanAJourneyPage()
        {
            jonrneyResults.CLickPlanAJourneyLink();
        }

        [When(@"I enter From location as ""(.*)""")]
        public void WhenIEnterFromLocationAs(string fromLocation)
        {
            planMyJourney.EnterFromLocation(fromLocation);
        }

        [When(@"I enter To location as ""(.*)""")]
        public void WhenIEnterToLocationAs(string toLocation)
        {
            planMyJourney.EnterToLocation(toLocation);
        }

        [When(@"I enter invalid From location as ""(.*)"" and To location as ""(.*)""")]
        public void WhenIEnterInvalidFromLocationAsAndToLocationAs(string from, string to)
        {
            planMyJourney.EnterFromAndToLocation(from, to);
        }

        [When(@"I enter From location as ""(.*)"" and select the value using ""(.*)"" dropdown")]
        public void WhenIEnterFromLocationAsAndSelectTheValueUsingDropdown(string from, string dataId)
        {
            planMyJourney.EnterFromValueUsingLocationCode(from, dataId);
        }

        [When(@"I enter To location as ""(.*)"" and select the value usig ""(.*)"" dropdown")]
        public void WhenIEnterToLocationAsAndSelectTheValueUsigDropdown(string to, string dataId)
        {
            planMyJourney.EnterToValueUsingLocationCode(to, dataId);
        }

        [When(@"I select the values from recent search")]
        public void WhenISelectTheValuesFromRecentSearch()
        {
            planMyJourney.SelectValueFromRecentSearch();
        }

        [When(@"I click on Plan my Jouney button")]
        public void WhenIClickOnButton()
        {
            planMyJourney.ClickPlanAJourneyButton();
        }


        [When(@"I directly click on Plan my Jouney button with no locations entered")]
        public void WhenIDirectlyClickOnPlanMyJourneyButtonWithNoLocationsEntered()
        {
            planMyJourney.ClickPlanAJourneyButton();
        }

        [When(@"I click on Edit Journey hyperlink")]
        public void WhenIClickOnEditJourneyHyperlink()
        {
            jonrneyResults.ClickEditJourney();
        }

        [When(@"I modify the To location as ""(.*)""")]
        public void WhenIModifyTheToLocationAs(string toLocation)
        {
            jonrneyResults.EditToLocation(toLocation);
        }

        [When(@"I click on Update Journey button")]
        public void WhenIClickOnUpdateJourneyButton()
        {
            jonrneyResults.ClickUpdateJourney();

        }

        [Then(@"I should see the Journey results as below")]
        public void ThenIShouldBeAbleToSeeTheJourneyResultsAsBelow(Table table)
        {
            try
            {
                var dictionary = TableExtensions.ToDictionary(table);
                StringAssert.Contains(jonrneyResults.fromLocationText, dictionary["From"]);
                StringAssert.Contains(jonrneyResults.toLocationText, dictionary["To"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in validating from the data table values: " + ex);
            }
        }

        [Then(@"I should see the following default options in cycling section")]
        public void ThenIShouldSeeTheFollowingOptionsInCyclingSection(Table table)
        {
            basePage.WaitUntilVisibleByXpath("//h2[text()='Cycling and other options']");
            basePage.ScrollToElemet(jonrneyResults.cyclingAndOtherOPtions);

            try
            {
                table.Rows.ToList().ForEach(row => Console.WriteLine(row));

                IList<IWebElement> list = _driverHelper.Driver.FindElements(By.XPath("//*[@data-tracking='accordion_expansion_details']/div"));
                foreach (IWebElement e in list)
                {
                    e.Text.Contains(table.Rows.ToList().ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in iterating the list for Cycling: " + ex);
            }
        }

        [Then(@"I should see the default section public transport and bus only")]
        public void IShouldSeePublicTransportBusAndCyclingSectionDisplayed()
        {
            basePage.WaitUntilVisibleByXpath("//h2[text()='Cycling and other options']");

            basePage.ScrollToElemet(jonrneyResults.cyclingAndOtherOPtions);
            Assert.True(jonrneyResults.cyclingAndOtherOPtions.Displayed);

            basePage.ScrollToElemet(jonrneyResults.fastestBypublicTransport);
            Assert.True(jonrneyResults.fastestBypublicTransport.Displayed);

            basePage.ScrollToElemet(jonrneyResults.busOnly);
            Assert.True(jonrneyResults.busOnly.Displayed);
        }

        [Then(@"I should see the default section public transport")]
        public void ThenIShouldSeeTheDefaultSectionPublicTransport()
        {
            basePage.WaitUntilVisibleByXpath("//h2[text()='Fastest by public transport']");

            basePage.ScrollToElemet(jonrneyResults.fastestBypublicTransport);
            Assert.True(jonrneyResults.fastestBypublicTransport.Displayed);
        }

        [Then(@"I should see the error ""(.*)""")]
        public void ThenIShouldSeeTheError(string p0)
        {
            basePage.WaitUntilVisibleByXpath("//li[@class='field-validation-error']");
            StringAssert.Contains(jonrneyResults.validationError, p0);
        }

        [Then(@"I should see the below error in")]
        public void ThenIShouldSeeTheBelowErrorIn(Table table)
        {
            var dictionary = TableExtensions.ToDictionary(table);
            StringAssert.Contains(planMyJourney.inputFormError, dictionary["From"]);
            StringAssert.Contains(planMyJourney.inputToError, dictionary["To"]);

        }

        [Then(@"I should see the Recent tab updated as '(.*)'")]
        public void ThenIShouldSeeTheRecentTabUpdatedAs(string p0)
        {
            StringAssert.Contains(planMyJourney.recentTab, p0);
        }

    }
}
