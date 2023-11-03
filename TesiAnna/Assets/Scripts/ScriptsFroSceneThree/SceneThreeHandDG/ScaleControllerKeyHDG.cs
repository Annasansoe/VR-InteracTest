using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleControllerKeyHDG : MonoBehaviour
{
    public List<GameObject> objectsToDeactivate = new List<GameObject>();

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public GameObject cubeManipulable;

    [Header("Cube after scale")]
    public GameObject cubeAfterScale;

    [Header("Mission state")]
    public TMP_Text requestTextK;
    public TMP_Text missionCompletedTextK;

    [Space]

    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;

    private bool hasBeenPlayed = false;

    private Vector3 originalScale;

    [Space]
    [Header("Feedback color")]
    public Color isEqual = Color.green;
    public Color isBigger = Color.gray;

    [Space]

    [Header("End Menu")]
    public GameObject endMenu;

    private void Start()
    {
        endMenu.SetActive(false);
        // Ensure that cube1 and cube2 are assigned in the Inspector
        if (cubeTarget == null || cubeManipulable == null)
        {
            Debug.LogError("Assign both cube1 and cube2 in the Inspector!");
            return;
        }
        cubeAfterScale.SetActive(false);
        originalScale = cubeManipulable.transform.localScale;

        missionCompletedTextK.gameObject.SetActive(false);

    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = cubeManipulable.transform.localScale;
        Vector3 positionToMatch = cubeManipulable.transform.position;

        // Change the color of the cube based on certain conditions
        if (sizeCube1.x * sizeCube1.y * sizeCube1.z >= sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            Renderer cubeRenderer = cubeAfterScale.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
               
            }
            cubeAfterScale.transform.position = positionToMatch;
            ScaleControllerH.scaleDone += 1;
            cubeManipulable.SetActive(false);
            cubeAfterScale.SetActive(true);
            missionCompletedTextK.gameObject.SetActive(true);
            if (!hasBeenPlayed)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
                hasBeenPlayed = true;
            }
            requestTextK.gameObject.SetActive(false);
            Debug.Log("The cubes are smaller than the target one.");
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