using System;
using BT;
using UnityEngine;

namespace LateUpdate
{
    public class Avatar : MonoBehaviour
    {
        public Animator animator;
        public Blackboard blackboard;
        private BTNode btNode;

        private float lastAtkTime;
        private float atkInterval;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            blackboard = new Blackboard();
            GetBT(gameObject.name);
        }

        private void Start()
        {
            lastAtkTime = 0;
            atkInterval = 1.2f;
        }

        protected BTNode GetBT(string key)
        {
            return BTManager.Instance.getBT(key, this);
        }

        public float GetLastAtkTime()
        {
            return lastAtkTime;
        }

        public void SetLastAtkTime()
        {
            lastAtkTime = Time.time;
        }

        public float GetAtkInterval()
        {
            return atkInterval;
        }

        public void PlayAnim(string key)
        {
            animator.Play(key);
        }

        public void MoveTo(Vector3 point)
        {
            
        }
    }
}