using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class BrainContainer : MonoBehaviour
{
   public event Action OnBuyBrain;

   private DrawBrain.BrainStatistics brainStatistics;

   [SerializeField] Sprite dextBrain;
   [SerializeField] Sprite intBrain;
   [SerializeField] Sprite strengthBrain;

   [SerializeField] Image brainImage;


   public void Init(DrawBrain.BrainStatistics brain)
   {
      brainStatistics = brain;

      var dexterityText = transform.Find("BrainStatsValues/Dexterity/ScoreText").GetComponent<TMP_Text>();
      var intelligenceText = transform.Find("BrainStatsValues/Intelligence/ScoreText").GetComponent<TMP_Text>();
      var strengthText = transform.Find("BrainStatsValues/Strength/ScoreText").GetComponent<TMP_Text>();
      var costText = transform.Find("Cost").GetComponent<TMP_Text>();

      dexterityText.SetText(brainStatistics.dexterity.ToString());
      intelligenceText.SetText(brainStatistics.intelligence.ToString());
      strengthText.SetText(brainStatistics.strength.ToString());
      costText.SetText(brainStatistics.cost.ToString() + "$");

      // var brainImage = transform.GetComponent<Image>();

      if (brainStatistics.dexterity > brainStatistics.intelligence && brainStatistics.dexterity > brainStatistics.strength)
      {
         brainImage.sprite = dextBrain;
      }
      else if (brainStatistics.intelligence > brainStatistics.strength)
      {
         brainImage.sprite = intBrain;
      }
      else
      {
         brainImage.sprite = strengthBrain;
      }
   }

   public void BuyBrain(){
       BuyBrainCoroutine();
   }
   public void BuyBrainCoroutine()
   {
      // if (PlayerManager.instance.budget - brainStatistics.cost < 0) return;
      if (!MoneyManager.instance.canAfford(brainStatistics.cost)) return;
      
      SoundManager.Instance.playSound_brainBought();
      PlayerManager.instance.ReceiveBrain(brainStatistics);

      OnBuyBrain?.Invoke();
   }
}
