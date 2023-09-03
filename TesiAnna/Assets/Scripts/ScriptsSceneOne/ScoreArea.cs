using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using System.IO;

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
    public TMP_Text verificationText;

    [Header("Return button")]
    public Button backToMenu;
    int totScoreEnd = 0;
    string dateTimeStart;
    string dateTimeEnd;

    //PROVA FILE
    private static int indexText;

    private void Start()
    {
        //START csv
        // Construct the full file path using persistentDataPath
        verificationText.text = "Sono entrato in back menu";
        dateTimeStart = System.DateTime.UtcNow.ToString();
        verificationText.gameObject.SetActive(false);
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

    public void BackToMenu()
    {
        //string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        totScoreEnd = totScore;
        verificationText.gameObject.SetActive(true);
        dateTimeEnd = System.DateTime.UtcNow.ToString();
        CSVManager.AppendToReport(GetReportLine());
        indexText++;
        totScore = 0;
        // Check if the file exists
        /*if (File.Exists(filePath))
        {
            // Load and display the data
            //string savedData = File.ReadAllText(filePath);
           
        }
        else
        {
            // If the file doesn't exist, create and save some sample data
            //string sampleData = "This is sample data saved on Oculus Quest.";
            //File.WriteAllText(filePath, sampleData);
            CSVManager.AppendToReport(GetReportLine());
            //Debug.Log("Saved sample data: " + sampleData);

            Debug.Log("Loaded data: " + filePath);
        }*/
    }

    string[] GetReportLine()
    {
        string[] returnable = new string[6];
        returnable[0] = "SceneOne.csv";
        returnable[1] = "Controllers";
        returnable[2] = indexText.ToString();
        returnable[3] = totScoreEnd.ToString();
        returnable[4] = dateTimeStart;
        returnable[5] = dateTimeEnd;
        
        return returnable;

    }

}
