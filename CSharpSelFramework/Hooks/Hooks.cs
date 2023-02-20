using System;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System.IO;
using OpenQA.Selenium.Chrome;
using SeleniumCSharpNetCore;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium;
using NUnit.Framework;
using NUnit.Framework.Interfaces;


namespace DemoPro.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string ReportPath;
        IWebDriver driver;

        private static DriverHelper _driverHelper;
        public Hooks(DriverHelper driverHelper) => _driverHelper = driverHelper;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Mohana");
            extent.AttachReporter(htmlReporter);
        }
        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
            Console.WriteLine("BeforeFeature");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            try
            {
                Console.WriteLine("BeforeScenario");
                scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in fetching scenario: " + ex);
            }
            try
            {
                ChromeOptions option = new ChromeOptions();
                option.AddArguments("start-maximized");
                option.AddArguments("--disable-gpu");
                //option.AddArguments("--headless");

                new DriverManager().SetUpDriver(new ChromeConfig());
                Console.WriteLine("Setup");
                _driverHelper.Driver = new ChromeDriver(option);
                _driverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in launching driver: " + ex);
            }
        }
        [AfterStep]
        public void InsertReportingSteps()
        {
            try
            {
                var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                if (ScenarioContext.Current.TestError == null)
                {
                    if (stepType == "Given")
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "When")
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "Then")
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                    else if (stepType == "And")
                        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (ScenarioContext.Current.TestError != null)
                {
                    if (stepType == "Given")
                    {
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    }
                    else if (stepType == "When")
                    {
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    }
                    else if (stepType == "Then")
                    {
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    }
                    else if (stepType == "And")
                    {
                        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Inserting test reports: " + ex);
            }
        }
        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;

                DateTime time = DateTime.Now;
                String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

                if (status == TestStatus.Failed)
                {

                    _ = scenario.Fail("Test failed", captureScreenShot(driver, fileName));
                    scenario.Log(Status.Fail, "test failed with logtrace" + stackTrace);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in capturing the test status" + ex);
            }
            finally
            {
                _driverHelper.Driver.Quit();
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }

        public static MediaEntityModelProvider captureScreenShot(IWebDriver driver, String screenShotName)

        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();

        }
    }
}