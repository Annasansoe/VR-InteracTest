using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public DialogueSegment[] DialogueSegments;
    [Space]
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;
    public Button GoToSceneOne;
    public TMP_Text CollectedObjects;
    [Space]
    public float TextSpeed;

    private bool buttonClicked = false;

    private bool CanContinue;
    private int DialogueIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        GoToSceneOne.gameObject.SetActive(false);
        CollectedObjects.gameObject.SetActive(false);

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

                GoToSceneOne.gameObject.SetActive(true);
                CollectedObjects.gameObject.SetActive(true);
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

[System.Serializable]
public class DialogueSegment
{
    public string Dialogue;
}
