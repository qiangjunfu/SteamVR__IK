using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

[RequireComponent(typeof(Animator))]
public class Actions : MonoBehaviour
{
    private Animator animator;
    const int countOfDamageAnimations = 3;
    int lastDamageAnimation = -1;


    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Rebind();  // 重置Animator状态
    }

    private void OnDestroy()
    {
        CleanAllEvent(animator);
    }
    public Animator SetupAnimatorForCharacter(GameObject character, RuntimeAnimatorController baseController)
    {
        Animator animator = character.GetComponent<Animator>();
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = baseController;

        // 为每个角色设置独立的 Animator Override Controller
        animator.runtimeAnimatorController = overrideController;

        return animator;
    }


    public Animator GetAnimator()
    {
        if (animator == null) { animator = GetComponent<Animator>(); }
        return animator;
    }

    public AnimatorClipInfo GetCurrentAniInfo()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Debug.LogFormat("当前动画片段: {0} , 时间: {1}", clipInfo[0].clip.name, clipInfo[0].clip.length);
        return clipInfo[0];
    }

    public AnimationClip[] GetAllClips()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            Debug.Log("Animation Clip Name: " + clip.name);
        }
        return clips;
    }


    #region 播放动画
    void ResetDate()
    {
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 0f);
        animator.SetBool("Squat", false);
        //animator.SetBool("Fire", false);
    }

    public void Stay()
    {
        ResetDate();
    }

    public void Walk()
    {
        ResetDate();
        animator.SetFloat("Speed", 0.5f);
    }

    public void Run()
    {
        ResetDate();
        animator.SetFloat("Speed", 1f);
    }

    //public void Fire()
    //{
    //    ResetDate();
    //    animator.SetBool("Fire", true);
    //}
    public void Attack()
    {
        Aiming();
        animator.SetTrigger("Attack");
    }

    public void Death()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.Play("Idle", 0);
        else
            animator.SetTrigger("Death");
    }

    public void Damage()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        int id = Random.Range(0, countOfDamageAnimations);
        if (countOfDamageAnimations > 1)
            while (id == lastDamageAnimation)
                id = Random.Range(0, countOfDamageAnimations);
        lastDamageAnimation = id;
        animator.SetInteger("DamageID", id);
        animator.SetTrigger("Damage");
    }

    public void Jump()
    {
        ResetDate();
        animator.SetTrigger("Jump");
    }

    public void Aiming()
    {
        ResetDate();
        animator.SetBool("Aiming", true);
    }

    public void Sitting()
    {
        animator.SetBool("Squat", !animator.GetBool("Squat"));
        animator.SetBool("Aiming", false);
    }
    #endregion



    #region 动画事件绑定
    /// <summary>
    /// 添加动画事件
    /// </summary>
    /// <param name="_animator"></param>
    /// <param name="_clipName">动画名称</param>
    /// <param name="_eventFunctionName">事件方法名称</param>
    /// <param name="_time">添加事件时间。单位：秒</param>
    public  void AddAnimationEvent(Animator _animator, string _clipName, string _eventFunctionName, float _time)
    {
        if (_animator == null || _animator.runtimeAnimatorController == null)
        {
            Debug.LogError("Animator or its controller is null.");
            return;
        }

        AnimationClip[] _clips = _animator.runtimeAnimatorController.animationClips;
        bool found = false;
        foreach (AnimationClip clip in _clips)
        {
            Debug .LogFormat ("{0} 所有动画: {1}" , _animator.name , clip.name);    
            if (clip.name == _clipName)
            {
                AnimationEvent _event = new AnimationEvent
                {
                    functionName = _eventFunctionName,
                    time = Mathf.Clamp(_time, 0, clip.length - 0.01f)
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

    public void CleanAllEvent(Animator _animator)
    {
        if (_animator == null || _animator.runtimeAnimatorController == null)
        {
            Debug.LogError("Animator or its controller is null.");
            return;
        }

        AnimationClip[] _clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in _clips)
        {
            clip.events = null; // Clear events
        }
        Debug.Log("Cleared all animation events.");
    }


    //void Start()
    //{
    //    AnimatorController ac = animator.runtimeAnimatorController as AnimatorController;
    //    if (ac != null)
    //    {
    //        foreach (var layer in ac.layers)
    //        {
    //            TraverseStateMachine(layer.stateMachine);
    //        }
    //    }
    //}

    //void TraverseStateMachine(AnimatorStateMachine sm)
    //{
    //    foreach (var state in sm.states)
    //    {
    //        Debug.Log("Animation Name: " + state.state.motion.name);
    //    }

    //    foreach (var childSM in sm.stateMachines)
    //    {
    //        TraverseStateMachine(childSM.stateMachine);
    //    }
    //}
    #endregion
}
