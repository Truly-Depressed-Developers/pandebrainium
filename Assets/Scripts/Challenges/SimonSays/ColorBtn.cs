using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBtn : MonoBehaviour
{
    public event Action<GameObject, int> OnClickToParent;

    private int btnColorId;
    private bool toClick = true;

    public void setBtnColorId(int val)
    {
        btnColorId = val;
    }

    public void disableClick()
    {
        toClick = false;
    }

    public void onClick()
    {
        SoundManager.Instance.playSound_simonSelect();
        if (toClick)
        {
            OnClickToParent?.Invoke(this.gameObject, btnColorId);
        }
    }
}
