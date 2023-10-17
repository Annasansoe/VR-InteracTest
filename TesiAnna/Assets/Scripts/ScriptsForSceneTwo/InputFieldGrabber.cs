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
    string dateTimeStart;
    string dateTimeEnd;
    private static int indexTextTwo;
    int wrongAnswers = 0;
    int rightAnswers = 0;
    private static int numCanc = 0;

    public List<Question> questions;
    public int currentQuestionIndex = 0;


    [SerializeField] private float _time = 1f;

    private void Start()
    {
        dateTimeStart = System.DateTime.UtcNow.ToString();
        questions = new List<Question>
    {
        new Question("The capital of France is PARIS", "Paris"),
        new Question("In the solar system there are EIGHT planets", "eight"),
        new Question("In a week there are SEVEN days", "Seven"),
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
        numCanc++;
    }

    public void OnSubmitAnswer()
    {
        string userAnswer = inputField.text.ToString();

        if (userAnswer.ToLower() != questions[currentQuestionIndex].expectedAnswer.ToLower())
        {
            Debug.Log("Answer is incorrect!");
            ShowMessageIn();
            if (invalidSource != null && invalidClip != null)
            {
                invalidSource.PlayOneShot(invalidClip);
            }
            resultText.color = Color.red;
            wrongAnswers++;
        }
        else
        {
            Debug.Log("Answer is correct.");
            ShowMessage();
            if (validSource != null && validClip != null)
            {
                validSource.PlayOneShot(validClip);
            }
            resultText.color = Color.green;
            // Move to the next question and display it
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
            questionText.text = "Questionnaire completed!";
            PlayEndSound();
            Debug.Log("Questionnaire completed!");
        }
    }
    private IEnumerator ShowMessage()
    {
        resultText.text = "Valid Input";
        resultText.enabled = true;

        yield return new WaitForSeconds(_time);

        resultText.enabled = false;
    }

    private IEnumerator ShowMessageIn()
    {
        resultText.text = "Invalid Input";
        resultText.enabled = true;

        yield return new WaitForSeconds(_time);

        resultText.enabled = false;
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
        dateTimeEnd = System.DateTime.UtcNow.ToString();
        CSVManager.AppendToReport(GetReportLine());
        indexTextTwo++;
        wrongAnswers = 0;
        rightAnswers = 0;
        numCanc = 0;
    }

    string[] GetReportLine()
    {
        string[] returnable = new string[8];
        returnable[0] = "SceneTwo.csv";
        returnable[1] = "Controllers";
        returnable[2] = indexTextTwo.ToString();
        returnable[3] = wrongAnswers.ToString();
        returnable[4] = rightAnswers.ToString();
        returnable[5] = numCanc.ToString();
        returnable[6] = dateTimeStart;
        returnable[7] = dateTimeEnd;
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









