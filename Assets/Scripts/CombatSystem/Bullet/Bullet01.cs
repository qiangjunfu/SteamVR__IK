using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class Bullet01 : IBullet
{
    private float timeAlive;      // 已存活时间
    private float distanceTravelled; // 已飞行距离



    public override void SetData(BulletData bulletData)
    {
        data.id = bulletData.id;
        data.name = bulletData.name;
        data.prePath = bulletData.prePath;
        data.type = bulletData.type;
        data.speed = bulletData.speed;
        data.lifeTime = bulletData.lifeTime;
        data.attack = bulletData.attack;
        data.fireRange = bulletData.fireRange;
        data.bulletHolePrePath = bulletData.bulletHolePrePath;

        InitData();
    }

    void InitData()
    {
        timeAlive = 0f;
        distanceTravelled = 0f;
    }

    void Start()
    {
        InitData();
    }

    void Update()
    {
        float moveDistance = data.speed * Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // 检查沿着子弹前进方向的射线是否有碰撞
        if (Physics.Raycast(ray, out hit, moveDistance))
        {
            if (hit.collider)
            {
                //log.LogFormat("{0} 子弹命中物体:{1} , pos: {2}", this.name, hit.collider.gameObject, hit.point);
                transform.position = hit.point; // 将子弹位置更新到碰撞点
                HandleCollision(hit); // 处理碰撞逻辑
                return;
            }
        }

        // 如果没有碰撞，正常移动子弹
        transform.Translate(Vector3.forward * moveDistance, Space.Self);
        distanceTravelled += moveDistance;
        timeAlive += Time.deltaTime;

        // 检查是否超过生命周期或最大距离
        if (timeAlive >= data.lifeTime || distanceTravelled >= data.fireRange)
        {
            RecycleBullet();
        }
    }

    void HandleCollision(RaycastHit hit)
    {
        IDamage enemyDamage = hit.collider.GetComponent<IDamage>();
        if (enemyDamage != null)
        {
            log.LogFormat("{0} 子弹命中敌人:{1} , pos: {2}", this.name, hit.collider.gameObject, hit.point);
            enemyDamage.TakeDamage(data.attack);

            //显示命中特效
            //string path = "";
            //HitEffect01 hitEffect01 = ComponentPoolManager.Instance.GetObject<HitEffect01>(path, hit.point, Quaternion.LookRotation(hit.normal));

        }
        else
        {
            // 显示弹孔特效
            string path = "Weapons/Effects/DirtImpact";
            HitEffect02 hitEffect02 = ComponentPoolManager.Instance .GetObject  <HitEffect02>(path ,hit.point, Quaternion.LookRotation(hit.normal));
        } 

        RecycleBullet();
    }

    private void RecycleBullet()
    {
        InitData();

        ComponentPoolManager.Instance.RecycleObject<IBullet>(this); // 回收子弹到对象池
    }


    ////处理高速子弹穿透  
    ////方法 1: 使用连续碰撞检测  
    ////方法 2: 使用 Raycasting 检测
    //void OnCollisionEnter(Collision collision)
    //{
    //    // 检查是否击中有 Health 组件的对象
    //    Health health = collision.gameObject.GetComponent<Health>();
    //    if (health != null)
    //    {
    //        health.TakeDamage(damage); // 假设子弹造成10点伤害
    //    }
    //    // 碰撞后销毁子弹或回收到对象池
    //    ObjectPoolManager.Instance.RecycleObject(gameObject);
    //}
}

