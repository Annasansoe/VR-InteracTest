using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueBoxForSceneThree : MonoBehaviour
{
    public DialogueSegment[] DialogueSegments;
    [Space]
    public XRGrabInteractable[] XRGrabInteractable;
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;
    public Button GoToSceneThree;
    [Space]
    public float TextSpeed;

    private bool buttonClicked = false;

    private bool CanContinue;
    private int DialogueIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        GoToSceneThree.gameObject.SetActive(false);
        foreach(XRGrabInteractable interactable in XRGrabInteractable)
        {
            GameObject interactableObject = interactable.gameObject;
            // Now you can work with the 'interactableObject'
            interactableObject.gameObject.SetActive(false);
        }
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

                GoToSceneThree.gameObject.SetActive(true);
                foreach (XRGrabInteractable interactable in XRGrabInteractable)
                {
                    GameObject interactableObject = interactable.gameObject;
                    // Now you can work with the 'interactableObject'
                    interactableObject.gameObject.SetActive(true);
                }
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

