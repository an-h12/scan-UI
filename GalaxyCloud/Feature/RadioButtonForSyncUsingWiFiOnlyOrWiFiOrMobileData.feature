@NCGP-313
Feature: Radio button for Sync using Wi-Fi Only or Wi-Fi or Mobile Data
As a PC Cloud SDK integrated user,
I want to see the radio button options for the Gallery app in the sub-menu 
so that I can change my Gallery sync in  the Samsung Cloud settings using "Wi-Fi only" or "Wi-Fi or Mobile Data" options 

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed

@NCGP-T218
Scenario Outline: Verify that Wi-Fi only or Wi-Fi or Mobile Data radio buttons are enabled for selection in App Settings
	When the "<appName>" settings screen is accessed
	Then the Wi-Fi only or Wi-Fi, Ethernet and mobile data options are enabled

Examples:
	| appName         |
	| Bluetooth       |
	| Samsung Gallery |
	| Samsung Notes   |
	| Samsung Pass    |
	| Wi-Fi           |

	