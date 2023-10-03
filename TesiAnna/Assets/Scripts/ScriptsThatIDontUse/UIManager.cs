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
    public Button goToSceneOneHand;
    public Button goToSceneTwoHand;
    public Button goToSceneThreeHand;

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
        goToSceneOneHand.onClick.AddListener(ToSceneOneHand);
        goToSceneTwoHand.onClick.AddListener(ToSceneTwoHand);
        goToSceneThreeHand.onClick.AddListener(ToSceneThreeHand);


    }

    public void ReturnToMenu()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void ToSceneOne()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(7);//1
    }

    public void ToSceneTwo()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(8);//2
    }

    public void ToSceneThree()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(9);//3
    }

    public void ToSceneOneHand()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(10);//4
    }

    public void ToSceneTwoHand()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(11);//5
    }

    public void ToSceneThreeHand()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(12);//6
    }

}
