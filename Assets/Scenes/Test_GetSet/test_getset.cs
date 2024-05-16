using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_getset : MonoBehaviour
{
    [SerializeField] int hp = 100;

    public int HP_GetSet(int? value = null)
    {
        if (value.HasValue)
        {
            Debug.LogFormat("hp ´Ó:{0} ÐÞ¸Ä¸Ã:{1}",  hp, value.Value);
            hp = value.Value;
        }
        return hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TakeDamage(10);

        //HP_GetSet (50);
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage(1);
    }


    int hp2;
    public void TakeDamage(int amount)
    {
        //data.hp -= amount;
        //if (data.hp <= 0)
        //{
        //    Die();
        //}
        hp2 = HP_GetSet() - amount;
        HP_GetSet(hp2);
    }
}
