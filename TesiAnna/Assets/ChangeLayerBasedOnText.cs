using UnityEngine;
using TMPro;
public class ChangeLayerBasedOnText : MonoBehaviour
{
    public string targetText = "DG"; // The target text to trigger the layer change
    public LayerMask newLayer; // The new layer (e.g., Ignore Raycasting)
    public LayerMask defaultLayer; // The default layer (e.g., Default)

    void Start()
    {
        // Find all game objects with the specific text in the scene
        GameObject[] objectsWithText = GameObject.FindGameObjectsWithTag("YourTag"); // Replace "YourTag" with your actual tag or use other criteria to find objects

        foreach (GameObject obj in objectsWithText)
        {
            TMP_Text textComponent = obj.GetComponent<TMP_Text>(); // Replace with your actual text component reference
            if (textComponent != null && textComponent.text == targetText)
            {
                // Change the layer to the new layer
                obj.layer = newLayer;
            }
            else
            {
                // Set the layer back to the default layer
                obj.layer = defaultLayer;
            }
        }
    }
}