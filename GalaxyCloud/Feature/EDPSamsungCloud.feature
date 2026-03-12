@NCGP-5374
Feature: EDP - Samsung Cloud
		As a user of the SC app,
		I want to have an onboarding page for the app 
		So that I can have some login information and the app version.


Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed


@NCGP-T1359, @NCGP-T1360
Scenario Outline: Verify string according to encryption status
	When the "<appName>" app settings is accessed
	Then the encryption status is checked
	And the string in the submenu is displayed depending on the status

	Examples:
		| appName         |
		| Bluetooth       |
		| Wi-Fi           |

@NCGP-T1355	
Scenario Outline: Verify that only Bluetooth and WiFi supports EDP
	When the "<appName>" app settings is accessed
	Then the EDP information is available as "Encrypt synced data" 

	Examples:
		| appName         |
		| Bluetooth       |
		| Wi-Fi           |
