using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun01 : IWeapon
{
    [SerializeField ] ICharacter owner;
    public Transform muzzlePosition;    // 枪口位置，用于确定子弹发射位置
    [SerializeField] int currentAmmo;   // 当前弹药量
    private float fireTimer;            // 射击计时器

    IBullet bullet;
    IMuzzleFlash flash;



    public override void SetData(WeaponData weaponData , ICharacter character)
    {
        data.id = weaponData.id;
        data.name = weaponData.name;
        data.prePath = weaponData.prePath;
        data.type = weaponData.type;
        data.attack = weaponData.attack;
        data.maxAmmo = weaponData.maxAmmo;
        data.reloadTime = weaponData.reloadTime;
        data.recoil = weaponData.recoil;
        data.fireRate = weaponData.fireRate;
        data.fireRange = weaponData.fireRange;
        data.bulletPrePath = weaponData.bulletPrePath;
        data.flashPrePath = weaponData.flashPrePath;
        data.audioPrePath = weaponData.audioPrePath;

        this.owner = character;

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
        currentAmmo = data.maxAmmo;          // 初始子弹数量设置为最大
        fireTimer = 0;                  // 初始设置计时器为0

    }

    void Start()
    {
        if (muzzlePosition == null ) { muzzlePosition = transform.Find("MuzzlePosition"); }
    }

    void Update()
    {
        // 更新射击计时器
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }


    public override void Shoot()
    {
        if (currentAmmo > 0 && fireTimer <= 0)
        {
            // 创建子弹
            BulletData bulletData = ExcelFileManager.Instance.GetBulletDataList()[0];
            bullet = ComponentPoolManager.Instance.GetObject<IBullet>(bulletData.prePath, muzzlePosition.position, muzzlePosition.rotation);
            bullet.SetData(bulletData ,owner , this );

            // 实例化枪口闪光
            flash = ComponentPoolManager.Instance.GetObject<IMuzzleFlash>(data.flashPrePath, muzzlePosition.position, muzzlePosition.rotation);

            // 播放射击声音
            AudioManager.Instance.Play3DSound(data.audioPrePath, muzzlePosition.position);


            fireTimer = data.fireRate; // 重置射击计时器
            currentAmmo--;        // 减少弹药数量
        }
    }

    public override void ContinueShoot()
    {
        
    }

    public void Reload(int ammoNum = 10)
    {
        currentAmmo = data.maxAmmo; // 重新装填弹药
    }
}
