using UnityEngine;

namespace BT
{
    public class Act_Chase : Action
    {
        protected override void OnEnter()
        {
            avatar.PlayAnim("BattleRunB");
        }

        protected override BTStatus OnUpdate()
        {
            if (!blackboard.HasValue("target"))
            {
                return BTStatus.Failure;
            }
            avatar.MoveTo(blackboard.GetValue<GameObject>("target").transform.position);
            return BTStatus.Running;
        }

        protected override void OnExit()
        {
            avatar.PlayAnim("Stand");
        }
    }
}