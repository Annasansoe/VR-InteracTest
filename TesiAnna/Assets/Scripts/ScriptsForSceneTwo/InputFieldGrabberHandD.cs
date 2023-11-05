using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFieldGrabberHandD : MonoBehaviour
{
    [Header("Showing the reaction to the player")]
    [SerializeField] private GameObject reactiongGroup;
    [SerializeField] private TMP_Text reactionTextBox;

    [Header("KeyBoard")]
    public GameObject keyBoard;

    [Header("End Menu")]
    public GameObject endMenu;

    [Header("Grab from input and see the result")]
    [SerializeField] TMP_InputField inputField;

    [SerializeField] TMP_Text validText;
    [SerializeField] TMP_Text invalidText;

    [Header("Audio sounds")]
    public AudioSource endSound;
    public AudioClip soundClipEnd;
    [Space]
    public AudioSource validSource;
    public AudioClip validClip;
    [Space]
    public AudioSource invalidSource;
    public AudioClip invalidClip;

    [Header("Question for test")]
    [SerializeField] public GameObject questionPanel;
    [SerializeField] public TMP_Text questionText;

    [Header("Return button")]
    public Button backToMenu;
    DateTime dateTimeStart;
    DateTime dateTimeEnd;
    private static int indexTextTwoHand;
    int wrongAnswers = 0;
    int rightAnswers = 0;
    private static int num = 0;
    private static int generalClick = 0;
    DateTime onStartQuestion;
    DateTime onEndQuestion;

    [SerializeField] private float _time = 1f;


    //public Button submitButton;

    public List<QuestionHandD> questions;
    public int currentQuestionIndex = 0;

    private void Start()
    {
        validText.gameObject.SetActive(false);
        invalidText.gameObject.SetActive(false);
        
        dateTimeStart = DateTime.Now;
        questions = new List<QuestionHandD>
    {
        new QuestionHandD("The capital of France is PARIS", "Paris"),
        new QuestionHandD("In the solar system there are EIGHT planets", "eight"),
        new QuestionHandD("In a week there are SEVEN days", "Seven"),
        // ... Add more questions here with their respective expected answers
    };

        DisplayQuestion(currentQuestionIndex);
    }
    void Update()
    {
        if (Timer.timeIsUp == 1)
        {
            endMenu.SetActive(true);
            questionText.gameObject.SetActive(false); ;
            inputField.gameObject.SetActive(false);
            keyBoard.SetActive(false);
            validText.gameObject.SetActive(false);
            invalidText.gameObject.SetActive(false);
            PlayEndSound();
            Debug.Log("Questionnaire completed!");
        }
    }

    private void HideValidText()
    {
        validText.gameObject.SetActive(false);
    }

    private void HideInvalidText()
    {
        invalidText.gameObject.SetActive(false);
    }

    public void DisplayQuestion(int index)
    {
        questionText.text = questions[index].questionText;
        inputField.text = "";
        onStartQuestion = DateTime.Now;
    }

   public void OnClickBackspace()
    {
        num++;
    }
    public void OnClickGeneral()
    {
        generalClick++;
    }

    public void OnSubmitAnswer()
    {
        string userAnswer = inputField.text.ToString();

        if (userAnswer.ToLower() != questions[currentQuestionIndex].expectedAnswer.ToLower())
        {
            Debug.Log("Answer is incorrect!");
            invalidText.text = "Invalid text";
            invalidText.gameObject.SetActive(true);
            Invoke("HideInvalidText", _time);
            if (invalidSource != null && invalidClip != null)
            {
                invalidSource.PlayOneShot(invalidClip);
            }
           
            wrongAnswers++;


        }
        else
        {
            Debug.Log("Answer is correct.");
            validText.text = "Valid text";
            validText.gameObject.SetActive(true);
            onEndQuestion = DateTime.Now;
            Invoke("HideValidText", _time);
            if (validSource != null && validClip != null)
            {
                validSource.PlayOneShot(validClip);
            }
            
            rightAnswers++;
            // Move to the next question and display it
            currentQuestionIndex++;
        }
        reactionTextBox.text = "Welcome to the team, " + userAnswer + "!";
       
        
        if (currentQuestionIndex < questions.Count && Timer.timeIsUp != 1) 
        {
            DisplayQuestion(currentQuestionIndex);
            
        }
        else
        {
            // No more questions
            //questionPanel.SetActive(false);
            endMenu.SetActive(true);
            questionText.gameObject.SetActive(false); ;
            inputField.gameObject.SetActive(false);
            keyBoard.SetActive(false);
            validText.gameObject.SetActive(false);
            invalidText.gameObject.SetActive(false);
            PlayEndSound();
            Debug.Log("Questionnaire completed!");
            BackToMenu();
        }
    }

    void PlayEndSound()
    {
        if (endSound != null && soundClipEnd != null)
        {
            endSound.PlayOneShot(soundClipEnd);
        }
    }

    public void BackToMenu()
    {
        dateTimeEnd = DateTime.Now;
        CSVManager.AppendToReport(GetReportLine());
        indexTextTwoHand++;
        wrongAnswers = 0;
        rightAnswers = 0;
        num = 0;
        generalClick = 0;

    }

    string[] GetReportLine()
    {
        string[] returnable = new string[11];
        returnable[0] = "SceneTwo.csv";
        returnable[1] = "Hand";
        returnable[2] = "Direct";
        returnable[3] = indexTextTwoHand.ToString();
        returnable[4] = wrongAnswers.ToString();
        returnable[5] = rightAnswers.ToString();
        returnable[6] = num.ToString();
        returnable[7] = generalClick.ToString();
        returnable[8] = dateTimeStart.ToString();
        returnable[9] = dateTimeEnd.ToString();    
        returnable[10] = onStartQuestion.ToString();
        returnable[11] = onEndQuestion.ToString();
        return returnable;
    }
}

[System.Serializable]

public class QuestionHandD
{
    public string questionText;
    public string expectedAnswer;

    public QuestionHandD(string text, string answer)
    {
        questionText = text;
        expectedAnswer = answer;
    }
}









