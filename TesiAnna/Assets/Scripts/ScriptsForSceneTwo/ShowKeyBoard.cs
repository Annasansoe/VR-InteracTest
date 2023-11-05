using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyBoard : MonoBehaviour
{
    [SerializeField]  private TMP_InputField inputField;
    public GameObject endMenu;
    [SerializeField] public TMP_Text questionText;
    [SerializeField] public GameObject keyBoard;
    [SerializeField] TMP_Text validText;
    [SerializeField] TMP_Text invalidText;

    [Header("Audio sounds")]
    public AudioSource endSound;
    public AudioClip soundClipEnd;
    // Start is called before the first frame update
    void Start()
    {
        endMenu.SetActive(false);
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }
    void Update()
    {
        if (Timer.timeIsUp == 1)
        {
            endMenu.SetActive(true);
            questionText.gameObject.SetActive(false); ;
            inputField.gameObject.SetActive(false);
            keyBoard.SetActive(false);
            validText.gameObject.SetActive(false);
            invalidText.gameObject.SetActive(false);
            PlayEndSound();
            Debug.Log("Questionnaire completed!");
        }
    }
    void PlayEndSound()
    {
        if (endSound != null && soundClipEnd != null)
        {
            endSound.PlayOneShot(soundClipEnd);
        }
    }
    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard();

    }

}
