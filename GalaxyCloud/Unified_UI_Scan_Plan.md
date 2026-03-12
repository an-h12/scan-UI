# Unified Plan: Prompt Dùng Chung + Runtime UI Scan Theo Session (GalaxyCloud + autoTemplate)

## Tóm tắt
Mục tiêu là tạo một hệ thống chung để sinh test automation từ Gherkin mà không cần truyền locator thủ công mỗi lần, bằng cách:
- Dùng `Core Prompt` chung cho mọi repo.
- Dùng `Repo Profile` riêng cho từng repo để khai báo session/tag/step-switch.
- Dùng local CLI `UiDiscovery` do Cline gọi để scan runtime, cập nhật `.ai-cache/ui-map.json`.
- Dùng `session-aware scan state machine` để xử lý các step mở session/window mới.

## Mục tiêu và phạm vi
- Input chính: Gherkin file + project root + repo profile id.
- Output chính: `Page.cs`, `Steps.cs`, `ResolvedElements.json`, `MissingMapReport.md` (nếu unresolved).
- Giai đoạn này chốt kiến trúc scan + map + contract với Cline cho 2 repo:
  - `C:\Users\Haha\Desktop\Test-auto-cloud\scloud_test-auto\GalaxyCloud`
  - `C:\Users\Haha\Desktop\Test-auto-cloud\test_automation-main\autoTemplate`

## Kiến trúc tổng thể
1. `Core Prompt`
- Rule generate code.
- Rule locator priority.
- Rule self-healing.
- Rule không bịa locator.

2. `Repo Profile`
- Mapping session.
- Mapping tag/step -> session transition.
- Path chuẩn của Feature/Page(s)/Steps/runsettings.

3. `UiDiscovery CLI`
- Scan runtime PageSource.
- Resolve element theo phrase.
- Upsert map vào `.ai-cache/ui-map.json`.

4. `Cline Orchestrator`
- Gọi CLI.
- Validate kết quả machine-readable.
- Chỉ gửi summary vào model để tránh tràn context.

## Public interfaces/types cần có
1. `repo-profile.json` schema
```json
{
  "repoId": "autoTemplate",
  "projectRoot": "C:/.../autoTemplate",
  "runsettingsPath": "C:/.../RunConfigurations.runsettings",
  "featureDir": "Feature",
  "pageDirs": ["Pages"],
  "stepsDir": "Steps",
  "defaultSession": "oneUiSession",
  "sessions": ["oneUiSession", "root", "external"],
  "tagSessionRules": [{"tag":"Root","session":"root"}],
  "stepSessionRules": [{"contains":"maximize window example","session":"external"}]
}
```

2. `DiscoveryRequest`
```json
{
  "runId":"uuid",
  "repoId":"autoTemplate",
  "sessionHint":"oneUiSession",
  "targetPhrase":"Sync using Wi-Fi only toggle",
  "stepText":"When ...",
  "threshold":0.78,
  "timeoutSec":30
}
```

3. `DiscoveryResult`
```json
{
  "runId":"uuid",
  "status":"resolved|unresolved|error",
  "activeSession":"oneUiSession|root|external",
  "scanPerformed":true,
  "mapUpdated":true,
  "bestMatch":{"automationId":"","name":"","className":"","xpath":"","score":0.0,"source":"runtime_scan"},
  "snapshotPath":"C:/.../.ui-snapshots/...xml",
  "mapPath":"C:/.../.ai-cache/ui-map.json",
  "errorCode":null,
  "errorMessage":null
}
```

## Luồng session-aware scan khi gặp step mở session mới
1. Parse scenario theo thứ tự step.
2. Khởi tạo `activeSession = defaultSession` từ repo profile.
3. Trước mỗi step, áp dụng `tagSessionRules` và `stepSessionRules`.
4. Nếu step gây mở context mới:
- `autoTemplate`: `@Root` hoặc `maximize window example` thì switch sang `root/external`.
- `GalaxyCloud`: switch theo app-session (`SamsungCloud/Gallery/Notes/Pass/Account`...).
5. Chỉ scan `PageSource` trên `activeSession` hiện tại.
6. Ghi element vào map với key có scope session/window: `repoId|session|locator-key`.
7. Nếu không resolve được sau khi đã đúng session/context thì trả `unresolved`.

