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

    private Vector3 originalScale;

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

        // Change the color of the cube based on certain conditions
        if (sizeCube1.z == sizeCube2.z)
        {
            Renderer cubeRenderer = cubeAfterScale.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
            }
            cubeAfterScale.transform.position = positionToMatch;
            cubeManipulable.SetActive(false);
            cubeAfterScale.SetActive(true);
            Debug.Log("Both cubes have the same size.");
            missionCompletedTextZ.gameObject.SetActive(true);
            requestTextZ.gameObject.SetActive(false);
        }
        else if (sizeCube1.z > sizeCube2.z)
        {
            Debug.Log("Cube 1 is larger than Cube 2.");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isSmaller;
            }
        }
        else if (sizeCube1.z < sizeCube2.z)
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
