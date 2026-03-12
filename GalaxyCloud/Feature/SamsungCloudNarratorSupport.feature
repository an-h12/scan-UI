Feature: Accessibility - Samsung Cloud Narrator Support

Background: 
    Given the "Samsung Cloud" app is launched
    And Narrator is enabled
    And Speech recap window is opened

Scenario: Verify Narrator announcements by comparing element properties with Speech recap
    When I navigate to the "Get started" button using Tab key
    Then I capture the current element properties
    And I get the last speech log from Speech recap
    And I compare the element properties with the speech log
    And I close Narrator
