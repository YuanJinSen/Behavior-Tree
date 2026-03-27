using System;
using UnityEngine;

namespace BT
{
    public class Cdt_IsInAtkRange : Condition
    {
        protected override bool CheckCondition()
        {
            if (!blackboard.HasValue("target"))
            {
                return false;
            }

            GameObject target = blackboard.GetValue<GameObject>("target");
            Vector3 myPos = avatar.transform.position;
            Vector3 targetPos = target.transform.position;
            return Vector3.SqrMagnitude(myPos - targetPos) < 9f;
        }
    }
}