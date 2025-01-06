using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    public AudioMixer mixer;
    
    private UIDocument document;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        
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
        GameManager.ResetStopwatch();
        GameManager.StartStopwatch();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void QuitButtonClicked()
    {
        Application.Quit();
        
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

}
