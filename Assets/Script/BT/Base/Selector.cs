using System;
using System.Collections.Generic;
using LateUpdate;

namespace BT
{
    public class Selector : BTNode
    {
        public List<BTNode> children = new List<BTNode>();
        public int curIdx = 0;
        public int curRunning = -1;
        
        public Selector(params BTNode[] nodes) { children.AddRange(nodes); }

        public override void Init(Avatar val)
        {
            base.Init(val);
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Init(val);
            }
        }

        public override BTStatus Tick()
        {
            AbortOther();
            for (; curIdx < children.Count; curIdx++)
            {
                BTStatus status = children[curIdx].Tick();
                if (status != BTStatus.Failure)
                {
                    State = status;
                    curRunning = curIdx;
                    if (status == BTStatus.Success) curIdx = 0;
                    return State;
                }
            }
            curIdx = 0;
            State = BTStatus.Failure;
            return State;
        }
        
        public override void Abort()
        {
            base.Abort();
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Abort();
            }
            curIdx = 0;
        }

        private void AbortOther()
        {
            if (curRunning < 0)
            {
                return;
            }
            for (int i = 0; i < children.Count; i++)
            {
                if (i != curRunning) children[i].Abort();
            }

            curRunning = -1;
        }
    }
}