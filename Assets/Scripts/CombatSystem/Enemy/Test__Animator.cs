using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test__Animator : MonoBehaviour
{
    #region --变量定义
    private Animator animator;
    private AnimationClip[] clips;
    #endregion

    #region --系统函数
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        clips = animator.runtimeAnimatorController.animationClips;

        AnimationClip _clip = clips[0];
        AddAnimationEvent(animator, _clip.name, "StartEvent", 0);
        AddAnimationEvent(animator, _clip.name, "HalfEvent", _clip.length * 0.5f);
        AddAnimationEvent(animator, _clip.name, "EndEvent", _clip.length);
    }
    private void OnDestroy()
    {
        CleanAllEvent();
    }
    #endregion

    #region --自定义函数
    private void StartEvent()
    {
        Debug.Log("开始播放动画");
    }
    private void HalfEvent()
    {
        Debug.Log("动画播放了一半");
    }
    private void EndEvent()
    {
        Debug.Log("播放动画完毕");
    }

    /// <summary>
    /// 添加动画事件
    /// </summary>
    /// <param name="_animator"></param>
    /// <param name="_clipName">动画名称</param>
    /// <param name="_eventFunctionName">事件方法名称</param>
    /// <param name="_time">添加事件时间。单位：秒</param>
    private void AddAnimationEvent(Animator _animator, string _clipName, string _eventFunctionName, float _time)
    {
        AnimationClip[] _clips = _animator.runtimeAnimatorController.animationClips;
        bool found = false;
        foreach (AnimationClip clip in _clips)
        {
            if (clip.name == _clipName)
            {
                AnimationEvent _event = new AnimationEvent
                {
                    functionName = _eventFunctionName,
                    time = Mathf.Clamp(_time, 0, clip.length - 0.01f) // Adjust end time slightly before clip ends
                };
                clip.AddEvent(_event);
                found = true;
                break;
            }
        }
        if (!found)
        {
            Debug.LogError("Animation clip not found: " + _clipName);
        }
    }

    private void CleanAllEvent()
    {
        foreach (AnimationClip clip in clips)
        {
            clip.events = null; // Clear events
        }
        Debug.Log("Cleared all animation events.");
    }




    /// <summary>
    /// 添加动画事件
    /// </summary>
    /// <param name="_animator"></param>
    /// <param name="_clipName">动画名称</param>
    /// <param name="_eventFunctionName">事件方法名称</param>
    /// <param name="_time">添加事件时间。单位：秒</param>
    private void AddAnimationEvent222(Animator _animator, string _clipName, string _eventFunctionName, float _time)
    {
        AnimationClip[] _clips = _animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < _clips.Length; i++)
        {
            if (_clips[i].name == _clipName)
            {
                AnimationEvent _event = new AnimationEvent();
                _event.functionName = _eventFunctionName;
                _event.time = _time;
                _clips[i].AddEvent(_event);
                break;
            }
        }
        _animator.Rebind();
    }
    #endregion
}