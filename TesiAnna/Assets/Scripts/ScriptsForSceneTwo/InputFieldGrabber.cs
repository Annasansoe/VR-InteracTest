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
    public static DateTime dateTimeStart;
    public static DateTime dateTimeEnd;
    private static int indexTextTwo;
    public static int wrongAnswers = 0;
    public static int rightAnswers = 0;
    private static int numCanc = 0;
    private static int generalClick = 0;
    public static DateTime onEndQuestion;
    private bool timeIsFinished = false;

    public static List<InteractionData> interactionDataList = new List<InteractionData>();
    public class InteractionData
    {
        public int QuestionIndex { get; set; }
        public DateTime TimeEndQ { get; set; }
    }

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
            new Question("The color that you get by mixing blue and yellow is GREEN", "green"),
            new Question("The largest planet in our solar system is JUPITER", "jupiter"),
            new Question("The Earth's natural satellite is called the MOON", "moon"),
            new Question("In a year, there are TWELVE months", "twelve"),
            new Question("The opposite of right is LEFT", "left")
            
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
          
            validText.text = "Valid text";
            validText.gameObject.SetActive(true);
           
            Invoke("HideValidText", _time);
            onEndQuestion = DateTime.Now;
            rightAnswers += 1;
            InteractionData dataOfAnswer = new InteractionData
            {
                QuestionIndex = currentQuestionIndex,
                TimeEndQ = onEndQuestion
            };
            interactionDataList.Add(dataOfAnswer);

            if (validSource != null && validClip != null)
            {
                validSource.PlayOneShot(validClip);
            }

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
            endMenu.SetActive(true);
            questionText.gameObject.SetActive(false); ;
            inputField.gameObject.SetActive(false);
            keyBoard.SetActive(false);
            validText.gameObject.SetActive(false);
            invalidText.gameObject.SetActive(false);
            PlayEndSound();

            Timer scriptAInstance = FindObjectOfType<Timer>();

            if (scriptAInstance != null)
            {
                scriptAInstance.stopTimer();
            }

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
        //dateTimeEnd = DateTime.Now;
        CSVManager.AppendToReport(GetReportLine());
        indexTextTwo++;
        wrongAnswers = 0;
        rightAnswers = 0;
        numCanc = 0;
        generalClick = 0;
        interactionDataList.Clear();
    }

    string[] GetReportLine()
    {
        int i = 0;
        string[] returnable = new string[40];
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









