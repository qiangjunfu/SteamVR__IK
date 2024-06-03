using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;


// 可以处理鼠标点击  但是对于悬停 推出没处理 

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class LaserPointerInputController : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    private GameObject currentObject = null;


    private void OnEnable()
    {
        if (laserPointer == null) laserPointer = GetComponent<SteamVR_LaserPointer>();

        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut += HandlePointerOut;
        laserPointer.PointerClick += HandlePointerClick;
    }

    private void OnDisable()
    {
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut += HandlePointerOut;
        laserPointer.PointerClick += HandlePointerClick;
    }

    private void HandlePointerClick(object sender, PointerEventArgs e)
    {
        ExecuteEvents.Execute(currentObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }
    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        currentObject = e.target.gameObject;
        ExecuteEvents.Execute(currentObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        ExecuteEvents.Execute(currentObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
        currentObject = null;
    }



    private void Update()
    {
        if (currentObject != null)
        {
            ExecuteEvents.Execute(currentObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
    }

}
