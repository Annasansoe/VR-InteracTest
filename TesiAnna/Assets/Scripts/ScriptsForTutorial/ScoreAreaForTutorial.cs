using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System;

public class ScoreAreaForTutorial : MonoBehaviour
{
    public XRGrabInteractable XRGrabInteractable;

    public static int totScore = 0;

    public AudioSource audioSource;
    public AudioClip soundClip;


    private bool hasBeenPlayed = false;
   
    public TMP_Text VerifyIsGrabbedT;

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Unsorted Waste"))
        {
            totScore++;
        }
        Destroy(otherCollider.gameObject);
    }
    private void Update()
    {
        if (totScore == 1)
        {

            VerifyIsGrabbedT.text = "Awesome you did it!";
            VerifyIsGrabbedT.gameObject.SetActive(true);
            if (!hasBeenPlayed)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
                hasBeenPlayed = true;
            }
        }
        if (DialogueBox.DialogueIndex >= 3)
        {
            VerifyIsGrabbedT.gameObject.SetActive(false);
        }

        }

   
}
