using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class Bullet01 : IBullet
{
    [SerializeField] ICharacter owner_Chara;
    [SerializeField] IWeapon owner_Weapon; 
    private float timeAlive;      // �Ѵ��ʱ��
    private float distanceTravelled; // �ѷ��о���



    public override void SetData(BulletData bulletData, ICharacter character , IWeapon weapon  )
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

        owner_Chara = character;
        owner_Weapon = weapon;

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
        if (Physics.Raycast(ray, out hit, moveDistance))
        {
            if (hit.collider)
            {
                //log.LogFormat("{0} �ӵ���������:{1} , pos: {2}", this.name, hit.collider.gameObject, hit.point);
                transform.position = hit.point; // ���ӵ�λ�ø��µ���ײ��
                HandleCollision(hit); // ������ײ�߼�
                return;
            }
        }

        // ���û����ײ�������ƶ��ӵ�
        transform.Translate(Vector3.forward * moveDistance, Space.Self);
        distanceTravelled += moveDistance;
        timeAlive += Time.deltaTime;

        // ����Ƿ񳬹��������ڻ�������
        if (timeAlive >= data.lifeTime || distanceTravelled >= data.fireRange)
        {
            RecycleBullet();
        }
    }



    string name1, name2;
    void HandleCollision(RaycastHit hit)
    {
        IDamage enemyDamage = hit.collider.GetComponent<IDamage>();
        if (enemyDamage != null)
        {
            name1 = this.owner_Chara.name + " " + owner_Chara.GetId();
            name2 = hit.collider.gameObject.name + " " + owner_Chara.GetId();
            log.LogFormat("{0} �ӵ����е���:{1} , pos: {2}", name1, name2, hit.point);
            // �������Ǽ���ǹ+�ӵ���������
            enemyDamage.TakeDamage(data.attack);

            //��ʾ������Ч
            //string path = "";             string path = "Weapons/Effects/DirtImpact";
            //HitEffect01 hitEffect01 = ComponentPoolManager.Instance.GetObject<HitEffect01>(path, hit.point, Quaternion.LookRotation(hit.normal));

        }
        else
        {
            // ��ʾ������Ч
            HitEffect02 hitEffect02 = ComponentPoolManager.Instance.GetObject<HitEffect02>(data.bulletHolePrePath, hit.point, Quaternion.LookRotation(hit.normal));
        }

        RecycleBullet();
    }

    private void RecycleBullet()
    {
        InitData();

        ComponentPoolManager.Instance.RecycleObject<IBullet>(this); // �����ӵ��������
    }


    ////��������ӵ���͸  
    ////���� 1: ʹ��������ײ���  
    ////���� 2: ʹ�� Raycasting ���
    //void OnCollisionEnter(Collision collision)
    //{
    //    // ����Ƿ������ Health ����Ķ���
    //    Health health = collision.gameObject.GetComponent<Health>();
    //    if (health != null)
    //    {
    //        health.TakeDamage(damage); // �����ӵ����10���˺�
    //    }
    //    // ��ײ�������ӵ�����յ������
    //    ObjectPoolManager.Instance.RecycleObject(gameObject);
    //}
}

