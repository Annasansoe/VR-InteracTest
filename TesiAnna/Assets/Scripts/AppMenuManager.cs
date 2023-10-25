using System.Collections;
using UnityEngine.SceneManagement;
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

    [Header("Dropdown Menus")]
    public TMPro.TMP_Dropdown methodDropdown;
    public TMPro.TMP_Dropdown metaphorDropdown;
    public TMPro.TMP_Dropdown taskDropdown;

    [Header("Main Menu Buttons")]
    public Button sceneOneTutorialButton;
    public Button sceneTwoTutorialButton;
    public Button sceneThreeTutorialButton;
    public Button sceneOneTutorialHand;
    public Button sceneTwoTutorialHand;
    public Button sceneThreeTutorialHand;
    public Button LoadSceneButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;

    [Header("Not loadable sound and text")]
    public TMP_Text notLoadable;
    public AudioSource notLoadbleSource;
    public AudioClip notLoadbleClip;

    public Button returnToMainMenu;

    public List<Button> returnButtons;

    public static AppMenuManager Instance { get; private set; }

    [SerializeField] private float _time = 3f;

    void Start()
    {
        EnableMainMenu();

        //Hook events
        notLoadable.gameObject.SetActive(false);
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

    private Dictionary<string, string> sceneMappings = new Dictionary<string, string>
    {
        { "Grab_Controllers_Ray-casting", "TutorialSceneOne" },
        { "Grab_Controllers_Direct Grab", "TutorialSceneOneDG" },
        { "Grab_Bare Hands_Ray-casting", "TutorialSceneOneHand" },
        { "Grab_Bare Hands_Direct Grab", "TutorialSceneOneHandDG" },
        { "Type_Controllers_Ray-casting", "TutorialSceneTwo" },
        { "Type_Controllers_Direct Grab", "" },
        { "Type_Bare Hands_Ray-casting", "TutorialSceneTwoHand" },
        { "Type_Bare Hands_Direct Grab", "TutorialSceneTwoHandDG" },
        { "Manipulate_Controllers_Ray-casting", "TutorialSceneThree" },
        { "Manipulate_Controllers_Direct Grab", "TutorialSceneThreeDG" },
        { "Manipulate_Bare Hands_Ray-casting", "TutorialSceneThreeHand" },
        { "Manipulate_Bare Hands_Direct Grab", "TutorialSceneThreeHandDG" }
        // Add more mappings as needed
    };
   
    public void LoadSelectedScene()
    {
        string selectedTask = taskDropdown.options[taskDropdown.value].text;
        string selectedMethod = methodDropdown.options[methodDropdown.value].text;
        string selectedMetaphor = metaphorDropdown.options[metaphorDropdown.value].text;
        
        // Construct the key based on the selected choices
        string key = $"{selectedTask}_{selectedMethod}_{selectedMetaphor}";
        if (sceneMappings.ContainsKey(key))
        {
            string sceneToLoad = sceneMappings[key];
            if (string.IsNullOrEmpty(sceneToLoad))
            {
                // Do something different here since the mapped value is an empty string
                ShowMessage();
                PlaySound();
                Debug.LogWarning("No scene specified for the selected combination.");
                // You can perform other actions or show a message to the user.
            }
            else
            {
                // Load the scene using the mapped value
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            Debug.LogWarning("Scene not found for the selected combination.");
        }
        
    }

    private IEnumerator ShowMessage()
    {

        notLoadable.gameObject.SetActive(true);

        yield return new WaitForSeconds(_time);


        notLoadable.gameObject.SetActive(false);
    }

    void PlaySound()
    {
        if (notLoadbleSource != null && notLoadbleClip != null)
        {
            notLoadbleSource.PlayOneShot(notLoadbleClip);
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
    }
    public void EnableAbout()
    {
        HideAll();
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);

    }

   
}