using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    public int collectedObjects = 0;
    public int targetObjects = 10;

    private void OnTriggerEnter(Collider other)
    {
        CollectibleObject collectible = other.GetComponent<CollectibleObject>();
        if (collectible != null)
        {
            collectible.Collect();
            collectedObjects++;
            UpdateCollectedObjectsUI();

            if (collectedObjects >= targetObjects)
            {
                LevelCompleted();
            }
        }
    }

    private void UpdateCollectedObjectsUI()
    {
        UIManager.Instance.UpdateCollectedObjectsText(collectedObjects.ToString());
    }

    private void LevelCompleted()
    {
        // Handle level completion logic here
    }
}
