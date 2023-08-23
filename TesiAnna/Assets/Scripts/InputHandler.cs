using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;
public class InputHandler : MonoBehaviour
{
    [Header("The value we got from the input field")]
    [SerializeField] private string inputText;

    [Header("Showing the reaction to the player")]
    [SerializeField] private GameObject reactiongGroup;
    [SerializeField] private TMP_Text reactionTextBox;

    [Header("Grab from input and see the result")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text resultText;

    public void OnValidate()
    {
        string input = inputField.text.ToString();

       
        if (input == string.Empty)
        {
            return;
        }
        else if (input.Length < 4)
        {
            resultText.text = "Invalid input";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = "Valid Input";
            resultText.color = Color.green;
        }

        reactionTextBox.text = "Welcome to the team, " + input + "!";
        reactiongGroup.SetActive(true);
    }

    private void DisplayReactionToInput()
    {
        
    }
}
