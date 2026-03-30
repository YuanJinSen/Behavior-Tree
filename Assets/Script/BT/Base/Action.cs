using UnityEngine;

namespace BT
{
    public abstract class Action : BTNode
    {
        private bool isRunning = false;
        
        public override BTStatus Tick()
        {
            if (!isRunning)
            {
                OnStart();
                isRunning = true;
            }

            State = OnUpdate();

            if (State != BTStatus.Running)
            {
                OnStop();
                isRunning = false;
            }
            return State == BTStatus.Failure ? BTStatus.Failure : BTStatus.Success;
        }
        
        public override void Abort()
        {
            if (isRunning)
            {
                OnStop();
                isRunning = false;
                base.Abort();
            }
        }
        
        protected abstract void OnEnter();
        protected abstract BTStatus OnUpdate();
        protected abstract void OnExit();
    
        protected sealed override void OnStart() => OnEnter();
        protected sealed override void OnStop() => OnExit();
    }
}