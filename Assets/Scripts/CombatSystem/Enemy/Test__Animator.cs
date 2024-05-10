using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test__Animator : MonoBehaviour
{
    #region --��������
    private Animator animator;
    private AnimationClip[] clips;
    #endregion

    #region --ϵͳ����
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

    #region --�Զ��庯��
    private void StartEvent()
    {
        Debug.Log("��ʼ���Ŷ���");
    }
    private void HalfEvent()
    {
        Debug.Log("����������һ��");
    }
    private void EndEvent()
    {
        Debug.Log("���Ŷ������");
    }

    /// <summary>
    /// ��Ӷ����¼�
    /// </summary>
    /// <param name="_animator"></param>
    /// <param name="_clipName">��������</param>
    /// <param name="_eventFunctionName">�¼���������</param>
    /// <param name="_time">����¼�ʱ�䡣��λ����</param>
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
    /// ��Ӷ����¼�
    /// </summary>
    /// <param name="_animator"></param>
    /// <param name="_clipName">��������</param>
    /// <param name="_eventFunctionName">�¼���������</param>
    /// <param name="_time">����¼�ʱ�䡣��λ����</param>
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