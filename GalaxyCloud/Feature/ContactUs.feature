@NCGP-249
Feature: Contact Us - Feature page

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And "Settings" button is oppened

@NCGP-T2331
Scenario: Display pop-up on "Contact us" button click
	When the "Contact us" button is clicked
	Then the popup should be displayed

@NCGP-T2333
Scenario: Validate if the text displayed in the pop-up matches the specified documentation
	When the "Contact us" button is clicked
	Then the popup should be displayed
	Then verify that the pop-up text matches with UI pattern

@NCGP-T2332
Scenario: Ensure that the pop-up can be reopened after being closed.
	Given the "Contact us" button is clicked
	And the popup should be displayed
	And pop-up is closed
	When the "Contact us" button is clicked
	Then the popup should be displayed