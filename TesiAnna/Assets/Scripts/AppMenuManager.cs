using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppMenuManager : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public Button sceneOneButton;
    public Button sceneTwoButton;
    public Button sceneThreeButton;
    public Button sceneOneHand;
    public Button sceneTwoHand;
    public Button sceneThreeHand;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;

   

    public Button returnToMainMenu;

    public List<Button> returnButtons;


    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        sceneOneButton.onClick.AddListener(SceneOne);
        sceneTwoButton.onClick.AddListener(SceneTwo);
        sceneThreeButton.onClick.AddListener(SceneThree);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitApp);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void SceneOne()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(4);
    }

    public void SceneTwo()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(5);
    }

    public void SceneThree()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(6);
    }

    public void SceneOneHand()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void SceneTwoHand()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(2);
    }

    public void SceneThreeHand()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(3);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
    }
    public void EnableOption()
    {
        HideAll();
        mainMenu.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
    }
    public void EnableAbout()
    {
        HideAll();
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
    }
}