## Sự khác biệt đã chuẩn hóa vào profile
1. `GalaxyCloud`
- Multi-app sessions.
- Folder Page là `Page`.
- Runsettings: `data.runsettings`.

2. `autoTemplate`
- One main session + `root/external`.
- Folder Page là `Pages`.
- Nhiều feature dùng `@Root`.
- Có step mở external window (`maximize window example`).

## Quy tắc đặc biệt cho AI rule
1. Không dùng một session cố định cho toàn scenario.
2. Không dùng locator cross-session.
3. Nếu step có điều hướng component kiểu `Given that the "<Component>" component is selected` thì phải đi qua step đó trước khi scan target step.
4. Với locator theo `Name`, phải cho phép đa ngôn ngữ UI (EN/PT).
5. Nếu unresolved thì tạo report, không yêu cầu full mapping table.

## Contract thành công cho Cline
1. Exit code của CLI phải bằng `0`.
2. `DiscoveryResult` phải tồn tại và parse được.
3. `status` phải hợp lệ (`resolved|unresolved|error`).
4. Nếu `resolved` thì `bestMatch` hợp lệ và `snapshotPath` tồn tại.
5. Chỉ đưa vào prompt AI các field tóm tắt:
- `status`, `activeSession`, `bestMatch`, `score`, `resultPath`.

## Kế hoạch triển khai
1. Tạo `Core Prompt` dùng chung và 2 file `repo-profile.json`.
2. Tạo `UiDiscovery` CLI với `scan/resolve`.
3. Thêm state machine switch session theo profile.
4. Tạo map store `.ai-cache/ui-map.json` + snapshot store `.ui-snapshots`.
5. Tích hợp Cline orchestration + result validator.
6. Gắn output resolve vào prompt generate code.

## Test cases bắt buộc
1. GalaxyCloud: step thuộc app phụ (Gallery/Notes) phải resolve đúng session app đó.
2. autoTemplate: scenario có `@Root` phải resolve bằng root session.
3. autoTemplate: step `maximize window example` phải resolve ở external session.
4. Scenario bình thường không switch session vẫn resolve đúng default session.
5. Khi scan thành công nhưng score thấp hơn threshold thì trả `unresolved`.
6. Khi WinAppDriver/app init lỗi thì trả `error` có `errorCode`.
7. Cline không nhét XML vào chat, chỉ gửi summary.
8. Lần chạy sau dùng lại `ui-map.json` phải giảm thời gian resolve.

## Tiêu chí nghiệm thu
1. Cline tự xác định được success/fail bằng contract machine-readable, không phụ thuộc đọc text tự do.
2. `ui-map.json` được cập nhật ổn định qua nhiều lần scan.
3. Không có lần nào XML/raw dump bị đẩy vào prompt chính.
4. Với cùng `targetPhrase`, lần chạy sau ưu tiên map và giảm thời gian resolve.

## Assumptions và defaults
1. Một `Core Prompt` dùng chung, không tách prompt theo app.
2. Mỗi repo có một `Repo Profile` riêng.
3. `ui-map.json` là per-project.
4. Threshold mặc định là `0.78`.
5. Retry threshold là `0.65`.
6. Timeout scan mặc định là `30s`.
7. Snapshot retention mặc định là `14 ngày` hoặc `200 file`.
8. WinAppDriver giả định đang chạy tại `http://127.0.0.1:4723`.
9. Window identification dùng `processName` + `windowTitleContains`.
10. Cline gọi CLI qua Bash/Shell command, per-element resolution.

---

## Tech Stack
- **CLI**: .NET/C# Console App (.NET 8)
- **WinAppDriver**: Giả định đang chạy sẵn tại `http://127.0.0.1:4723`
- **Resolution**: Weighted scoring (automationId 40%, name 30%, className 20%, contentDesc 10%)

---

## Public interfaces/types (Mở rộng)

