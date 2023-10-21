using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueBox : MonoBehaviour
{
    [Header("Dialogue segments")]
    public DialogueSegment[] DialogueSegments;

    [Space]
    [Header("Target cube")]
    public XRGrabInteractable XRGrabInteractable;
    public GameObject Bin;

    [Space]
    [Header("Dialogue Box")]
    public Button SkipIndicator;
    public TMP_Text DialogueDisplay;

    [Space]
    [Header("Button start test")]
    public Button GoToSceneOne;
    public TMP_Text VerifyIsGrabbed;
    public TMP_Text TypeOfMetaphor;
    public static string StringMetaphor; 

    [Space]
    [Header("Feedback audio")]
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioSource letterSound;
    public AudioClip soundClipLetter;

    [Space]
    [Header("Instructions Image")]
    public Image imageInstruction; 

    [Space]
    [Header("Text speed")]
    public float TextSpeed;
    [Space]
    private bool buttonClicked = false;

    private bool CanContinue;
    private static int DialogueIndex = 0;

    private bool hasBeenPlayed = false;

    static int totScore = 0;
    static int totGrab = 0;

    public XRDirectInteractor directInteractorLeft;
    public XRRayInteractor rayInteractorLeft;
    public XRDirectInteractor directInteractorRight;
    public XRRayInteractor rayInteractorRight;


    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
       
        GoToSceneOne.gameObject.SetActive(false);
        VerifyIsGrabbed.gameObject.SetActive(false);
        Bin.gameObject.SetActive(false);
        imageInstruction.gameObject.SetActive(false);
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

            if (DialogueIndex < DialogueSegments.Length)
            {
                StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
            }
            else
            {
                // Last sentence of the dialogue
                SkipIndicator.gameObject.SetActive(false);
                DialogueDisplay.gameObject.SetActive(false);
                GoToSceneOne.gameObject.SetActive(true);
                DialogueIndex = 0;
                totGrab = 0;
                totScore = 0;
            }
        }
        
        else if (DialogueIndex == 1)
        {
            directInteractorLeft.enabled = true;
            directInteractorRight.enabled = true;
            SkipIndicator.enabled = false;
            XRGrabInteractable.gameObject.SetActive(true);
            imageInstruction.gameObject.SetActive(true);
            if (totGrab > 0)
            {
                VerifyIsGrabbed.text = "Great job!";
                VerifyIsGrabbed.gameObject.SetActive(true);
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
                    imageInstruction.gameObject.SetActive(false);
                }
            }
        }
        else if (DialogueIndex == 2)
        {
            directInteractorLeft.enabled = true;
            directInteractorRight.enabled = true;
            SkipIndicator.enabled = false;
            Bin.gameObject.SetActive(true);
            VerifyIsGrabbed.gameObject.SetActive(false);

            if (totScore == 2)
            {
                VerifyIsGrabbed.text = "Awesome you did it!";
                VerifyIsGrabbed.gameObject.SetActive(true);
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

        for (int i = 0; i < Dialogue.Length; i++)
        {
            DialogueDisplay.text += Dialogue[i];
            PlayLetterSound(); // Play the sound effect
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

[System.Serializable]
public class DialogueSegment
{
    public string Dialogue;
}
