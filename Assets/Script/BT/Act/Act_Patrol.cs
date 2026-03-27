namespace BT
{
    public class Act_Patrol : Action
    {
        protected override void OnEnter()
        {
            avatar.PlayAnim("Alchemy_Start");
        }

        protected override BTStatus OnUpdate()
        {
            return BTStatus.Running;
        }

        protected override void OnExit()
        {
            avatar.PlayAnim("Alchemy_End");
        }
    }
}