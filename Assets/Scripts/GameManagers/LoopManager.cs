using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoopManager : MonoBehaviour {
    public static LoopManager instance;

    [SerializeField] GameObject laptopDisplay;

    private void Awake() {
        instance = this;
    }

    private bool enableOptional = false;

    public void StartLoop() {
        Debug.LogWarning("Start Loop");
        LoopIntro1();
    }

    public void SetEnableOptional(bool enable) {
        enableOptional = enable;
    }

    // Intro 1
    private void LoopIntro1() {
        Debug.LogWarning("Start Loop Intro 1");

        if (enableOptional) {
            CutsceneManager.instance.Play(0, (_) => { LoopDayX(); });
        } else {
            LoopDayX();
        }
    }

    // Day X
    private void LoopDayX() {
        Debug.LogWarning("Start Loop Day X");
        CutsceneManager.instance.Play(1, (_) => { LoopScreenIn(); });
    }

    // Screen IN
    private void LoopScreenIn() {
        Debug.LogWarning("Start Loop Screen In");
        CutsceneManager.instance.Play(2, (_) => { LoopLevel(); });
    }

    // Level
    private void LoopLevel() {
        Debug.LogWarning("Start Loop Level");

        laptopDisplay.SetActive(true);

        ChallengeManager.instance.StartDay(2, 2, 2, 20, 0, new Vector3(0.5f, 0.25f, 0.25f),
            () => { laptopDisplay.SetActive(false); LoopScreenOut(); },
            () => { laptopDisplay.SetActive(false); Debug.LogWarning("L"); });
    }

    // Screen OUT
    private void LoopScreenOut() {
        Debug.LogWarning("Start Loop Screen Out");
        CutsceneManager.instance.Play(3, (_) => { LoopIntro2(); });
    }

    // Intro 2
    private void LoopIntro2() {
        Debug.LogWarning("Start Loop Intro 2");

        if (enableOptional) {
            CutsceneManager.instance.Play(4, (_) => { LoopShop(); });
        } else {
            LoopShop();
        }
    }

    // Shop
    private void LoopShop() {
        Debug.LogWarning("Start Loop Shop");

        LoopIntro3();
    }

    // Intro 3
    private void LoopIntro3() {
        Debug.LogWarning("Start Loop Intro 3");

        if (enableOptional) {
            CutsceneManager.instance.Play(5, (_) => { LoopIntro1(); });
        } else {
            LoopIntro1();
        }
    }
}