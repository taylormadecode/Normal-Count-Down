## Normal Countdown Documentation
---

### About Normal Countdown

This script was made to quickly and easily fit into any project as well as to be used by anyone at any skill level.

This is achieved by the use of UnityEvents (A method to call code via GUI), this is the same system the base UI button use for on click.

---

### Using Normal Countdown

   Using normal countdown is very simple, 

   1. Add Normal Countdown to an Game Object
   2. Click "Setup Countdown"
   3. (Optional) Change the art and font to make countdown more polished

   A video tutorial and a more in-depth written tutorial can be found at (https://www.taylormadecode.com/docs/tmc-normal-countdown)

---

### Technical details

#### Requirements
- Normal Countdown is compatible with unity Versions:
   - 2020.3 and later

#### Known limitations
- Normal Countdown Version 1.0.0 includes the following issues :
   - Can only support Unity Text Gui and not TextMeshPro Text

#### Package Content
   | File Directory | Description |
   | ------ | -------- |
   | Art/Sprites/Clock.png | Art used in the product photos |
   | Demo/Demo Scene.unity | Unity Scene to demonstrate the use of the Normal Countdown Script |
   | Documentation/Documentation.pdf | Contains information about the Asset (About the Script, How to use the script, Requirements, Known limitations and technical documentation) |
   | Scripts/TMC_Normal_Countdown.cs | File that contains all the code for the countdown |



#### Functions

Class is called TMC_Normal_Countdown and inherits from TMC_Monobehaviour 
(TMC_Monobehaviour supports the when to start and custom GUI)

- #### Functions

   | Scope | Return Value | Name | Parameters | Description |
   | - | - | - | - | - |
   | public | void | StartFunction | ------------ | Code needed to start the script (This function is used for the automatic start on Awake, start, enable and disable) |
   | public | void | SetupScript | ------------ | Override function declared in TMC_Monobehaviour use to ensure all TMC scripts have easy setup capabilities |
   | public | void | RemoveScript | ------------ | Override function declared in TMC_Monobehaviour used to ensure all TMC scripts have easy remove capabilities |
   | private | void | Update | ------------ | Update loop called by unity |
   | public | void | StartCountdown | ------------ | Resets then starts the countdown |
   | public | void | PauseCountdown | ------------ | Stops the countdown from running |
   | private | void | ResetCountdown | ------------ | Resets the script to allow the countdown to run again |



- #### Variables

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
   | private | System | bool | CountDownStarted | This bool is used to check if the countdown should be running |

---

#### Document Revision History
   | Date | Reason |
   | ------ | -------- |
   | 18/12/2021 | Initial Creation |

---