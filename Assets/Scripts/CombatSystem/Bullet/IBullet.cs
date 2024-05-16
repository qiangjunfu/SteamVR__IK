using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IBullet : MonoBehaviour
{
    [SerializeField] protected BulletData data;
    public abstract void SetData(BulletData bulletData, ICharacter character, IWeapon weapon);
}
