using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ScoreArea : MonoBehaviour
{
    public XRGrabInteractable[] XRGrabInteractable;

    static int totScore = 0;
    int unsortedScore = 0;
    int gMScore = 0;
    int paperScore = 0;
    int organicScore = 0;
    int plasticScore = 0;

    [Header("CollectedObjects")]
    public TMP_Text collectedTotObjectsText;
    public TMP_Text collectedUnsortedObjectsText;
    public TMP_Text collectedGMObjectsText;
    public TMP_Text collectedPaperObjectsText;
    public TMP_Text collectedOrganicObjectsText;
    public TMP_Text collectedPlasticObjectsText;


    private void Start()
    {
        collectedTotObjectsText.text = "Total collected objects: " + totScore.ToString() + " of 25";
        collectedUnsortedObjectsText.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
        collectedGMObjectsText.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
        collectedPaperObjectsText.text = "Paper waste: " + paperScore.ToString() + " of 5";
        collectedOrganicObjectsText.text = "Organic waste: " + organicScore.ToString() + " of 5";
        collectedPlasticObjectsText.text = "Plastic waste: " + plasticScore.ToString() + " of 5";

    }


   void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Unsorted Waste"))
        {
            unsortedScore += 1;
            collectedUnsortedObjectsText.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
            totScore += 1;
        }
        else if (otherCollider.CompareTag("G&M Waste"))
        {
            gMScore += 1;
            collectedGMObjectsText.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
            totScore += 1;
        }
        else if (otherCollider.CompareTag("Paper Waste"))
        {
            paperScore += 1;
            collectedPaperObjectsText.text = "Paper waste: " + paperScore.ToString() + " of 5";
            totScore += 1;
        }

        else if (otherCollider.CompareTag("Organic Waste"))
        {
            organicScore += 1;
            collectedOrganicObjectsText.text = "Organic waste: " + organicScore.ToString() + " of 5";
            totScore += 1;
        }
        else if (otherCollider.CompareTag("Plastic Waste"))
        {
            plasticScore += 1;
            collectedPlasticObjectsText.text = "Plastic waste: " + plasticScore.ToString() + " of 5";
            totScore += 1;
        }
        collectedTotObjectsText.text = "Total collected objects: " + totScore.ToString() + " of 25";
        Destroy(otherCollider.gameObject);
    }



}
