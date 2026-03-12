# GalaxyCloud Technology Context

## Core Technologies

### Programming Language
- **C#** - Primary language for test implementation
- **.NET Framework 4.8** - Target framework version

### Testing Framework
- **SpecFlow** - Behavior-Driven Development framework for Gherkin feature files
- **NUnit** - Unit testing framework for test execution
- **Selenium WebDriver** - Web automation library for desktop application testing
- **Appium** - Mobile automation framework integration

### Key Dependencies
- **WebDriver.dll** - Selenium WebDriver core library
- **TechTalk.SpecFlow.dll** - SpecFlow core functionality
- **nunit.framework.dll** - NUnit testing framework
- **Castle.Core.dll** - Dynamic proxy generation for mocking
- **Newtonsoft.Json.dll** - JSON serialization/deserialization
- **Gherkin.dll** - Gherkin parser for feature files

## Development Environment
- **IDE**: Visual Studio or Visual Studio Code
- **Build System**: MSBuild via .csproj files
- **Package Management**: NuGet
- **Version Control**: Git (assumed based on standard practices)

## Project Structure
- **GalaxyCloud.csproj** - Main project file
- **Feature/** - Gherkin feature files and generated code
- **Steps/** - Step definition implementations
- **Page/** - Page object models
- **Helpers/** - Utility classes and common functionality
- **SupportImage/** - Support images for documentation/testing

## Configuration Files
- **GalaxyCloud.dll.config** - Application configuration
- **data.runsettings** - Test run settings
- **GalaxyCloudRules.ruleset** - Code analysis rules
- **TestExecution.json** - Test execution configuration

## Development Setup
1. Install Visual Studio with .NET desktop development workload
2. Restore NuGet packages via `nuget restore` or IDE package manager
3. Build project using MSBuild or IDE
4. Run tests via NUnit test runner or IDE test explorer

## Key Technical Constraints
- **Windows-only**: Designed for Windows desktop application testing
- **.NET Framework 4.8**: Limited to this framework version
- **Selenium-based**: UI testing focused on desktop applications
- **SpecFlow-dependent**: Tied to Gherkin-based test scenarios

## Tool Usage Patterns
- **Test Execution**: Run via NUnit test adapter
- **Debugging**: Standard Visual Studio debugging capabilities
- **Reporting**: NUnit test results and SpecFlow reporting
- **CI/CD**: Can be integrated with build servers via MSBuild
