using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int collectedObjects = 0;
    private int targetObjects = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void CollectObject()
    {
        collectedObjects++;

        if (collectedObjects >= targetObjects)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        // Handle level completion logic here
    }
}
