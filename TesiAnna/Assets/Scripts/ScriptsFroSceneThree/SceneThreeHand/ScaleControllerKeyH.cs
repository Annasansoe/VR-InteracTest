using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine;
using TMPro;

public class ScaleControllerKeyH : MonoBehaviour
{
    public List<GameObject> objectsToDeactivate = new List<GameObject>();

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public XRGeneralGrabTransformer cubeManipulable;

    [Header("Mission state")]
    public TMP_Text requestTextK;
    public TMP_Text missionCompletedTextK;

    [Space]

    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;


    [Space]
    [Header("Feedback color")]
    public Color isEqual = Color.green;
    public Color isBigger = Color.gray;

    [Space]

    [Header("End Menu")]
    public GameObject endMenu;

    private bool sizesEqualized = false;
    private bool hasBeenPlayed = false;
    public static string finishScaleKey;
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
        missionCompletedTextK.gameObject.SetActive(false);

    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = cubeManipulable.transform.localScale;
        XRGeneralGrabTransformer grabTransformer = cubeManipulable.GetComponent<XRGeneralGrabTransformer>();

        // Change the color of the cube based on certain conditions
        if (sizeCube1.x * sizeCube1.y * sizeCube1.z >= sizeCube2.x * sizeCube2.y * sizeCube2.z && !sizesEqualized)
        {
            requestTextK.gameObject.SetActive(false);
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
                finishScaleKey = DateTime.Now.ToString();
                hasBeenPlayed = true;
            }

            missionCompletedTextK.gameObject.SetActive(true);
            Debug.Log("Both cubes have the same size.");

        }
        else if (sizeCube1.x * sizeCube1.y * sizeCube1.z < sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            Debug.Log("The cubes are bigger than the target one..");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isBigger;
            }
        }

        if (ScaleControllerH.scaleDone == 4)
        {
            Invoke("PlaySound", 2f);
            DeactivateObjectsInList();
            activateEndMenu();
            ScaleControllerH.dateTimeEnd = DateTime.Now.ToString();
            //ScaleController.scaleDone = 0;
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