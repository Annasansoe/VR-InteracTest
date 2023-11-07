using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using System;

public class ScoreArea : MonoBehaviour
{
    public XRGrabInteractable[] XRGrabInteractable;

    public static int totScore = 0;
    int unsortedScore = 0;
    int gMScore = 0;
    int paperScore = 0;
    int organicScore = 0;
    int plasticScore = 0;
    private bool timeIsFinished = false;

    [Header("CollectedObjects")]
    public TMP_Text collectedTotObjectsText;
    public TMP_Text collectedUnsortedObjectsText;
    public TMP_Text collectedGMObjectsText;
    public TMP_Text collectedPaperObjectsText;
    public TMP_Text collectedOrganicObjectsText;
    public TMP_Text collectedPlasticObjectsText;
    public GameObject menuAtTheEnd;

    [Header("Return button")]
    public Button backToMenu;
    

    [Header("Audios")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;

    [Header("Text to disable at the end")]
    public TMP_Text[] textElements;

    //PROVA FILE
    public static int indexText;
    public static int totScoreEnd = 0;
    public static DateTime dateTimeStart;
    public static DateTime dateTimeEnd;

    public static List<InteractionData> interactionDataList = new List<InteractionData>();
    public static List<InteractionData> interactionDataListStart = new List<InteractionData>();


    private Dictionary<string, bool> hasBeenGrabbed = new Dictionary<string, bool>();
    public class InteractionData
    {
        public DateTime Timestamp { get; set; }
        public string ObjectName { get; set; }
        public string InteractionType { get; set; }
    }

    

    private void Start()
    {
        timeIsFinished = false;
        menuAtTheEnd.SetActive(false);
        XRGrabInteractable = GetComponentsInChildren<XRGrabInteractable>();
        
        dateTimeStart = DateTime.Now;
        collectedTotObjectsText.text = "Total collected objects: " + totScore.ToString() + " of 25";
        collectedUnsortedObjectsText.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
        collectedGMObjectsText.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
        collectedPaperObjectsText.text = "Paper waste: " + paperScore.ToString() + " of 5";
        collectedOrganicObjectsText.text = "Organic waste: " + organicScore.ToString() + " of 5";
        collectedPlasticObjectsText.text = "Plastic waste: " + plasticScore.ToString() + " of 5";
        foreach (var interactable in XRGrabInteractable)
        {
            hasBeenGrabbed[interactable.gameObject.name] = false;
        }
    }

    private void Update()
    {
        
        if (totScore == 25 || Timer.timeIsUp == 1)
        {
            if (!timeIsFinished)
            {
                PlaySoundEnd();
                foreach (TMP_Text textElement in textElements)
                {
                    textElement.gameObject.SetActive(false);
                }
                menuAtTheEnd.SetActive(true);
                totScoreEnd = totScore;
                dateTimeEnd = DateTime.Now;
                timeIsFinished = true;
            }
        }
    }

    public void SelecetedXRGrab(XRGrabInteractable XRGrabInteractable)
    {
        // The object was just grabbed.
        var interactionData = new InteractionData
        {
            Timestamp = DateTime.Now,
            ObjectName = XRGrabInteractable.name,
            InteractionType = "Start Grabbing"
        };
        interactionDataListStart.Add(interactionData);
            
    }


    /*public void IsTimerFinished()
    {
        if (Timer.timeIsUp == 1)
        {
            timeIsFinished = true;
        }
    }
    */

    void OnTriggerEnter(Collider otherCollider)
    {
        string objectName = otherCollider.gameObject.name;
        string interactionType = "";

        if (otherCollider.CompareTag("Unsorted Waste"))
        {
            unsortedScore += 1;
            collectedUnsortedObjectsText.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Unsorted Waste";
        }

        else if (otherCollider.CompareTag("G&M Waste"))
        {
            gMScore += 1;
            collectedGMObjectsText.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "G&M Waste";
        }
        else if (otherCollider.CompareTag("Paper Waste"))
        {
            paperScore += 1;
            collectedPaperObjectsText.text = "Paper waste: " + paperScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Paper Waste";
        }

        else if (otherCollider.CompareTag("Organic Waste"))
        {
            organicScore += 1;
            collectedOrganicObjectsText.text = "Organic waste: " + organicScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Organic Waste";
        }
        else if (otherCollider.CompareTag("Plastic Waste"))
        {
            plasticScore += 1;
            collectedPlasticObjectsText.text = "Plastic waste: " + plasticScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Plastic waste";

        }
        collectedTotObjectsText.text = "Total collected objects: " + totScore.ToString() + " of 25";

        InteractionData trashDisposalData = new InteractionData
        {
            Timestamp = DateTime.Now,
            ObjectName = objectName,
            InteractionType = interactionType 
        };

        interactionDataList.Add(trashDisposalData);
        Destroy(otherCollider.gameObject);

        
    }
    

    void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }

    void PlaySoundEnd()
    {
        if (audioSourceEnd != null && soundClipEnd != null)
        {
            audioSourceEnd.PlayOneShot(soundClipEnd);
        }
    }
  
}



