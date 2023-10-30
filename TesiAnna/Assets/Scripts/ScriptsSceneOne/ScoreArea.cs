using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;

public class ScoreArea : MonoBehaviour
{
    

    public XRGrabInteractable[] XRGrabInteractable;
    public GameObject[] Bins;

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
    public GameObject menuAtTheEnd;

    [Header("Return button")]
    public Button backToMenu;
    int totScoreEnd = 0;
    DateTime dateTimeStart;
    DateTime dateTimeEnd;

    [Header("Audios")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;

    [Header("Text to disable at the end")]
    public TMP_Text[] textElements;

    private bool hasBeenPlayed = false;
    //PROVA FILE
    private static int indexText;

    private List<InteractionData> interactionDataList = new List<InteractionData>();
    private List<InteractionData> interactionDataListStart = new List<InteractionData>();


    private Dictionary<string, bool> hasBeenGrabbed = new Dictionary<string, bool>();
    public class InteractionData
    {
        public DateTime Timestamp { get; set; }
        public string ObjectName { get; set; }
        public string InteractionType { get; set; }
    }

    

    private void Start()
    {
        menuAtTheEnd.SetActive(false);
        //START csv
        // Construct the full file path using persistentDataPath
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
    public void OnSelect(XRGrabInteractable interactable)
    {
        string objectName = interactable.gameObject.name;
        InteractionData startGrabbingData = new InteractionData
        {
            Timestamp = DateTime.Now,
            ObjectName = objectName,
            InteractionType = "Start Grabbing"
        };
        /*
        // Check if this object has been grabbed before
        if (!hasBeenGrabbed[objectName])
        {
            // If it hasn't been grabbed before, record the interaction
            hasBeenGrabbed[objectName] = true;

            

            interactionDataListStart.Add(startGrabbingData);
        }*/

        interactionDataListStart.Add(startGrabbingData);
    }
    
    
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

        if(totScore == 25)
        {
           
            PlaySoundEnd();
            foreach (TMP_Text textElement in textElements)
            {
                textElement.gameObject.SetActive(false);
            }
            menuAtTheEnd.SetActive(true);
            BackToMenu();
        }
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
    

   

    public void BackToMenu()
    {
        //string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        totScoreEnd = totScore;
        dateTimeEnd = DateTime.Now;
        CSVManager.AppendToReport(GetReportLine());
        indexText++;
        totScore = 0;
        ObjectResetPlaneForSceneOne.objectFell = 0;
    }

    string[] GetReportLine()
    {
        /*List<string> reportLines = new List<string>();

        reportLines.Add("SceneOne.csv");
        reportLines.Add("Controllers");
        reportLines.Add("Ray-casting");
        reportLines.Add(indexText.ToString());
        reportLines.Add(totScoreEnd.ToString());
        reportLines.Add(ObjectResetPlaneForSceneOne.objectFell.ToString());
        reportLines.Add(dateTimeStart.ToString());
        reportLines.Add(dateTimeEnd.ToString());

        // Add a header line for the interaction data
        foreach (InteractionData interaction in interactionDataListStart)
        {
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            reportLines.Add(interactionLine);
        }
        // Add each interaction as a separate line
        foreach (InteractionData interaction in interactionDataList)
        {
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            reportLines.Add(interactionLine);
        }*/

        int i = 0;
        int j = 0;
            string[] returnable = new string[60];
            returnable[0] = "SceneOne.csv";
            returnable[1] = "Controllers";
            returnable[2] = "Raycasting";
            returnable[3] = indexText.ToString();
            returnable[4] = totScoreEnd.ToString();
            returnable[5] = ObjectResetPlaneForSceneOne.objectFell.ToString();
            returnable[6] = dateTimeStart.ToString();
            returnable[7] = dateTimeEnd.ToString();
            foreach (InteractionData interaction in interactionDataListStart)
            {
                i++;
                string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
                returnable[6+i] = interactionLine;
            }
            foreach (InteractionData interaction in interactionDataList)
            {
                 j++;
                string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
                returnable[6 + i +j] = interactionLine;
        }

        return returnable;
        

       // return reportLines.ToArray();
    }

  

}



