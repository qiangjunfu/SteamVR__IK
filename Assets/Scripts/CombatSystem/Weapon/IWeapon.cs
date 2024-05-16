using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
    [SerializeField] protected  WeaponData data;
    public abstract void SetData(WeaponData weaponData, ICharacter character);
    public abstract int GetBulletCount();
    public abstract bool IsCanShoot();

    public abstract void Shoot();
    public abstract void ContinueShoot(); 

}