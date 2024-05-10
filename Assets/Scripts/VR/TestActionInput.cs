using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class TestActionInput : MonoBehaviour
{

    void Start()
    {
        // 扳机
        SteamVR_Actions.default_GrabPinch.onChange += Default_GrabPinch_onChange;

        // 握把
        SteamVR_Actions.default_GrabGrip.onChange += Default_GrabGrip_onChange;

        // 摇杆  *** Joystick_V2要去steamvr绑定位置
        SteamVR_Actions.default_Joystick_V2.onChange += Default_Joystick_V2_onChange;



    }

    private void Default_GrabPinch_onChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        Debug.LogFormat("Default_GrabPinch_onChange: {0} -- {1} ", fromAction.activeDevice, newState);
    }
    private void Default_GrabGrip_onChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        //Debug.LogFormat("Default_GrabGrip_onChange: {0} -- {1} ", fromAction.activeDevice, newState);
        
        //if (newState)
        //{
        //    //手柄震动
        //    SteamVR_Actions.default_Haptic.Execute(0, 0.5f, 100, 1, SteamVR_Input_Sources.RightHand);
        //}
    }
    private void Default_Joystick_V2_onChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        //Debug.LogFormat("Default_Joystick_V2_onChange: {0} -- {1} , delta:{2}", fromAction.activeDevice, axis , delta);
    }




    private void Update()
    {
        //Debug.LogFormat("11  " +  SteamVR_Actions.default_GrabPinch.GetLastState(SteamVR_Input_Sources.RightHand));
        //Debug.LogFormat("22  " +  SteamVR_Actions.default_GrabGrip.GetLastState(SteamVR_Input_Sources.RightHand));
    }

}
