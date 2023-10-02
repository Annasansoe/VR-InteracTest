using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

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
    public TMP_Text youDidItH;


    [Header("Return button")]
    public Button backToMenu;
    static int indexTextOneHand = 0;
    int totScoreEnd = 0;
    string dateTimeStart;
    string dateTimeEnd;

    [Header("Return button")]
    public AudioSource audioSource;
    public AudioClip soundClip;

    private bool hasBeenPlayed = false;

    private void Start()
    {
        youDidItH.gameObject.SetActive(false);
        dateTimeStart = System.DateTime.UtcNow.ToString();
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
            PlaySound();
        }
        else if (otherCollider.CompareTag("G&M Waste"))
        {
            gMScore += 1;
            collectedGMObjectsTextH.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
        }
        else if (otherCollider.CompareTag("Paper Waste"))
        {
            paperScore += 1;
            collectedPaperObjectsTextH.text = "Paper waste: " + paperScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
        }

        else if (otherCollider.CompareTag("Organic Waste"))
        {
            organicScore += 1;
            collectedOrganicObjectsTextH.text = "Organic waste: " + organicScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
        }
        else if (otherCollider.CompareTag("Plastic Waste"))
        {
            plasticScore += 1;
            collectedPlasticObjectsTextH.text = "Plastic waste: " + plasticScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
        }
        collectedTotObjectsTextH.text = "Total collected objects: " + totScore.ToString() + " of 25";
        Destroy(otherCollider.gameObject);

        if (totScore == 25)
        {
            youDidItH.gameObject.SetActive(true);
            collectedTotObjectsTextH.gameObject.SetActive(false);
            collectedUnsortedObjectsTextH.gameObject.SetActive(false);
            collectedGMObjectsTextH.gameObject.SetActive(false);
            collectedPaperObjectsTextH.gameObject.SetActive(false);
            collectedOrganicObjectsTextH.gameObject.SetActive(false);
            collectedPlasticObjectsTextH.gameObject.SetActive(false);
        }
   }
    void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }

    public void BackToMenu()
    {
        totScoreEnd = totScore;
        dateTimeEnd = System.DateTime.UtcNow.ToString();
        CSVManager.AppendToReport(GetReportLine());
        indexTextOneHand++;
        totScore = 0;
        ObjectResetPlaneForSceneOne.objectFell = 0;
        
    }
    string[] GetReportLine()
    {
        string[] returnable = new string[7];
        returnable[0] = "SceneOne.csv";
        returnable[1] = "Hands";
        returnable[2] = indexTextOneHand.ToString();
        returnable[3] = totScoreEnd.ToString();
        returnable[4] = ObjectResetPlaneForSceneOne.objectFell.ToString();
        returnable[5] = dateTimeStart;
        returnable[6] = dateTimeEnd;


        return returnable;
    }



}
