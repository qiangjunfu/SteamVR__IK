using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class DetectionDamage : Conditional
    {
        public SharedInt fleeHP;
        public int hp = 100;
        //public SharedGameObject targetObj;

        public override void OnStart()
        {
            base.OnStart();

        }

        public override TaskStatus OnUpdate()
        {
            //if (targetObj.Value == null) return TaskStatus.Failure;
            hp = this.gameObject.GetComponent<EnemyCtrl>().GetData().hp;

            if (hp < fleeHP.Value)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;

        }

    }
}