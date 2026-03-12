@NCGP-5551
Feature: Samsung Cloud Settings
	As a PC Cloud SDK integrated App user, 
	I want to see the options for the Samsung Cloud Dashboard 
	so that I can change my Samsung Cloud settings .

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And "Settings" button is oppened

Scenario: Verify if is possible go to settings and return to the main screen
	Then return to the "Samsung Cloud" main screen

@NCGP-T764
Scenario: Verify that the Samsung Cloud version is shown on the settings screen
	Then Samsung Cloud version should be displayed in main screen

Scenario: Verify that the Open Source button is shown on the settings screen
	Then the Open Source button is shown on the settings screen

@NCGP-T759
Scenario: Verify that the Samsung Cloud user email is shown on the settings screen
	Then the user email address should be displayed on the settings screen

@NCGP-T2340
Scenario: Verify that there are buttons on the settings pages
	Then the "<buttonName>" button is displayed

	Examples:
		| buttonName           |
		| Sign out             |
		| Contact us           |
		| Open source licenses |