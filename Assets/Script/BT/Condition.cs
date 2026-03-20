namespace BT
{
    public abstract class Condition : BTNode
    {
        protected abstract bool CheckCondition();
        
        public override BTStatus Tick()
        {
            bool conditionMet = CheckCondition();
            State = conditionMet ? BTStatus.Success : BTStatus.Failure;
            return State;
        }
    }
}