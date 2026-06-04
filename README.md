# Audio_Visualization

一個用 Unity 製作的簡易**音樂視覺化（Music Visualization）**程式，目的是從底層理解音樂是如何運作的 —— 把音訊檔案經過 FFT 頻譜分析後，即時轉換成會隨節奏律動、變色的視覺畫面。

> It's a simple Visualization Music program, made to understand how music works at a low level.

---

## 畫面預覽 Screenshots

**環形頻譜 ・ Each 上色模式（Theme 2）**

![環形頻譜 - 藍色 Each 模式](https://github.com/user-attachments/assets/61b9dbe8-bfb4-4122-9032-7b19b61c8d02)

**主題切換選單與條狀頻譜（Theme 1）**

![主題切換與條狀頻譜](https://github.com/user-attachments/assets/d0fc2f58-b6af-4d2f-b05c-e5004886fe1e)

**環形頻譜 ・ Clamp 上色模式（Theme 2）**

![環形頻譜 - 紅色 Clamp 模式](https://github.com/user-attachments/assets/7d84a49c-b7d5-4df8-a48b-1418a428f60f)

---

## 主要功能 Features

- 🎵 **載入本機音樂**：透過跨平台檔案瀏覽器選取音訊檔，並用 NAudio 將 MP3 解碼成 Unity 可播放的 `AudioClip`。
- 📊 **即時頻譜分析**：使用 Unity 的 `AudioSource.GetSpectrumData` 進行 FFT 取樣，把頻率資料映射到一根根律動的長條（Bar）上。
- 🎨 **多主題（Theme）**：內建三種視覺主題，可即時切換。
- 🌈 **可自訂顏色**：內建 HSV 色彩選擇器，能自由調整視覺化所使用的顏色。
- ⚙️ **即時參數調整**：可在執行時調整顏色與長條的上升 / 下降反應速度。
- 🖥️ **自動隱藏 UI**：滑鼠靜止一段時間後，播放控制列會淡出，專注於畫面。

---

## 三種主題 Themes

| 主題 | 視覺呈現 | 攝影機 |
| --- | --- | --- |
| **Theme 1** | 橫向排列的條狀頻譜 | 正交（Orthographic） |
| **Theme 2** | 環形排列的頻譜，長條圍成一圈 | 正交（Orthographic） |
| **Theme 3** | 透視視角的立體呈現 | 透視（Perspective） |

> 主題切換由 `_GlobalSetting._SwitchTheme()` 負責，會銷毀目前的 `ThemePrefab` 並實例化對應主題的預製物件，同時切換攝影機投影模式。

---

## 操作說明 Controls

畫面右下角提供三顆控制按鈕（滑鼠移動時淡入，靜止後淡出）：

- ▶️ **播放 / 暫停**：控制目前音訊的播放狀態。
- 🔊 **音量**：點擊後滑出音量滑桿調整大小聲。
- ⚙️ **設定**：開啟 / 關閉設定面板（Setting GUI）。

### 設定面板 Setting Panel

開啟設定面板後可調整以下項目：

- **Rebuild Spectrum**：重新建立頻譜長條。
- **Color Setting**
  - `ColorUpSpeed` / `ColorDownSpeed`：顏色變亮 / 變暗的插值速度。
  - `BarUpSpeed` / `BarDownSpeed`：長條伸長 / 縮短的反應速度。
- **Theme Setting**：在 Theme 1 / 2 / 3 之間切換。
- **Color Mode**：切換上色模式
  - `Each`：每根長條依振幅隨機在主題色之間漸變。
  - `Clamp`：依振幅在「最小色 → 最大色」之間做數值夾擠（clamp）漸變。

左側的四個色塊可點擊開啟 HSV 色彩選擇器，自訂主題所使用的顏色。

---

## 運作原理 How It Works

1. **載入音訊** — `FileManager.cs`
   使用 [StandaloneFileBrowser](https://github.com/gkngkc/UnityStandaloneFileBrowser) 開啟系統檔案視窗，並透過 [TagLib#](https://github.com/mono/taglib-sharp) 讀取檔案資料。

2. **解碼音訊** — `NAudioPlayer.cs`
   以 [NAudio](https://github.com/naudio/NAudio) 把 MP3 位元流轉成 WAV / PCM，再解析成 `float[]` 聲道資料建立 `AudioClip`。

3. **頻譜取樣** — `_SimpleSpectrum.cs`
   每一幀呼叫 `GetSpectrumData()` 取得 FFT 頻譜（預設使用 `BlackmanHarris` 視窗），把頻率區間映射到各個長條的高度（`Scale.Y`），並支援對數頻率分布與依頻率加權。

4. **視覺映射與上色** — `_SimpleSpectrum.cs` + `_GlobalSetting.cs`
   長條高度與顏色都用 `Mathf.Lerp` 做平滑插值，速度由設定面板的 Up/Down Speed 控制；上色則依 `Each` / `Clamp` 兩種模式運算。

5. **延伸物件** — `SphereAmpliate.cs`
   可讓額外的物件（如球體）依指定頻段（band）的振幅縮放與變色，做出更多視覺變化。

---

## 專案結構 Project Structure

```
Assets/
├── _Scripts/                 # 專案核心程式碼
│   ├── _GlobalSetting.cs     # 全域設定、主題切換、設定面板 GUI
│   ├── _SimpleSpectrum.cs    # FFT 頻譜取樣與長條視覺化
│   ├── NAudioPlayer.cs       # MP3 → AudioClip 解碼
│   ├── FileManager.cs        # 載入檔案、播放 / 暫停 / 音量控制
│   ├── InpuManager.cs        # 滑鼠閒置時 UI 淡入 / 淡出
│   ├── PickColor.cs          # 顏色選擇與套用
│   └── SphereAmpliate.cs     # 額外物件依頻段律動
├── SimpleSpectrum/           # 頻譜視覺化參考資源（第三方）
├── HSVPicker/                # HSV 色彩選擇器（第三方）
├── StandaloneFileBrowser/    # 跨平台檔案瀏覽器（第三方）
├── _Prefabs / _Materials / _Mesh / _Animation / _UI_interface / _Audio
└── Scenes/                   # Unity 場景
```

---

## 開發環境 Requirements

- **Unity 2018.3.8f1**（見 `ProjectSettings/ProjectVersion.txt`）

### 如何執行 Getting Started

1. 以 Unity 2018.3.8f1（或相容版本）開啟此專案。
2. 開啟 `Assets/Scenes` 中的主場景並按下 Play。
3. 透過設定按鈕載入音樂檔，即可看到頻譜隨音樂律動。

---

## 第三方資源 Credits

本專案使用了以下開源資源：

- [NAudio](https://github.com/naudio/NAudio) — 音訊解碼
- [TagLib#](https://github.com/mono/taglib-sharp) — 讀取音訊檔案標籤
- [UnityStandaloneFileBrowser](https://github.com/gkngkc/UnityStandaloneFileBrowser) — 跨平台檔案選取
- [HSV Color Picker](https://github.com/judah4/HSV-Color-Picker-Unity) — 顏色選擇器
- SimpleSpectrum — 頻譜視覺化參考實作

> 本專案為學習用途，目的在於理解音訊處理與視覺化的底層運作。
