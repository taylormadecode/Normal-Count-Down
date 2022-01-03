<font size = "6"> Normal Countdown Technical Documentation </font>

---

<font size = "5"> Functions </font>

Class is called TMC_Normal_Countdown and inherets from TMC_Monobehaviour 
(TMC_Monobehaviour supports the when to start and custom GUI)

<font size = "4"> Functions </font>

| Scope | Return Value | Name | Paramaters | Description |
| - | - | - | - | - |
| public | void | StartFunction | ------------ | Code needed to start the script (This function is used for the automatic start on Awake, start, enable and disable) |
| public | void | SetupScript | ------------ | Overrided function declared in TMC_Monobehaviour use to ensure all TMC scripts have easy setup capabilites |
| public | void | RemoveScript | ------------ | Overrided function declared in TMC_Monobehaviour used to ensure all TMC scripts have easy remove capabilites |
| private | void | Update | ------------ | Update loop called by unity |
| public | void | StartCountdown | ------------ | Resets then starts the countdown |
| public | void | PauseCountdown | ------------ | Stops the countdown from running |
| private | void | ResetCountdown | ------------ | Resets the script to allow the countdown to run again |



<font size = "4"> Variables </font>

| Scope | namespace | Data Type | Name | Description |
| - | - | - | - | - |
| public | TaylorMadeCode | ScriptOptionData | GraphicalSettings | Used in the creation of the custom GUI to control data around all visual elements |
| public | TaylorMadeCode | ScriptOptionData | CountDownSettings | Used in the creation of the custom GUI to control data about the count down (What number to start at, what to end at ect.) |
| public | UnityEngine.UI | Text | Text | The text component that is used on screen |
| public | UnityEngine.UI | Image | background | The image component that is the background image for the countdown |
| public | System | int | StartAt | The number that the countdown starts at |
| public | System | int | FinishAt | The number that the countdown Finishes at |
| ------ | ----------------- | ----------------- | -------------------- | ------------------------------------------- |
| private | UnityEngine | GameObject | TextObj | The gameObject that Text is attached to |
| private | UnityEngine | GameObject | backgroundObj | The gameObject that the background is attached to |
| private | System | int | ToDisplay | The number that is currently being shown as text |
| private | System | float | timer | Timer that keeps track how long is passed |
| private | System | bool | CountDownStarted | This bool is used to check if the countdown should be occouring |



--- 