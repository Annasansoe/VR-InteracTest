using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleControllerForZAxisCube : MonoBehaviour
{

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public GameObject cubeManipulable;

    [Header("Cube after scale")]
    public GameObject cubeAfterScale;

    [Header("Mission state")]
    public TMP_Text requestTextZ;
    public TMP_Text missionCompletedTextZ;

    [Space]

    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;

    private bool hasBeenPlayed = false;

    private Vector3 originalScale;
    [Space]
    [Header("Feedback color")]
    public Color isSmaller = Color.red;
    public Color isEqual = Color.green;
    public Color isBigger = Color.gray;

    private void Start()
    {
        // Ensure that cube1 and cube2 are assigned in the Inspector
        if (cubeTarget == null || cubeManipulable == null)
        {
            Debug.LogError("Assign both cube1 and cube2 in the Inspector!");
            return;
        }
        cubeAfterScale.SetActive(false);
        originalScale = cubeManipulable.transform.localScale;
        missionCompletedTextZ.gameObject.SetActive(false);

    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = cubeManipulable.transform.localScale;
        Vector3 positionToMatch = cubeManipulable.transform.position;

        // Modify the Z-axis scale while keeping X and Y axes locked
        float newScaleX = sizeCube2.x + Input.GetAxis("Vertical") * Time.deltaTime;

        // Clamp the new Z-axis scale to a desired range if necessary
        //newScaleZ = Mathf.Clamp(newScaleZ, minScaleZ, maxScaleZ); // Adjust minScaleZ and maxScaleZ as needed

        // Apply the new local scale with Z-axis modification
        transform.localScale = new Vector3(newScaleX, sizeCube2.y, sizeCube2.z);

        // Change the color of the cube based on certain conditions
        if (sizeCube1.x == newScaleX)
        {
            Renderer cubeRenderer = cubeAfterScale.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
                ScaleController.scaleDone++;
            }
            cubeAfterScale.transform.position = positionToMatch;
            cubeManipulable.SetActive(false);
            cubeAfterScale.SetActive(true);
            Debug.Log("Both cubes have the same size.");
            missionCompletedTextZ.gameObject.SetActive(true);
           
            if (!hasBeenPlayed)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
                hasBeenPlayed = true;
            }
            requestTextZ.gameObject.SetActive(false);
        }
        else if (sizeCube1.x > newScaleX)
        {
            Debug.Log("Cube 1 is larger than Cube 2.");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isSmaller;
            }
        }
        else if (sizeCube1.x < newScaleX)
        {
            Debug.Log("Cube 2 is larger than Cube 1.");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isBigger;
            }
        }
    }

}
