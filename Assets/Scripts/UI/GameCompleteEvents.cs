using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameCompleteEvents : MonoBehaviour
{
    private UIDocument document;
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        document.rootVisualElement.Q<Button>("MainMenuButton").clicked += () =>
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        };
        
        var wineLabel = document.rootVisualElement.Q<Label>("WineLabel");
        var timeLabel = document.rootVisualElement.Q<Label>("TimeLabel");
        
        var time = TimeSpan.FromSeconds(GameManager.CurrentTime);
        
        wineLabel.text = $"WINE: {GameManager.WineCollected}";
        timeLabel.text = $"TIME: {time.Minutes} minutes, {time.Seconds} seconds";
    }
}
