using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuAtTheEnd : MonoBehaviour
{
    [Header("Dropdown Menus")]
    public TMPro.TMP_Dropdown methodDropdown;
    public TMPro.TMP_Dropdown metaphorDropdown;
    public TMPro.TMP_Dropdown taskDropdown;

    [Header("Not loadable sound and text")]
    public TMP_Text notLoadable;
    public AudioSource notLoadbleSource;
    public AudioClip notLoadbleClip;

    [Header("Load scene button")]
    public Button LoadSceneButton;

    [SerializeField] private float _time = 3f;

    // Start is called before the first frame update
    void Start()
    {
        notLoadable.gameObject.SetActive(false);
        LoadSceneButton.gameObject.SetActive(true);
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
                StartCoroutine(ShowMessage());
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
}
