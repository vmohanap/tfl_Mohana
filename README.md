Basic Specflow Framework using Nunit for automating Plan a Journey Widget

Uses:
SpecFlow (BDD)
Selenium (WebDriver)
NUnit 7.x
Extent reports 4.1.0 (for reporting)
utilises Page Object Model pattern
takes screenshots on failure of web tests

Reporting:

Reports after test execution can be viewed in index.html 

Notes:

The Hooks class contains code which runs before and after scenarios (and can be expanded to use other annotations).The scenarios are tagged with "POC" to ensure that webdriver instances are only created for UI tests.  Use the tag @POC when creating scenarios
	
+ The project contains code to insert screenshots and page source html on failure and is taken from here: http://stackoverflow.com/questions/18512918/insert-screenshots-in-specrun-specflow-test-execution-reports
   (Note - these are not links - they are the path and filename... might need some tweaking of the standard specrun report template?)

+ NUnit is currently used as the test runner. 






