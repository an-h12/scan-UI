# Unified Plan: Prompt Dùng Chung + Runtime UI Scan Theo Session

## Tóm tắt
Mục tiêu là tạo một hệ thống chung để sinh test automation từ Gherkin mà không cần truyền locator thủ công mỗi lần.

- **Core Prompt**: Rule generate code chung cho mọi repo.
- **Repo Profile**: Mapping session/tag/step-switch cho từng repo.
- **UiDiscovery CLI**: Scan runtime → resolve element → update cache.
- **Session-aware state machine**: Xử lý step mở session/window mới.

## Mục tiêu và phạm vi
- **Input**: Gherkin file + project root + repo profile id
- **Output**: `Page.cs`, `Steps.cs`, `ResolvedElements.json`, `MissingMapReport.md`
- **Repos**:
  - GalaxyCloud (multi-window)
  - autoTemplate (single-window)
  - PENUP (single-window, drawing app - future)

---

## CORE FLOW

```
GHERKIN → PARSE → CHECK CACHE → MISS? → SCAN → RESOLVE → CACHE → GENERATE CODE
```

---

## BLIND SPOTS (A-Z)

### BLIND SPOT A: WAD Connection
- **Problem**: WinAppDriver không chạy hoặc không kết nối được
- **Solution**: Validate WAD trước scan + retry N lần + error suggestion

### BLIND SPOT B: Multiple Matches
- **Problem**: Nhiều elements cùng match 1 phrase ("OK button" có thể có 3 button)
- **Solution**: Score chênh lệch > 0.1 → auto-select; gần nhau → unresolved + candidates

### BLIND SPOT C: Low Score Resolution
- **Problem**: Element found nhưng score < 0.78
- **Solution**: Threshold 0.78, retry 0.65, dưới 0.65 → unresolved

### BLIND SPOT D: Error Handling & Retry
- **Problem**: Scan fail vì window close, timeout
- **Solution**: Retry 3 lần với delay tăng dần (1s, 3s, 5s)

### BLIND SPOT E: Cache HIT
- **Problem**: Lần sau dùng lại phrase đã scan → tốn thời gian scan lại
- **Solution**: Check cache TRƯỚC TIÊN, Key: {session}|{language}|{phrase}

### BLIND SPOT F: Session State Difference
- **Problem**: Cùng phrase nhưng context khác nhau ("Login button" - chưa login vs đã login)
- **Solution**: Thêm session_state vào cache key

### BLIND SPOT G: Dynamic Element IDs
- **Problem**: Samsung apps có dynamic AutomationId ("btn_12345" → "btn_67890")
- **Solution**: Version-based cache + fallback Name > XPath > ClassName

### BLIND SPOT H: Batch Scan Performance
- **Problem**: Scan từng phrase rất chậm với 50+ elements (50 × 30s = 25 phút)
- **Solution**: Batch mode + parallel workers (max 3)

### BLIND SPOT I: Parallel Execution
- **Problem**: Nhiều scans chạy song song → WAD session conflicts
- **Solution**: Single-thread by default hoặc queue-based

### BLIND SPOT J: Code Validation
- **Problem**: Generate Page.cs xong không biết có work không
- **Solution**: Compile check hoặc preview mode

### BLIND SPOT K: State-Dependent Elements
- **Problem**: Element chỉ xuất hiện SAU KHI navigation ("previous page" button chỉ có sau khi vào Tips page)
- **Solution**: User-Driven Navigation - User điều hướng app thủ công → UiDiscovery scan sau đó

### BLIND SPOT L: Cline Integration
- **Problem**: Cline gọi UiDiscovery CLI như thế nào?
- **Solution**:
```bash
UiDiscovery.exe resolve --repo-id "GalaxyCloud" --phrase "back button" --output-json
```
- CLI Response:
```json
{
  "status": "resolved",
  "bestMatch": { "xpath": "//Button[@AutomationId='back']", "score": 0.85 },
  "summary": { "status": "resolved", "locator": "...", "score": 0.85 }
}
```

### BLIND SPOT M: Gherkin Parsing
- **Problem**: Trích xuất phrase từ Gherkin step như thế nào?
- **Solution**:
| Step | Extraction |
|------|------------|
| "tap the OK button" | "OK button" |
| "enter {text} into Username field" | "Username field" |
| "see Welcome message" | "Welcome message" |

