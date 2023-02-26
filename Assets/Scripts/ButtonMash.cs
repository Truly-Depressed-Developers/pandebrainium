using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonMash : MonoBehaviour
{
   [SerializeField] private TMP_Text buttonText;

   [SerializeField] private float multiplayer = 2.0f;
   [SerializeField] private int baseClickCount = 5;
   private int clickCounter;

   [SerializeField] private ChallengeTrigger challengeScript;


   // Start is called before the first frame update
   void Start()
   {
      clickCounter = Mathf.FloorToInt(baseClickCount * multiplayer * challengeScript.difficultyMod);
      buttonText.SetText(clickCounter.ToString());
   }


   public void ButtonActivation()
   {
      --clickCounter;

      if (clickCounter <= 0)
      {
         challengeScript.Fulfill();
      }

      buttonText.SetText(clickCounter.ToString());
   }


}
