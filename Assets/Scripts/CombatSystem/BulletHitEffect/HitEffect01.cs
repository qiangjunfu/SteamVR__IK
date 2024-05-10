using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect01 : IHitEffect
{
    [SerializeField] private float timeAlive = 0 ; // 特效已存活的时间
    [SerializeField] private float lifeTime = 3; // 特效存活的时间


    void OnEnable()
    {
        timeAlive = 0f; 
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifeTime )
        {
            timeAlive = 0f; // 重置时间，以防再次启用
            ComponentPoolManager.Instance.RecycleObject(this);
        }
    }
}
