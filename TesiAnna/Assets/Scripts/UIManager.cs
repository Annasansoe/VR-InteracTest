using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } 

    /*[Header("Scores")]
    public TMP_Text collectedObjectsText;
    int score = 0;*/

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
        
        //collectedObjectsText.text = "Collected Objects: " + score.ToString();
    }

    //public void UpdateCollectedObjectsText(int score)
    //{
    //    collectedObjectsText.text = "Collected Objects: " + score.ToString();
    //}
   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            other.gameObject.SetActive(false);
            ScoreManager.instance.AddPoint();
        }
    }
   */
    public void ReturnToMenu()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }
}
