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
    public XRGrabInteractable XRGrabInteractable;
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;
    public TMP_Text VerifyIsGrabbed;
    public Button GoToSceneThree;
    AudioSource DidIt;
    [Space]
    public float TextSpeed;

    private bool buttonClicked = false;

    static int IsSelected = 0;

    private bool CanContinue;
    private int DialogueIndex;

    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {

        XRGrabInteractable.gameObject.SetActive(false);
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        GoToSceneThree.gameObject.SetActive(false);
        VerifyIsGrabbed.gameObject.SetActive(false);
        originalScale = XRGrabInteractable.transform.localScale;
        DidIt.gameObject.SetActive(false);
        XRGrabInteractable.gameObject.SetActive(false);
        
    }
    public void Click()
    {
        buttonClicked = true;
    }

    public void isSelect()
    {
        IsSelected++;
        if (IsSelected > 1)
        {
            DidIt.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sizeCube2 = XRGrabInteractable.transform.localScale;
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

               
            }
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
        }
        else if (DialogueIndex == 1)
        {
            SkipIndicator.enabled = false;
            XRGrabInteractable.gameObject.SetActive(true);
            VerifyIsGrabbed.gameObject.SetActive(true);
            VerifyIsGrabbed.text = "Selected:" + IsSelected.ToString() ;
            if (IsSelected > 1)
            {
                VerifyIsGrabbed.text = "Great job!";
                SkipIndicator.enabled = true;
               // DidIt.gameObject.SetActive(true);

                if (buttonClicked && CanContinue)
                {
                    DidIt.gameObject.SetActive(false);
                    buttonClicked = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                    VerifyIsGrabbed.gameObject.SetActive(false);
                }
            }
        }
        else if (DialogueIndex == 2)
        {
            DidIt.gameObject.SetActive(false);
            SkipIndicator.enabled = false;
            VerifyIsGrabbed.gameObject.SetActive(false);
            if (originalScale.x * originalScale.y * originalScale.z < sizeCube2.x * sizeCube2.y * sizeCube2.z)
            {
                VerifyIsGrabbed.text = "Great job!";
                SkipIndicator.enabled = true;
                DidIt.gameObject.SetActive(true);

                if (buttonClicked && CanContinue)
                {
                    DidIt.gameObject.SetActive(false);
                    buttonClicked = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                    VerifyIsGrabbed.gameObject.SetActive(false);
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

