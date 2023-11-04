using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine;
using TMPro;
using System;

public class ScaleControllerForDrawers : MonoBehaviour 
{
    public List<GameObject> objectsToDeactivate = new List<GameObject>();

    [Header("Target cube")]
    public GameObject cubeTargetD;

    [Header("Manipulable cube")]
    public XRGeneralGrabTransformer cubeManipulableD;

    [Header("Mission state")]
    public TMP_Text requestTextD;
    public TMP_Text missionCompletedTextD;

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
    static int cubeDrawersResized;
    public static string finishScaleBook1;

    private void Start()
    {
        // Ensure that cube1 and cube2 are assigned in the Inspector
        if (cubeTargetD == null || cubeManipulableD == null)
        {
            Debug.LogError("Assign both cube1 and cube2 in the Inspector!");
            return;
        }
       
        missionCompletedTextD.gameObject.SetActive(false);
        
    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTargetD.transform.localScale;
        Vector3 sizeCube2 = cubeManipulableD.transform.localScale;

        XRGeneralGrabTransformer grabTransformer = cubeManipulableD.GetComponent<XRGeneralGrabTransformer>();
        // Change the color of the cube based on certain conditions
        if (sizeCube1.x * sizeCube1.y * sizeCube1.z >= sizeCube2.x * sizeCube2.y * sizeCube2.z && !sizesEqualized)
        {
            if (grabTransformer != null)
            {
                grabTransformer.allowTwoHandedScaling = false;

            }
            Renderer cubeRenderer = cubeManipulableD.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
            }
            cubeDrawersResized+=1;
            sizesEqualized = true;
           

            Debug.Log("Both cubes have the same size.");
            
        }
        else if (sizeCube1.x * sizeCube1.y * sizeCube1.z < sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            Debug.Log("The cubes are bigger than the target one..");
            Renderer cubeRenderer = cubeManipulableD.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isBigger;
            }
        }
  

        if (cubeDrawersResized == 4)
        {
            ScaleController.scaleDone += 1;
            cubeDrawersResized = 5;
            missionCompletedTextD.gameObject.SetActive(true);
            requestTextD.gameObject.SetActive(false);
            if (!hasBeenPlayed)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
                finishScaleBook1 = DateTime.Now.ToString();
                hasBeenPlayed = true;

            }
        }

        if (ScaleController.scaleDone == 4)
        {
            Invoke("PlaySound", 2f);
            ScaleController.scaleDone = 0;
            DeactivateObjectsInList();
            activateEndMenu();
            ScaleController.dateTimeEnd = DateTime.UtcNow.ToString();
            //ScaleController.scaleDone = 0;
        }

    }

    public void activateEndMenu()
    {
        endMenu.SetActive(true);
    }

    public void PlaySound()
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