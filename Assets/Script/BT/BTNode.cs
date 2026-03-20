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