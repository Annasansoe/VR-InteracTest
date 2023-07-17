using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public XRGrabInteractable XRGrabInteractable;

    [Header("CollectedObjects")]
    public TMP_Text collectedObjectsText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        collectedObjectsText.text = "Collected Objects: " + score.ToString();
        XRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void AddPoint()
    {
        if (!XRGrabInteractable.isSelected | !XRGrabInteractable.CompareTag("Coins"))
        {
            return;
        }
        score += 1;
        collectedObjectsText.text = "Collected Objects: " + score.ToString();
    }
}
