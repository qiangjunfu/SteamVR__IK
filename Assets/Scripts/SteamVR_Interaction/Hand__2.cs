using UnityEngine;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class Hand__2 : Hand
    {
        public SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Move");
        public SteamVR_Action_Vector2 teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Teleport");
        public float moveSpeed = 2.0f;
        public float teleportSpeed = 5.0f;

        protected override void Update()
        {
            base.Update();

            HandleMovement(); // �����ƶ�������
        }

        private void HandleMovement()
        {
            Vector2 moveValue = moveAction.GetAxis(handType);
            Vector2 teleportValue = teleportAction.GetAxis(handType);

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (teleportValue != Vector2.zero)
                {
                    // ����˲��
                    HandleTeleport(teleportValue);
                }
                else
                {
                    // ����ƽ���ƶ�
                    HandleSmoothMovement(moveValue);
                }
            }
            else if (handType == SteamVR_Input_Sources.LeftHand)
            {
                // ����ֻ����ƽ���ƶ�
                HandleSmoothMovement(moveValue);
            }
        }

        private void HandleTeleport(Vector2 teleportValue)
        {
            // ʵ��˲���߼�
            Vector3 teleportDirection = new Vector3(teleportValue.x, 0, teleportValue.y);
            transform.position += teleportDirection * teleportSpeed * Time.deltaTime;
        }

        private void HandleSmoothMovement(Vector2 moveValue)
        {
            // ʵ��ƽ���ƶ��߼�
            Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
