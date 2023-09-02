using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ScoreAreaHands : MonoBehaviour
{
    public XRGrabInteractable[] XRGrabInteractable;

    static int totScore = 0;
    int unsortedScore = 0;
    int gMScore = 0;
    int paperScore = 0;
    int organicScore = 0;
    int plasticScore = 0;

    [Header("CollectedObjects")]
    public TMP_Text collectedTotObjectsTextH;
    public TMP_Text collectedUnsortedObjectsTextH;
    public TMP_Text collectedGMObjectsTextH;
    public TMP_Text collectedPaperObjectsTextH;
    public TMP_Text collectedOrganicObjectsTextH;
    public TMP_Text collectedPlasticObjectsTextH;


    private void Start()
    {
        collectedTotObjectsTextH.text = "Total collected objects: " + totScore.ToString() + " of 25";
        collectedUnsortedObjectsTextH.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
        collectedGMObjectsTextH.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
        collectedPaperObjectsTextH.text = "Paper waste: " + paperScore.ToString() + " of 5";
        collectedOrganicObjectsTextH.text = "Organic waste: " + organicScore.ToString() + " of 5";
        collectedPlasticObjectsTextH.text = "Plastic waste: " + plasticScore.ToString() + " of 5";

    }


   void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Unsorted Waste"))
        {
            unsortedScore += 1;
            collectedUnsortedObjectsTextH.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
            totScore += 1;
        }
        else if (otherCollider.CompareTag("G&M Waste"))
        {
            gMScore += 1;
            collectedGMObjectsTextH.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
            totScore += 1;
        }
        else if (otherCollider.CompareTag("Paper Waste"))
        {
            paperScore += 1;
            collectedPaperObjectsTextH.text = "Paper waste: " + paperScore.ToString() + " of 5";
            totScore += 1;
        }

        else if (otherCollider.CompareTag("Organic Waste"))
        {
            organicScore += 1;
            collectedOrganicObjectsTextH.text = "Organic waste: " + organicScore.ToString() + " of 5";
            totScore += 1;
        }
        else if (otherCollider.CompareTag("Plastic Waste"))
        {
            plasticScore += 1;
            collectedPlasticObjectsTextH.text = "Plastic waste: " + plasticScore.ToString() + " of 5";
            totScore += 1;
        }
        collectedTotObjectsTextH.text = "Total collected objects: " + totScore.ToString() + " of 25";
        Destroy(otherCollider.gameObject);
    }



}
