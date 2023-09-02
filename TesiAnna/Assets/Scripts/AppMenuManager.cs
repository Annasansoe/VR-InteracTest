using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppMenuManager : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public Button sceneOneTutorialButton;
    public Button sceneTwoTutorialButton;
    public Button sceneThreeTutorialButton;
    public Button sceneOneTutorialHand;
    public Button sceneTwoTutorialHand;
    public Button sceneThreeTutorialHand;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;

    [Header("Title scenes")]
    public TMP_Text titleSceneControllers;
    public TMP_Text titleSceneHands;


    public Button returnToMainMenu;

    public List<Button> returnButtons;


    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        sceneOneTutorialButton.onClick.AddListener(SceneOneTutorial);
        sceneTwoTutorialButton.onClick.AddListener(SceneTwoTutorial);
        sceneThreeTutorialButton.onClick.AddListener(SceneThreeTutorial);
        sceneOneTutorialHand.onClick.AddListener(SceneOneHandTutorial);
        sceneTwoTutorialHand.onClick.AddListener(SceneTwoHandTutorial);
        sceneThreeTutorialHand.onClick.AddListener(SceneThreeHandTutorial);
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

    public void SceneOneTutorial()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void SceneTwoTutorial()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(2);
    }

    public void SceneThreeTutorial()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(3);
    }

    public void SceneOneHandTutorial()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(4);
    }

    public void SceneTwoHandTutorial()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(5);
    }

    public void SceneThreeHandTutorial()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(6);
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
        titleSceneControllers.gameObject.SetActive(false);
        titleSceneHands.gameObject.SetActive(false);
    }
    public void EnableAbout()
    {
        HideAll();
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
        titleSceneControllers.gameObject.SetActive(false);
        titleSceneHands.gameObject.SetActive(false);
    }
}