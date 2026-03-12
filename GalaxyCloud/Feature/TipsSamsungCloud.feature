Feature: Tips - Samsung Cloud
		As a user of the SC app,
		I want to have an onboarding page for the app 
		So that I can have an tips button.

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And the Tips page is accessed

@NCGP-T2328
Scenario: Validate if the back button correctly redirects the user to the main screen
	When return to the previous page
	Then the Dashboard page should be displayed

@NCGP-T2326
Scenario: Validate if the next button is not displayed on the last tip page
	Then the next button is not displayed on the last tip page

@NCGP-T2327
Scenario: Validate if the previous button is not displayed on the first tip page
	Then the previous button is not displayed on the first tip page

@NCGP-T2328
Scenario: Verify if the current page and total pages are displayed correctly when navigating
	Then the current page and total pages are displayed

@NCGP-2324 @NCGP-2325
Scenario: Validate the title and explanatory text on the tips page
	Then the title and explanatory text are according to the UI