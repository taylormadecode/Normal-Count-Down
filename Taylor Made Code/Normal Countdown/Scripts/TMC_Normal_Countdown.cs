using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace TaylorMadeCode
{
    [AddComponentMenu("TMC/Genral/Normal Countdown")]
    public class TMC_Normal_Countdown : TMC_MonoBehaviour<TMC_Count_down_GUI>
    {
        public ScriptOptionData GraphicalSettings = new ScriptOptionData("Graphical Elements", true);
        public ScriptOptionData CountDownSettings = new ScriptOptionData("Count Down Settings", true);

        private GameObject TextObj;
        public Text Text;
            
        private GameObject BackgroundObj;
        public Image Background;
            
        public int StartAt;
        public int FinishAt;

        public UnityEvent OnNumberChange;

        public int ToDisplay;
        private float Timer;
        private bool CountDownStarted;

        public override void StartFunction()
        {
            StartCountdown();
        }

        public override void SetupScript()
        {
            //- Make the UI Elements child of canvas to work correctly -/
            TMC.CreateGUISystem();

            //- Create the logic behind creating the text and image -//
            BackgroundObj = new GameObject("Background", typeof(Image));

            BackgroundObj.transform.SetParent(GameObject.Find("Canvas").transform);
            Background = BackgroundObj.GetComponent<Image>();

            //- Create the Text Object -//
            TextObj = new GameObject("Text Object", typeof(Text));

            TextObj.transform.SetParent(BackgroundObj.transform);
            TextObj.GetComponent<Text>().text = "Text";
            TextObj.GetComponent<Text>().color = Color.black;
            TextObj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            Text = TextObj.GetComponent<Text>();

            HasBeenSetup = true;

            StartAt = 3;
            FinishAt = 0;
        }

        public override void RemoveScript()
        {
            Undo.DestroyObjectImmediate(Text.gameObject);
            Undo.DestroyObjectImmediate(Background.gameObject);
            HasBeenSetup = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (CountDownStarted)
            {
                if (ToDisplay == FinishAt)
                {
                    Text.enabled = false;
                    Background.enabled = false;
                    PauseCountdown();

                    if (AfterScriptEvent != null) 
                        AfterScriptEvent.Invoke();
                }

                //  Stop at FinishAt -----  Count down in seconds
                if (ToDisplay > FinishAt && Timer < 0)
                {
                    ToDisplay--;
                    Timer = 1;

                    if (OnNumberChange != null) 
                        OnNumberChange.Invoke();
                }
                else
                {
                    Timer -= Time.deltaTime;
                    Text.text = ToDisplay.ToString();
                }
            }
        }

        public void StartCountdown()
        {
            ResetCountdown();
            CountDownStarted = true;

            if (BeforeScriptEvent != null)
                BeforeScriptEvent.Invoke();
        }

        public void PauseCountdown()
        {
            CountDownStarted = false;
        }

        private void ResetCountdown()
        {
            CountDownStarted = false;
            ToDisplay = StartAt;
            Timer = 1;
            Text.enabled = true;
            Text.text = ToDisplay.ToString();
            Background.enabled = true;
        }

    }
    //--------------------------- End Of Class -------------------------//


    [CustomEditor(typeof(TMC_Normal_Countdown))]
    public class TMC_Count_down_GUI : Editor
    {
        //- Allows Refrance Back To Main Script -//
        TMC_Normal_Countdown Self;
        List<System.Func<int>> funcs;
        public ScriptOptions m_scriptOptions;
        
        public void Awake()
        {
            Self = (TMC_Normal_Countdown)target;
            Self.RefToCustomGUIClass = this;
            funcs = new List<System.Func<int>>();


            System.Func<int> GraphicalElement = () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Text"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Background"));
                return 0;
            };
            System.Func<int> CountDownSettings = () =>
            {
                Self.StartAt = EditorGUILayout.IntField("Start Counting From", Self.StartAt);
                Self.FinishAt = EditorGUILayout.IntField("Stop Counting At", Self.FinishAt);
                return 0;
            };
            System.Func<int> Settings = () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("StartScriptOn"));
                return 0;
            };
            System.Func<int> Event = () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("BeforeScriptEvent"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnNumberChange"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("AfterScriptEvent"));
                return 0;
            };

            funcs.Add(GraphicalElement);
            funcs.Add(CountDownSettings);
            funcs.Add(Settings);
            funcs.Add(Event);

            List<string> OptionMenu = new List<string>()
            {
                "GraphicalSettings",
                "CountDownSettings",
                "Settings",
                "Events"
            };

            m_scriptOptions = new ScriptOptions(OptionMenu, 2);
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            TMC_Editor.title("Normal Countdown", "Scripts counts down between two given numbers then runs an event", true);

            if (serializedObject.FindProperty("HasBeenSetup").boolValue == false)
            {
                if (GUILayout.Button("Set Up Countdown"))
                {
                    Self.SetupScript();
                }

                TMC_Editor.line();
            }
            else
            {
                if (GUILayout.Button("Remove Countdown"))
                {
                    Self.RemoveScript();
                }
                m_scriptOptions.GUICall(serializedObject, funcs);
            }

            if (EditorGUI.EndChangeCheck())
            {
                 serializedObject.ApplyModifiedProperties();
            }
        }
    } 
}
