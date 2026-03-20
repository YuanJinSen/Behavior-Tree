using UnityEngine;

namespace LateUpdate
{
    public class PlayerController : MonoBehaviour
    {
        [Header("移动设置")] public float moveSpeed = 5.0f; // 移动速度
        public float rotationSpeed = 10.0f; // 转向速度
        public float acceleration = 10.0f; // 加速度
        public float deceleration = 10.0f; // 减速度

        [Header("跳跃设置")] public float jumpForce = 8.0f; // 跳跃力
        public float gravity = 20.0f; // 重力
        public LayerMask groundMask; // 地面层
        public Transform groundCheck; // 地面检测点
        public float groundCheckRadius = 0.2f; // 地面检测半径
        public float groundCheckDistance = 0.4f; // 地面检测距离

        [Header("摄像机参考")] public Transform cameraTransform; // 摄像机Transform

        // 私有变量
        private CharacterController controller;
        private Vector3 moveDirection = Vector3.zero;
        private Vector3 velocity = Vector3.zero;
        private float currentSpeed = 0.0f;
        private float verticalVelocity = 0.0f;
        public bool isGrounded = false;

        void Start()
        {
            // 获取或添加CharacterController组件
            controller = GetComponent<CharacterController>();
            if (controller == null)
            {
                controller = gameObject.AddComponent<CharacterController>();
                controller.center = new Vector3(0, 1, 0);
                controller.height = 2.0f;
            }
        }

        void Update()
        {
            if (!cameraTransform) return;

            // 地面检测
            CheckGround();

            // 处理移动输入
            HandleMovement();

            // 处理跳跃输入
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                verticalVelocity = jumpForce;
            }

            // 应用重力
            ApplyGravity();

            // 应用移动
            ApplyMovement();
        }

        /// <summary>
        /// 检测是否在地面
        /// </summary>
        private void CheckGround()
        {
            Vector3 checkPosition = groundCheck.position;

            // 使用SphereCast进行地面检测
            RaycastHit hit;
            if (Physics.SphereCast(checkPosition, groundCheckRadius, Vector3.down, out hit, groundCheckDistance, groundMask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        /// <summary>
        /// 处理移动输入
        /// </summary>
        private void HandleMovement()
        {
            // 获取输入
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            if (horizontal != 0 || vertical != 0)
            {
                // 计算输入向量
                Vector3 input = new Vector3(horizontal, 0, vertical);
                
                // 计算移动方向（基于摄像机方向）
                Vector3 cameraForward = cameraTransform.forward;
                Vector3 cameraRight = cameraTransform.right;

                // 忽略摄像机的垂直分量
                cameraForward.y = 0;
                cameraRight.y = 0;
                cameraForward.Normalize();
                cameraRight.Normalize();

                // 计算相对于摄像机的移动方向
                Vector3 moveRelative = cameraForward * input.z + cameraRight * input.x;
                moveRelative.Normalize();

                // 计算目标速度
                float targetSpeed = moveSpeed;

                // 平滑加速
                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

                // 设置移动方向
                moveDirection = moveRelative;

                // 旋转角色面向移动方向
                if (moveRelative.magnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveRelative);
                    transform.rotation =
                        Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                // 平滑减速
                currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
            }
        }

        /// <summary>
        /// 应用重力
        /// </summary>
        private void ApplyGravity()
        {
            if (!isGrounded)
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
            else if (verticalVelocity < 0)
            {
                // 在地面上时重置垂直速度
                verticalVelocity = -2.0f; // 轻微向下的力确保贴地
            }
        }

        /// <summary>
        /// 应用移动
        /// </summary>
        private void ApplyMovement()
        {
            // 计算最终移动向量
            Vector3 moveVector = moveDirection * currentSpeed;
            moveVector.y = verticalVelocity;

            // 应用移动
            controller.Move(moveVector * Time.deltaTime);
        }
    }
}