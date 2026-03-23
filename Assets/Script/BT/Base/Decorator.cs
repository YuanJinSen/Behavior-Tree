namespace BT
{
    // 装饰器基类：修饰一个子节点的行为
    public abstract class Decorator : BTNode
    {
        protected BTNode child;
    
        public Decorator(BTNode childNode)
        {
            child = childNode;
        }
    }

    // 取反装饰器：反转子节点的结果
    public class Inverter : Decorator
    {
        public Inverter(BTNode childNode) : base(childNode) { }
    
        public override BTStatus Tick()
        {
            if (child == null) 
            {
                State = BTStatus.Failure;
                return State;
            }
        
            BTStatus childState = child.Tick();
        
            switch (childState)
            {
                case BTStatus.Success:
                    State = BTStatus.Failure;
                    break;
                case BTStatus.Failure:
                    State = BTStatus.Success;
                    break;
                default:
                    State = childState;
                    break;
            }
        
            return State;
        }
    }

    // 重复装饰器：重复执行子节点N次
    public class Repeater : Decorator
    {
        private int repeatCount;
        private int currentCount = 0;
    
        public Repeater(BTNode childNode, int count) : base(childNode)
        {
            repeatCount = count;
        }
    
        public override BTStatus Tick()
        {
            if (child == null) 
            {
                State = BTStatus.Failure;
                return State;
            }
        
            for (int i = currentCount; i < repeatCount; i++)
            {
                BTStatus childState = child.Tick();
            
                if (childState == BTStatus.Running)
                {
                    State = BTStatus.Running;
                    return State;
                }
            
                if (childState == BTStatus.Failure)
                {
                    currentCount = 0;
                    State = BTStatus.Failure;
                    return State;
                }
            
                currentCount++;
            }
        
            currentCount = 0;
            State = BTStatus.Success;
            return State;
        }
    }
}