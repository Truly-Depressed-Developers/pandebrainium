using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
   [SerializeField] GameObject brainDataContainerPrefab;
   [SerializeField] Transform brainRowContainer;

   List<DrawBrain.BrainStatistics> brainStatisticsList = new List<DrawBrain.BrainStatistics>();

   // Start is called before the first frame update
   void Start()
   {
      for (int i = 0; i < 3; i++)
      {
         var newBrainStatisticData = DrawBrain.DrawRandomBrain();
         brainStatisticsList.Add(newBrainStatisticData);
      }

      foreach (var brainStatistic in brainStatisticsList)
      {
         var brainDataGameObject = Instantiate(brainDataContainerPrefab, brainRowContainer);

         var dexterityText = brainDataGameObject.transform.Find("BrainStatsValues/DexterityScore").GetComponent<TMP_Text>();
         var intelligenceText = brainDataGameObject.transform.Find("BrainStatsValues/IntelligenceScore").GetComponent<TMP_Text>();
         var strengthText = brainDataGameObject.transform.Find("BrainStatsValues/StrengthScore").GetComponent<TMP_Text>();
         var costText = brainDataGameObject.transform.Find("Cost").GetComponent<TMP_Text>();

         dexterityText.SetText(brainStatistic.dexterity.ToString());
         intelligenceText.SetText(brainStatistic.intelligence.ToString());
         strengthText.SetText(brainStatistic.strength.ToString());
         costText.SetText(brainStatistic.cost.ToString()+ "$");
      }
   }
}
