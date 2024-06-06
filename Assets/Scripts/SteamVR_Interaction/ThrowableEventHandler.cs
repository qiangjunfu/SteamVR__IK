using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;



[RequireComponent(typeof(Throwable__2))]
public class ThrowableEventHandler : MonoBehaviour
{
    [SerializeField] private Throwable__2 throwable;


    #region MyRegion
    //UnityEvent: 适合在Unity编辑器中进行配置，允许通过Inspector进行拖拽设置，非常适合设计人员和非程序员使用。
    //UnityAction: 适合在代码中进行动态设置和调用，性能更高，灵活性更强，适合程序员在代码中使用。
    private void OnEnable()
    {
        if (throwable == null) throwable = GetComponent<Throwable__2>();

        if (throwable != null)
        {
            throwable.onPickUp.AddListener(OnPickUp);
            throwable.onDetachFromHand.AddListener(OnDetachFromHand);
            throwable.onHeldUpdate.AddListener(OnHeldUpdate);
        }
    }
    private void OnDisable()
    {
        if (throwable != null)
        {
            throwable.onPickUp.RemoveListener(OnPickUp);
            throwable.onDetachFromHand.RemoveListener(OnDetachFromHand);
            throwable.onHeldUpdate.RemoveListener(OnHeldUpdate);
        }
    }

    // 处理对象被抓取时的事件
    public void OnPickUp(Hand hand)
    {

        Debug.Log("Object picked up by: " + hand.name);
    }

    // 处理对象从手中分离时的事件
    public void OnDetachFromHand(Hand hand)
    {

        Debug.Log("Object detached from: " + hand.name);
    }

    // 处理对象被保持时的事件
    public void OnHeldUpdate(Hand hand)
    {
        Debug.Log("Object is being held by: " + hand.name);
    }
    #endregion


    void Start()
    {
        //UnityAction
        //UnityEvent
    }

    void OnDestroy()
    {

    }


}
