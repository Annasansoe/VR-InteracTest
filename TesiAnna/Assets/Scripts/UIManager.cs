using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } 

    [Header("Scores")]
    public TMP_Text collectedObjectsText;


    [Header("Return Buttons")]
    public Button returnToMainMenu;

    private void Awake()
    {
       
         Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {

        returnToMainMenu.onClick.AddListener(ReturnToMenu);
    }

    public void UpdateCollectedObjectsText(string count)
    {
        collectedObjectsText.text = "Collected Objects: " + count;
    }

    public void ReturnToMenu()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }
}
