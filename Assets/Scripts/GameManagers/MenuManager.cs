using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    private void Awake() {
        instance = this;
    }

    [SerializeField] int mainSceneBuildIndex;

    public void LoadGame() {
        SceneManager.LoadScene(mainSceneBuildIndex);
        SceneManager.sceneLoaded += LoadGameHandler;
    }

    private void LoadGameHandler(Scene s, LoadSceneMode lsm) {
        LoopManager.instance.StartLoop();
        SceneManager.sceneLoaded -= LoadGameHandler;
    }

    public void GoBackToMenu() {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    public void Quit() {
        Application.Quit();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            PlayerPrefs.DeleteKey("tutorial_completed");
        }
    }
}
