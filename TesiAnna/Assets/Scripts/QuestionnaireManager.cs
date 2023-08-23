using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireManager : MonoBehaviour
{
    public GameObject questionPanel;
    public Text questionText;
    public InputField answerInputField;
    public Button submitButton;

    private List<Question> questions;
    private int currentQuestionIndex = 0;

    private void Start()
    {
        questions = new List<Question>
        {
            new Question("Capital of France:"),
            new Question("Number of Planets:"),
            new Question("Primary Color:"),
            // ... Add more questions here
        };

        DisplayQuestion(currentQuestionIndex);
    }

    public void DisplayQuestion(int index)
    {
        questionText.text = questions[index].questionText;
        answerInputField.text = "";
    }

    public void OnSubmitAnswer()
    {
        string userAnswer = answerInputField.text;

        // Validate user input if needed
        // For this example, let's check if the user's answer matches the expected answer
        if (userAnswer.ToLower() == questions[currentQuestionIndex].expectedAnswer.ToLower())
        {
            Debug.Log("Answer is correct!");
        }
        else
        {
            Debug.Log("Answer is incorrect.");
        }

        // Move to the next question
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            // No more questions
            questionPanel.SetActive(false);
            Debug.Log("Questionnaire completed!");
        }
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string expectedAnswer;

    public Question(string text)
    {
        questionText = text;
        expectedAnswer = ""; // Set the expected answer for validation
    }
}