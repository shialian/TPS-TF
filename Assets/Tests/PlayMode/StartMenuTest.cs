using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartMenuTest
{
    private StartMenu startMenu;
    private VolumeSetting volumeSetting;

    [SetUp]
    public void SetUp()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Start");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Assert.AreEqual("Start", scene.name, "Scene should be Start.");
        SceneManager.sceneLoaded -= OnSceneLoaded;

        startMenu = Object.FindFirstObjectByType<StartMenu>();
        volumeSetting = Object.FindFirstObjectByType<VolumeSetting>(FindObjectsInactive.Include);

        Assert.IsNotNull(startMenu, "Can not find StartMenu object");
        Assert.IsNotNull(volumeSetting, "Can not find VolumeSetting object");
    }

    [UnityTest]
    public IEnumerator Test_Awake()
    {
        yield return null;

        Assert.AreEqual(CursorLockMode.None, Cursor.lockState);
        Assert.IsTrue(Cursor.visible);
    }

    [UnityTest]
    public IEnumerator Test_OpenVolumeSetting()
    {
        yield return null;

        // Arrange
        Button volumeButton = startMenu.GetType().GetField("volumeButton", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(startMenu) as Button;
        MethodInfo prepareInvoke = typeof(UnityEvent).GetMethod("PrepareInvoke", BindingFlags.NonPublic | BindingFlags.Instance);
        IList resultList = prepareInvoke.Invoke(volumeButton.onClick, null) as IList;

        // Assert
        Assert.IsNotNull(volumeButton, "StartMenu should have a volume button.");
        Assert.AreEqual(1, resultList.Count, "Volume button should have one event.");

        // Act
        volumeButton.onClick.Invoke();

        // Assert
        Assert.IsTrue(volumeSetting.gameObject.activeInHierarchy, "VolumeSetting should be active in hierarchy.");

        // Clean
        MethodInfo prevMethod = volumeSetting.GetType().GetMethod("Prev", BindingFlags.NonPublic | BindingFlags.Instance);
        prevMethod.Invoke(volumeSetting, null);
    }

    [UnityTest]
    public IEnumerator Test_QuitGame()
    {
        yield return null;

        // Arrange
        Button quitButton = startMenu.GetType().GetField("quitButton", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(startMenu) as Button;
        MethodInfo prepareInvoke = typeof(UnityEvent).GetMethod("PrepareInvoke", BindingFlags.NonPublic | BindingFlags.Instance);
        IList resultList = prepareInvoke.Invoke(quitButton.onClick, null) as IList;

        // Assert
        Assert.IsNotNull(quitButton, "StartMenu should have a quit button.");
        Assert.AreEqual(1, resultList.Count, "Quit button should have one event.");
        Assert.DoesNotThrow(() => quitButton.onClick.Invoke(), "QuitGame should not throw any exception.");
    }
}
