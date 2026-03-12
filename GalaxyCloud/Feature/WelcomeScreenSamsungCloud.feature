@NCGP-2420
Feature: Welcome Screen - Samsung Cloud
		As a user of the SC app,
		I want to have an onboarding page for the app 
		So that I can have some login information and the app version.


Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed


@NCGP-T1394
Scenario: Verify that the Settings button is displayed on the main screen
	Then "Settings" button is displayed on the main screen

@NCGP-T1392
Scenario: Verify that the Samsung Cloud title is displayed on the main screen
	Then the title "Samsung Cloud" is displayed

@NCGP-T2321
Scenario: Validate if the Tips icon is available on the main page of Samsung Cloud desktop app
	Then the Tips icon is available on the main page