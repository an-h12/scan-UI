Feature: Authentication - Samsung Cloud
		As a user of the SC app,
		I want to have an onboarding page for the app 
		So that I can have some login information and the app version.
Background: Given the "Samsung Cloud" app is launched

Scenario: Perform Login in Samsung Cloud
	Given the initial page is displayed with the "Get started" button
	Then it is possible to sign in of the account

Scenario: Verify Samsung Account Login
 	
 	Then "Keep your data safe and synced on all your devices using Samsung Cloud." text must be displayed
 	And "Get started" button must be displayed
 	When the "Get started" button on welcome screen is clicked
 	Then Samsung Account login screen must be displayed
 	Then Samsung Cloud screen displays "Samsung Cloud" text
