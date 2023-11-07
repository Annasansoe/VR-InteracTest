using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine;
using TMPro;

public class ScaleControllerForZAxisCubeH : MonoBehaviour
{
    public List<GameObject> objectsToDeactivate = new List<GameObject>();

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public XRGeneralGrabTransformer cubeManipulable;

    [Header("Mission state")]
    public TMP_Text requestTextZ;
    public TMP_Text missionCompletedTextZ;

    [Space]

    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;

    
    [Space]
    [Header("Feedback color")]
    public Color isSmaller = Color.red;
    public Color isEqual = Color.green;
    public Color isBigger = Color.gray;
    [Space]

    [Header("End Menu")]
    public GameObject endMenu;

    private bool hasBeenPlayed = false;
    private bool sizesEqualized = false;
    public static string finishScaleCap;
    public static DateTime dateTimeEnd;

    private void Start()
    {
        endMenu.SetActive(false);
        // Ensure that cube1 and cube2 are assigned in the Inspector
        if (cubeTarget == null || cubeManipulable == null)
        {
            Debug.LogError("Assign both cube1 and cube2 in the Inspector!");
            return;
        }
       
        missionCompletedTextZ.gameObject.SetActive(false);

    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = cubeManipulable.transform.localScale;
        XRGeneralGrabTransformer grabTransformer = cubeManipulable.GetComponent<XRGeneralGrabTransformer>();

        // Modify the Z-axis scale while keeping X and Y axes locked
        if (sizeCube2.x * sizeCube2.y * sizeCube2.z >= sizeCube1.x * sizeCube1.y * sizeCube1.z && !sizesEqualized)
        {
            requestTextZ.gameObject.SetActive(false);
            if (grabTransformer != null)
            {
                grabTransformer.allowTwoHandedScaling = false;

            }
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
            }
            ScaleControllerH.scaleDone += 1;
            sizesEqualized = true;
            if (!hasBeenPlayed)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
                finishScaleCap = DateTime.Now.ToString();
                hasBeenPlayed = true;
            }

            missionCompletedTextZ.gameObject.SetActive(true);
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

        if (ScaleControllerH.scaleDone == 4)
        {
            Invoke("PlaySound", 2f);
            DeactivateObjectsInList();
            activateEndMenu();
            ScaleControllerH.dateTimeEnd = DateTime.Now.ToString();
            /* ScaleController instanceScoreManager = new ScaleController();
             instanceScoreManager.BackToMenu();*/
            //ScaleController.scaleDone =0;
        }
    }
    public void activateEndMenu()
    {
        endMenu.SetActive(true);
    }
    private void PlaySound()
    {
        if (audioSourceEnd != null && soundClipEnd != null)
        {

            audioSourceEnd.PlayOneShot(soundClipEnd);
        }
    }
    public void DeactivateObjectsInList()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }

}
