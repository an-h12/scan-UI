# Unified UI Scan Plan - Tổng hợp

## Context

Plan gốc (Unified_UI_Scan_Plan.md) mô tả hệ thống sinh test automation từ Gherkin với UiDiscovery CLI. Qua các phiên ultra-think, đã có những điều chỉnh quan trọng:

1. **Lazy Discovery**: User muốn chỉ discover khi cần (element not found → scan → save → reuse)
2. **Optimized Workflow**: Thay vì chạy test trước (luôn fail), workflow mới là Gherkin → Resolve → Generate → Test pass từ đầu
3. **Failure Analysis**: Test vẫn có thể fail dù đã resolve vì: Timing (40%), State (30%), Locator (20%), Logic (5%), System (5%)
4. **ElementExtensions**: Thay vì SmartElement wrapper mới, giữ nguyên pattern hiện tại và thêm extension methods

---

## Mục tiêu

Tạo hệ thống unified để:
- Nhận input là Gherkin file + repo profile
- Resolve elements (check cache → missing thì scan)
- Generate Page.cs + Steps.cs với locators sẵn
- Test chạy thành công từ lần đầu (với resilience layer)

---

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    USER INPUT                                │
│     Gherkin File + Repo Profile (GalaxyCloud/autoTemplate) │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                 GHERKIN PARSER                               │
│  - Extract phrases từ steps                                 │
│  - Identify variables ({text}, {int})                       │
│  - Group by session/screen                                  │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────┐
│              ELEMENT RESOLUTION ENGINE                       │
│                                                              │
│  For each phrase:                                           │
│    1. Check ElementLibrary (JSON cache)                    │
│    2. HIT → Use locator                                    │
│    3. MISS → UiDiscovery Scan → Save to Library            │
│    4. STALE → Re-scan → Update                             │
│                                                              │
│  Output: ResolvedElements.json                              │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                  CODE GENERATOR                              │
│                                                              │
│  Input:  ResolvedElements.json + Gherkin                    │
│  Output: Page.cs (giữ nguyên pattern hiện tại)             │
│          Steps.cs (binding)                                 │
│          Elements.json (library snapshot)                   │
│          MissingReport.md (unresolved elements)             │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                    READY TO RUN                              │
│  Test chạy với ElementExtensions (auto-wait + retry)      │
└─────────────────────────────────────────────────────────────┘
```

---

## Components

### 1. ElementLibrary (Core)
- **File**: JSON storage tại `cache/{repoId}/elements.json`
- **Structure**:
```json
{
  "repoId": "GalaxyCloud",
  "version": "1.0",
  "entries": {
    "oneUiSession|en|login_button": {
      "phrase": "Login button",
      "locator": "//Button[@AutomationId='login']",
      "score": 0.85,
      "createdAt": "2026-03-12",
      "appVersion": "1.0.0"
    }
  }
}
```

### 2. ElementExtensions (Enhanced Helper Methods)
```csharp
// Giữ nguyên pattern hiện tại:
// FindElementByID(Hooks.sessionGalaxyCloud, "btnActionID").Click();

// ElementExtensions bổ sung:
public static class ElementExtensions
{
    public static void ClickWithRetry(this WindowsElement element, int retryCount = 3)
    {
        for (int i = 0; i < retryCount; i++)
        {
            try
            {
                element.WaitForInteractable();
                element.Click();
                return;
            }
            catch (ElementNotInteractableException)
            {
                Thread.Sleep(500 * (i + 1));
            }
        }
        Screenshot.Capture();
        throw;
    }

    public static void SendKeysWithRetry(this WindowsElement element, string text)
    {
        // Retry logic tương tự
    }
}

// Usage:
FindElementByID(Hooks.sessionGalaxyCloud, "btnActionID").ClickWithRetry();
FindElementByID(Hooks.sessionSamsungAccount, "email").SendKeysWithRetry("admin");
```

### 3. UiDiscovery CLI (CLI Wrapper)
```bash
# CLI Wrapper gọi WinAppDriver qua HTTP API
# Dùng cho Cline/agent integration

