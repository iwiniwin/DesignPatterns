/*
组合模式[CompositePattern]

定义：
允许你将对象组合成树形结构来表现“整体/部分”的层次结构。组合能让客户以一致的方式处理个别对象以及对象组合

组合模式让我们能用树形方式创建对象的结构，树里面包含了组合以及个别的对象。使用组合结构，我们能把相同的操作应用在组合和个别对象上。换句话说，在大多数情况下，我们可以忽略对象组合和个别对象之间的差别
当有数个对象的结合，他们彼此之间有“整体/部分”的关系，并且你想用一致的方式对待这些对象时，可以使用组合模式

组合模式以单一责任设计原则换取透明性。什么是透明性？通过让组件的接口同时包含一些子节点和叶节点的操作，客户就可以将组合和叶节点一视同仁。也就是说一个元素究竟是组合还是子节点，对客户是透明的，客户不再需要使用大量的If语句来判断哪个对象是用哪个接口。
但是这样也会失去一些“安全性”，因为客户有机会对一个元素做一些不恰当或没有意义的操作（例如为叶节点添加子节点）。这是一个很典型的透明性与安全性的折中案例
*/
using System;
using System.Collections.Generic;
namespace CompositePattern 
{
    /// <summary>
    /// 菜单组件
    /// 菜单组件的角色是为叶节点和组合节点提供一个共同的接口
    /// 为这些接口提供了默认的实现（默认抛异常），这样如果叶节点不想实现GetChild就可以不实现
    /// </summary>
    public abstract class MenuComponent 
    {
        /// <summary>
        /// 添加菜单组件，组合方法
        /// </summary>
        public virtual void Add(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 移除菜单组件，组合方法
        /// </summary>
        public virtual void Remove(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 取得菜单组件，组合方法
        /// </summary>
        public virtual MenuComponent GetChild(int i)
        {
            throw new NotSupportedException();
        }

        public virtual string GetName()
        {
            throw new NotSupportedException();
        }

        public virtual void Print() 
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// 菜单项
    /// 组合类图里的叶类
    /// </summary>
    public class MenuItem : MenuComponent
    {
        private string m_Name;
        public MenuItem(string name) 
        {
            m_Name = name;
        }

        public override string GetName()
        {
            return m_Name;
        }

        public override void Print() 
        {
            Console.WriteLine(m_Name);
        }
    }

    /// <summary>
    /// 组合菜单，可以持有菜单项或其他菜单
    /// </summary>
    public class Menu : MenuComponent
    {
        // 组合菜单可以有任意数量的孩子
        List<MenuComponent> m_MenuComponents = new List<MenuComponent>();
        private string m_Name;

        public Menu(string name) 
        {
            m_Name = name;
        }

        public override void Add(MenuComponent menuComponent)
        {
            m_MenuComponents.Add(menuComponent);
        }

        public override void Remove(MenuComponent menuComponent)
        {
            m_MenuComponents.Remove(menuComponent);
        }

        public override MenuComponent GetChild(int i)
        {
            return m_MenuComponents[i];
        }

        public override string GetName()
        {
            return m_Name;
        }

        /// <summary>
        /// 组合菜单包含了菜单项和其他的菜单，所以Print应该打印出它所包含的一切
        /// </summary>
        public override void Print()
        {
            Console.WriteLine(m_Name + ":");
            // 使用迭代器遍历所有子菜单
            foreach (var item in m_MenuComponents)
            {
                // 利用组合模式，能够对整个菜单结构应用相同的操作，例如打印
                item.Print();
            }
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            MenuComponent breakfastMenu = new Menu("早餐");
            MenuComponent lunchMenu = new Menu("午餐");

            MenuComponent allMenus = new Menu("总菜单");

            allMenus.Add(breakfastMenu);
            allMenus.Add(lunchMenu);

            breakfastMenu.Add(new MenuItem("油条"));
            breakfastMenu.Add(new MenuItem("豆浆"));

            lunchMenu.Add(new MenuItem("黄焖鸡米饭"));

            // 利用组合模式，能够对整个菜单结构应用相同的操作，例如打印
            allMenus.Print();
        }
    }
}