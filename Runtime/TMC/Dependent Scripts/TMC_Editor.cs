using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TaylorMadeCode
{
    static public class TMC_Editor
    {
        public static GUIStyle SectionTitleFormatting()
        {
            GUIStyle SectionTitleStyle = new GUIStyle();
            SectionTitleStyle.alignment = TextAnchor.MiddleCenter;
            SectionTitleStyle.fontSize = 15;
            SectionTitleStyle.normal.textColor = Color.white;

            return SectionTitleStyle;
        }

        public static GUIStyle ScriptTitleFormatting()
        {
            GUIStyle SectionTitleStyle = new GUIStyle();
            SectionTitleStyle.alignment = TextAnchor.MiddleCenter;
            SectionTitleStyle.fontSize = 15;
            SectionTitleStyle.normal.textColor = Color.white;

            return SectionTitleStyle;
        }

        public static GUIStyle ScriptDiscFormmating()
        {
            GUIStyle SectionTitleStyle = new GUIStyle();
            SectionTitleStyle.alignment = TextAnchor.MiddleCenter;
            SectionTitleStyle.fontSize = 8;
            SectionTitleStyle.normal.textColor = Color.gray;

            return SectionTitleStyle;
        }

        public static void line()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        public static void title(string Title, string desc, bool includeLine = true)
        {
            TMC_Editor.line();
            EditorGUILayout.LabelField(Title, TMC_Editor.ScriptTitleFormatting());
            EditorGUILayout.LabelField(desc, TMC_Editor.ScriptDiscFormmating());

            if (includeLine)
                TMC_Editor.line();
        }

        public static void ShowGUINormaly(string NameOfVar, SerializedObject serializedObject)
        {
            if (serializedObject.FindProperty(NameOfVar).isArray == true)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.PropertyField(serializedObject.FindProperty(NameOfVar));
            }    
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(NameOfVar));
            }
        }
    }

    [Serializable]
    public struct ScriptOptionData
    {
        //- FoldOut Data bools
        public bool PreviousSelection;
        public bool CurrentSelection;

        public bool FoldOutGroup;

        //- Features Name and Descripton
        public string NameOfOption;

        //- Exslucsive Feature bool
        public bool ExclusiveFeature;
        public bool MandatoryOption;

        public ScriptOptionData(string a_NameOfOption, bool ab_Manditory, bool ab_ExclusiveFeature = true)
        {
            NameOfOption = a_NameOfOption;

            PreviousSelection = false;
            CurrentSelection = false;
            FoldOutGroup = true;
            MandatoryOption = ab_Manditory;
            ExclusiveFeature = ab_ExclusiveFeature;
        }
    }

    [Serializable]
    public struct ScriptOptions
    {
        public int OptionsPerRow;
        public List<int> WhatFeaturesToDisplay;

        public List<string> ScriptOptionNames;


        public ScriptOptions(List<string> option, int ai_OptionsPerRow)
        {
            ScriptOptionNames = option;
            OptionsPerRow = ai_OptionsPerRow;
            WhatFeaturesToDisplay = new List<int>();
        }

        public void GUICall(SerializedObject serializedObject, List<System.Func<int>> functionsToCall)
        {
            SerializedProperty Option;
            SerializedProperty previousSelection;
            SerializedProperty currentSelection;
            SerializedProperty FoldOutGroup;
            SerializedProperty NameOfOption;
            SerializedProperty IsOptionMandatory;

            SerializedProperty InternalLoop_Option;
            SerializedProperty InternalLoop_previousSelection;
            SerializedProperty InternalLoop_currentSelection;
            SerializedProperty InternalLoop_ExclusiveFeature;



            //- Toggle Off All sections Of Optional GUI Call -//
            WhatFeaturesToDisplay.Clear();

            //- Starting New Horizontal -//
            EditorGUILayout.BeginHorizontal();
            // Loop though all Script Options
            for (int i = 0, SelectableOptionCount = 0; i < ScriptOptionNames.Count; i++)
            {
                //- Getting Easy Acsess to all Data From Script Via Serialized Object -//
                Option = serializedObject.FindProperty(ScriptOptionNames[i]);
                previousSelection = Option.FindPropertyRelative("PreviousSelection");
                currentSelection = Option.FindPropertyRelative("CurrentSelection");
                NameOfOption = Option.FindPropertyRelative("NameOfOption");
                IsOptionMandatory = Option.FindPropertyRelative("MandatoryOption");

                //- If Manditory Add to render list and check next Option -//
                if (IsOptionMandatory.boolValue)
                {
                    WhatFeaturesToDisplay.Add(i);
                    continue;
                }

                //Horizontal Layout Control
                if (SelectableOptionCount % OptionsPerRow == 0)
                {
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                }

                //- Recording Last Frames Selecion to check for update -//
                previousSelection.boolValue = currentSelection.boolValue;

                //- Creates GUI To select this feature to be active
                //currentSelection.boolValue = EditorGUILayout.Toggle(NameOfOption.stringValue, currentSelection.boolValue);
                EditorGUILayout.PropertyField(currentSelection, new GUIContent(NameOfOption.stringValue));
                

                //- If new selection remove all other features that are mutaliy exclusive
                if (previousSelection.boolValue != currentSelection.boolValue)
                {
                    for (int j = 0; j < ScriptOptionNames.Count; j++)
                    {
                        if (i == j)
                            continue;

                        //- Getting Easy Acsess to all Data From Script Via Serialized Object -//
                        InternalLoop_Option = serializedObject.FindProperty(ScriptOptionNames[j]);
                        InternalLoop_previousSelection = InternalLoop_Option.FindPropertyRelative("PreviousSelection");
                        InternalLoop_currentSelection = InternalLoop_Option.FindPropertyRelative("CurrentSelection");
                        InternalLoop_ExclusiveFeature = InternalLoop_Option.FindPropertyRelative("ExclusiveFeature");

                        if (!InternalLoop_ExclusiveFeature.boolValue)
                            continue;

                        InternalLoop_currentSelection.boolValue = false;
                        InternalLoop_previousSelection.boolValue = false;
                    }
                }

                //- If Selected Render
                if (currentSelection.boolValue)
                {
                    WhatFeaturesToDisplay.Add(i);
                }

                //Increment Selectable Option Amount (using in correct option layout)
                SelectableOptionCount++;
            }

            EditorGUILayout.EndHorizontal();
            TMC_Editor.line();

            for (int i = 0; i < WhatFeaturesToDisplay.Count; i++)
            {
                //- Getting Easy Acsess to all Data From Script Via Serialized Object -//
                Option = serializedObject.FindProperty(ScriptOptionNames[WhatFeaturesToDisplay[i]]);
                FoldOutGroup = Option.FindPropertyRelative("FoldOutGroup");
                NameOfOption = Option.FindPropertyRelative("NameOfOption");

                FoldOutGroup.boolValue = EditorGUILayout.BeginFoldoutHeaderGroup(FoldOutGroup.boolValue, NameOfOption.stringValue);

                if (FoldOutGroup.boolValue)
                    functionsToCall[WhatFeaturesToDisplay[i]]();

                EditorGUILayout.EndFoldoutHeaderGroup();
                TMC_Editor.line();
            }
        }
    }
}