using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBoxSceneTwo : MonoBehaviour
{
    [Header("Dialogue segments")]
    public DialogueSegment[] DialogueSegments;

    [Space]
    [Header("Dialogue Box")]
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;

    [Space]
    [Header("Button start test")]
    public Button GoToSceneTwo;

    [Space]
    [Header("Button start test")]
    public GameObject Keyboard;

    [Space]
    [Header("Interactable keyboard")]
    public TMP_InputField InputField;
    public TMP_Text ValidationText;
    public TMP_Text QuestionBox;
    public TMP_Text Verify;

    [Space]
    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource letterSound;
    public AudioClip soundClipLetter;

    [Space]
    [Header("Instructions Image")]
    public Image imageTrigger;
    public Image imagePinch;

    [Space]
    [Header("Text speed")]
    public float TextSpeed;

    private bool buttonClicked = false;
    public bool inputFieldClicked = false;
    private bool hasBeenPlayed = false;

    private bool CanContinue;
    private static int DialogueIndex;

    static int isClicked = 0;
    static int isEnterClickedO = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        GoToSceneTwo.gameObject.SetActive(false);
        InputField.gameObject.SetActive(false);
        ValidationText.gameObject.SetActive(false);
        QuestionBox.gameObject.SetActive(false);
        imageTrigger.gameObject.SetActive(false);
        imagePinch.gameObject.SetActive(true);
        Verify.gameObject.SetActive(false);
        
    }
    public void Click()
    {
        buttonClicked = true;
    }

    public void isInputFieldClicked()
    {
        inputFieldClicked = true;
        isClicked += 1;
    }
    public void isEnterClicked()
    {

        string userAnswer = InputField.text.ToString();
        //inputFieldClicked = true;
        if (userAnswer != "") 
        {
            isEnterClickedO += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SkipIndicator.enabled = CanContinue;
        if (DialogueIndex != 1 && DialogueIndex != 2 && buttonClicked && CanContinue)
        {
            buttonClicked = false;
            DialogueIndex++;
            if (DialogueIndex == DialogueSegments.Length)
            {
                gameObject.SetActive(false);
                DialogueDisplay.gameObject.SetActive(false);
                Keyboard.gameObject.SetActive(false);
                DialogueIndex = 0;
                isEnterClickedO = 0;
                isClicked = 0;
                SkipIndicator.gameObject.SetActive(false);
                GoToSceneTwo.gameObject.SetActive(true);
            }
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
        }
        else if (DialogueIndex == 1)
        {
            SkipIndicator.enabled = false;
            InputField.gameObject.SetActive(true);

            imageTrigger.gameObject.SetActive(true);
            if (isClicked > 0)
            {
                Verify.gameObject.SetActive(true);
                Verify.text = "Awesome you did it!";
                SkipIndicator.enabled = true;

                if (!hasBeenPlayed)
                {
                    audioSource.clip = soundClip;
                    audioSource.Play();
                    hasBeenPlayed = true;
                }

                if (buttonClicked && CanContinue)
                {
                    Verify.gameObject.SetActive(false);

                    hasBeenPlayed = false;
                    buttonClicked = false;
                    imageTrigger.gameObject.SetActive(false);
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                }

            }
        }
        else if (DialogueIndex == 2)
        {
            Verify.gameObject.SetActive(false);

            imagePinch.gameObject.SetActive(false);
            SkipIndicator.enabled = false;
            ValidationText.gameObject.SetActive(true);
            QuestionBox.gameObject.SetActive(true);
            if (isEnterClickedO > 0)
            {
                Verify.gameObject.SetActive(true);
                Verify.text = "Great job!";
                SkipIndicator.enabled = true;

                if (!hasBeenPlayed)
                {
                    audioSource.clip = soundClip;
                    audioSource.Play();
                    hasBeenPlayed = true;
                }

                if (buttonClicked && CanContinue)
                {
                    Verify.gameObject.SetActive(false);
                    ValidationText.gameObject.SetActive(false);
                    QuestionBox.gameObject.SetActive(false);
                    InputField.gameObject.SetActive(false);
                    Keyboard.gameObject.SetActive(false);
                    hasBeenPlayed = false;
                    buttonClicked = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                }
            }
        }
    }

    

    IEnumerator PlayDialogue(string Dialogue)
    {
        CanContinue = false;
        DialogueDisplay.SetText(string.Empty);

        for(int i = 0; i < Dialogue.Length; i++)
        {
            DialogueDisplay.text += Dialogue[i];
            PlayLetterSound();
            yield return new WaitForSeconds(1f / TextSpeed);
        }
        CanContinue = true;
    }

    void PlayLetterSound()
    {
        if (letterSound != null && soundClipLetter != null)
        {
            letterSound.PlayOneShot(soundClipLetter);
        }
    }

}


