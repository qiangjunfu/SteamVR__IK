using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable  ]
public class MyClass
{
    public int value;
}



public class Test_refout : MonoBehaviour
{
    [SerializeField]MyClass myClass1;
    [SerializeField]MyClass myClass2;
    [SerializeField]MyClass myClass3;
    [SerializeField]MyClass myClass4;


    void Start()
    {
         myClass1 = new MyClass();
        myClass1.value = 5;

         myClass2 = new MyClass();
        myClass2.value = 10;

        myClass3 = new MyClass();
        myClass3.value = 10;

        myClass4 = new MyClass();

        // 使用 ref 关键字传递引用类型
        ModifyRef(ref myClass1);
        Debug.Log("Value after ModifyRef: " + myClass1.value);

        // 使用 out 关键字传递引用类型
        ModifyOut(out myClass2);
        Debug.Log("Value after ModifyOut: " + myClass2.value);

        aaaaa( myClass3);
    }

    void ModifyRef(ref MyClass myClass)
    {
        myClass = new MyClass();
        myClass.value = 20;
    }

    void ModifyOut(out MyClass myClass)
    {
        myClass = new MyClass();
        myClass.value = 30;
    }

    void  aaaaa( MyClass myClass)
    {
        myClass = new MyClass();
        myClass.value = 30;
    }
}