using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun01 : IWeapon
{
    [SerializeField ] ICharacter character;
    public Transform muzzlePosition;    // ǹ��λ�ã�����ȷ���ӵ�����λ��
    [SerializeField] int currentAmmo;   // ��ǰ��ҩ��
    private float fireTimer;            // �����ʱ��

    IBullet bullet;
    IMuzzleFlash flash;



    public override void SetData(WeaponData weaponData , ICharacter character)
    {
        data.id = weaponData.id;
        data.name = weaponData.name;
        data.prePath = weaponData.prePath;
        data.type = weaponData.type;
        data.attack = weaponData.attack;
        //data.maxAmmo = weaponData.maxAmmo;
        data.maxAmmo = 10000000; //��ʱĬ��һֱ���ӵ�
        data.reloadTime = weaponData.reloadTime;
        data.recoil = weaponData.recoil;
        data.fireRate = weaponData.fireRate;
        data.fireRange = weaponData.fireRange;
        data.bulletPrePath = weaponData.bulletPrePath;
        data.flashPrePath = weaponData.flashPrePath;
        data.audioPrePath = weaponData.audioPrePath;

        this.character = character;

        InitData();
    }

    public override int GetBulletCount()
    {
        return currentAmmo;
    }
    public override bool IsCanShoot()
    {
        return fireTimer <= 0 ? true : false;
    }

    void InitData()
    {
        currentAmmo = data.maxAmmo;          // ��ʼ�ӵ���������Ϊ���
        fireTimer = 0;                  // ��ʼ���ü�ʱ��Ϊ0
    }

    void Start()
    {

    }

    void Update()
    {
        // ���������ʱ��
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }


    public override void Shoot()
    {
        if (currentAmmo > 0 && fireTimer <= 0)
        {
            // �Ӷ�����л�ȡ�ӵ���������λ�úͷ���
            BulletData bulletData = JsonFileManager.Instance.GetBulletDataList()[0];
            bullet = ComponentPoolManager.Instance.GetObject<IBullet>(bulletData.prePath, muzzlePosition.position, muzzlePosition.rotation);
            bullet.SetData(bulletData);

            // ʵ����ǹ������
            flash = ComponentPoolManager.Instance.GetObject<IMuzzleFlash>(data.flashPrePath, muzzlePosition.position, muzzlePosition.rotation);

            // �����������
            AudioManager.Instance.Play3DSound(data.audioPrePath, muzzlePosition.position);


            fireTimer = data.fireRate; // ���������ʱ��
            currentAmmo--;        // ���ٵ�ҩ����
        }
    }

    public override void ContinueShoot()
    {
        
    }

    public void Reload(int ammoNum = 10)
    {
        currentAmmo = data.maxAmmo; // ����װ�ҩ
    }
}
