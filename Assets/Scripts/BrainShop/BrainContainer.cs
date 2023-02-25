using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrainContainer : MonoBehaviour
{
   private DrawBrain.BrainStatistics brainStatistics;


   public void Init(DrawBrain.BrainStatistics brain)
   {
      brainStatistics = brain;

      var dexterityText = transform.Find("BrainStatsValues/DexterityScore").GetComponent<TMP_Text>();
      var intelligenceText = transform.Find("BrainStatsValues/IntelligenceScore").GetComponent<TMP_Text>();
      var strengthText = transform.Find("BrainStatsValues/StrengthScore").GetComponent<TMP_Text>();
      var costText = transform.Find("Cost").GetComponent<TMP_Text>();

      dexterityText.SetText(brainStatistics.dexterity.ToString());
      intelligenceText.SetText(brainStatistics.intelligence.ToString());
      strengthText.SetText(brainStatistics.strength.ToString());
      costText.SetText(brainStatistics.cost.ToString() + "$");
   }

   public void BuyBrain()
   {
      if (PlayerManager.instance.budget - brainStatistics.cost < 0) return;
      PlayerManager.instance.ReceiveBrain(brainStatistics);
   }
}
