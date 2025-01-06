using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuEvents : MonoBehaviour
{
    private UIDocument document;

    public AudioMixer mixer;

    private Slider sfxSlider;
    private Slider musicSlider;
    private Button continueButton;
    private Button quitButton;
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        sfxSlider = document.rootVisualElement.Q<Slider>("SfxSlider");
        musicSlider = document.rootVisualElement.Q<Slider>("MusicSlider");
        continueButton = document.rootVisualElement.Q<Button>("ContinueButton");
        quitButton = document.rootVisualElement.Q<Button>("QuitButton");

        mixer.GetFloat("sfx", out float sfxValue);
        mixer.GetFloat("music", out float musicValue);
        sfxSlider.value = Mathf.Pow(10, sfxValue / 20);
        musicSlider.value = Mathf.Pow(10, musicValue / 20);
        
        continueButton.RegisterCallback<ClickEvent>(ContinueClicked);
        quitButton.RegisterCallback<ClickEvent>(QuitClicked);
        sfxSlider.RegisterCallback<ChangeEvent<float>>(SfxChanged);
        musicSlider.RegisterCallback<ChangeEvent<float>>(MusicChanged);
    }

    private void OnDisable()
    {
        continueButton.UnregisterCallback<ClickEvent>(ContinueClicked);
        quitButton.UnregisterCallback<ClickEvent>(QuitClicked);
        sfxSlider.UnregisterCallback<ChangeEvent<float>>(SfxChanged);
        musicSlider.UnregisterCallback<ChangeEvent<float>>(MusicChanged);
    }

    private void SfxChanged(ChangeEvent<float> e)
    {
        mixer.SetFloat("sfx", Mathf.Log10(e.newValue) * 20);
    }

    private void MusicChanged(ChangeEvent<float> e)
    {
        mixer.SetFloat("music", Mathf.Log10(e.newValue) * 20);        
    }

    private void ContinueClicked(ClickEvent evt)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInput>().enabled = true;
    }

    private void QuitClicked(ClickEvent evt)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
