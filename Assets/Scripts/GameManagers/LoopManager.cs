using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoopManager : MonoBehaviour {
    public static LoopManager instance;

    private void Awake() {
        instance = this;
    }

    private bool enableOptional = true;

    public void StartLoop() {
        LoopIntro1();
    }

    public void SetEnableOptional(bool enable) {
        enableOptional = enable;
    }

    // Intro 1
    private void LoopIntro1() {
        if (enableOptional) {
            CutsceneManager.instance.Play(0, (_) => { LoopDayX(); });
        } else {
            LoopDayX();
        }
    }

    // Day X
    private void LoopDayX() {
        CutsceneManager.instance.Play(1, (_) => { LoopScreenIn(); });
    }

    // Screen IN
    private void LoopScreenIn() {
        CutsceneManager.instance.Play(2, (_) => { LoopLevel(); });
    }

    // Level
    private void LoopLevel() {
        LoopScreenOut();
    }

    // Screen OUT
    private void LoopScreenOut() {
        CutsceneManager.instance.Play(3, (_) => { LoopIntro2(); });
    }

    // Intro 2
    private void LoopIntro2() {
        if (enableOptional) {
            CutsceneManager.instance.Play(4, (_) => { LoopShop(); });
        } else {
            LoopShop();
        }
    }

    // Shop
    private void LoopShop() {
        LoopIntro3();
    }

    // Intro 3
    private void LoopIntro3() {
        if (enableOptional) {
            CutsceneManager.instance.Play(5, (_) => { LoopIntro1(); });
        } else {
            LoopIntro1();
        }
    }
}