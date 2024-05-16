using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect01 : IHitEffect
{
    [SerializeField] private float timeAlive = 0 ; // ��Ч�Ѵ���ʱ��
    [SerializeField] private float lifeTime = 3; // ��Ч����ʱ��


    void OnEnable()
    {
        timeAlive = 0f; 
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifeTime )
        {
            timeAlive = 0f; // ����ʱ�䣬�Է��ٴ�����
            ComponentPoolManager.Instance.RecycleObject(this);
        }
    }
}
