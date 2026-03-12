# GalaxyCloud System Patterns

## Architecture Overview
The GalaxyCloud testing framework follows a Behavior-Driven Development (BDD) architecture using SpecFlow for test scenario definition and execution. The system is organized into logical layers that separate concerns and promote maintainability.

## Key Design Patterns

### Page Object Model (POM)
- **Location**: `Page/` directory
- **Purpose**: Encapsulates UI element interactions for each application page
- **Examples**: 
  - `SamsungCloudPage.cs` - Main cloud interface
  - `SamsungAccountPage.cs` - Authentication flows
  - `SamsungSettingsRuntimePage.cs` - Settings management

### Step Definition Pattern
- **Location**: `Steps/` directory
- **Purpose**: Maps Gherkin feature steps to executable code
- **Pattern**: One step definition class per feature file
- **Naming Convention**: 
  - Class name: `[FeatureName]Steps` inheriting from relevant page object
  - Method names: Start with step type (`Given`, `When`, `Then`) followed by descriptive action in PascalCase
  - Example: `WhenTheUserClicksTheButton(string buttonName)`
- **Method Structure**:
  - Each method maps to one Gherkin step with matching regex pattern
  - Methods should call page object methods for UI interactions
  - Use Assert statements for verifications in `Then` steps
  - Include parameter names that match the step description
- **Regex Pattern Guidelines**:
  - Use `@"..."` format for regex strings
  - Use `([^"]*)` for text within double quotes
  - Use `(.*)` for general text parameters
  - Use `(\d+)` for numeric parameters
- **Code Organization**:
  - Group methods by step type using `#region Given`, `#region When`, `#region Then`
  - Include documentation comments for class and public methods
  - Handle exceptions appropriately with try-catch blocks
  - Use appropriate waits (Thread.Sleep for simple cases, WebDriverWait for better practices)
- **Examples**:
  - `AuthenticationSamsungCloudSteps.cs`
  - `SamsungCloudDashboardSteps.cs`

### Feature File Organization
- **Location**: `Feature/` directory
- **Purpose**: Define test scenarios in Gherkin syntax
- **Structure**: Each feature has corresponding `.feature` and `.feature.cs` files

### Helper/Utility Pattern
- **Location**: `Helpers/` directory
- **Purpose**: Common functionality and utilities
- **Components**:
  - `Session.cs` - Test session management
  - `Hooks.cs` - Test lifecycle events
  - `DataString.cs` - Test data management
  - `Performance.cs` - Performance monitoring

## Component Relationships

### Test Execution Flow
1. **Feature Files** (.feature) → Define test scenarios
2. **Step Definitions** (Steps/*.cs) → Implement scenario logic
3. **Page Objects** (Page/*.cs) → Handle UI interactions
4. **Helpers** (Helpers/*.cs) → Provide common utilities
5. **Test Runner** → Execute tests via NUnit

### Data Flow
- Test data defined in feature files
- Step definitions process scenario data
- Page objects interact with application UI
- Helpers manage session state and utilities

## Critical Implementation Paths

### Authentication Flow
- Feature: `AuthenticationCloud.feature`
- Steps: `AuthenticationSamsungCloudSteps.cs`
- Pages: `SamsungAccountPage.cs`, `SamsungCloudPage.cs`

### Dashboard Management
- Feature: `ListSamsungCloudDashboard.feature`
- Steps: `ListSamsungCloudDashboardSteps.cs`
- Pages: `SamsungCloudPage.cs`

### Sync Settings
- Feature: `SamsungCloudSyncSettings.feature`
- Steps: `SamsungCloudSyncSettingsSteps.cs`
- Pages: `SamsungSettingsRuntimePage.cs`

## Integration Points
- **Selenium WebDriver**: UI automation for desktop applications
- **Appium**: Mobile device testing capabilities
- **NUnit**: Test execution and reporting framework
- **SpecFlow**: BDD scenario execution
