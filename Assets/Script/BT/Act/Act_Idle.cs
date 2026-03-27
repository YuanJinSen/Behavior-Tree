namespace BT
{
    public class Act_Idle : Action
    {
        protected override void OnEnter()
        {
            avatar.PlayAnim("Stand");
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