using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoSingleTon<GameMain>, IManager
{

    protected override void OnAwake()
    {
        base.OnAwake();

        Init();
    }

    public void Init()
    {
        // ��ʼ������ 
        JsonFileManager.Instance.Init();
        ExcelFileManager.Instance.Init();

        ComponentPoolManager.Instance.Init();
        GameObjectPoolManager.Instance.Init();
        AssetsLoadManager.Instance.Init();
        AudioManager.Instance.Init();


        ScoreManager.Instance.Init();
        EnemyManager.Instance.Init();
    }
}
