using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueBoxForSceneThree : MonoBehaviour
{
    [Header("Dialogue segments")]
    public DialogueSegment[] DialogueSegments;
    [Space]
    [Header("Target cube")]
    public GameObject cubeTarget;

    [Header("Interactable cube")]
    public XRGrabInteractable XRGrabInteractable;

    [Header("Dialogue Box")]
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;

    public TMP_Text VerifyIsGrabbed;
    public TMP_Text VerifyScale;

    [Header("Button start test")]
    public Button GoToSceneThree;
    [Space]

    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;

    [Space]
    [Header("Text speed")]
    public float TextSpeed;

    private bool buttonClicked = false;

    static int IsSelected = 0;
    private bool hasBeenPlayed = false;

    private bool CanContinue;
    private static int DialogueIndex;

    private Vector3 originalScale;

    // Start is called before the first frame updates
    void Start()
    {

        XRGrabInteractable.gameObject.SetActive(false);
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        GoToSceneThree.gameObject.SetActive(false);
        VerifyIsGrabbed.gameObject.SetActive(false);
        VerifyScale.gameObject.SetActive(false);
        originalScale = XRGrabInteractable.transform.localScale;
       
        XRGrabInteractable.gameObject.SetActive(false);
        cubeTarget.gameObject.SetActive(false);

    }
    public void Click()
    {
        buttonClicked = true;
    }

    public void isSelect()
    {
        IsSelected++;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sizeCube1 = cubeTarget.transform.localScale;
        Vector3 sizeCube2 = XRGrabInteractable.transform.localScale;

        SkipIndicator.enabled = CanContinue;

        if(DialogueIndex == 0 || DialogueIndex == 4) 
        {            
            VerifyIsGrabbed.gameObject.SetActive(false);
            VerifyScale.gameObject.SetActive(false);
            XRGrabInteractable.gameObject.SetActive(false);
            if ( buttonClicked && CanContinue)
            {
                buttonClicked = false;
                DialogueIndex++;
                if (DialogueIndex == DialogueSegments.Length)
                {
                   
                    VerifyIsGrabbed.gameObject.SetActive(false);
                    VerifyScale.gameObject.SetActive(false);
                    XRGrabInteractable.gameObject.SetActive(false);
                    DialogueDisplay.gameObject.SetActive(false);
                    SkipIndicator.gameObject.SetActive(false);

                    GoToSceneThree.gameObject.SetActive(true);
                                  
                }
                StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
            }
        }
        else if (DialogueIndex == 1)
        {

            VerifyIsGrabbed.gameObject.SetActive(true);
            SkipIndicator.enabled = false;
            XRGrabInteractable.gameObject.SetActive(true);
            if (IsSelected > 1)
            {
                SkipIndicator.enabled = true;
                VerifyIsGrabbed.text = "Great job!";
                if (!hasBeenPlayed)
                {
                    audioSource.clip = soundClip;
                    audioSource.Play();
                    hasBeenPlayed = true;
                }

                if (buttonClicked && CanContinue)
                {
                    buttonClicked = false;
                    hasBeenPlayed = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                    VerifyIsGrabbed.gameObject.SetActive(false);
                    
                }
            }
        }
        else if (DialogueIndex == 2)
        {
           
            SkipIndicator.enabled = false;
            if (sizeCube1.x * sizeCube1.y * sizeCube1.z < sizeCube2.x * sizeCube2.y * sizeCube2.z)
            {
                SkipIndicator.enabled = true;
                
                VerifyScale.gameObject.SetActive(true);
                VerifyScale.text = "Great job! The object is bigger";
                if (!hasBeenPlayed)
                {
                    audioSource.clip = soundClip;
                    audioSource.Play();
                    hasBeenPlayed = true;
                }


                if (buttonClicked && CanContinue)
                {
                    buttonClicked = false;
                    hasBeenPlayed = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                    //VerifyIsGrabbed.gameObject.SetActive(false);
                    VerifyScale.gameObject.SetActive(false);
                  
                }
            }
        }
        else if (DialogueIndex == 3)
        {
           
            SkipIndicator.enabled = false;

            if (sizeCube1.x * sizeCube1.y * sizeCube1.z > sizeCube2.x * sizeCube2.y * sizeCube2.z)
            {
                SkipIndicator.enabled = true;

                VerifyScale.gameObject.SetActive(true);
                VerifyScale.text = "Great job! The object is smaller";

                if (!hasBeenPlayed)
                {
                    audioSource.clip = soundClip;
                    audioSource.Play();
                    hasBeenPlayed = true;
                }

                if (buttonClicked && CanContinue)
                {
                    
                    buttonClicked = false;
                    hasBeenPlayed = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                    VerifyScale.gameObject.SetActive(false);
                   
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

