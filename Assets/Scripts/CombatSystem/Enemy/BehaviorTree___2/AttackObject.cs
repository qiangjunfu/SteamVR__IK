using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackObject : Action
{
    public SharedGameObject targetObj;
    public SharedFloat attackDis = 2;


    public override void OnStart()
    {
        base.OnStart();

        int id = this.gameObject.GetComponent<EnemyCtrl>().GetData().selfid;
        MessageManager.Broadcast<int , int >(GameEventType.EnemyStateChange, 7 ,id );
    }

    float distance = 0;
    public override TaskStatus OnUpdate()
    {
        if (targetObj.Value == null) return TaskStatus.Failure;

        distance = Vector3.Distance(this.gameObject.transform.position, targetObj.Value.transform.position);
        if (distance > attackDis.Value)
        {
            Debug.LogFormat("AttackObject --> ³¬³ö¹¥»÷¾àÀë --- ");
            return TaskStatus.Failure;
        }

        transform.LookAt(targetObj.Value.transform);
        //Debug.LogFormat("AttackObject --> ¹¥»÷Ä¿±ê: " + targetObj.Name);
        return TaskStatus.Running;
    }
}
