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

    private bool enabledOptionals = false;

    public void StartLoop() {
        if(enabledOptionals == true) {
            currentDay = 0;
        }

        LoopIntro1();
    }

    public void SetEnableOptional(bool enable) {
        enabledOptionals = enable;
    }

    // Intro 1
    private void LoopIntro1() {
        if (enabledOptionals) {
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

        var sanity = enabledOptionals == true ? -542 : PlayerManager.instance.sanity;
        Vector3 probabilities = enabledOptionals ? new Vector3(1f, 0, 0) : new Vector3(0.33f, 0.34f, 0.33f);

        laptopDisplay.SetActive(true);
        ChallengeManager.instance.StartDay(1 + brain.strength, 1 + brain.dexterity, 1+brain.intelligence, sanity, currentDay, probabilities,
            () => { laptopDisplay.SetActive(false); LoopScreenOut(); },
            () => { laptopDisplay.SetActive(false); Debug.LogWarning("L"); },
            enabledOptionals
            );
    }

    // Screen OUT
    private void LoopScreenOut() {
        CutsceneManager.instance.Play(3, (_) => { LoopIntro2(); });
    }

    // Intro 2
    private void LoopIntro2() {
        if (enabledOptionals) {
            CutsceneManager.instance.Play(4, (_) => { LoopShop(); });
        } else {
            LoopShop();
        }
    }

    // Shop
    private void LoopShop() {
        brainShopManager.SpawnBrainsAndSetProbabilitiesList();
        brainShopScreen.SetActive(true);

        brainShopManager.OnBuyAnyBrain += () => { brainShopScreen.SetActive(false); LoopIntro3(); };
    }

    // Intro 3
    private void LoopIntro3() {
        ++currentDay;

        if (enabledOptionals) {
            CutsceneManager.instance.Play(5, (_) => { LoopIntro1(); });

            enabledOptionals = false;
        } else {
            LoopIntro1();
        }
    }
}