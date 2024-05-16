using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ½ÇÉ«Åö×²Ìå¼ì²âÌæÉí
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
