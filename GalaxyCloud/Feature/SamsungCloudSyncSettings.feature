@NCGP-1078
Feature: Samsung Cloud Sync Settings
	As a Samsung Account App user,
	I want to get access to the application list which supports Samsung Cloud Sync feature
	so that I can change my settings and start synchronization for each application.

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed

@NCPG-T351
Scenario Outline: Validate network configuration (Wifi only or Wifi + mobile)
	Given the "<appsName>" app settings is accessed
	And the sync status is changed to "ON"
	Then the "<radio>" radio button selection in the application on the Samsung Cloud is changed

	Examples:
		| appsName        | radio                            |
		| Samsung Gallery | Wi-Fi and Ethernet only          |
		| Samsung Gallery | Wi-Fi, Ethernet, and mobile data |
		| Samsung Notes   | Wi-Fi and Ethernet only          |
		| Samsung Notes   | Wi-Fi, Ethernet, and mobile data |
		| Samsung Pass    | Wi-Fi and Ethernet only          |
		| Samsung Pass    | Wi-Fi, Ethernet, and mobile data |

@NCGP-T413 @NCGP-297 @NCGP-301
Scenario Outline: Verify that with toggle button on OFF is not possible to sync
	Given the "<appsName>" app settings is accessed
	When the toggle button status is "OFF" on app settings
	Then sync now button should be disabled

	Examples:
		| appsName        |
		| Bluetooth       |
		| Samsung Gallery |
		| Samsung Notes   |
		| Samsung Pass    |
		| Wi-Fi           |

@NCGP-T423
Scenario Outline: Verify that the submenu App Settings can be opened
	When the "<appsName>" app settings is accessed
	Then should be can opened the "<titleName>" app settings page

	Examples:
		| appsName        | titleName                  |
		| Bluetooth       | Bluetooth sync             |
		| Samsung Gallery | Gallery sync with OneDrive |
		| Samsung Notes   | Samsung Notes sync         |
		| Samsung Pass    | Samsung Pass sync          |
		| Wi-Fi           | Wi-Fi sync                 |

@NCGP-T424 @NCGP-297 @NCGP-301
Scenario Outline: Verify if the app toggle button at the dashboard page is off, the "Sync now" button is disabled in the app settings
	When the "<appsName>" toggle button status is "OFF"
	And the "<appsName>" app settings is accessed
	Then sync now button should be disabled

	Examples:
		| appsName        |
		| Bluetooth       |
		| Samsung Gallery |
		| Samsung Notes   |
		| Samsung Pass    |
		| Wi-Fi           |

@NCGP-T545
Scenario Outline: Verify that "Last Sync" info is displayed at Cloud settings page
	Given the "<appsName>" app settings is accessed
	Then last Sync info should be displayed

	Examples:
		| appsName      |
		| Bluetooth     |
		| Samsung Notes |
		| Samsung Pass  |
		| Wi-Fi         |

@NCGP-T546
Scenario Outline: Verify that "Last Sync" displays the right date and time at Cloud settings page
	Given the "<appsName>" app settings is accessed
	When the sync ocurrs
	Then sync info for date and time should be updated

	Examples:
		| appsName        |
		| Bluetooth       |
		| Samsung Gallery |
		| Samsung Notes   |
		| Samsung Pass    |
		| Wi-Fi           |

@NCGP-T592
Scenario Outline: Verify that the "Sync using" selection persists when switching between screens
	Given the "<appsName>" app settings is accessed
	And the sync status is changed to "ON"
	When the "<radioSelection>" radio button selection in the application on the Samsung Cloud is changed
	And switch between the Dashboard and the "<appsName>" app settings screen
	Then the radio button "<radioSelection>" in the app settings screen must remain selected

	Examples:
		| appsName        | radioSelection                   |
		| Bluetooth       | Wi-Fi and Ethernet only          |
		| Bluetooth       | Wi-Fi, Ethernet, and mobile data |
		| Samsung Gallery | Wi-Fi and Ethernet only          |
		| Samsung Gallery | Wi-Fi, Ethernet, and mobile data |
		| Samsung Notes   | Wi-Fi and Ethernet only          |
		| Samsung Notes   | Wi-Fi, Ethernet, and mobile data |
		| Samsung Pass    | Wi-Fi and Ethernet only          |
		| Samsung Pass    | Wi-Fi, Ethernet, and mobile data |
		| Wi-Fi           | Wi-Fi and Ethernet only          |
		| Wi-Fi           | Wi-Fi, Ethernet, and mobile data |

@NCGP-T637
Scenario Outline: Verify that the more information page is displayed
	Given the "<appsName>" app settings is accessed
	When the more information menu is accessed
	Then the "More information" page is displayed

	Examples:
		| appsName      |
		| Bluetooth     |
		| Samsung Notes |
		| Samsung Pass  |
		| Wi-Fi         |

@NCGP-T638
Scenario Outline: Verify that the more information pop-up menu is displayed on Samsung Notes and Samsung Settings
	Given the "<appsName>" app settings is accessed
	When the more option menu is clicked
	Then the popup button is displayed

	Examples:
		| appsName      |
		| Bluetooth     |
		| Samsung Notes |
		| Samsung Pass  |
		| Wi-Fi         |

@NCGP-T639
Scenario Outline: Verify that the more information menu icon is enabled
	Given the "<appsName>" app settings is accessed
	Then the more information menu icon is enabled

	Examples:
		| appsName      |
		| Bluetooth     |
		| Samsung Notes |
		| Samsung Pass  |
		| Wi-Fi         |

@NCGP-642
Scenario Outline: Verify that the more information menu icon is not displayed on Samsung Gallery
	Given the "<appsName>" app settings is accessed
	Then the more information menu icon should not be displayed

	Examples:
		| appsName        |
		| Samsung Gallery |

@NCGP-T366
Scenario Outline: Validate cancel sync
	Given the "<appsName>" app settings is accessed
	And the sync status is changed to "ON"
	And get last synchronization date
	When the user start/stop synchronization
	Then the synchronization date has not changed

	Examples:
		| appsName        |
		| Bluetooth       |
		| Samsung Gallery |
		| Samsung Notes   |
		| Samsung Pass    |
		| Wi-Fi           |