UiDiscovery.exe resolve --phrase "login button" --repo-id "GalaxyCloud"
# Output: { "locator": "//Button[@AutomationId='login']", "score": 0.85 }

UiDiscovery.exe resolve --gherkin "features/login.feature" --repo-id "GalaxyCloud"
# Output:
# - Page.cs (với resolved locators)
# - Steps.cs
# - Elements.json
# - MissingReport.md
```

**Kiến trúc CLI:**
- Standalone Python script hoặc .exe
- Gọi WinAppDriver qua HTTP API (localhost:4723)
- Input: phrase/repo profile
- Output: JSON với locator + score

### 4. Code Generator
- Generate Page.cs giữ nguyên pattern hiện tại
- Generate Steps.cs với enhanced helper methods (.ClickWithRetry(), .SendKeysWithRetry())
- Generate MissingReport.md cho unresolved elements

---

## Blind Spots (Tổng hợp)

| ID | Issue | Priority | Solution |
|----|-------|----------|----------|
| AA | Gherkin parser edge cases | High | Handle variables ({text}, {int}) as wildcards |
| AB | Large feature file (50+ elements) | Medium | Parallel scan (max 3 workers), progress bar |
| AC | Partial resolution (45/50 resolved) | High | Generate Page.cs với null, report riêng |
| AD | Library sync & stale detection | High | Version-based cache, detect stale |
| AE | Timing issues (40% failures) | High | ElementExtensions auto-wait |
| AF | State difference | Medium | Session-aware library key |
| AG | Locator quality (ambiguous) | Medium | Fallback chain, multiple strategies |
| AH | Multi-machine sync | Low | Git-based sync (future) |
| AI | CLI Process Management | High | Spawn/wait/kill CLI processes, handle timeouts |
| AJ | WAD Connection | High | HTTP client to localhost:4723, retry logic |
| AK | CLI Response Parsing | Medium | Parse JSON output, handle errors |

### Blind Spots CHƯA ĐƯỢC COVER (Cần bổ sung)

1. **Session/Multi-Window Handling**: GalaxyCloud có multi-window, mỗi window cần session riêng
2. **Test Framework Integration**: Chưa xác định dùng SpecFlow, NUnit, hay xUnit
3. **Navigation Chains**: Element cần navigate qua nhiều screen mới đến được
4. **Data-Driven Testing (Scenario Outline)**: Gherkin có Examples table
5. **Visual Verification**: Screenshot comparison
6. **Error Recovery Strategies**: Error codes và retry logic
7. **CI/CD Pipeline**: Build agents requirement, WAD installation

---

## CLI Interface

```bash
# Full resolution workflow
UiDiscovery.exe resolve --gherkin "features/login.feature" --repo-id "GalaxyCloud"

