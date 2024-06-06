using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;



[RequireComponent(typeof(Throwable__2))]
public class ThrowableEventHandler : MonoBehaviour
{
    [SerializeField] private Throwable__2 throwable;


    #region MyRegion
    //UnityEvent: �ʺ���Unity�༭���н������ã�����ͨ��Inspector������ק���ã��ǳ��ʺ������Ա�ͷǳ���Աʹ�á�
    //UnityAction: �ʺ��ڴ����н��ж�̬���ú͵��ã����ܸ��ߣ�����Ը�ǿ���ʺϳ���Ա�ڴ�����ʹ�á�
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

    // �������ץȡʱ���¼�
    public void OnPickUp(Hand hand)
    {

        Debug.Log("Object picked up by: " + hand.name);
    }

    // �����������з���ʱ���¼�
    public void OnDetachFromHand(Hand hand)
    {

        Debug.Log("Object detached from: " + hand.name);
    }

    // ������󱻱���ʱ���¼�
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
