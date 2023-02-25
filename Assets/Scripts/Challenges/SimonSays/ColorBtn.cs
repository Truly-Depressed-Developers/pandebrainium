using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBtn : MonoBehaviour
{
    public event Action<GameObject, int> OnClickToParent;

    private int btnColorId;
    private bool toClick = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        if (toClick)
        {
            Debug.Log("CLICK");
            OnClickToParent?.Invoke(this.gameObject, btnColorId);
        }
    }
}
