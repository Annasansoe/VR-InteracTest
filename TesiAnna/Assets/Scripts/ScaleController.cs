using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{

    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Manipulable cube")]
    public GameObject cubeManipulable;

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

        originalScale = cubeManipulable.transform.localScale;


    }
    private void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = cubeManipulable.transform.localScale;

        

        // Change the color of the cube based on certain conditions
        if (sizeCube1.x * sizeCube1.y * sizeCube1.z == sizeCube2.x * sizeCube2.y * sizeCube2.z)
        {
            Debug.Log("Both cubes have the same size.");
            Renderer cubeRenderer = cubeManipulable.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material.color = isEqual;
            }
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
        else
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


