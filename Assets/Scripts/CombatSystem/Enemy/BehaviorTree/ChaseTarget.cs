using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : Action
{
    public SharedGameObject target;  // 追击的目标
    public SharedFloat chase_Speed = 3.0f;  // 追击速度
    public float gravity = -9.81f;  // 重力加速度
    public SharedFloat chase_Dis = 10f;     // 成功追踪到目标的距离阈值
    public SharedFloat chaseLose_Dis = 20f;  // 成功追踪到目标的距离阈值

    private CharacterController controller;
    private float verticalSpeed = 0;  // 垂直方向的速度
    private bool hasLoggedTargetReached = false;
    private bool hasLoggedTargetTooFar = false;
    private bool hasLoggedChasing = false;




    public override void OnStart()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController component is not attached to the object.");
        }
        hasLoggedTargetReached = false;
        hasLoggedTargetTooFar = false;
        hasLoggedChasing = false;

        int id = this.gameObject.GetComponent<EnemyCtrl>().GetData().selfid;
        MessageManager.Broadcast<int,int >(GameEventType.EnemyStateChange, 3, id);
    }

    public override TaskStatus OnUpdate()
    {
        if (target.Value == null || controller == null)
        {
            Debug.LogError("Target or Controller is missing.");
            return TaskStatus.Failure;
        }

        Vector3 direction = (target.Value.transform.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.Value.transform.position);

        // 检测是否到达目标
        if (distanceToTarget < chase_Dis.Value )
        {
            if (!hasLoggedTargetReached)
            {
                Debug.Log("目标追击成功");
                hasLoggedTargetReached = true;
            }
            return TaskStatus.Success;  // 如果到达了目标则返回成功状态
        }

        if (distanceToTarget > chaseLose_Dis.Value )
        {
            if (!hasLoggedTargetTooFar)
            {
                Debug.Log("目标超出追击距离.");
                hasLoggedTargetTooFar = true;

                int id = this.gameObject.GetComponent<EnemyCtrl>().GetData().selfid;
                MessageManager.Broadcast<int, int>(GameEventType.EnemyStateChange, 1, id);
            }
            return TaskStatus.Failure;  // 如果目标超出追击距离则返回失败状态
        }

        // Reset log flags when target is neither too far nor reached
        hasLoggedTargetReached = false;
        hasLoggedTargetTooFar = false;

        // 计算方向并且面向目标
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
        }

        // 应用重力并向前移动
        if (controller.isGrounded)
        {
            verticalSpeed = 0;
        }
        else
        {
            verticalSpeed += gravity * Time.deltaTime;
        }
        Vector3 move = direction * chase_Speed.Value;
        move.y += verticalSpeed;
        controller.Move(move * Time.deltaTime);

        if (!hasLoggedChasing)
        {
            Debug.Log("开始目标追击中 ...");
            hasLoggedChasing = true;
        }

        return TaskStatus.Running;  // 追踪中返回运行状态
    }
}