### 1. repo-profile.json schema (Full)
```json
{
  "repoId": "autoTemplate",
  "projectRoot": "C:/path/to/project",
  "winAppDriverUrl": "http://127.0.0.1:4723",
  "featureDir": "Feature",
  "pageDirs": ["Pages"],
  "stepsDir": "Steps",
  "runsettingsPath": "C:/path/to/runsettings",
  "defaultSession": "oneUiSession",
  "sessions": {
    "oneUiSession": {
      "processName": "ApplicationUnderTest",
      "windowTitleContains": "MainWindow",
      "description": "Main app window"
    },
    "root": {
      "processName": "explorer",
      "windowTitleContains": "Program Manager",
      "description": "Windows Desktop"
    },
    "external": {
      "processName": null,
      "windowTitleContains": "maximize",
      "description": "External window"
    }
  },
  "tagSessionRules": [
    { "tag": "@Root", "session": "root" }
  ],
  "stepSessionRules": [
    { "contains": "maximize window example", "session": "external" }
  ],
  "supportedLanguages": ["en", "pt"],
  "threshold": 0.78,
  "retryThreshold": 0.65,
  "timeoutSec": 30
}
```

### 2. DiscoveryRequest
```json
{
  "runId": "uuid",
  "repoId": "autoTemplate",
  "sessionHint": "oneUiSession",
  "targetPhrase": "Sync using Wi-Fi only toggle",
  "stepText": "When ...",
  "language": "en",
  "threshold": 0.78,
  "retryThreshold": 0.65,
  "timeoutSec": 30
}
```

### 3. DiscoveryResult
```json
{
  "runId": "uuid",
  "status": "resolved|unresolved|error",
  "activeSession": "oneUiSession|root|external",
  "scanPerformed": true,
  "mapUpdated": true,
  "bestMatch": {
    "automationId": "",
    "name": "",
    "className": "",
    "xpath": "",
    "score": 0.0,
    "source": "runtime_scan|cache"
  },
  "snapshotPath": "C:/.../.ui-snapshots/...xml",
  "mapPath": "C:/.../.ai-cache/ui-map.json",
  "errorCode": null,
  "errorMessage": null,
  "summary": {
    "status": "resolved",
    "session": "oneUiSession",
    "element": "sync_button",
    "score": 0.85,
    "xpath": "//Button[@AutomationId='sync_button']",
    "resultPath": "C:/project/.ai-cache/ui-map.json"
  }
}
```

---

## Cache Structure

### .ai-cache/ui-map.json
```json
{
  "repoId": "autoTemplate",
  "lastUpdated": "2026-03-12T10:30:00Z",
  "entries": {
    "oneUiSession|en|sync_button": {
      "phrase": "Sync button",
      "resolvedAt": "2026-03-12T10:30:00Z",
      "score": 0.85,
      "locator": {
        "type": "xpath",
        "value": "//Button[@AutomationId='sync_btn']"
      }
    }
  }
}
```

**Key format:** `{session}|{language}|{normalizedPhrase}`

**Mục đích:**
- Lần đầu: Scan runtime → mất 5-30s
- Lần sau: Cache hit → trả về ngay (0.1s)
- Element đã resolve có thể tái sử dụng nhiều lần

---

## CLI Resolution Flow

```
1. LOAD PROFILE
   - Validate repoId
   - Load sessions, rules, supportedLanguages

2. RESOLVE SESSION
   - Check tagSessionRules → session
   - Check stepSessionRules → session
   - Set activeSession

3. CHECK CACHE
   Key: {activeSession}|*|{normalizedPhrase}
   └─→ HIT → Return cached locator (source: cache)
   └─→ MISS → Continue to scan

4. ATTACH WINDOW (WinAppDriver)
   - Find window: processName + titleContains
   - Create session

5. GET PAGE SOURCE
   - driver.PageSource
   - Save: .ui-snapshots/{runId}_{session}.xml

6. SCORE ELEMENTS (Weighted)
   For each element:
   - automationId match → +40%
   - name match → +30%
   - className match → +20%
   - contentDesc match → +10%

7. SELECT BEST MATCH
   - score >= 0.78 → RESOLVED
   - score >= 0.65 AND firstTry → RETRY with 0.65
   - else → UNRESOLVED

8. UPDATE CACHE & SNAPSHOT
   - Upsert: .ai-cache/ui-map.json
   - Save snapshot if resolved

9. RETURN DiscoveryResult
```