Multi-language:
- EN: tap, click, press
- VN: nhấn, bấm, chạm
- PT: tocar, clicar

### BLIND SPOT N: Test Execution
- **Problem**: Generated test chạy như thế nào?
- **Solution**:
```csharp
[BeforeScenario]
public void Setup()
{
    var options = new AppiumOptions();
    options.AddAdditionalCapability("app", "SamsungCloud");
    driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
}

[AfterScenario]
public void TearDown()
{
    driver?.Quit();
}
```
- Parallel: Max 3 sessions, per-session unique port

### BLIND SPOT O: CI/CD
- **Problem**: Chạy trong CI như thế nào?
- **Solution**: Agent cần WinAppDriver, screenshot capture khi fail, cache sync qua git LFS

### BLIND SPOT P: Element Stability Validation
- **Problem**: Cache có còn valid không sau app update?
- **Solution**: Cache versioning {phrase}|{appVersion}, warning khi cache > 30 ngày

### BLIND SPOT Q: Accessibility
- **Problem**: Elements không có AutomationId
- **Solution**: Fallback: Name > XPath > ClassName > ContentDesc

### BLIND SPOT R: Visual/Diff
- **Problem**: UI thay đổi → detect được không?
- **Solution**: Screenshot comparison on demand, visual diff khi element not found

### BLIND SPOT S: Reporting
- **Problem**: Test results và unresolved elements tracking
- **Solution**: HTML report với screenshots, unresolved elements list, trend analysis

### BLIND SPOT T: Multi-step Navigation
- **Problem**: Element cần navigate qua NHIỀU screen
- **Solution**: Navigation chain: Screen1 → Screen2 → Screen3 → Element

### BLIND SPOT U: Fuzzy Matching
- **Problem**: Phrase matching không chính xác ("OK button" vs "OK" vs "OKBtn")
- **Solution**: Levenshtein distance, normalization, synonym dictionary

### BLIND SPOT V: Context Window Optimization
- **Problem**: Pass quá nhiều data vào model context
- **Solution**: Chỉ pass summary: status, locator, score - KHÔNG pass full XML/PageSource

---

## APP TYPE CLASSIFICATION

| Type | Description | Ví dụ |
|------|-------------|--------|
| single-window | Một cửa sổ chính | autoTemplate, PENUP |
| multi-window | Nhiều cửa sổ, cần session switch | GalaxyCloud |

**PENUP Profile (ví dụ):**
```json
{
  "repoId": "PENUP",
  "appType": "single-window",
  "sessionStrategy": "per-scenario",
  "drawingLibrary": "Helpers/PenupDrawing.cs"
}
```

---

## Public Interfaces

### DiscoveryResult
```json
{
  "status": "resolved|unresolved|error",
  "activeSession": "sessionName",
  "bestMatch": { "automationId": "", "name": "", "xpath": "", "score": 0.85 },
  "errorCode": null
}
```

### Cache Structure
```json
{
  "repoId": "autoTemplate",
  "entries": {
    "oneUiSession|en|sync_button": {
      "phrase": "Sync button",
      "locator": "//Button[@AutomationId='sync_btn']",
      "score": 0.85
    }
  }
}
```

---

## Error Codes

| Category | Code | Description |
|----------|------|-------------|
| CONNECTION | WAD_CONNECTION_FAILED | Cannot connect to WinAppDriver |
| CONNECTION | WAD_SESSION_CREATE_FAILED | Failed to create session |
| WINDOW | WINDOW_NOT_FOUND | Window not found |
| WINDOW | WINDOW_AMBIGUOUS | Multiple windows match |
| RESOLUTION | NO_MATCH_ABOVE_THRESHOLD | No element above threshold |
| SYSTEM | FILE_WRITE_FAILED | Failed to write output |

---

## Verification

1. Test GalaxyCloud: @Gallery session switch
2. Test autoTemplate: @Root session switch
3. Test cache hit: Lần 2 return ngay
4. Test error: WAD not running → return error with suggestion
5. Test State-Dependent: prompt user → scan thành công
6. Test Cline Integration: CLI → JSON → generate code
