using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        
        var playButton = document.rootVisualElement.Q<Button>("PlayButton");
        playButton.clicked += PlayButtonClicked;
        
        var settingsButton = document.rootVisualElement.Q<Button>("SettingsButton");
        settingsButton.clicked += SettingsButtonClicked;
        
        var quitButton = document.rootVisualElement.Q<Button>("QuitButton");
        quitButton.clicked += QuitButtonClicked;

    }

    private void PlayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SettingsButtonClicked()
    {
        //
    }

    private void QuitButtonClicked()
    {
        Application.Quit();
        
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

}
