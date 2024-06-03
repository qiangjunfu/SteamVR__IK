using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VR_UIClick : UIVRInteractable
{
    [SerializeField] Button btn;

    private void Start()
    {
        btn.onClick.AddListener(BtnClick);
    }

    private void BtnClick()
    {
        Debug.Log("11111111   BtnClick");
    }

    public override void OnPointerClick(object sender)
    {
        base.OnPointerClick(sender);

        btn.onClick.Invoke();
    }

    public override void OnPointerIn(object sender)
    {
        Debug.Log(sender.ToString() + "  OnPointerIn: " + this.gameObject.name);
    }

    public override void OnPointerOut(object sender)
    {
        Debug.Log(sender.ToString() + "  OnPointerOut: " + this.gameObject.name);
    }
}
