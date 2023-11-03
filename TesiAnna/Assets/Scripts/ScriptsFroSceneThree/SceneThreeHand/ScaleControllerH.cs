using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class ScaleControllerH : MonoBehaviour
{
   

    public List<GameObject> objectsToDeactivate = new List<GameObject>();

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public GameObject cubeManipulable;

    [Header("Cube after scale")]
    public GameObject cubeAfterScale;

    [Header("Mission state")]
    public TMP_Text requestText;
    public TMP_Text missionCompletedText;
    public TMP_Text fellOff;

    [Header("Return button")]
    public Button backToMenu;

    [Space]

    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;

    private bool hasBeenPlayed = false;

    private Vector3 originalScale;

    string dateTimeStart;
    string dateTimeEnd;
    int totScaleEnd = 0;
    public static int scaleDone = 0;

    private static int indexTextSThreeHands;

    [Space]
    [Header("Feedback color")]
    public Color isSmaller = Color.red;
    public Color isEqual = Color.green;
    public Color isBigger = Color.gray;

    [Space]

    [Header("End Menu")]
    public GameObject endMenu;


    private void Start()
    {
        endMenu.SetActive(false);
        dateTimeStart = System.DateTime.UtcNow.ToString();
        // Ensure that cube1 and cube2 are assigned in the Inspector
        if (cubeTarget == null || cubeManipulable == null)
        {
            Debug.LogError("Assign both cube1 and cube2 in the Inspector!");
            return;
        }
        cubeAfterScale.SetActive(false);
        originalScale = cubeManipulable.transform.localScale;       
        missionCompletedText.gameObject.SetActive(false);
       
    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = cubeManipulable.transform.localScale; 
        Vector3 positionToMatch = cubeManipulable.transform.position;
        
        // Change the color of the cube based on certain conditions
        if (sizeCube1.x * sizeCube1.y * sizeCube1.z == sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            requestText.gameObject.SetActive(false);
            Renderer cubeRenderer = cubeAfterScale.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
                scaleDone += 1;
            }
            if (!hasBeenPlayed)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
                hasBeenPlayed = true;
            }

            missionCompletedText.gameObject.SetActive(true);
            cubeAfterScale.transform.position = positionToMatch;
            cubeManipulable.SetActive(false);
            cubeAfterScale.SetActive(true);
            Debug.Log("Both cubes have the same size.");
            
        }
        else if(sizeCube1.x * sizeCube1.y * sizeCube1.z > sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            Debug.Log("Cube 1 is larger than Cube 2.");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isSmaller;
            }
        }
        else if(sizeCube1.x * sizeCube1.y * sizeCube1.z < sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            Debug.Log("Cube 2 is larger than Cube 1.");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isBigger;
            }
        }
        if (scaleDone == 4)
        {
            Invoke("PlaySound", 2f);
            DeactivateObjectsInList();
            activateEndMenu();
        }
    }
    public void activateEndMenu()
    {
        endMenu.SetActive(true);
    }
    public void DeactivateObjectsInList()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
    private void PlaySound()
    {
        if (audioSourceEnd != null && soundClipEnd != null)
        {

            audioSourceEnd.PlayOneShot(soundClipEnd);
        }
    }

    public void BackToMenu()
    {
        totScaleEnd = scaleDone;
        dateTimeEnd = System.DateTime.UtcNow.ToString();
        CSVManager.AppendToReport(GetReportLine());
        indexTextSThreeHands++;
        scaleDone = 0;
        ObjectResetPlaneForSceneThree.objectFellSceneThree = 0;
    }

    string[] GetReportLine()
    {
        string[] returnable = new string[8];
        returnable[0] = "SceneThree.csv";
        returnable[1] = "Hands";
        returnable[2] = "Ray-casting";
        returnable[3] = indexTextSThreeHands.ToString();
        returnable[4] = totScaleEnd.ToString();
        returnable[5] = ObjectResetPlaneForSceneThree.objectFellSceneThree.ToString();
        returnable[6] = dateTimeStart;
        returnable[7] = dateTimeEnd;


        return returnable;

    }

}