# Options:
# --mode [pre-scan|lazy|incremental]
# --parallel [1-3]
# --output ./output
# --force-rebuild
```

---

## Implementation Phases

### Phase 1: CLI Tool - UiDiscovery (Week 1)
- [ ] `main.py` - CLI entry point với argparse
- [ ] `wad_client.py` - WinAppDriver HTTP client (localhost:4723)
- [ ] `resolver.py` - Phrase → locator resolution logic
- [ ] Basic CLI test: `python main.py resolve --phrase "login button"`

### Phase 2: Core Infrastructure (Week 2)
- [ ] ElementLibrary class (JSON persistence)
- [ ] Gherkin Parser (extract phrases từ .feature files)
- [ ] Basic cache check logic
- [ ] Session manager cho multi-window apps
- [ ] CLI wrapper trong C# (UiDiscoveryClient.cs)

### Phase 3: Code Generation (Week 3)
- [ ] Page.cs generator (giữ nguyên pattern hiện tại)
- [ ] Steps.cs generator (giữ nguyên pattern hiện tại)
- [ ] MissingReport.md generator
- [ ] Framework-specific hooks (SpecFlow)
- [ ] Scenario Outline support (Examples table)

### Phase 4: Resilience Layer (Week 4)
- [ ] ElementExtensions (auto-wait + retry) - enhance existing pattern
- [ ] Fallback locator strategies
- [ ] Screenshot on failure
- [ ] Visual verification (screenshot comparison)
- [ ] Error recovery strategies

### Phase 5: Testing & Polish (Week 5)
- [ ] Integration test với GalaxyCloud
- [ ] Multi-window/session test
- [ ] CI/CD pipeline setup
- [ ] Documentation
- [ ] Edge cases handling

---

## Verification

1. **Test GalaxyCloud**: @Gallery session switch
2. **Test autoTemplate**: @Root session switch
3. **Test cache hit**: Lần 2 return ngay
4. **Test error**: WAD not running → return error with suggestion
5. **Test timing**: ElementExtensions auto-wait hoạt động
6. **Test partial resolution**: Generate được dù có unresolved elements

---

## Files to Modify/Create

### CLI Tool (Standalone)
| File | Action |
|------|--------|
| `UiDiscovery/cli/main.py` | Create - CLI entry point |
| `UiDiscovery/cli/resolver.py` | Create - Phrase to locator resolution |
| `UiDiscovery/cli/wad_client.py` | Create - WinAppDriver HTTP client |
| `UiDiscovery/cli/ai_resolver.py` | Create - AI/ML resolver (optional) |

### C# Library (Integration)
| File | Action |
|------|--------|
| `GalaxyCloud/ElementLibrary.cs` | Create - Core library class |
| `GalaxyCloud/GherkinParser.cs` | Create - Parse Gherkin to phrases |
| `GalaxyCloud/ElementExtensions.cs` | Create - Enhanced methods (.ClickWithRetry, .SendKeysWithRetry) |
| `GalaxyCloud/CodeGenerator.cs` | Create - Generate Page.cs + Steps.cs |
| `GalaxyCloud/UiDiscoveryClient.cs` | Create - CLI wrapper (spawns process) |
| `GalaxyCloud/SessionManager.cs` | Create - Multi-window/session handling |
| `GalaxyCloud/NavigationChain.cs` | Create - Multi-step navigation |
| `GalaxyCloud/VisualVerifier.cs` | Create - Screenshot comparison |
| `GalaxyCloud/SpecFlowHooks.cs` | Create - SpecFlow hooks (BeforeScenario, etc.) |
| `GalaxyCloud/RepoProfile.cs` | Create - Repo configuration class |
| `cache/GalaxyCloud/elements.json` | Create - Initial cache |
| `docs/Unified_UI_Scan_Design.md` | Create - Design doc |

---

## Decisions Made

| Decision | Value | Notes |
|----------|-------|-------|
| Test Framework | **SpecFlow** | Phổ biến, nhiều plugins |
| Priority | **Core trước** | Infrastructure → Resolution → Generation → Resilience |
| Session Strategy | Per-scenario (default) | Có thể config trong repo profile |
| Element Approach | **ElementExtensions** | Giữ nguyên pattern hiện tại, thêm .ClickWithRetry() |
| Discovery Approach | **CLI Wrapper** | Standalone .exe/Python, gọi WAD qua HTTP |

---

## Current Code Pattern (Để tham khảo)

```csharp
// Pattern hiện tại trong codebase:
FindElementByID(Hooks.sessionGalaxyCloud, "btnActionID").Click();
FindElementByName(Hooks.sessionNotes, notesSettingsButtonName).Click();
FindElementByID(Hooks.sessionNotes, notesSyncSwitchID).Click();

// Sau khi có ElementExtensions:
FindElementByID(Hooks.sessionGalaxyCloud, "btnActionID").ClickWithRetry();
FindElementByName(Hooks.sessionNotes, notesSettingsButtonName).ClickWithRetry();
```
