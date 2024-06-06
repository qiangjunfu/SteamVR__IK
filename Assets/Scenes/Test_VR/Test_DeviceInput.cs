using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Test_DeviceInput : MonoBehaviour
{


    // 定义动作
    public SteamVR_Action_Boolean triggerAction;
    public SteamVR_Action_Boolean grabPinchAction;
    public SteamVR_Action_Single squeezeAction;

    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Boolean menuAction;
    public SteamVR_Action_Vector2 joystickAction;


    public SteamVR_Action_Pose pose;


    #region 按键事件处理

    #region MyRegion
    //void Start()
    //{
    //    SteamVR_Actions.default_GrabGrip.onStateDown += Default_GrabGrip_onStateDown;
    //}

    //private void Default_GrabGrip_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{

    //    Debug.LogFormat("握把建按下: {0} , {1}", fromAction.activeDevice, fromSource.ToString());
    //}

    //private void Update()
    //{
    //    bool isDown = SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand);
    //    if(isDown)
    //    {
    //        Debug.LogFormat("握把建按下: {0} , {1}", SteamVR_Input_Sources.RightHand);
    //    }
    //}
    #endregion

    void OnEnable()
    {
        // 动态获取动作
        triggerAction = SteamVR_Input.GetBooleanAction("default", "InteractUI"); //扳机建
        grabPinchAction = SteamVR_Input.GetBooleanAction("default", "GrabPinch");
        squeezeAction = SteamVR_Input.GetSingleAction("default", "Squeeze");

        gripAction = SteamVR_Input.GetBooleanAction("default", "GrabGrip");  //握持建

        joystickAction = SteamVR_Input.GetVector2Action("default", "Joystick_V2");  //摇杆建

        pose = SteamVR_Input.GetPoseAction("default", "Pose");


        //triggerAction = SteamVR_Actions.default_GrabGrip ;
        //gripAction = SteamVR_Actions.default_GrabGrip;
        //menuAction = SteamVR_Actions.default_Menu;
        //joystickAction = SteamVR_Actions.default_Joystick;


        // 订阅事件
        triggerAction.AddOnStateDownListener(OnTriggerPressed, SteamVR_Input_Sources.Any);
        triggerAction.AddOnStateUpListener(OnTriggerReleased, SteamVR_Input_Sources.Any);
        grabPinchAction.AddOnStateDownListener(OnGrabPinchPressed, SteamVR_Input_Sources.Any);
        grabPinchAction.AddOnStateUpListener(OnGrabPinchReleased, SteamVR_Input_Sources.Any);
        squeezeAction.AddOnChangeListener(OnSqueezeChanged, SteamVR_Input_Sources.Any);

        gripAction.AddOnStateDownListener(OnGripPressed, SteamVR_Input_Sources.Any);
        gripAction.AddOnStateUpListener(OnGripReleased, SteamVR_Input_Sources.Any);

        //menuAction.AddOnStateDownListener(OnMenuPressed, SteamVR_Input_Sources.Any);
        //menuAction.AddOnStateUpListener(OnMenuReleased, SteamVR_Input_Sources.Any);

        joystickAction.AddOnChangeListener(OnJoystickMoved, SteamVR_Input_Sources.Any);
        pose.AddOnChangeListener(SteamVR_Input_Sources.Any, OnPoseChange);

    }


    void OnDisable()
    {
        // 取消订阅事件
        triggerAction.RemoveOnStateDownListener(OnTriggerPressed, SteamVR_Input_Sources.Any);
        triggerAction.RemoveOnStateUpListener(OnTriggerReleased, SteamVR_Input_Sources.Any);
        grabPinchAction.RemoveOnStateDownListener(OnGrabPinchPressed, SteamVR_Input_Sources.Any);
        grabPinchAction.RemoveOnStateUpListener(OnGrabPinchReleased, SteamVR_Input_Sources.Any);
        squeezeAction.RemoveOnChangeListener(OnSqueezeChanged, SteamVR_Input_Sources.Any);

        gripAction.RemoveOnStateDownListener(OnGripPressed, SteamVR_Input_Sources.Any);
        gripAction.RemoveOnStateUpListener(OnGripReleased, SteamVR_Input_Sources.Any);

        //menuAction.RemoveOnStateDownListener(OnMenuPressed, SteamVR_Input_Sources.Any);
        //menuAction.RemoveOnStateUpListener(OnMenuReleased, SteamVR_Input_Sources.Any);

        joystickAction.RemoveOnChangeListener(OnJoystickMoved, SteamVR_Input_Sources.Any);
        pose.RemoveOnChangeListener(SteamVR_Input_Sources.Any, OnPoseChange);

    }

    // 按键事件处理
    private void OnTriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.LogFormat("Trigger Pressed: {0} ", fromAction.activeDevice);

        //if (fromAction.activeDevice == SteamVR_Input_Sources.RightHand)
        //{
        //    // 手柄震动
        //    //SteamVR_Actions._default.Haptic.Execute(0, 0.5f, 10, 0.1f, fromSource);
        //    SteamVR_Actions._default.Haptic.Execute(0, 0.5f, 10, 0.1f, SteamVR_Input_Sources.RightHand);
        //}
    }

    private void OnTriggerReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.LogFormat("Trigger Released: {0} ", fromAction.activeDevice.ToString());
    }

    private void OnGrabPinchPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.LogFormat("GrabPinch Pressed: {0} ", fromAction.activeDevice.ToString());
    }
    private void OnGrabPinchReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.LogFormat("GrabPinch Released: {0} ", fromAction.activeDevice.ToString());
    }
    private void OnSqueezeChanged(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta)
    {
        //Debug.LogFormat("Squeeze Change:  axis:{0}  delta:{1}  , {2}", newAxis, newDelta, fromAction.activeDevice.ToString());
    }


    private void OnGripPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.LogFormat("Grip Pressed: {0} ", fromAction.activeDevice.ToString());
    }

    private void OnGripReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.LogFormat("Grip Released: {0} ", fromAction.activeDevice.ToString());
    }


    private void OnMenuPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Menu Pressed: " + fromSource);
    }

    private void OnMenuReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Menu Released: " + fromSource);
    }

    private void OnJoystickMoved(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        Debug.LogFormat("Joystick Moved:  axis:{0}  delta:{1}  , {2}", axis, delta, fromAction.activeDevice.ToString());
    }


    private void OnPoseChange(SteamVR_Action_Pose fromAction, SteamVR_Input_Sources fromSource)
    {
        // 只能检测到左手手柄
        //Debug.LogFormat("Pose Change:  axis:{0}  delta:{1}  , {2}", fromAction.localPosition, fromAction.localRotation, fromAction.activeDevice.ToString());
    }

    #endregion



    private void Update()
    {
        ////if (pose.activeDevice == SteamVR_Input_Sources.LeftHand)
        ////{
        ////    Debug.LogFormat("{0} , {1} ", pose.localPosition, pose.localRotation);
        ////}
        ////if (SteamVR_Actions._default.Pose.activeDevice == SteamVR_Input_Sources.RightHand) 
        ////{
        ////    Debug.LogFormat("Pose RightHand: {0} , {1} ", SteamVR_Actions._default.Pose.localPosition, SteamVR_Actions._default.Pose.localRotation);
        ////}

        //Vector3 pos1 = SteamVR_Actions._default.Pose.GetLocalPosition(SteamVR_Input_Sources.LeftHand);
        //Vector3 pos2 = SteamVR_Actions._default.Pose.GetLocalPosition(SteamVR_Input_Sources.RightHand);
        //Debug.LogFormat("Pose : {0} , {1} ", pos1, pos2);
    }
}
