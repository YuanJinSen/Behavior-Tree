using BT;
using UnityEngine;
using UnityEngine.AI;

namespace LateUpdate
{
    public class Avatar : MonoBehaviour
    {
        public Animator animator;
        public Blackboard blackboard;
        private BTNode btNode;
        private NavMeshAgent nav;

        private float lastAtkTime;
        private float atkInterval;
        protected void Awake()
        {
            animator = GetComponent<Animator>();
            blackboard = new Blackboard();
            btNode = GetBT(gameObject.name);
            nav = gameObject.AddComponent<NavMeshAgent>();
        }

        protected void Start()
        {
            lastAtkTime = 0;
            atkInterval = 1.2f;
        }

        protected void Update()
        {
            btNode.Tick();
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
            print(key);
            animator.Play(key);
        }

        public void MoveTo(Vector3 point)
        {
            Debug.Log("moving");
            //nav.Move(point);
        }
    }
}