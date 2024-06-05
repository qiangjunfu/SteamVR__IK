using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIVRInteract_Button : UIVRInteractable
{
    [SerializeField] private Button btn;
    [SerializeField] Color originalColor = Color.white;
    [SerializeField] Color selectColor = Color.green;


    private void Start()
    {
        btn = btn ?? GetComponent<Button>();
        btn.onClick.AddListener(BtnClick);
        originalColor = btn.image.color;
    }

    private void BtnClick()
    {
        Debug.Log(this.gameObject.name + " --- ��ť�����");
    }

    public override void OnPointerClick(object sender)
    {
        base.OnPointerClick(sender);
        btn.onClick.Invoke();
    }

    public override void OnPointerIn(object sender)
    {
        base.OnPointerIn(sender);

        ChangeButtonColor(selectColor); // ����ʱ�ı䰴ť��ɫ 
    }

    public override void OnPointerOut(object sender)
    {
        base.OnPointerOut(sender);

        ChangeButtonColor(originalColor); // �˳�ʱ�ָ���ť��ɫ
    }

    private void ChangeButtonColor(Color newColor)
    {
        if (btn != null && btn.image != null)
        {
            btn.image.color = newColor;
        }
    }
}