using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash01 : IMuzzleFlash
{
    [SerializeField] float flashDuration = 1; // �������ʱ��
    [SerializeField] private float timer; // ��ʱ��
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
            timer -= Time.deltaTime; // ���¼�ʱ��
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
        timer = flashDuration; // ���ü�ʱ��
    }

}
