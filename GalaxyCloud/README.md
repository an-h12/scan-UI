# **Test Automation - Galaxy Platform Cloud**

This project contains the Automation Test Template. The project was developed using C# and the SpecFlow Framework, leveraging BDD (Behavior-Driven Development) concepts.

The automation was designed specifically for Intel-based systems. If you wish to use it on ARM-based systems, please note that some tests may fail.  
Additionally, the **Samsung Pass desktop app** is designed to work exclusively on the **Intel Galaxy Book 3 series**.

The purpose of this document is to explain:  
- How to set up the project  
- How the project works  
- How to generate reports

---

## **Before Beginning**

Before using this template, a few configuration steps are required:

1. **Download and Install the necessary tools:**
   - [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/)
   - [.NET Framework 4.8 Developer Pack](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer)
   - [WindowsApplicationDriver_1.2.1.msi](https://github.com/microsoft/WinAppDriver/releases/tag/v1.2.1)
   - [Node.js](https://nodejs.org/en/download/)

2. **Install Appium via Node.js:**
   - Open **CMD** and paste the following code:
     ```bash
     npm install -g appium
     ```

3. **Download and Install [Git](https://git-scm.com/download/win)** for Windows.

---

## **Initial Setup**

1. **Clone the [Automation Test Template Project]([http://dmz-2.dmz.org.br:7990/scm/pcinves/pc-testtemplate.git](https://github.ecodesamsung.com/SRBR-SIDI-NCGP/NCGP_scloud_source_test))**.

2. **Check the required packages** to ensure the correct versions are installed:
    ```
    TargetFramework: net4.8
    
    Package: Appium.WebDriver | Version: 4.3.1
    Package: ClosedXML | Version: 0.96.0
    Package: NUnit | Version: 3.13.2
    Package: NUnit3TestAdapter | Version: 4.0.0
    Package: Microsoft.NET.Test.Sdk | Version: 16.5.0
    Package: SpecFlow | Version: 3.9.22
    Package: SpecFlow.Internal.Json | Version: 1.0.8
    Package: SpecFlow.NUnit | Version: 3.9.22
    Package: SpecFlow.Plus.LivingDocPlugin | Version: 3.9.57
    Package: SpecFlow.Tools.MsBuild.Generation | Version: 3.9.22
    Package: StyleCop.Analyzers | Version= 1.1.118
    ```

3. **Install the SpecFlow for Visual Studio extension** by following the instructions here: [SpecFlow Installation for Visual Studio](https://docs.specflow.org/projects/specflow/en/latest/visualstudio/visual-studio-installation.html).

At this point, you should have everything required to write, maintain, and execute your automated tests.

---

## **Running Automated Tests**

Once the initial setup is complete, executing the test reports is a simple task.

1. **Open the project solution** in Visual Studio.
2. **Open the Test Explorer tab** (Toolbar: **Test → Test Explorer**).
3. The **Test Explorer** will attach to the Visual Studio main window.
4. **Run the test suite** by clicking the **Run** button.

---

## **Application Setup**

Once the initial setup is complete, the applications must be configured. For Cloud tests, install the following:

- Samsung Cloud
- Samsung Gallery  
- Samsung Notes  
- Samsung Pass
- Samsung Bluetooth
- Samsung Wi-Fi

Remember, **Samsung Pass** is only designed to work with Intel-based Galaxy Book devices. If it is used on other devices, it may not function correctly.

Always check with your team for the latest versions of all required applications before beginning the tests.

**Important Notes:**
- A valid Samsung Account, linked to **OneDrive**, is required. 
- Linking a Samsung account with OneDrive requires a mobile device. Any team member with a mobile device can assist with this process.
- The installation order is flexible, but all apps must be set up before testing begins.

**Steps to configure applications:**
1. After installing all required apps, log in using your **Samsung Account** credentials.
2. Open and configure **Samsung Gallery**, **Notes**, **Bluetooth**, and **Pass**.
3. To set up **Samsung Pass**, authentication using a mobile device is necessary. Any team member can handle this authentication.
4. **Windows Hello** is also required to set up **Samsung Pass**. A valid Microsoft account must be logged in on the sample.

If you have any further questions, please reach out to a Cloud team member.

---

## **Generating Automated Test Reports**

SpecFlow uses the **LivingDocPlugin** to generate test reports. Each time a test is executed, the plugin generates a **TestExecution.json** file, which is used to create living documentation. The file is located at the following path in the project:

```bash
"..\bin\Debug\net4.8\TestExecution.json"
