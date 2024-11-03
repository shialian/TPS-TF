using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Button volumeButton;

    [SerializeField]
    private Button quitButton;

    private void Awake()
    {
        volumeButton.onClick.AddListener(OpenVolumeSetting);
        quitButton.onClick.AddListener(QuitGame);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        // intro.SetActive(true);
        // intro.GetComponent<GameIntroduction>().Init("Start");
        // gameObject.SetActive(false);
    }

    public void OpenVolumeSetting()
    {
        EventManager.Invoke(EventType.UI.OpenVolumeSetting);
    }

    private void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
