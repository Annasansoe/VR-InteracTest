using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFieldGrabberTutorial : MonoBehaviour
{
    [Header("Grab from input and see the result")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text resultText;

    [Header("Question for test")]
    [SerializeField] public GameObject questionPanel;
    [SerializeField] public TMP_Text questionText;
    
    //public Button submitButton;

    public List<QuestionTutorial> questionsT;
    public int currentQuestionIndex = 0;

    private void Start()
    {
        questionsT = new List<QuestionTutorial>
    {
        new QuestionTutorial("What is your name?"),
        new QuestionTutorial("What is your surname?"),
        new QuestionTutorial("What is your email?"),
        // ... Add more questions here with their respective expected answers
    };

        DisplayQuestion(currentQuestionIndex);
    }

    public void DisplayQuestion(int index)
    {
        questionText.text = questionsT[index].questionText;
        inputField.text = "";
    }
   
    public void OnSubmitAnswer()
    {
        string userAnswer = inputField.text.ToString();

        if (userAnswer == "")
        {
            Debug.Log("Answer is incorrect!");
            resultText.text = "Invalid input";
            resultText.color = Color.red;
           
        }
        else
        {
            Debug.Log("Answer is correct.");
            resultText.text = "Valid Input";
            resultText.color = Color.green;
            // Move to the next question and display it
            currentQuestionIndex++;
        }
       
        
        if (currentQuestionIndex < questionsT.Count)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            // No more questions
            //questionPanel.SetActive(false);
            questionText.text = "Questionnaire completed!";
            Debug.Log("Questionnaire completed!");
        }
    }
}

[System.Serializable]
public class QuestionTutorial
{
    public string questionText;

    public QuestionTutorial(string text)
    {
        questionText = text;
    }
}









