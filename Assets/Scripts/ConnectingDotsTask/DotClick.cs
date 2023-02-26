using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DotClick : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public UnityEvent<int> onClickEvent;

    public void ClickHandler(){
        SoundManager.Instance.playSound_osuClick();
        onClickEvent.Invoke(int.Parse(text.text));
        Destroy(gameObject);
    }
}
