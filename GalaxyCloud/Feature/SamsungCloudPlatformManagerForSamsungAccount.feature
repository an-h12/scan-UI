@NCGP-1223
Feature: Samsung Cloud Platform Manager for Samsung Account
As a Samsung Account user, 
I want to know when the SCPM is not accessible 
So that I can install or open it.

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed

@NCGP-T422
Scenario Outline: Verify App List Options
	Then the "<appName>" session should be shown

Examples:
	| appName         |
	| Samsung Gallery |
	| Bluetooth       |
	| Samsung Pass    |
	| Wi-Fi           |

@NCGP-T224
Scenario Outline: Verify if sync now function is started when user click on the sub-menu list item Sync Now
	When the "<appName>" app settings is accessed
	Then the syncing text is displayed during the sync process

Examples:
	| appName         |
	| Samsung Gallery |
	| Bluetooth       |
	| Samsung Pass    |
	| Wi-Fi           |