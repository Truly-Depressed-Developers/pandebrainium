using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMP_Text survivedText;
    [SerializeField] TMP_Text earnedText;
    [SerializeField] Button goBackButton;

    private void Start() {
        goBackButton.onClick.AddListener(() => { MenuManager.instance.GoBackToMenu(); });
    }

    public void SetText(int days, int earned) {
        survivedText.text = $"Days survived: {days}";
        earnedText.text = $"Money earned: ${earned}";
    }
}
