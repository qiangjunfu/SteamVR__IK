using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_GetChild : MonoBehaviour
{
    [SerializeField]
    GameObject parent;
    [SerializeField] List<GameObject> Patrol_Waypoints;
    [SerializeField] List<Transform> Patrol_Waypoints2;


    // Start is called before the first frame update
    void Start()
    {
         parent = GameObject.Find("Patrol_Waypoints");

        if (Patrol_Waypoints.Count == 0)
        {
            GameObject parent = GameObject.Find("Patrol_Waypoints");
            //Patrol_Waypoints = UnityTools.GetAllChildrenGameObject(parent);
            Patrol_Waypoints = parent.GetChild();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
