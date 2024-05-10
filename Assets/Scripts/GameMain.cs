using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoSingleTon<GameMain>
{


    protected override void OnAwake()
    {
        base.OnAwake();

        // 初始化数据 
        JsonFileManager.Instance.Init();
        ExcelFileManager.Instance.Init();

        ComponentPoolManager.Instance.Init();
        GameObjectPoolManager.Instance.Init();
        AssetsLoadManager.Instance.Init();
        AudioManager.Instance.Init();


        EnemyManager.Instance.Init();
    }

}
