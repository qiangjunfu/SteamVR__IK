using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : Action
{
    public SharedGameObject targetObj;

    public override void OnStart()
    {
        base.OnStart();

        if (targetObj.Value == null) return;

        transform.LookAt(targetObj.Value.transform);

    }
}
