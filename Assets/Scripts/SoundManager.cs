using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource brainBought;
    public AudioSource buttonMash;
    public AudioSource buttonSelect1;
    public AudioSource captchaSelect;
    public AudioSource cleanup_swipe1;
    public AudioSource endOfTheDay;
    public AudioSource osuClick;
    public AudioSource simonSelect;
    public AudioSource taskComplete;
    public AudioSource taskFailed;
    public AudioSource taskSpawn;

    public static SoundManager Instance;

    public void Awake()
    {
        Instance = this;
    }
    public void playSound_brainBought()
    {
        brainBought.Play();
    }
    public void playSound_buttonMash()
    {
        buttonMash.Play();
    }
    public void playSound_buttonSelect1()
    {
        buttonSelect1.Play();
    }
    public void playSound_captchaSelect()
    {
        captchaSelect.Play();
    }
    public void playSound_cleanup_swipe1()
    {
        cleanup_swipe1.Play();
    }
    public void playSound_endOfTheDay()
    {
        endOfTheDay.Play();
    }
    public void playSound_osuClick()
    {
        osuClick.Play();
    }
    public void playSound_simonSelect()
    {
        simonSelect.Play();
    }
    public void playSound_taskComplete()
    {
        taskComplete.Play();
    }
    public void playSound_taskFailed()
    {
        taskFailed.Play();
    }
    public void playSound_taskSpawn()
    {
        taskSpawn.Play();
    }
}
