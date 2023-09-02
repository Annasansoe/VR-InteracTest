using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ScoreAreaForTutorialHand : MonoBehaviour
{
    public XRGrabInteractable[] XRGrabInteractable;

    static int totScore = 1;
    
    [Header("CollectedObjects")]
    public TMP_Text collectedTotObjectsText;

    void OnTriggerEnter(Collider otherCollider)
    {
        collectedTotObjectsText.text = "Total collected objects: " + totScore.ToString();
        totScore += 1;
        Destroy(otherCollider.gameObject);
    }



}
