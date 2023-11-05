using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFieldGrabber : MonoBehaviour
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

    [Header("Question for test")]
    [SerializeField] public GameObject questionPanel;
    [SerializeField] public TMP_Text questionText;

    [Header("Audio sounds")]
    public AudioSource endSound;
    public AudioClip soundClipEnd;
    [Space]
    public AudioSource validSource;
    public AudioClip validClip;
    [Space]
    public AudioSource invalidSource;
    public AudioClip invalidClip;

    [Header("Return button")]
    public Button backToMenu;
    DateTime dateTimeStart;
    DateTime dateTimeEnd;
    private static int indexTextTwo;
    int wrongAnswers = 0;
    int rightAnswers = 0;
    private static int numCanc = 0;
    private static int generalClick = 0;
    DateTime onStartQuestion;
    DateTime onEndQuestion;

    public List<Question> questions;
    public int currentQuestionIndex = 0;


    private float _time = 3f;

   void Start()
    {
        validText.gameObject.SetActive(false);
        invalidText.gameObject.SetActive(false);
        dateTimeStart = DateTime.Now;
        questions = new List<Question>
        {
            new Question("The capital of France is PARIS", "Paris"),
            new Question("In the solar system there are EIGHT planets", "eight"),
            new Question("In a week there are SEVEN days", "Seven"),
            // ... Add more questions here with their respective expected answers
        };

        DisplayQuestion(currentQuestionIndex);
       
    }
    void Update()
    {
        if(Timer.timeIsUp == 1)
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
        numCanc++;
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
            onEndQuestion = DateTime.Now;
            validText.text = "Valid text";
            validText.gameObject.SetActive(true);

            Invoke("HideValidText", _time);
            if (validSource != null && validClip != null)
            {
                validSource.PlayOneShot(validClip);
            }
            currentQuestionIndex++;
            rightAnswers++;
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
        indexTextTwo++;
        wrongAnswers = 0;
        rightAnswers = 0;
        numCanc = 0;
        generalClick = 0;
    }

    string[] GetReportLine()
    {
        string[] returnable = new string[11];
        returnable[0] = "SceneTwo.csv";
        returnable[1] = "Controllers";
        returnable[2] = "RayCasting";
        returnable[3] = indexTextTwo.ToString();
        returnable[4] = wrongAnswers.ToString();
        returnable[5] = rightAnswers.ToString();
        returnable[6] = numCanc.ToString();
        returnable[7] = generalClick.ToString();
        returnable[8] = dateTimeStart.ToString();
        returnable[9] = dateTimeEnd.ToString();
        returnable[10] = onStartQuestion.ToString();
        returnable[11] = onEndQuestion.ToString();
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









