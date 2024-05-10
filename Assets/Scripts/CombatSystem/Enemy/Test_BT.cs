using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_BT : MonoBehaviour
{
    [SerializeField] private BehaviorTree behaviorTree;
    [SerializeField] float speed;
    [SerializeField] float speed2;
    [SerializeField] float Patrol_Speed;
    [SerializeField] List<GameObject> Patrol_Waypoints;


    void Start()
    {
        if (behaviorTree == null)
        {
            Debug.LogError("BehaviorTree is not assigned on " + gameObject.name);
            return;
        }


        //Invoke("qq", 2);  CharacterController
        SetValue();
    }

    void Update()
    {

    }

    void qq()
    {


        //speed = (int)behaviorTree.GetVariable("Int111").GetValue();
        //speed2 = (float)behaviorTree.GetVariable("Speed2").GetValue();
        //Debug.LogFormat("11: " + speed);
        //Debug.LogFormat ("22: " + speed2);


        Patrol_Speed = (float)behaviorTree.GetVariable("Patrol_Speed").GetValue();
    }


    void SetValue()
    {
        //Patrol_Speed = 5;
        //Patrol_Waypoints = 

        behaviorTree.SetVariable("Patrol_Speed", (SharedFloat)Patrol_Speed);
        behaviorTree.SetVariable("Patrol_Waypoints", (SharedGameObjectList)Patrol_Waypoints);  
    }
}
