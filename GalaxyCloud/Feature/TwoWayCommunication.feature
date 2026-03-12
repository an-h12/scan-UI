@NCGP-1103
Feature: Two Way Communication
	As a user of the Samsung Account app, 
	I want the data of the sync is automatically updated 
	when the Gallery changes

Background: Verify if Samsung Cloud is opened
	Given "Samsung Cloud" app is launched
	And Samsung Cloud app list is displayed

@NCGP-T427 @Gallery
Scenario Outline: Verify that when toggle button is changed on the Samsung Gallery application it reflects at Samsung Cloud APP
	Given the "Samsung Gallery" app is opened
	When the toggle button status is changed on Gallery app to "<status>"
	Then the related toggle button "Samsung Gallery" status in the Samsung Cloud must be updated to "<status>"

	Examples:
		| Status |
		| ON     |
		| OFF    |

@NCGP-T508 @Gallery
Scenario Outline: Verify that when the Sync Using radio button selection is changed in the application, the related toggle button status in the Samsung Cloud is updated
	Given the "Samsung Gallery" app is opened
	And the Sync Using '<status>' radio button selection is changed in the Gallery application
	And the "Samsung Gallery" settings screen is accessed
	Then the related radio button "<status>" selection in the Samsung Cloud should be updated

	Examples:
		| status                           |
		| Wi-Fi and Ethernet only          |
		| Wi-Fi, Ethernet, and mobile data |

@NCGP-T509 @NCGP-1210 @Gallery
Scenario Outline: Verify that when the Sync Using radio button selection is changed in the Samsung Cloud, the related radio button selection in the Samsung Gallery app is updated
	When the "Samsung Gallery" settings screen is accessed
	And the sync status is changed to "ON"
	And the "<status>" radio button selection in the application on the Samsung Cloud is changed
	Then the "Samsung Gallery" app is opened
	And the "Samsung Gallery" application is updated for the same "<status>" selection

	Examples:
		| status                           |
		| Wi-Fi and Ethernet only          |
		| Wi-Fi, Ethernet, and mobile data |

@NCGP-T509 @NCGP-1210 @Pass
Scenario Outline: Verify that when the Sync Using radio button selection is changed in the Samsung Cloud, the related radio button selection in the Samsung Pass app is updated
	When the "Samsung Pass" settings screen is accessed
	And the sync status is changed to "ON"
	And the "<status>" radio button selection in the application on the Samsung Cloud is changed
	Then the "Samsung Pass" app is opened
	And the "Samsung Pass" application is updated for the same "<status>" selection

	Examples:
		| status                           |
		| Wi-Fi and Ethernet only          |
		| Wi-Fi, Ethernet, and mobile data |

@NCGP-T510 @Notes
Scenario Outline: Verify that when toggle button is changed on the Samsung Notes APP it reflects at Samsung Cloud APP
	Given the "Samsung Notes" app is opened
	When the toggle button status is changed on Samsung Notes app to "<status>"
	Then the related toggle button "Samsung Notes" status in the Samsung Cloud must be updated to "<status>"

	Examples:
		| status |
		| OFF    |
		| ON     |

@NCGP-T428 @Gallery
Scenario Outline: Verify that when toggle button is changed on the Samsung Cloud APP it reflects at gallery APP
	When the toggle button status of "Samsung Gallery" is changed on Cloud app to "<status>"
	And the "Samsung Gallery" app is opened
	Then the status of the related toggle button in the Gallery app should be updated to "<status>"

	Examples:
		| status |
		| ON     |
		| OFF    |

@NCGP-T530 @Notes
Scenario Outline: Verify that when toggle button is changed on the Samsung Cloud APP it reflects at Samsung Notes APP
	Given the "Samsung Notes" app is opened
	When the toggle button status of "Samsung Notes" is changed on Cloud app to "<status>"
	Then the status of the related toggle button in the Notes app should be updated to "<status>"

	Examples:
		| status |
		| OFF    |
		| ON     |

