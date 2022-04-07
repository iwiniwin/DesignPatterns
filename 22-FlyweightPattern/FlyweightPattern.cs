/*
享元模式[FlyweightPattern]

定义：
运用共享技术有效地支持大量细粒度的对象。又被称为蝇量模式

享元模式可以避免大量非常相似类的开销

在享元对象内部并且不会随环境改变而改变的共享部分，可以称为对象的内部状态，而随环境改变而改变的，不可以共享的状态就是外部状态了

如果一个应用程序使用了大量的对象，而大量的这些对象造成了很大的存储开销时就应该考虑使用享元模式
如果能把实例间那些不同的参数移到实例的外面，在方法调用时将它们传递进来，就可以通过共享大幅度地减少单个实例的数目

享元模式的限制在于，一旦实现了它，那么单个的逻辑实例将无法拥有独立而不同的行为

优点：
1. 减少运行时对象实例的个数，节省内存
2. 便于将许多“虚拟”对象的状态集中管理

实际中的例子：
1. 在.Net中，字符串string就是使用了享元模式，如果第一次创建了字符串对象，下次再需要相同的字符串时，只是把它的引用指向内存中已有的字符串
2. 对象池。比如射击游戏中的子弹
*/
using System;
using System.Collections.Generic;
namespace FlyweightPattern 
{
    /// <summary>
    /// 围棋棋子
    /// </summary>
    abstract class Piece 
    {
        // 所有具体享元类的超类或接口，通过这个接口，Flyweight可以接受并作用于外部状态（比如这里的下棋位置x,y就是外部状态）
        public abstract void Display(int x, int y);
    }

    class ConcretePiece : Piece
    {
        private string m_Color;  // 棋子的颜色，是内部状态
        public ConcretePiece(string color)
        {
            m_Color = color;
        }

        public override void Display(int x, int y)
        {
            Console.WriteLine("{0}棋下在[{1}, {2}]位置", m_Color, x, y);
        }
    }
    
    /// <summary>
    /// 棋子工厂
    /// 一个享元工厂，用来创建并管理Flyweight，它主要用来确保合理地共享Flyweight
    /// </summary>
    class PieceFactory
    {
        private Dictionary<string, Piece> m_Flyweights = new Dictionary<string, Piece>();

        /// <summary>
        /// 当用户请求一个Flyweight时，Factory对象提供一个已创建的实例或者创建一个（如果不存在的话）
        /// </summary>
        public Piece GetPiece(string color)
        {
            if(!m_Flyweights.ContainsKey(color))
            {
                m_Flyweights.Add(color, new ConcretePiece(color));
            }
            return m_Flyweights[color];
        }

        public int GetPieceCount()
        {
            return m_Flyweights.Count;
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            PieceFactory factory = new PieceFactory();

            factory.GetPiece("白").Display(1, 3);
            factory.GetPiece("黑").Display(2, 3);
            factory.GetPiece("黑").Display(4, 3);
            factory.GetPiece("白").Display(3, 2);
            Console.WriteLine(factory.GetPieceCount());

            string title1 = "test";
            string title2 = "test";
            string title3 = new string("test");
            bool equal1 = Object.ReferenceEquals(title1, title2);  // 享元模式指向同一地址
            bool equal2 = Object.ReferenceEquals(title1, title3);  // title3是显示new的，地址不同
            Console.WriteLine(equal1);  // true
            Console.WriteLine(equal2);  // false
        }
    }
}