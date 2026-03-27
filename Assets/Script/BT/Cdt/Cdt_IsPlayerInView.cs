using UnityEngine;

namespace BT
{
    public class Cdt_IsPlayerInView : Condition
    {
        protected override bool CheckCondition()
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
            Vector3 myPos = avatar.transform.position;
            Vector3 forward = avatar.transform.forward;
            foreach (var go in gos)
            {
                Transform target = go.transform;
                Vector3 dir = target.position - myPos;

                if (Vector3.SqrMagnitude(dir) <= 100f && Vector3.Angle(forward, dir) <= 30f)
                {
                    blackboard.SetValue("target", go);
                    return true;
                }
            }
            blackboard.ClearValue("target");
            return false;
        }
    }
}