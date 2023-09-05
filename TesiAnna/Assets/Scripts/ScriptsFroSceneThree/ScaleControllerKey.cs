using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleControllerKey : MonoBehaviour
{

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public GameObject cubeManipulable;

    [Header("Cube after scale")]
    public GameObject cubeAfterScale;

    [Header("Mission state")]
    public TMP_Text requestTextK;
    public TMP_Text missionCompletedTextK;

    private Vector3 originalScale;


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
               ScaleController.scaleDone++;
            }
            cubeAfterScale.transform.position = positionToMatch;
            cubeManipulable.SetActive(false);
            cubeAfterScale.SetActive(true);
            missionCompletedTextK.gameObject.SetActive(true);
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


    }

}