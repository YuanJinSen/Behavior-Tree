using UnityEngine;

namespace BT
{
    public class Act_Attack : Action
    {
        protected override void OnEnter()
        {
            avatar.PlayAnim("SkillA");
            avatar.SetLastAtkTime();
        }

        protected override BTStatus OnUpdate()
        {
            float endTime = avatar.GetLastAtkTime() + avatar.GetAtkInterval();
            return endTime <= Time.time ? BTStatus.Running : BTStatus.Success;
        }

        protected override void OnExit()
        {
            avatar.PlayAnim("Stand");
        }
    }
}