using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;


[RequireComponent(typeof(BoxCollider))]
public abstract class UIVRInteractable : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] BoxCollider boxCollider;


    private void Reset()
    {
        /*
         * // ��ȡ Pos X, Pos Y, Pos Z
         * Vector3 localPosition = rect.localPosition;
         * // ��ȡ Width �� Height
         * Vector2 size = rect.sizeDelta;
         * // ��ȡ Anchors
         * Vector2 anchorMin = rect.anchorMin;
         * Vector2 anchorMax = rect.anchorMax;
         * // ��ȡ Pivot
         * Vector2 pivot = rect.pivot;
         * // ��ȡ Rotation
         * Vector3 localRotation = rect.localEulerAngles; 
         * // ��ȡ Scale
         * Vector3 localScale = rect.localScale;
        */
        if (rect == null) { rect = GetComponent<RectTransform>(); }
        if (boxCollider == null) { boxCollider = GetComponent<BoxCollider>(); }
        boxCollider.size = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y, 1);

    }


    protected virtual void Awake()
    {
        rect = rect ?? GetComponent<RectTransform>();
        boxCollider = boxCollider ?? GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y, 1);

    }


    public virtual void OnPointerClick(object sender)
    {
        Debug.Log(sender.ToString() + "  OnPointerClick: " + this.gameObject.name);
    }

    public virtual void OnPointerIn(object sender)
    {
        Debug.Log(sender.ToString() + "  OnPointerIn: " + this.gameObject.name);
    }

    public virtual void OnPointerOut(object sender)
    {
        Debug.Log(sender.ToString() + "  OnPointerOut: " + this.gameObject.name);
    }


}
