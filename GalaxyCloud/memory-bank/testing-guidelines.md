# GalaxyCloud Testing Guidelines

## Step Definition Development Guidelines

### 1. Method Naming Convention

**Pattern**: `[StepType][DescriptiveAction][ParameterName]`

**Examples**:
```csharp
// Simple actions without parameters
[When(@"maximize window example")]
public void WhenMaximizeWindowExample()

// Actions with parameters
[When(@"the user clicks ""(.*)""")]
public void WhenTheUserClicks(string buttonName)

// Multiple parameters
[When(@"the ""(.*)"" toggle button status is ""(.*)""")]
public void WhenTheToggleButtonStatusIs(string appName, string status)
```

### 2. Step Type Prefixes
- **Given steps**: Start with `Given`
- **When steps**: Start with `When` 
- **Then steps**: Start with `Then`
- **StepDefinition steps**: Use appropriate prefix based on action

### 3. Action Description
- Use clear, descriptive verbs
- Follow PascalCase convention
- Mirror the Gherkin step language as closely as possible
- Avoid abbreviations unless they are widely understood

### 4. Parameter Handling
- Parameter names should be meaningful and match the step description
- Order parameters according to their appearance in the regex
- Use appropriate data types (string, int, bool, etc.)

## Regex Pattern Guidelines

### Common Patterns:
```csharp
// Text within double quotes
@"the user clicks ""(.*)"""

// General text parameters  
@"the (.*) button is displayed"

// Numeric parameters
@"wait for (\d+) seconds"

// Multiple parameters
@"click (.*) button and verify (.*) text"
```

### Best Practices:
- Use `@"..."` format for regex strings
- Escape special characters properly
- Test regex patterns to ensure they match intended steps
- Keep patterns specific enough to avoid false matches

## Code Structure and Organization

### 1. Class Structure
```csharp
[Binding]
public class FeatureNameSteps : BasePageObject
{
    #region Given
    // Given step methods
    #endregion

    #region When  
    // When step methods
    #endregion

    #region Then
    // Then step methods
    #endregion
}
```

### 2. Method Structure
```csharp
/// <summary>
/// Clear description of what this step does
/// </summary>
/// <param name="parameterName">Description of parameter</param>
[When(@"the user clicks ""(.*)""")]
public void WhenTheUserClicks(string buttonName)
{
    try
    {
        // Call page object method
        ClickButtonByName(buttonName);
        
        // Wait if necessary
        Thread.Sleep(1000);
    }
    catch (Exception ex)
    {
        throw new Exception($"Failed to click button {buttonName}: {ex.Message}", ex);
    }
}
```

### 3. Error Handling
- Use try-catch blocks for operations that might fail
- Provide meaningful error messages
- Include original exception in throw statements
- Log important actions and errors

### 4. Assertions
- Use NUnit Assert statements in Then steps
- Provide clear failure messages
- Assert.IsTrue/False for boolean checks
- Assert.AreEqual for value comparisons

## Best Practices

### 1. Page Object Interaction
- Always call methods from page objects rather than directly interacting with elements
- Keep step definitions focused on mapping Gherkin to code
- Let page objects handle UI interactions and element locators

### 2. Waits and Timing
- Use appropriate waits for UI synchronization
- Thread.Sleep for simple cases (but prefer WebDriverWait when possible)
- Wait for elements to be displayed/enabled before interacting

### 3. Code Readability
- Group related methods using #region directives
- Include XML documentation comments for classes and public methods
- Use meaningful variable names
- Keep methods focused on single responsibilities

### 4. Maintainability
- Follow existing project patterns and conventions
- Keep step definitions simple and focused
- Reuse existing page object methods
- Update documentation when patterns change

## Example Implementation

### Feature File:
```gherkin
@NCCOM-T1995 @PageIndicator @Functional
Scenario: Verify that clicking dots changes the displayed image
    Given that the "Page Indicator" component is selected
    When the user clicks "Show example"
    And maximize window example
    And the user clicks the <position> dot
    And the user clicks flipview
    Then the image should move to <position> image
    
    Examples:
    | position |
    | second   |
    | third    |
```

### Step Definition:
```csharp
[Binding]
public class PageIndicatorSteps : PageIndicatorPage
{
    #region Given
    [Given(@"that the ""(.*)"" component is selected")]
    public void GivenThatTheComponentIsSelected(string componentName)
    {
        // Implementation to select component
    }
    #endregion

    #region When
    [When(@"the user clicks ""(.*)""")]
    public void WhenTheUserClicks(string buttonName)
    {
        if (buttonName.Equals("Show example", StringComparison.OrdinalIgnoreCase))
        {
            ClickShowPageIndicatorExample();
            Thread.Sleep(3000);
        }
    }

    [When(@"maximize window example")]
    public void WhenMaximizeWindowExample()
    {
        SwitchToAndMaximizeExternalWindow();
        SetInitialImageName(GetCurrentImageName());
    }

    [When(@"the user clicks the (.*) dot")]
    public void WhenTheUserClicksTheDot(string position)
    {
        try
        {
            int dotIndex = GetDotIndex(position);
            ClickDotByIndex(dotIndex);
            Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to click {position} dot: {ex.Message}", ex);
        }
    }

    [When(@"the user clicks flipview")]
    public void WhenTheUserClicksFlipview()
    {
        ClickFlipview();
    }
    #endregion

    #region Then
    [Then(@"the image should move to (.*) image")]
    public void ThenTheImageShouldMoveToImage(string position)
    {
        bool isImageChanged = VerifyImageChangedToPosition(position);
        Assert.IsTrue(isImageChanged, $"Failed to verify image change to {position} position");
    }
    #endregion
}