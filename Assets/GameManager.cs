using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public static int LastSceneIndex;
    public static int WineCollected = 0;
    public static float CurrentTime = 0f;

    private static bool _stopwatchRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!_stopwatchRunning) return;
        
        CurrentTime += Time.deltaTime;
    }

    public static void StartStopwatch()
    {
        _stopwatchRunning = true;
    }

    public static void StopStopwatch()
    {
        _stopwatchRunning = false;
    }

    public static void ResetStopwatch()
    {
        _stopwatchRunning = false;
        CurrentTime = 0f;
    }
}
