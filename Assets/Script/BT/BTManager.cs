using LateUpdate;

namespace BT
{
    public class BTManager : Singleton<BTManager>
    {
        public BTNode getBT(string key, Avatar avatar)
        {
            switch (key)
            {
                case "Warlord":
                    return getWarlordTree(avatar);
                default:
                    return null;
            }
        }

        public BTNode getWarlordTree(Avatar avatar)
        {
            BTNode root = new Selector(
                new Sequence(
                    new Cdt_IsPlayerInView(), // 是否视野范围内有目标
                    new Selector(
                        new Sequence(
                            new Cdt_IsInAtkRange(), // Y:是否在攻击范围内
                            new Selector(
                                new Sequence(
                                    new Cdt_CanAtk(), // Y:是否可以进行攻击
                                    new Act_Attack()  // Y:攻击
                                ),
                                new Act_Idle() // N:呆住
                            )
                        ),
                        new Act_Chase() // N:追逐
                    )
                ),
                new Act_Patrol()// 巡逻
            );
            root.Init(avatar);
            return root;
        }
    }
}