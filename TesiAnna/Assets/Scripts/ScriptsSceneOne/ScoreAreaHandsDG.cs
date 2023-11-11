using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System;
using UnityEngine.UI;

public class ScoreAreaHandsDG : MonoBehaviour
{
    public XRGrabInteractable[] XRGrabInteractable;

    public static int totScore = 0;
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

    public static int indexTextOneHand = 0;
    public static int totScoreEnd = 0;
    public static DateTime dateTimeStart;
    public static DateTime dateTimeEnd;
    private bool timeIsFinished = false;


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
        menuAtTheEnd.gameObject.SetActive(false);
        XRGrabInteractable = GetComponentsInChildren<XRGrabInteractable>();
        dateTimeStart = DateTime.Now;
        collectedTotObjectsTextH.text = "Total collected objects: " + totScore.ToString() + " of 25";
        collectedUnsortedObjectsTextH.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
        collectedGMObjectsTextH.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
        collectedPaperObjectsTextH.text = "Paper waste: " + paperScore.ToString() + " of 5";
        collectedOrganicObjectsTextH.text = "Organic waste: " + organicScore.ToString() + " of 5";
        collectedPlasticObjectsTextH.text = "Plastic waste: " + plasticScore.ToString() + " of 5";
        foreach (var interactable in XRGrabInteractable)
        {
            hasBeenGrabbed[interactable.gameObject.name] = false;
        }
    }
    private void Update()
    {

        
        if (!timeIsFinished && (totScore == 25 || Timer.timeIsUp == 1))
        {
            PlaySoundEnd();
            foreach (TMP_Text textElement in textElements)
            {
                textElement.gameObject.SetActive(false);
            }
            menuAtTheEnd.SetActive(true);
            totScoreEnd = totScore;
            dateTimeEnd = DateTime.Now;

            Timer scriptAInstance = FindObjectOfType<Timer>();

            if (scriptAInstance != null)
            {
                scriptAInstance.stopTimer();
            }
            timeIsFinished = true;    
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
    void OnTriggerEnter(Collider otherCollider)
    {
        string objectName = otherCollider.gameObject.name;
        string interactionType = "";

        if (otherCollider.CompareTag("Unsorted Waste"))
        {
            unsortedScore += 1;
            collectedUnsortedObjectsTextH.text = "Unsorted waste:  " + unsortedScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Unsorted Waste";
        }
        
        else if (otherCollider.CompareTag("Paper Waste"))
        {
            paperScore += 1;
            collectedPaperObjectsTextH.text = "Paper waste: " + paperScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Paper Waste";
        }
        else if (otherCollider.CompareTag("G&M Waste"))
        {
            gMScore += 1;
            collectedGMObjectsTextH.text = "Glass & Metal waste:  " + gMScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "G&M Waste";
        }
        else if (otherCollider.CompareTag("Organic Waste"))
        {
            organicScore += 1;
            collectedOrganicObjectsTextH.text = "Organic waste: " + organicScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Organic Waste";
        }
        else if (otherCollider.CompareTag("Plastic Waste"))
        {
            plasticScore += 1;
            collectedPlasticObjectsTextH.text = "Plastic waste: " + plasticScore.ToString() + " of 5";
            totScore += 1;
            PlaySound();
            interactionType = "Plastic waste";
        }
        collectedTotObjectsTextH.text = "Total collected objects: " + totScore.ToString() + " of 25";
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
