using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : Action
{
    public SharedGameObject target;  // ׷����Ŀ��
    public SharedFloat chase_Speed = 3.0f;  // ׷���ٶ�
    public float gravity = -9.81f;  // �������ٶ�
    public SharedFloat chase_Dis = 10f;     // �ɹ�׷�ٵ�Ŀ��ľ�����ֵ
    public SharedFloat chaseLose_Dis = 20f;  // �ɹ�׷�ٵ�Ŀ��ľ�����ֵ

    private CharacterController controller;
    private float verticalSpeed = 0;  // ��ֱ������ٶ�
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

        // ����Ƿ񵽴�Ŀ��
        if (distanceToTarget < chase_Dis.Value )
        {
            if (!hasLoggedTargetReached)
            {
                Debug.Log("Ŀ��׷���ɹ�");
                hasLoggedTargetReached = true;
            }
            return TaskStatus.Success;  // ���������Ŀ���򷵻سɹ�״̬
        }

        if (distanceToTarget > chaseLose_Dis.Value )
        {
            if (!hasLoggedTargetTooFar)
            {
                Debug.Log("Ŀ�곬��׷������.");
                hasLoggedTargetTooFar = true;

                int id = this.gameObject.GetComponent<EnemyCtrl>().GetData().selfid;
                MessageManager.Broadcast<int, int>(GameEventType.EnemyStateChange, 1, id);
            }
            return TaskStatus.Failure;  // ���Ŀ�곬��׷�������򷵻�ʧ��״̬
        }

        // Reset log flags when target is neither too far nor reached
        hasLoggedTargetReached = false;
        hasLoggedTargetTooFar = false;

        // ���㷽��������Ŀ��
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
        }

        // Ӧ����������ǰ�ƶ�
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
            Debug.Log("��ʼĿ��׷���� ...");
            hasLoggedChasing = true;
        }

        return TaskStatus.Running;  // ׷���з�������״̬
    }
}
