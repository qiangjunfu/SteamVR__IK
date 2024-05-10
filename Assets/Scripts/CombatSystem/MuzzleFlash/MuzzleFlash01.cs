using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash01 : IMuzzleFlash
{
    [SerializeField] float flashDuration = 1; // 闪光持续时间
    [SerializeField] private float timer; // 计时器
    private string flashPath;
    GameObject flashObj;





    void InitData()
    {
        timer = flashDuration;
    }
    private void Start()
    {
        InitData();

        if (flashObj == null)
        {
            flashPath = "Weapons/Effects/MuzzleFlash10";
            flashObj = AssetsLoadManager.Instance.LoadObject<GameObject>(flashPath, this.transform);
            //flashObj.transform .localPosition = Vector3.zero;   
            //flashObj.transform.localRotation = Quaternion.identity;
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // 更新计时器
        }
        else
        {
            InitData();
            gameObject.SetActive(false);
            ComponentPoolManager.Instance.RecycleObject<IMuzzleFlash>(this);
        }
    }

    public override void ActivateFlash()
    {
        gameObject.SetActive(true);
        timer = flashDuration; // 重置计时器
    }

}
