using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] int mainSceneBuildIndex;

    public void LoadGame() {
        SceneManager.LoadScene(mainSceneBuildIndex);
        SceneManager.sceneLoaded += (_,_) => { LoopManager.instance.StartLoop(); };
    }

    private void StartGameLoop(Scene s, LoadSceneMode lsm) {
        LoopManager.instance.StartLoop();
    }

    public void Quit() {
        Application.Quit();
    }
}