---

## Cline Integration

### CLI Invocation
```bash
UiDiscovery.exe resolve ^
  --repo-id "autoTemplate" ^
  --session-hint "oneUiSession" ^
  --phrase "Sync button" ^
  --threshold 0.78 ^
  --timeout 30
```

### Cline Workflow
```
1. Cline parse Gherkin → extract phrases cần resolve
2. Cline load repo profile → xác định active session
3. Cline gọi CLI với DiscoveryRequest
4. CLI resolve → trả DiscoveryResult
5. Cline nhận result → generate Page.cs / Steps.cs với locator đã resolve
```

**Cline chỉ sử dụng `summary` field, không đọc full XML**

---

## Error Code Schema

| Category | Code | Description |
|----------|------|-------------|
| CONNECTION | WAD_CONNECTION_FAILED | Cannot connect to WinAppDriver |
| CONNECTION | WAD_SESSION_CREATE_FAILED | Failed to create session |
| WINDOW | WINDOW_NOT_FOUND | Window not found by process/title |
| WINDOW | WINDOW_AMBIGUOUS | Multiple windows match the filter |
| WINDOW | WINDOW_ATTACH_FAILED | Failed to attach to window |
| SCAN | PAGE_SOURCE_FAILED | Failed to get page source |
| SCAN | ELEMENT_NOT_FOUND | Element not found in page source |
| SCAN | LOCATOR_BUILD_FAILED | Failed to build XPath locator |
| RESOLUTION | NO_MATCH_ABOVE_THRESHOLD | No element matches above threshold |
| RESOLUTION | ALL_LANGUAGES_FAILED | Failed to resolve in all supported languages |
| INPUT | INVALID_PROFILE | Invalid or missing repo profile |
| INPUT | INVALID_SESSION | Session not defined in profile |
| INPUT | GHERKIN_PARSE_ERROR | Failed to parse Gherkin step |
| SYSTEM | FILE_WRITE_FAILED | Failed to write output file |
| SYSTEM | SNAPSHOT_FAILED | Failed to save snapshot |

---

## Repo-Specific Profiles

### GalaxyCloud Profile
```json
{
  "repoId": "GalaxyCloud",
  "projectRoot": "C:/Users/Haha/Desktop/Test-auto-cloud/scloud_test-auto/GalaxyCloud",
  "winAppDriverUrl": "http://127.0.0.1:4723",
  "featureDir": "Feature",
  "pageDirs": ["Page"],
  "stepsDir": "Steps",
  "runsettingsPath": "C:/.../data.runsettings",
  "defaultSession": "oneUiSession",
  "sessions": {
    "oneUiSession": {
      "processName": "GalaxyCloud",
      "windowTitleContains": "Samsung",
      "description": "Main OneUI session"
    },
    "SamsungCloud": {
      "processName": "SamsungCloud",
      "windowTitleContains": "Samsung Cloud",
      "description": "Samsung Cloud app"
    },
    "Gallery": {
      "processName": "Gallery",
      "windowTitleContains": "Gallery",
      "description": "Gallery app"
    },
    "Notes": {
      "processName": "Notes",
      "windowTitleContains": "Samsung Notes",
      "description": "Notes app"
    },
    "Pass": {
      "processName": "SamsungPass",
      "windowTitleContains": "Samsung Pass",
      "description": "Samsung Pass app"
    }
  },
  "tagSessionRules": [
    { "tag": "@Gallery", "session": "Gallery" },
    { "tag": "@Notes", "session": "Notes" },
    { "tag": "@Pass", "session": "Pass" }
  ],
  "supportedLanguages": ["en", "pt"],
  "threshold": 0.78,
  "retryThreshold": 0.65,
  "timeoutSec": 30
}
```

