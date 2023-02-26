using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoopManager : MonoBehaviour {
    public static LoopManager instance;

    [SerializeField] GameObject laptopDisplay;

    [SerializeField] DayCounter dayCounter;

    [SerializeField] GameObject brainShopScreen;
    [SerializeField] ShopManager brainShopManager;

    public int currentDay { get; private set; }

    private void Awake() {
        currentDay = 1;
        instance = this;
    }

    private bool enableOptional = false;

    public void StartLoop() {
        if(enableOptional == true) {
            currentDay = 0;
        }

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
        dayCounter.SetText(currentDay);

        CutsceneManager.instance.Play(1, (_) => { LoopScreenIn(); });
    }

    // Screen IN
    private void LoopScreenIn() {
        CutsceneManager.instance.Play(2, (_) => { LoopLevel(); });
    }

    // Level
    private void LoopLevel() {
        var brain = PlayerManager.instance.selectedBrain;

        laptopDisplay.SetActive(true);
        ChallengeManager.instance.StartDay(1 + brain.strength, 1 + brain.dexterity, 1+brain.intelligence, PlayerManager.instance.sanity, currentDay, new Vector3(0.5f, 0.25f, 0.25f),
            () => { laptopDisplay.SetActive(false); LoopScreenOut(); },
            () => { laptopDisplay.SetActive(false); Debug.LogWarning("L"); });
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
        brainShopScreen.SetActive(true);

        brainShopManager.OnBuyAnyBrain += () => { brainShopScreen.SetActive(false); LoopIntro3(); };
    }

    // Intro 3
    private void LoopIntro3() {
        ++currentDay;

        if (enableOptional) {
            CutsceneManager.instance.Play(5, (_) => { LoopIntro1(); });
        } else {
            LoopIntro1();
        }
    }
}