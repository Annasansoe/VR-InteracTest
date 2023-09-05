using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFieldGrabber : MonoBehaviour
{
    [Header("Showing the reaction to the player")]
    [SerializeField] private GameObject reactiongGroup;
    [SerializeField] private TMP_Text reactionTextBox;

    [Header("Grab from input and see the result")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text resultText;

    [Header("Question for test")]
    [SerializeField] public GameObject questionPanel;
    [SerializeField] public TMP_Text questionText;

    [Header("Return button")]
    public Button backToMenu;
    string dateTimeStart;
    string dateTimeEnd;
    private static int indexTextTwo;
    int wrongAnswers = 0;
    int rightAnswers = 0;

    public List<Question> questions;
    public int currentQuestionIndex = 0;

    private void Start()
    {
        questions = new List<Question>
    {
        new Question("What is the capital of France?", "Paris"),
        new Question("How many planets are there in our solar system?", "eight"),
        new Question("How many days are there in a week?", "Seven"),
        // ... Add more questions here with their respective expected answers
    };

        DisplayQuestion(currentQuestionIndex);
    }

    public void DisplayQuestion(int index)
    {
        questionText.text = questions[index].questionText;
        inputField.text = "";
    }
   
    public void OnSubmitAnswer()
    {
        string userAnswer = inputField.text.ToString();

        if (userAnswer.ToLower() != questions[currentQuestionIndex].expectedAnswer.ToLower())
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
        reactionTextBox.text = "Welcome to the team, " + userAnswer + "!";
       
        
        if (currentQuestionIndex < questions.Count)
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

    

    public void BackToMenu()
    {
        dateTimeEnd = System.DateTime.UtcNow.ToString();
        CSVManager.AppendToReport(GetReportLine());
        indexTextTwo++;
        wrongAnswers = 0;
        rightAnswers = 0;

    }

    string[] GetReportLine()
    {
        string[] returnable = new string[7];
        returnable[0] = "SceneTwo.csv";
        returnable[1] = "Controllers";
        returnable[2] = indexTextTwo.ToString();
        returnable[3] = wrongAnswers.ToString();
        returnable[4] = rightAnswers.ToString();
        returnable[5] = dateTimeStart;
        returnable[6] = dateTimeEnd;


        return returnable;
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string expectedAnswer;

    public Question(string text, string answer)
    {
        questionText = text;
        expectedAnswer = answer;
    }
}









