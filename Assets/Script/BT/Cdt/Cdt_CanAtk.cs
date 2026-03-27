using UnityEngine;

namespace BT
{
    public class Cdt_CanAtk : Condition
    {
        protected override bool CheckCondition()
        {
            float needTime = avatar.GetLastAtkTime() + avatar.GetAtkInterval();
            return needTime <= Time.time;
        }
    }
}