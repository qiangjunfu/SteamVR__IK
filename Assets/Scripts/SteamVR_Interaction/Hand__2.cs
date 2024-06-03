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

            HandleMovement(); // 调用移动处理方法
        }

        private void HandleMovement()
        {
            Vector2 moveValue = moveAction.GetAxis(handType);
            Vector2 teleportValue = teleportAction.GetAxis(handType);

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (teleportValue != Vector2.zero)
                {
                    // 处理瞬移
                    HandleTeleport(teleportValue);
                }
                else
                {
                    // 处理平滑移动
                    HandleSmoothMovement(moveValue);
                }
            }
            else if (handType == SteamVR_Input_Sources.LeftHand)
            {
                // 左手只进行平滑移动
                HandleSmoothMovement(moveValue);
            }
        }

        private void HandleTeleport(Vector2 teleportValue)
        {
            // 实现瞬移逻辑
            Vector3 teleportDirection = new Vector3(teleportValue.x, 0, teleportValue.y);
            transform.position += teleportDirection * teleportSpeed * Time.deltaTime;
        }

        private void HandleSmoothMovement(Vector2 moveValue)
        {
            // 实现平滑移动逻辑
            Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
