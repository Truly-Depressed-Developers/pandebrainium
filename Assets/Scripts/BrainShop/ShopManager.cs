using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopManager : MonoBehaviour {
    public event Action OnBuyAnyBrain;

    public static ShopManager instance;

    [SerializeField] GameObject brainDataContainerPrefab;
    [SerializeField] Transform brainRowContainer;

    List<DrawBrain.BrainStatistics> brainStatisticsList = new List<DrawBrain.BrainStatistics>();

    // Statistic Screen
    [SerializeField] TMP_Text dexterityValue;
    [SerializeField] TMP_Text intelligenceValue;
    [SerializeField] TMP_Text strengthValue;
    [SerializeField] TMP_Text sanityValue;
    [SerializeField] TMP_Text moneyCountValue;

    // Start is called before the first frame update
    void Start() {
        instance = this;

        SpawnBrains();
        UpdatePlayerValues();

        PlayerManager.instance.changeEvent.AddListener(SpawnBrains);
        PlayerManager.instance.changeEvent.AddListener(UpdatePlayerValues);
    }

    private void SpawnBrains() {
        brainStatisticsList = new List<DrawBrain.BrainStatistics>();

        for (int i = 0; i < 3; i++) {
            var newBrainStatisticData = DrawBrain.DrawRandomBrain();
            brainStatisticsList.Add(newBrainStatisticData);
        }

        foreach (Transform childTransform in brainRowContainer) {
            GameObject.Destroy(childTransform.gameObject);
        }

        foreach (var brainStatistics in brainStatisticsList) {
            var brainDataGameObject = Instantiate(brainDataContainerPrefab, brainRowContainer);
            var brainContainerScript = brainDataGameObject.GetComponent<BrainContainer>();

            brainContainerScript.OnBuyBrain += OnBuyBrain;

            brainContainerScript.Init(brainStatistics);
        }
    }

    private void UpdatePlayerValues() {
        dexterityValue.SetText(PlayerManager.instance.dexterity.ToString());
        intelligenceValue.SetText(PlayerManager.instance.intelligence.ToString());
        strengthValue.SetText(PlayerManager.instance.strength.ToString());
        sanityValue.SetText(PlayerManager.instance.sanity.ToString());
        moneyCountValue.SetText(PlayerManager.instance.budget.ToString() + "$");
    }

    private void OnBuyBrain() {
        OnBuyAnyBrain?.Invoke();
        OnBuyAnyBrain = null;
    }
}
