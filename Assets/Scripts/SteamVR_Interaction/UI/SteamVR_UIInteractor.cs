using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;


[RequireComponent(typeof(SteamVR_LaserPointer))]
public class SteamVR_UIInteractor : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;


    private void OnEnable()
    {
        if (laserPointer == null) laserPointer = GetComponent<SteamVR_LaserPointer>(); 

        laserPointer.PointerIn += OnPointerIn;
        laserPointer.PointerOut += OnPointerOut;
        laserPointer.PointerClick += OnPointerClick;
    }

    private void OnDisable()
    {
        laserPointer.PointerIn += OnPointerIn;
        laserPointer.PointerOut += OnPointerOut;
        laserPointer.PointerClick += OnPointerClick;
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

        UIVRInteractable ui_Interactable  = e.target .GetComponent <UIVRInteractable>();    
        if(ui_Interactable != null)
        {
            ui_Interactable.OnPointerIn(sender);
        }
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
        //Debug.Log(sender.ToString() + " Pointer exited " + e.target.name);

        UIVRInteractable ui_Interactable = e.target.GetComponent<UIVRInteractable>();
        if (ui_Interactable != null)
        {
            ui_Interactable.OnPointerOut(sender);
        }
    }
}
