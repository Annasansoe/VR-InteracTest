using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HoverTipsManager : MonoBehaviour
{
    public TMP_Text hoverText; // Reference to the Text UI element

    public Image hoverImage;

    private void Start()
    {
        hoverImage.gameObject.SetActive(false);
        hoverText.gameObject.SetActive(false);

    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        hoverImage.gameObject.SetActive(true);
        hoverText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverImage.gameObject.SetActive(false);
        hoverText.gameObject.SetActive(false);
    }*/

    public void OnPointerEnter()
    {
        hoverImage.gameObject.SetActive(true);
        hoverText.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        hoverImage.gameObject.SetActive(false);
        hoverText.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        //string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        
        ObjectResetPlaneForSceneOne.objectFell = 0;
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
}

