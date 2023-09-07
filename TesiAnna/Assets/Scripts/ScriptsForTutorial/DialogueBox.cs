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
    public AudioSource DidIt;
    public AudioSource binAudioSource;

    [Space]
    public float TextSpeed;

    private bool buttonClicked = false;

    private bool CanContinue;
    public bool cubeIsGrabbed = false;
    public bool cubeIsInTheBin = false;
    private int DialogueIndex;

    static int totScore = 0;
    static int totGrab = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        DidIt.gameObject.SetActive(false);
        //binAudioSource.gameObject.SetActive(false);
        GoToSceneOne.gameObject.SetActive(false);
        VerifyIsGrabbed.gameObject.SetActive(false);
        Bin.gameObject.SetActive(false);
        // Now you can work with the 'interactableObject'
        XRGrabInteractable.gameObject.SetActive(false);
        
    }
    public void Click()
    {
        buttonClicked = true;
        //CollectedObjects.gameObject.SetActive(true);
    }

    public void isGrabbed()
    {
        VerifyIsGrabbed.gameObject.SetActive(true);
        totGrab += 1;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Unsorted Waste"))
        {
            if (binAudioSource != null && binAudioSource.clip != null)
            {
                binAudioSource.Play();
            }
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

            if (DialogueIndex == DialogueSegments.Length)
            {
                gameObject.SetActive(false);
                DialogueDisplay.gameObject.SetActive(false);

                GoToSceneOne.gameObject.SetActive(true);               
            }
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
        }
        else if(DialogueIndex == 2 )
        {
            SkipIndicator.enabled = false;
            Bin.gameObject.SetActive(true);
            VerifyIsGrabbed.gameObject.SetActive(false);
            //VerifyIsGrabbed.text = "Tot score " + totScore.ToString();
            if (totScore == 2)
            {
                VerifyIsGrabbed.gameObject.SetActive(true);
                VerifyIsGrabbed.text = "Awesome you did it!";
                SkipIndicator.enabled = true;
               // DidIt.Play();

                if (buttonClicked && CanContinue ) 
                {
                    buttonClicked = false;
                    DialogueIndex++;
                    StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue));
                    VerifyIsGrabbed.gameObject.SetActive(false);
                }
            }
        }
        else if (DialogueIndex == 1)
        {
            SkipIndicator.enabled = false;
            XRGrabInteractable.gameObject.SetActive(true);

            if (totGrab > 0)
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

[System.Serializable]
public class DialogueSegment
{
    public string Dialogue;
}
