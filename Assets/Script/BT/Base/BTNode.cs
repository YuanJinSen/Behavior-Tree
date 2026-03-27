using LateUpdate;

namespace BT
{
    public enum BTStatus
    {
        Success,
        Failure,
        Running
    }
    
    public abstract class BTNode
    {
        public BTStatus State { get; protected set; } = BTStatus.Failure;
        protected Avatar avatar;
        protected Blackboard blackboard;

        public virtual void Init(Avatar val)
        {
            avatar = val;
            blackboard = val.blackboard;
        }
        
        public abstract BTStatus Tick();
    
        // 节点开始执行
        protected virtual void OnStart() { }
    
        // 节点结束执行
        protected virtual void OnStop() { }
    
        // 中止节点
        public virtual void Abort()
        {
            State = BTStatus.Failure;
            OnStop();
        }
    }
}