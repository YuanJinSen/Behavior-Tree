using System;
using BT;
using UnityEngine;

namespace LateUpdate
{
    public class Avatar : MonoBehaviour
    {
        private Animator animator;
        private BTNode btNode;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected BTNode getBT(string key)
        {
            return BTManager.Instance.getBT(key, this);
        }
    }
}