using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBoxSceneTwo : MonoBehaviour
{
    public DialogueSegment[] DialogueSegments;
    [Space]
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;
    public Button GoToSceneTwo;
    public TMP_InputField InputField;
    public TMP_Text ValidationText;
    public TMP_Text QuestionBox;
    public TMP_Text Verify;
    public AudioSource inputNormal;

    public AudioSource enterNormal;
    public AudioSource inputFirstTime;
    public AudioSource enterFirstTime;

    [Space]
    public float TextSpeed;

    private bool buttonClicked = false;
    public bool inputFieldClicked = false;

    private bool CanContinue;
    private int DialogueIndex;

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
        Verify.gameObject.SetActive(false);
        inputNormal.gameObject.SetActive(false);
        enterNormal.gameObject.SetActive(false);
        inputFirstTime.gameObject.SetActive(false);
        enterFirstTime.gameObject.SetActive(false);
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
        //inputFieldClicked = true;
        isEnterClickedO += 1;
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

                SkipIndicator.gameObject.SetActive(false);
                GoToSceneTwo.gameObject.SetActive(true);
            }
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
        }
        else if (DialogueIndex == 1)
        {
            SkipIndicator.enabled = false;
            InputField.gameObject.SetActive(true);
            if (isClicked > 0)
            {
                Verify.gameObject.SetActive(true);
                Verify.text = "Awesome you did it!";
                SkipIndicator.enabled = true;
                inputFirstTime.gameObject.SetActive(false);
                inputNormal.gameObject.SetActive(true);
                if (buttonClicked && CanContinue)
                {
                    Verify.gameObject.SetActive(false);
                   
                    inputFirstTime.gameObject.SetActive(false);
                    inputNormal.gameObject.SetActive(true);
                    buttonClicked = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                }

            }
        }
        else if (DialogueIndex == 2)
        {
            Verify.gameObject.SetActive(false);

            inputFirstTime.gameObject.SetActive(false);
            enterFirstTime.gameObject.SetActive(true);
            inputNormal.gameObject.SetActive(true);
            SkipIndicator.enabled = false;
            ValidationText.gameObject.SetActive(true);
            QuestionBox.gameObject.SetActive(true);

            if (isEnterClickedO > 0)
            {
                Verify.gameObject.SetActive(true);
                Verify.text = "Great job!";
                SkipIndicator.enabled = true;
                enterFirstTime.gameObject.SetActive(false);
                enterNormal.gameObject.SetActive(true);
                if (buttonClicked && CanContinue)
                {
                    enterNormal.gameObject.SetActive(true);
                    Verify.gameObject.SetActive(false);
                    ValidationText.gameObject.SetActive(false);
                    QuestionBox.gameObject.SetActive(false);
                    InputField.gameObject.SetActive(false);
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
            yield return new WaitForSeconds(1f / TextSpeed);
        }
        CanContinue = true;
    }

   
}


