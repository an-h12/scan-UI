@NCGP-1213
Feature: Samsung Cloud Platform Manager Application
As a Samsung Account user,
I want to set and update the sync functions of my applications 
so that I can change my Samsung Cloud settings.

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed

@NCGP-T223
Scenario Outline: Verify if connected app sync now button is available in the sub-menu list item.
	When the "<appsName>" app settings is accessed
	Then sync now button must be displayed

Examples:
	| appsName        |
	| Samsung Gallery |
	| Bluetooth       |
	| Wi-Fi           |
	| Samsung Pass    |
	| Samsung Notes   |

@NCGP-T219
Scenario Outline: Verify if the "Wi-Fi only" and "Wi-Fi or Mobile Data" options will only be available for some devices.
	Given the "<appsName>" app settings is accessed
	Then the sync using is displayed on compatible devices with a sim card

Examples:
	| appsName        |
	| Samsung Gallery |
	| Bluetooth       |
	| Wi-Fi           |
	| Samsung Pass    |
	| Samsung Notes   |

@NCGP-T442
Scenario Outline: Verify that clicking on the back button in the App settings screen the App go back to the Dashboard page
	Given the "<appsName>" app settings is accessed
	When return to the previous page
	Then the Dashboard page should be displayed

Examples:
	| appsName        |
	| Samsung Gallery |
	| Bluetooth       |
	| Wi-Fi           |
	| Samsung Pass    |
	| Samsung Notes   |

@NCGP-643
Scenario Outline: Verify that clicking on the back button in the More information page screen the app go back to the App Settings page
	Given the More information page of the "<appsName>" app is accessed
	When return to the previous page
	Then the "<appsName>" app settings is displayed

Examples:
	| appsName      |
	| Bluetooth     |
	| Wi-Fi         |
	| Samsung Pass  |
	| Samsung Notes |