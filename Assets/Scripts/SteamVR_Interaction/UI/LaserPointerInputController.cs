using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.Extras;


[RequireComponent(typeof(SteamVR_LaserPointer))]
public class LaserPointerInputController : MonoBehaviour
{
    [SerializeField] SteamVR_LaserPointer laserPointer;
    [SerializeField] SteamVR_Action_Single squeeze = SteamVR_Input.GetSingleAction("Squeeze");
    [SerializeField] float squeezeAxis = 0;
    [SerializeField] UIVRInteractable currentUI = null;


    #region MyRegion

    private void OnEnable()
    {
        if (laserPointer == null) laserPointer = GetComponent<SteamVR_LaserPointer>();

        laserPointer.PointerIn += OnPointerIn;
        laserPointer.PointerOut += OnPointerOut;
        laserPointer.PointerClick += OnPointerClick;
    }

    private void OnDisable()
    {
        laserPointer.PointerIn -= OnPointerIn;
        laserPointer.PointerOut -= OnPointerOut;
        laserPointer.PointerClick -= OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        //Debug.Log(sender.ToString() + " Clicked on " + e.target.name);

        UIVRInteractable ui_Interactable = e.target.GetComponent<UIVRInteractable>();
        if (ui_Interactable != null)
        {
            ui_Interactable.OnPointerClick(sender);
        }
    }
    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        //Debug.Log(sender.ToString() + " Pointer entered " + e.target.name);

        UIVRInteractable ui_Interactable = e.target.GetComponent<UIVRInteractable>();
        if (ui_Interactable != null)
        {
            ui_Interactable.OnPointerIn(sender);
            currentUI = ui_Interactable;
        }
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
        //Debug.Log(sender.ToString() + " Pointer exited " + e.target.name);

        UIVRInteractable ui_Interactable = e.target.GetComponent<UIVRInteractable>();
        if (ui_Interactable != null)
        {
            ui_Interactable.OnPointerOut(sender);
            currentUI = null;
        }
    }

    #endregion


    private void Update()
    {
        squeezeAxis = squeeze.axis;
        if (squeezeAxis <= 0.05)
        {
            laserPointer.ActiveLaserPointer(false);
        }
        else
        {
            laserPointer.ActiveLaserPointer(true);
        }
    }
}
