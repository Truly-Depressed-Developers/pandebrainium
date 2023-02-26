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

    [SerializeField] GameObject gameOverScreen;

    public float dayStart;

    public int currentDay { get; private set; }

    private void Awake() {
        currentDay = 1;
        instance = this;
    }

    private bool enabledOptionals = false;

    public void StartLoop() {
        //SoundManager.Instance.playSound_postapoc();

        if(PlayerPrefs.HasKey("tutorial_completed")) {
            if(PlayerPrefs.GetInt("tutorial_completed") == 1) {
                enabledOptionals = false;
            } else {
                enabledOptionals = true;
            }
        } else {
            enabledOptionals = true;
        }

        if (enabledOptionals == true) {
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
        Vector3 probabilities = enabledOptionals ? new Vector3(1f, 0, 0) : new Vector3(
            ShopManager.instance.drownProbabilities[2],
            ShopManager.instance.drownProbabilities[0],
            ShopManager.instance.drownProbabilities[1]);

        laptopDisplay.SetActive(true);
        dayStart = Time.time;
        ChallengeManager.instance.StartDay(1 + brain.strength, 1 + brain.dexterity, 1 + brain.intelligence, sanity, currentDay, probabilities,
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
        brainShopManager.UpdatePlayerValues();
        brainShopScreen.SetActive(true);

        if (ShopManager.instance.CanBuyAnyBrain() == false) {
            brainShopScreen.SetActive(false);
            LoopGameOver();
        } else {
            brainShopManager.OnBuyAnyBrain += () => { brainShopScreen.SetActive(false); LoopIntro3(); };
        }
    }

    // Intro 3
    private void LoopIntro3() {
        ++currentDay;

        if (enabledOptionals) {
            CutsceneManager.instance.Play(5, (_) => { LoopIntro1(); });

            enabledOptionals = false;
            PlayerPrefs.SetInt("tutorial_completed", 1);
        } else {
            LoopIntro1();
        }
    }


    // Game Over
    private void LoopGameOver() {
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverScreen>().SetText(currentDay, MoneyManager.instance.income);
    }
}