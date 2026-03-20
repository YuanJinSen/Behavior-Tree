using System;
using System.Collections.Generic;

namespace BT
{
    public class Sequence : BTNode
    {
        public List<BTNode> children = new List<BTNode>();
        public int curIdx = 0;
        
        public Sequence(params BTNode[] nodes) { children.AddRange(nodes); }

        public override BTStatus Tick()
        {
            for (; curIdx < children.Count; curIdx++)
            {
                BTStatus status = children[curIdx].Tick();
                if (status != BTStatus.Success)
                {
                    State = status;
                    if (status == BTStatus.Failure) curIdx = 0;
                    return State;
                }
            }
            curIdx = 0;
            State = BTStatus.Success;
            return State;
        }
        
        public override void Abort()
        {
            base.Abort();
            if (curIdx < children.Count) children[curIdx].Abort();
            curIdx = 0;
        }
    }
}