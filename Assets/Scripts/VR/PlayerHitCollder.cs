using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色碰撞体检测替身
/// 由于SteamVR的一些设置 忽略Player层的碰撞
/// </summary>
public class PlayerHitCollder : MonoBehaviour, IDamage
{
    [SerializeField] PlayerVR playerVR;


    public void SetData(PlayerVR playerVR)
    {
        this.playerVR = playerVR;
    }


    public void TakeDamage(int amount)
    {
        if (playerVR == null)
        {
            playerVR = transform.parent.GetComponent<PlayerVR>();
        }

        playerVR.TakeDamage(amount);
    }


}
