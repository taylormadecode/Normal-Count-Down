using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TaylorMadeCode
{
    //- TMC CLASSES -//
    static public class TMC
    {
        static public bool ON = true;
        static public bool OFF = false;

        static public List<GameObject> GetAllObjectsThatIsMasked(LayerMask mask)
        {
            //Create List
            List<GameObject> InLayer = new List<GameObject>();
            
            //Get All Transform Components (Only mandatory component so will get all Objects)
            Transform[] transforms = GameObject.FindObjectsOfType<Transform>();
            
            // if the Objects are in the layer add to InLayer List
            foreach (Transform transform in transforms)
            {
                if (transform.gameObject.layer == mask)
                {
                    InLayer.Add(transform.gameObject);
                }
            }

            //Return whats in the layer
            return InLayer;
        }

        static public bool AreObjectsColliding(GameObject A, GameObject B)
        {
            //ToDo: See if this can be improved
            //Checks if there is some type of collider attached to the object if not then print an error
            if (A.GetComponent<Collider>() == null || B.GetComponent<Collider>() == null)
                Debug.LogError("Please Ensure both " + A.name + " and " + B.name + "Have a collider Attached");

            //POSSIBLE_ISSUE: This only returns true if the Mesh's Itercents. Possible issue with large meshes collising with small meshes
            // Check if Mesh intercets and return the responce
            return A.GetComponent<Collider>().bounds.Intersects(B.GetComponent<Collider>().bounds);
        }

        static public bool HasGUISystemBeenCreated()
        {
            if ((GameObject.Find("Canvas") != null) && (GameObject.Find("Event System") != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool CreateGUISystem()
        {
            GameObject canvas;
            canvas = GameObject.Find("Canvas");
            if (canvas == null)
            {
                canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        
                Canvas CanvasComponent = canvas.GetComponent<Canvas>();
                CanvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
                CanvasComponent.pixelPerfect = false;
                CanvasComponent.sortingOrder = 0;
                CanvasComponent.targetDisplay = 0;
                CanvasComponent.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
                
                CanvasScaler CanvasScalerComponent = canvas.GetComponent<CanvasScaler>();
                CanvasScalerComponent.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                CanvasScalerComponent.scaleFactor = 1;
                CanvasScalerComponent.referencePixelsPerUnit = 100;

                GraphicRaycaster GraphicsRaycasterComponent = canvas.GetComponent<GraphicRaycaster>();
                GraphicsRaycasterComponent.ignoreReversedGraphics = true;
                GraphicsRaycasterComponent.blockingObjects = GraphicRaycaster.BlockingObjects.None;
                GraphicsRaycasterComponent.blockingMask = LayerMask.NameToLayer("Everything");

                 //POSSIBLE_ISSUE: This might need to be dynamicly changed by unity settings (Need to do more research and understand both Unity UI and Event Systems)
            }

            //- If Event System is already created dont re-create -//
            GameObject EventSystem;
            EventSystem = GameObject.Find("Event System");
            if (EventSystem == null)
            {
                EventSystem = new GameObject("Event System", typeof(EventSystem), typeof(StandaloneInputModule));
            }

            return true;
        }
    }

    [System.Serializable]
    public abstract class TMC_MonoBehaviour<T> : MonoBehaviour
    {
        // Custom GUI Class Requirements
        public T RefToCustomGUIClass;

        // GUI Script Creation and remove
        public abstract void SetupScript();
        public abstract void RemoveScript();
        public bool HasBeenSetup = false;

        //----- Custom GUI Class Settings And Events Options ----//
        // When To Settings GUI Requirement
        public ScriptOptionData Settings = new ScriptOptionData("Settings", true);
        public TaylorMadeCode.WhenToStart StartScriptOn;
        
        public abstract void StartFunction();

        private void Start() {
            if (StartScriptOn == TaylorMadeCode.WhenToStart.Start)
                StartFunction();
        }

        private void Awake() {
            if (StartScriptOn == TaylorMadeCode.WhenToStart.Awake)
                StartFunction();
        }

        private void OnEnable() {
            if (StartScriptOn == TaylorMadeCode.WhenToStart.OnEnable)
                StartFunction();
        }

        private void OnDisable() {
            if (StartScriptOn == TaylorMadeCode.WhenToStart.OnDisable)
                StartFunction();
        }


        // Events GUI Requirements
        public ScriptOptionData Events = new ScriptOptionData("Events", true);
        public UnityEvent BeforeScriptEvent;
        public UnityEvent AfterScriptEvent;
    }

    //- TMC ENUMS -//

    public enum WhenToStart
    {
        DontStartAutomaticly = 0,
        Start = 1,
        Awake = 2,
        OnEnable = 3,
        OnDisable = 4
    }

    public enum ToToggle
    {
        ToggleOn = 1,
        ToggleOff = 0
    }
}