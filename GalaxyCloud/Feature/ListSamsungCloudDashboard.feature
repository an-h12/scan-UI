@NCGP-249
Feature: List Samsung Cloud Dashboard
	As a PC Cloud SDK integrated App user, 
	I want to see the options for the Samsung Cloud Dashboard 
	so that I can change my Samsung Cloud settings .

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed

@NCGP-T90 @NCGP-175
Scenario: Verify that the account linking status is displayed on the sub-text on Gallery
	Then Samsung Gallery should display the account link status in the subtext below the app name

@NCGP-T349
Scenario Outline: Verify if the app name is displayed
	Then "<appTitleName>" name should be displayed correctly on the "<ContainedAppName>" application

	Examples:
		| appTitleName    | ContainedAppName |
		| Bluetooth       | Bluetooth        |
		| Samsung Gallery | Samsung Gallery  |
		| Samsung Notes   | Samsung Notes    |
		| Samsung Pass    | Samsung Pass     |
		| Wi-Fi           | Wi-Fi            |

@NCGP-T350
Scenario Outline: Verify app icon is displayed in Cloud tab
	Then the '<appName>' icon is displayed

	Examples:
		| appName         |
		| Bluetooth       |
		| Samsung Gallery |
		| Samsung Notes   |
		| Samsung Pass    |
		| Wi-Fi           |

@NCGP-T409
Scenario Outline: Verify that the buttons is displayed on the dashboard page
	Then the toggle buttons should be displayed on the '<appName>'

	Examples:
		| appName         |
		| Bluetooth       |
		| Samsung Notes   |
		| Wi-Fi           |