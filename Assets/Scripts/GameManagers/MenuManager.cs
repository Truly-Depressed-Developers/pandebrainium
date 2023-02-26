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

    public void Quit() {
        Application.Quit();
    }
}
