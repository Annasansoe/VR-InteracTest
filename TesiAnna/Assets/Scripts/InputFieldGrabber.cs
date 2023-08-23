using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldGrabber : MonoBehaviour
{
    [Header("The value we got from the input field")]
    [SerializeField] private string inputText;

    [Header("Showing the reaction to the player")]
    [SerializeField] private GameObject reactiongGroup;
    [SerializeField] private TMP_Text reactionTextBox;

    [Header("Parsed into int and float")]
    [SerializeField] private int inputParsedAsInt;
    [SerializeField] private float inputParsedAsFloat;

    public void GrabFromInputField (string input)
    {
        if(input == string.Empty)
        {
            return;
        }
        inputText = input;

        inputParsedAsInt = int.Parse(input);
        inputParsedAsFloat = float.Parse(input);

        DisplayReactionToInput();
    }

    private void DisplayReactionToInput()
    {
        reactionTextBox.text = "Welcome to the team, " + inputText + "!";
        reactiongGroup.SetActive(true);
    }
}
