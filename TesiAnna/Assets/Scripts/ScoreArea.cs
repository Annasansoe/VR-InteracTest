using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class ScoreArea : MonoBehaviour
{
    public XRGrabInteractable[] XRGrabInteractable;

    int score = 0;

    [Header("CollectedObjects")]
    public TMP_Text collectedObjectsText;

    private void Start()
    {
        collectedObjectsText.text = "Collected Objects: " + score.ToString();
        
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Coins"))
        {
            score += 1;
            collectedObjectsText.text = "Collected Objects: " + score.ToString();
            Destroy(otherCollider.gameObject);
        }
        
    }
}
