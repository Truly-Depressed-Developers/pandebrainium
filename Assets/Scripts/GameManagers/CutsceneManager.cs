using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class CutsceneManager : MonoBehaviour {

    [SerializeField] List<Canvas> cutscenes;

    public static CutsceneManager instance;

    void Awake() {
        instance = this;
    }

    public void Play(int index, Action<PlayableDirector> onStopped) {
        Canvas canvas = cutscenes[index];
        canvas.TryGetComponent<PlayableDirector>(out PlayableDirector playableDirector);

        if (playableDirector == null) {
            Debug.LogWarning($"Cutscene index {index} is not defined");
            return;
        }

        if(canvas.gameObject.activeInHierarchy == false) {
            canvas.gameObject.SetActive(true);
        }

        Debug.Log("Playing " + playableDirector.name);
        playableDirector.Play();

        playableDirector.stopped += (PlayableDirector _) => { canvas.gameObject.SetActive(false) ; };
        playableDirector.stopped += onStopped;
    }
}
