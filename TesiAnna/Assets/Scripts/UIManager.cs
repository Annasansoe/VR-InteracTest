using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } 


    [Header("Return Buttons")]
    public Button returnToMainMenu;
    public Button goToSceneOne;
    public Button goToSceneTwo;
    public Button goToSceneThree;

    private void Awake()
    {
       
         Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {

        returnToMainMenu.onClick.AddListener(ReturnToMenu);
        goToSceneOne.onClick.AddListener(ToSceneOne);
        goToSceneTwo.onClick.AddListener(ToSceneTwo);
        goToSceneThree.onClick.AddListener(ToSceneThree);


    }

    public void ReturnToMenu()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void ToSceneOne()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);//1
    }

    public void ToSceneTwo()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);//2
    }

    public void ToSceneThree()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);//3
    }

}
