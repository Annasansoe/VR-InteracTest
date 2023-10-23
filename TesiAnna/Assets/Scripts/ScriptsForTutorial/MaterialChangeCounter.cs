using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeCounter : MonoBehaviour
{
    private Material currentMaterial;
    public static int materialChangeCount = 0;

    private void Start()
    {
        // Store the initial material of the cube.
        currentMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Check if the material has changed.
        if (currentMaterial != GetComponent<Renderer>().material)
        {
            // Material has changed. Increment the counter.
            materialChangeCount++;

            // Update the currentMaterial to the new material.
            currentMaterial = GetComponent<Renderer>().material;

            // You can optionally print the count for debugging purposes.
            Debug.Log("Material Change Count: " + materialChangeCount);
        }
    }
}