@NCGP-T430 @Gallery
Scenario Outline: Verify that when the "Sync Now" button in the Samsung Cloud is clicked, the "Last refreshed on"/"Last synced" information is updated in the Samsung Gallery application
	Given the "Samsung Gallery" app settings is accessed
	When the sync ocurrs
	Given the "Samsung Gallery" app is opened
	Then the information of Last Synced "Samsung Gallery" and Sync info are updated

@NCGP-T430 @Notes
Scenario Outline: Verify that when the "Sync Now" button in the Samsung Cloud is clicked, the "Last refreshed on"/"Last synced" information is updated in the related Samsung Notes application
	Given the "Samsung Notes" app settings is accessed
	When the sync ocurrs
	Given the "Samsung Notes" app is opened
	Then the information of Last Synced "Samsung Notes" and Sync info are updated

@NCGP-T430 @Pass
Scenario Outline: Verify that when the "Sync Now" button in the Samsung Cloud is clicked, the "Last refreshed on"/"Last synced" information is updated in the Samsung Pass application
	Given the "Samsung Pass" app settings is accessed
	When the sync ocurrs
	Given the "Samsung Pass" app is opened
	Then the information of Last Synced "Samsung Pass" and Sync info are updated

@NCGP-T295 @Gallery
Scenario Outline: Verify that when the toggle button is changed in the Samsung Cloud, sync selection in  Samsung Gallerythe application is updated
	Given the toggle button status of "Samsung Gallery" is changed on Cloud app to "<status>"
	When the "Samsung Gallery" app is opened
	Then the status of the related toggle button in the "Samsung Gallery" app should be updated to "<status>"

	Examples:
		| status |
		| OFF    |
		| ON     |

@NCGP-T295 @Notes
Scenario Outline: Verify that when the toggle button is changed in the Samsung Cloud, sync selection in the Samsung Notes application is updated
	Given the toggle button status of "Samsung Notes" is changed on Cloud app to "<status>"
	When the "Samsung Notes" app is opened
	Then the status of the related toggle button in the "Samsung Notes" app should be updated to "<status>"

	Examples:
		| status |
		| OFF    |
		| ON     |

@NCGP-T1205 @Pass
Scenario Outline: Verify that when the toggle button status is changed in the Samsung Pass, the related toggle button status in the Samsung Cloud is updated
	Given the "Samsung Pass" app is opened
	When the toggle button status is changed on Samsung Pass app to "<status>"
	Then the related toggle button "Samsung Pass" status in the Samsung Cloud must be updated to "<status>"

	Examples:
		| status |
		| OFF    |
		| ON     |

@NCGP-T1206 @Pass
Scenario Outline: Verify that when toggle button is changed on the Samsung Cloud app it reflects at Samsung Pass app
	Given the "Samsung Pass" app is opened
	When the toggle button status of "Samsung Pass" is changed on Cloud app to "<status>"
	Then the status of the related toggle button in the Samsung Pass application should be updated to "<status>"

	Examples:
		| status |
		| OFF    |
		| ON     |

@NCGP-T1208 @Pass
Scenario Outline: Verify that when the Sync now button is clicked in the Samsung Pass Application, the information of "Last Synced" and "Sync info" are updated in the related app in Samsung Cloud
	Given the "Samsung Pass" app is opened
	When the "Samsung Pass" sync now is runs
	And the "Samsung Pass" app settings is accessed
	Then the "Samsung Pass" information of Last Synced and Sync info are updated in the Samsung Cloud

@NCGP-T1209 @Pass
Scenario Outline: Verify that when the Sync Using radio button selection is changed on the Samsung Pass, the related toggle button status in the Samsung Cloud is updated
	Given the "Samsung Pass" app is opened
	When the sync using "<status>" radio button selection is changed on the Samsung Pass application
	Then the "Samsung Pass" app settings is accessed
	And the related radio button "<status>" selection in the Samsung Cloud should be updated

	Examples:
		| status                           |
		| Wi-Fi and Ethernet only          |
		| Wi-Fi, Ethernet, and mobile data |