### autoTemplate Profile
```json
{
  "repoId": "autoTemplate",
  "projectRoot": "C:/Users/Haha/Desktop/Test-auto-cloud/test_automation-main/autoTemplate",
  "winAppDriverUrl": "http://127.0.0.1:4723",
  "featureDir": "Feature",
  "pageDirs": ["Pages"],
  "stepsDir": "Steps",
  "runsettingsPath": "C:/.../RunConfigurations.runsettings",
  "defaultSession": "oneUiSession",
  "sessions": {
    "oneUiSession": {
      "processName": "ApplicationUnderTest",
      "windowTitleContains": "MainWindow",
      "description": "Main app window"
    },
    "root": {
      "processName": "explorer",
      "windowTitleContains": "Program Manager",
      "description": "Windows Desktop"
    },
    "external": {
      "processName": null,
      "windowTitleContains": "maximize",
      "description": "External window for maximize example"
    }
  },
  "tagSessionRules": [
    { "tag": "@Root", "session": "root" }
  ],
  "stepSessionRules": [
    { "contains": "maximize window example", "session": "external" }
  ],
  "supportedLanguages": ["en", "pt"],
  "threshold": 0.78,
  "retryThreshold": 0.65,
  "timeoutSec": 30
}
```

---

## Implementation Details

### Project Structure
```
UiDiscovery/
├── UiDiscovery.csproj
├── Program.cs                 # CLI entry point
├── Models/
│   ├── DiscoveryRequest.cs    # Request models
│   ├── DiscoveryResult.cs     # Response models
│   └── RepoProfile.cs         # Profile schema
├── Services/
│   ├── ProfileLoader.cs       # Load repo-profile.json
│   ├── WinAppDriverConnector.cs # WAD connection
│   ├── CacheManager.cs        # ui-map.json read/write
│   ├── Resolver.cs            # Weighted scoring logic
│   └── SnapshotManager.cs     # Snapshot saving/cleanup
└── Utils/
    ├── PhraseNormalizer.cs    # Normalize phrase for cache key
    └── ErrorCodes.cs          # Error code definitions
```

---

## Test Cases chi tiết

### Test Case 1: GalaxyCloud - Gallery session
```gherkin
@Gallery
Scenario: Delete photo from Gallery
  Given I am in Gallery app
  When I select "photo.jpg"
  And I tap delete button
  Then photo should be deleted
```
**Expected:** Step "I select photo.jpg" → session = "Gallery"

### Test Case 2: autoTemplate - @Root tag
```gherkin
@Root
Scenario: Access desktop from root
  Given I am on desktop
  When I click Settings icon
  Then settings should open
```
**Expected:** Tag "@Root" → session = "root"

### Test Case 3: autoTemplate - external window
```gherkin
Scenario: Maximize window example
  Given I have a window open
  When I maximize window example
  Then window should be maximized
```
**Expected:** Step chứa "maximize window example" → session = "external"

### Test Case 4: Cache hit
**Expected:** Lần 1: Scan runtime → Lần 2: Cache hit, return ngay

### Test Case 5: Unresolved - low score
**Expected:** Return status = "unresolved", score < 0.78

### Test Case 6: Error - WinAppDriver not running
**Expected:** Return status = "error", errorCode = "WAD_CONNECTION_FAILED"

---

## Edge Cases

### Edge Case 1: Multiple windows match filter
- Return errorCode = "WINDOW_AMBIGUOUS"

### Edge Case 2: Session not defined in profile
- Return errorCode = "INVALID_SESSION"

### Edge Case 3: Language fallback
- Thử tất cả languages trong supportedLanguages

### Edge Case 4: Element exists but score below threshold
- Retry với threshold = 0.65

### Edge Case 5: Cache key collision
- Cache key có session + language + phrase → không collision

### Edge Case 6: Window closes during scan
- Return errorCode = "WINDOW_CLOSED_DURING_SCAN"

### Edge Case 7: Very large PageSource
- Parse XML bằng XmlReader (streaming), giới hạn max 10000 elements

### Edge Case 8: Snapshot retention exceeded
- Cleanup oldest files khi CLI khởi động

---

## Verification Commands

### Build CLI
```bash
cd UiDiscovery
dotnet build -c Release
```

### Manual test - resolve phrase
```bash
UiDiscovery.exe resolve --repo-id "autoTemplate" --session-hint "oneUiSession" --phrase "Sync button"
```

### Check cache
```bash
type C:\project\.ai-cache\ui-map.json
```

### Test with GalaxyCloud
```bash
UiDiscovery.exe resolve --repo-id "GalaxyCloud" --session-hint "Gallery" --phrase "Delete photo"
```
