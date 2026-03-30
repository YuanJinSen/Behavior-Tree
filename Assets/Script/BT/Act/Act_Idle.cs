namespace BT
{
    public class Act_Idle : Action
    {
        protected override void OnEnter()
        {
            
        }

        protected override BTStatus OnUpdate()
        {
            return BTStatus.Running;
        }

        protected override void OnExit()
        {
            
        }
    }
}