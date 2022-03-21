/*
备忘录模式[MementoPattern]

定义：
在不破坏封装性的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态。这样以后就可将该对象恢复到原先保存的状态

将要保存的细节封装在Memento中，哪一天要更改保存的细节时也不会影响客户
将被储存的状态放在外面，不要和关键对象混在一起，这可以帮助维护内聚
当需要让对象返回之前的状态时，或者需要保存的属性只是众多属性中的一小部分时，可以考虑使用备忘录模式
*/
using System;
using System.Collections.Generic;
namespace MementoPattern 
{
    /// <summary>
    /// 游戏角色
    /// </summary>
    public class Role
    {
        // 生命力
        public int vitality { get; set; }

        // 攻击力
        public int attack { get; set; } = 10;

        // 防御力
        public int defense { get; set; } = 10;

        /// <summary>
        /// 保存角色状态的方法
        /// </summary>
        /// <returns></returns>
        public RoleStateMemento SaveState()
        {
            return new RoleStateMemento(vitality);
        }

        /// <summary>
        /// 恢复角色状态的方法
        /// 根据memento保存的信息还原到之前的状态
        /// </summary>
        /// <param name="memento"></param>
        public void RecoveryState(RoleStateMemento memento)
        {
            vitality = memento.GetVitality();
        }

        public void DisplayState()
        {
            Console.WriteLine("玩家状态：生命力-{0} 攻击力-{1} 防御力-{2}", vitality, attack, defense);
        }
    }

    /// <summary>
    /// 游戏状态存储箱，仅保存角色的生命力
    /// 备忘录
    /// </summary>
    public class RoleStateMemento 
    {
        // 生命力
        private int m_Vitality;

        public RoleStateMemento(int vitality)
        {
            m_Vitality = vitality;
        }

        public int GetVitality()
        {
            return m_Vitality;
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            Role role = new Role();
            // 游戏开始，角色生命力为100
            role.vitality = 100;

            role.DisplayState();  // 玩家状态：生命力-100 攻击力-10 防御力-10

            // 存档，保存游戏状态
            RoleStateMemento memento = role.SaveState();  // 保存状态的操作封装在Memento中，因此客户不知道保存了哪些具体的角色数据

            // 游戏过程中，角色被击杀，生命力为0
            role.vitality = 0;

            role.DisplayState();  // 玩家状态：生命力-0 攻击力-10 防御力-10

            // 重新开始，恢复之前存档的生命力
            role.RecoveryState(memento);

            role.DisplayState();  // 玩家状态：生命力-100 攻击力-10 防御力-10
        }
    }
}