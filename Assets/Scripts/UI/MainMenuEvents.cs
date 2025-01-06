using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    public AudioMixer mixer;

    private VisualElement mainMenuContainer;
    private VisualElement howToPlayContainer;
    
    private UIDocument document;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        mainMenuContainer = document.rootVisualElement.Q<VisualElement>("MainMenuContainer");
        howToPlayContainer = document.rootVisualElement.Q<VisualElement>("HowToPlayContainer");
        
        var playButton = document.rootVisualElement.Q<Button>("PlayButton");
        playButton.clicked += PlayButtonClicked;
        
        var quitButton = document.rootVisualElement.Q<Button>("QuitButton");
        quitButton.clicked += QuitButtonClicked;

        var sfxSlider = document.rootVisualElement.Q<Slider>("SfxSlider");
        var musicSlider = document.rootVisualElement.Q<Slider>("MusicSlider");
        
        mixer.GetFloat("sfx", out float sfxValue);
        mixer.GetFloat("music", out float musicValue);
        sfxSlider.value = Mathf.Pow(10, sfxValue / 20);
        musicSlider.value = Mathf.Pow(10, musicValue / 20);
        
        sfxSlider.RegisterCallback<ChangeEvent<float>>(SfxChanged);
        musicSlider.RegisterCallback<ChangeEvent<float>>(MusicChanged);
        
        var continueButton = document.rootVisualElement.Q<Button>("ContinueButton");
        var backButton = document.rootVisualElement.Q<Button>("BackButton");

        continueButton.clicked += () =>
        {
            GameManager.ResetStopwatch();
            GameManager.StartStopwatch();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        };

        backButton.clicked += () =>
        {
            howToPlayContainer.style.display = DisplayStyle.None;
            mainMenuContainer.style.display = DisplayStyle.Flex;
        };
    }

    private void SfxChanged(ChangeEvent<float> e)
    {
        mixer.SetFloat("sfx", Mathf.Log10(e.newValue) * 20);
    }

    private void MusicChanged(ChangeEvent<float> e)
    {
        mixer.SetFloat("music", Mathf.Log10(e.newValue) * 20);        
    }
    
    private void PlayButtonClicked()
    {
        mainMenuContainer.style.display = DisplayStyle.None;
        howToPlayContainer.style.display = DisplayStyle.Flex;
    }
    
    private void QuitButtonClicked()
    {
        Application.Quit();
        
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

}
