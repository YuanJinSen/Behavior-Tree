using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LateUpdate
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float xSpeed = 120.0f;      // 水平旋转速度
        public float ySpeed = 120.0f;      // 垂直旋转速度
        public float yMinLimit = -20f;     // 垂直最小角度
        public float yMaxLimit = 80f;      // 垂直最大角度
    
        private float x = 0.0f;            // 当前水平旋转角度
        private float y = 0.0f;            // 当前垂直旋转角度
        private bool isCursorLocked = true;
        private void Start()
        {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
            UpdateCursorState();
        }

        private void Update()
        {
            if (isCursorLocked)
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    isCursorLocked = false;
                    UpdateCursorState();
                }
                else
                {
                    MouseMove();
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    isCursorLocked = true;
                    UpdateCursorState();
                }
            }
        }

        private void LateUpdate()
        {
            if (!target) return;
            transform.position = target.position;
            transform.rotation = Quaternion.Euler(y, x, 0);
        }

        private void UpdateCursorState()
        {
            Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCursorLocked;
        }

        private void MouseMove()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            // 更新旋转角度
            x += mouseX * xSpeed * Time.deltaTime;
            y -= mouseY * ySpeed * Time.deltaTime;
            
            // 限制垂直角度
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            
            // 保持x在0-360度范围内
            if (x > 360) x -= 360;
            else if (x < 0) x += 360;
        }
    }
}