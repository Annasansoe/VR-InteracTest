using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using TMPro;
using UnityEngine.UI;
using System;

public class ScaleControllerDG : MonoBehaviour
{
    public List<GameObject> objectsToDeactivate = new List<GameObject>();

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public XRGeneralGrabTransformer cubeManipulable;

    [Header("Mission state")]
    public TMP_Text requestText;
    public TMP_Text missionCompletedText;

    [Header("Return button")]
    public Button backToMenu;

    [Space]

    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;

    [Space]

    [Header("End Menu")]
    public GameObject endMenu;

    [Space]
    [Header("Feedback color")]
    public Color isSmaller = Color.red;
    public Color isEqual = Color.green;
    public Color isBigger = Color.gray;

    private bool hasBeenPlayed = false;
    private bool sizesEqualized = false;

    private static int indexTextSThree = 0;
    static string dateTimeStart;
    static string finishScaleCube;
    public static string dateTimeEnd;
    static int totalFellObjects = 0;
    public static int scaleDone = 0;

    private bool timeIsFinished = false;

    public static List<InteractionData> interactionDataListStart = new List<InteractionData>();
    public class InteractionData
    {
        public DateTime Timestamp { get; set; }
        public string ObjectName { get; set; }
        public string InteractionType { get; set; }
    }

    private void Start()
    {
        endMenu.SetActive(false);
        dateTimeStart = DateTime.Now.ToString();
        // Ensure that cube1 and cube2 are assigned in the Inspector
        if (cubeTarget == null || cubeManipulable == null)
        {
            Debug.LogError("Assign both cube1 and cube2 in the Inspector!");
            return;
        }
        
       missionCompletedText.gameObject.SetActive(false);
       
    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = cubeManipulable.transform.localScale;
        XRGeneralGrabTransformer grabTransformer = cubeManipulable.GetComponent<XRGeneralGrabTransformer>();

        // Change the color of the cube based on certain conditions
        if (sizeCube2.x * sizeCube2.y * sizeCube2.z >= sizeCube1.x * sizeCube1.y * sizeCube1.z && !sizesEqualized)
        {
            requestText.gameObject.SetActive(false);
            if (grabTransformer != null)
            {
                grabTransformer.allowTwoHandedScaling = false;
            }
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
            }
            scaleDone += 1;
            sizesEqualized = true;
            if (!hasBeenPlayed)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
                finishScaleCube = DateTime.Now.ToString();
                hasBeenPlayed = true;
            }

            missionCompletedText.gameObject.SetActive(true);
            Debug.Log("Both cubes have the same size.");

        }
        else if (sizeCube1.x * sizeCube1.y * sizeCube1.z > sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            Debug.Log("Cube 1 is larger than Cube 2.");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isSmaller;
            }
        }

        if (!timeIsFinished && (scaleDone == 4 || Timer.timeIsUp == 1))
        {

            dateTimeEnd = DateTime.Now.ToString();
            Invoke("PlaySound", 2f);
            DeactivateObjectsInList();
            activateEndMenu();
            Timer scriptAInstance = FindObjectOfType<Timer>();

            if (scriptAInstance != null)
            {
                scriptAInstance.stopTimer();
            }
            timeIsFinished = true;

        }

    }

    

    public void PlaySound()
    {
        if (audioSourceEnd != null && soundClipEnd != null)
        {
            audioSourceEnd.PlayOneShot(soundClipEnd);
        }
    }
    public void SelecetedXRGrab(XRGeneralGrabTransformer XRGrabInteractable)
    {
        // The object was just grabbed.
        var interactionData = new InteractionData
        {
            Timestamp = DateTime.Now,
            ObjectName = XRGrabInteractable.name,
            InteractionType = "Start Grabbing"
        };
        interactionDataListStart.Add(interactionData);

    }
    public void DeactivateObjectsInList()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
    public void activateEndMenu()
    {
        endMenu.SetActive(true);
    }

    public void IsTimerFinished()
    {
        if (Timer.timeIsUp == 1)
        {
            timeIsFinished = true;
        }
    }
    public void BackToMenu()
    {
        //totScaleEnd = scaleDone;
        totalFellObjects = ObjectResetPlaneAll.objectFellCube + ObjectResetPlaneCap.objectFellCap + ObjectResetPlaneKey.objectFellKey + ObjectResetPlaneDrawers.objectFellDrawers;
        CSVManager.AppendToReport(GetReportLine());
        indexTextSThree++;
        ObjectResetPlaneAll.objectFellCube = 0;
        ObjectResetPlaneCap.objectFellCap = 0;
        ObjectResetPlaneKey.objectFellKey = 0;
        ObjectResetPlaneDrawers.objectFellDrawers = 0;
        ScaleControllerForDrawersDG.cubeDrawersResized = 0;
        scaleDone = 0;
    }


    public static string[] GetReportLine()
    {

        int i = 0;
        string[] returnable = new string[60];
        returnable[0] = "SceneThree.csv";
        returnable[1] = "Controllers";
        returnable[2] = "Direct";
        returnable[3] = indexTextSThree.ToString();
        returnable[4] = scaleDone.ToString();
        returnable[5] = totalFellObjects.ToString();
        returnable[6] = dateTimeStart;
        returnable[7] = dateTimeEnd;
        returnable[8] = ScaleControllerForZAxisCubeDG.finishScaleCap;
        returnable[9] = finishScaleCube;
        returnable[10] = ScaleControllerKeyDG.finishScaleKey;
        returnable[11] = ScaleControllerForDrawersDG.finishScaleBook1;
        returnable[12] = ObjectResetPlaneAll.objectFellCube.ToString();
        returnable[13] = ObjectResetPlaneCap.objectFellCap.ToString();
        returnable[14] = ObjectResetPlaneKey.objectFellKey.ToString();
        returnable[15] = ObjectResetPlaneDrawers.objectFellDrawers.ToString();

        foreach (InteractionData interaction in interactionDataListStart)
        {
            i++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[16 + i] = interactionLine;
        }

        return returnable;
    }

}


