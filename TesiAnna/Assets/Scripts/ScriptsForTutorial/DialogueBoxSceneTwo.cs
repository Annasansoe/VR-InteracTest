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
    public GameObject Keyboard;

    [Space]
    public float TextSpeed;

    private bool buttonClicked = false;

    private bool CanContinue;
    private int DialogueIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        GoToSceneTwo.gameObject.SetActive(false);
        InputField.gameObject.SetActive(false);
        ValidationText.gameObject.SetActive(false);
        QuestionBox.gameObject.SetActive(false);
        Keyboard.gameObject.SetActive(false);
    }
    public void Click()
    {
        buttonClicked = true;
    }

    // Update is called once per frame
    void Update()
    {
        SkipIndicator.enabled = CanContinue;
        if (buttonClicked && CanContinue)
        {
            buttonClicked = false;
            DialogueIndex++;
            if (DialogueIndex == DialogueSegments.Length)
            {
                gameObject.SetActive(false);
                DialogueDisplay.gameObject.SetActive(false);

                GoToSceneTwo.gameObject.SetActive(true);
                InputField.gameObject.SetActive(true);
                ValidationText.gameObject.SetActive(true);
                QuestionBox.gameObject.SetActive(true);
                Keyboard.gameObject.SetActive(true);
            }
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
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


