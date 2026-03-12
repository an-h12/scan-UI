Feature: Open Source - Samsung Cloud
		As a user of the SC app,
		I want to have an onboarding page for the app 
		So that I can have an open source button in settings page.

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And "Settings" button is oppened
	And "Open source licenses" button is oppened

@NCGP-T2313
Scenario: Validate that tapping the "Open Source Licenses" button displays the Open Source Licenses screen
	Then the Open Source License screen is shown

@NCGP-T2314
Scenario: Verify that the "Back" button redirects the user to the settings screen or the previous screen
	And the Back button is clicked
	Then the user is redirected to the settings screen