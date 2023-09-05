using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFieldGrabberHand : MonoBehaviour
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
    private static int indexTextTwoHand;
    int wrongAnswers = 0;
    int rightAnswers = 0;
    private static int num = 0;


    //public Button submitButton;

    public List<QuestionHand> questions;
    public int currentQuestionIndex = 0;

    private void Start()
    {
        dateTimeStart = System.DateTime.UtcNow.ToString();
        questions = new List<QuestionHand>
    {
        new QuestionHand("What is the capital of France?", "Paris"),
        new QuestionHand("How many planets are there in our solar system?", "eight"),
        new QuestionHand("How many days are there in a week?", "Seven"),
        // ... Add more questions here with their respective expected answers
    };

        DisplayQuestion(currentQuestionIndex);
    }

    public void DisplayQuestion(int index)
    {
        questionText.text = questions[index].questionText;
        inputField.text = "";
    }

   public void OnClickBackspace()
    {
        num++;
    }
   
    public void OnSubmitAnswer()
    {
        string userAnswer = inputField.text.ToString();

        if (userAnswer.ToLower() != questions[currentQuestionIndex].expectedAnswer.ToLower())
        {
            Debug.Log("Answer is incorrect!");
            resultText.text = "Invalid input";
            resultText.color = Color.red;
            wrongAnswers++;


        }
        else
        {
            Debug.Log("Answer is correct.");
            resultText.text = "Valid Input";
            resultText.color = Color.green;
            rightAnswers++;
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
        //string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        //totScoreEnd = totScore;
        //verificationText.gameObject.SetActive(true);
        dateTimeEnd = System.DateTime.UtcNow.ToString();
        CSVManager.AppendToReport(GetReportLine());
        indexTextTwoHand++;
        wrongAnswers = 0;
        rightAnswers = 0;
        num = 0;

    }

    string[] GetReportLine()
    {
        string[] returnable = new string[8];
        returnable[0] = "SceneTwo.csv";
        returnable[1] = "Hands";
        returnable[2] = indexTextTwoHand.ToString();
        returnable[3] = wrongAnswers.ToString();
        returnable[4] = rightAnswers.ToString();
        returnable[5] = num.ToString();
        returnable[6] = dateTimeStart;
        returnable[7] = dateTimeEnd;

        return returnable;
    }
}

[System.Serializable]
public class QuestionHand
{
    public string questionText;
    public string expectedAnswer;

    public QuestionHand(string text, string answer)
    {
        questionText = text;
        expectedAnswer = answer;
    }
}









