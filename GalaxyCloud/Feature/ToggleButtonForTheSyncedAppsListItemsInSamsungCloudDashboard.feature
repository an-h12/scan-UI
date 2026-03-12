@NCGP-292
Feature: Toggle Button For The Synced Apps List Items In Samsung Cloud Dashboard
	As a PC Cloud SDK integrated user, 
	I want to see the toggle button for the Synced app in the Samsung Cloud Dashboard, 
	so that I can change my Synced app sync in  the Samsung Cloud settings .

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed

@NCGP-T172
Scenario: Verify if synced app toggle button is available on Dashboard
	When the Samsung Gallery was linked with OneDrive
	Then the app toggle button must be displayed