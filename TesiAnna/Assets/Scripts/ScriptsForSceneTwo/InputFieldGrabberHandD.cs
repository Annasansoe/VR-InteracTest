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
    
    private static int indexTextTwoHand;
    public static int wrongAnswers = 0;
    public static int rightAnswers = 0;
    private static int num = 0;
    private static int generalClick = 0;
    private bool timeIsFinished = false;
    public static DateTime onStartQuestion;
    public static DateTime onEndQuestion;
    public static DateTime dateTimeStart;
    public static DateTime dateTimeEnd;

    public static List<InteractionData> interactionDataList = new List<InteractionData>();
    public class InteractionData
    {
        public int QuestionIndex { get; set; }
        public DateTime TimeEndQ { get; set; }
    }

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
            if (!timeIsFinished)
            {
                dateTimeEnd = DateTime.Now;
                endMenu.SetActive(true);
                questionText.gameObject.SetActive(false); ;
                inputField.gameObject.SetActive(false);
                keyBoard.SetActive(false);
                validText.gameObject.SetActive(false);
                invalidText.gameObject.SetActive(false);
                PlayEndSound();
                Debug.Log("Questionnaire completed!");
                timeIsFinished = true;
            }
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
            wrongAnswers += 1;
            invalidText.text = "Invalid text";
            invalidText.gameObject.SetActive(true);
            Invoke("HideInvalidText", _time);
            if (invalidSource != null && invalidClip != null)
            {
                invalidSource.PlayOneShot(invalidClip);
            }
        }
        else
        {
            Debug.Log("Answer is correct.");
            rightAnswers += 1;

            onEndQuestion = DateTime.Now;

            InteractionData dataOfAnswer = new InteractionData
            {
                QuestionIndex = currentQuestionIndex,
                TimeEndQ = onEndQuestion
            };
            interactionDataList.Add(dataOfAnswer);

            validText.text = "Valid text";
            validText.gameObject.SetActive(true);
           
            Invoke("HideValidText", _time);
            if (validSource != null && validClip != null)
            {
                validSource.PlayOneShot(validClip);
            }            
            
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
            dateTimeEnd = DateTime.Now;
            dateTimeEnd = DateTime.Now;
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
       
        CSVManager.AppendToReport(GetReportLine());
        indexTextTwoHand++;
        wrongAnswers = 0;
        rightAnswers = 0;
        num = 0;
        generalClick = 0;
        interactionDataList.Clear();
    }

    string[] GetReportLine()
    {
        int i = 0;
        string[] returnable = new string[40];
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
        foreach (InteractionData interaction in interactionDataList)
        {
            i++;
            string interactionLine = $"{interaction.QuestionIndex},{interaction.TimeEndQ}";
            returnable[9 + i] = interactionLine;
        }
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









