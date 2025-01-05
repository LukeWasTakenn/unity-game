using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathSceneEvents : MonoBehaviour
{
    private UIDocument document;
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        
        var restartButton = document.rootVisualElement.Q<Button>("RestartButton");
        var mainMenuButton = document.rootVisualElement.Q<Button>("MainMenuButton");
        
        restartButton.clicked += () => { SceneManager.LoadScene(GameManager.LastSceneIndex); };
        mainMenuButton.clicked += () => { SceneManager.LoadScene(0); };
    }
}
