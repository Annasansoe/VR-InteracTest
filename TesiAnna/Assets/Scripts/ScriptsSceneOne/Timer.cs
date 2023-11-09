using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private float timeDuration = 3f*60;
   // private float timeDuration = 10f;

    public static float timer;
    public static int timeIsUp = 0;

    
    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;

    [SerializeField]
    private AudioSource tickingAudioSource; // Attach an AudioSource for the ticking sound here
    [SerializeField]

    [Header("Audios")]
    public AudioSource audioSourceEnd;
    public AudioClip soundClipEnd;

    private float flashTimer;
    private float flashDuration = 1f;

    private bool shouldUpdateTimerDisplay = true;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldUpdateTimerDisplay && timer>0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
            if (Mathf.FloorToInt(timer / 60) != Mathf.FloorToInt((timer + Time.deltaTime) / 60) ||
               Mathf.FloorToInt(timer % 60) != Mathf.FloorToInt((timer + Time.deltaTime) % 60))
            {
                PlayTickingSound();
            }
        }
        else
        {
            Flash();
        }
        if (firstMinute.text.Equals("0") && secondMinute.text.Equals("0") && firstSecond.text.Equals("0") && secondSecond.text.Equals("0"))
        {
            // Timer is up
            timeIsUp = 1;
            
           // PlaySoundEnd(); // Play the end sound when the timer is up
        }
    }


    private void ResetTimer()
    {
        timer = timeDuration;
        timeIsUp = 0;
    }

   public void stopTimer()
    {
        /*firstMinute.gameObject.SetActive(false);
        secondMinute.gameObject.SetActive(false);
        firstSecond.gameObject.SetActive(false);
        secondSecond.gameObject.SetActive(false);

        tickingAudioSource.Stop();*/
        StopUpdatingTimerDisplay();
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();

    }

    private void Flash()
    {
        if(timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
           
        }
       

        if(flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }
        else if (flashTimer >= flashDuration / 2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }
        else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
       

    }

    private void SetTextDisplay(bool enable)
    {
        firstMinute.enabled = enable;
        secondMinute.enabled = enable;
        separator.enabled = enable;
        firstSecond.enabled = enable;
        secondSecond.enabled = enable;
    }

    private void PlayTickingSound()
    {
        if (tickingAudioSource != null)
        {
            tickingAudioSource.Play();
        }
    }
    public void StartUpdatingTimerDisplay()
    {
        shouldUpdateTimerDisplay = true;
    }

    // Call this method to stop updating the timer display
    public void StopUpdatingTimerDisplay()
    {
        shouldUpdateTimerDisplay = false;
    }


}
