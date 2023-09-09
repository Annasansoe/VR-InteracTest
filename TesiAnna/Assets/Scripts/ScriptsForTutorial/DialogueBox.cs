using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueBox : MonoBehaviour
{
    public DialogueSegment[] DialogueSegments;
    [Space]
    public XRGrabInteractable XRGrabInteractable;
    public GameObject Bin;
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;
    public Button GoToSceneOne;
    public TMP_Text VerifyIsGrabbed;

    public AudioSource audioSource;
    public AudioClip soundClip;

    [Space]
    public float TextSpeed;

    private bool buttonClicked = false;

    private bool CanContinue;
    public bool cubeIsGrabbed = false;
    public bool cubeIsInTheBin = false;
    private static int DialogueIndex;

    private bool hasBeenPlayed = false;

    static int totScore = 0;
    static int totGrab = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
       
        GoToSceneOne.gameObject.SetActive(false);
        VerifyIsGrabbed.gameObject.SetActive(false);
        Bin.gameObject.SetActive(false);

        XRGrabInteractable.gameObject.SetActive(false);
        
    }
    public void Click()
    {
        buttonClicked = true;
    }

    public void isGrabbed()
    {
        totGrab += 1;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Unsorted Waste"))
        {
            totScore++;
        }
        Destroy(otherCollider.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SkipIndicator.enabled = CanContinue;
        
        if (DialogueIndex != 1 && buttonClicked && CanContinue)
        {
            buttonClicked = false;
            DialogueIndex++;

            VerifyIsGrabbed.gameObject.SetActive(false);

            if (DialogueIndex == DialogueSegments.Length)
            {
                gameObject.SetActive(false);
                DialogueDisplay.gameObject.SetActive(false);

                GoToSceneOne.gameObject.SetActive(true);               
            }
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
        }
        
        else if (DialogueIndex == 1)
        {
            SkipIndicator.enabled = false;
            XRGrabInteractable.gameObject.SetActive(true);

            if (totGrab > 0)
            {
                VerifyIsGrabbed.text = "Great job!";
                SkipIndicator.enabled = true;
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
            Bin.gameObject.SetActive(true);
            VerifyIsGrabbed.gameObject.SetActive(false);

            if (totScore == 2)
            {
                VerifyIsGrabbed.gameObject.SetActive(true);
                VerifyIsGrabbed.text = "Awesome you did it!";
                SkipIndicator.enabled = true;
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
