/*
访问者模式[VisitorPattern]

定义：
表示一个作用于某对象结构中的各元素的操作。它使你可以在不改变各元素的类的前提下定义作用于这些元素的新操作

访问者模式适用于数据结构相对稳定的系统，它把数据结构和作用于结构上的操作之间的耦合解脱开，使得操作集合可以相对自由地演化
访问者模式的目的是要把处理从数据结构分离出来，访问者模式使得算法操作的增加变得容易
当你想要为一个对象的组合增加新的能力，且封装并不重要时，就使用访问者模式

优点：
1. 允许对组合结构加入新的操作，而无需改变结构本身
2. 增加新的操作很容易，因为增加新的操作就意味着增加一个新的访问者
3. 访问者模式将有关的行为集中到一个访问者对象中

缺点：
1. 采用访问者模式的时候，就会打破组合类的封装
2. 使增加新的数据结构变得困难了
*/
using System;
using System.Collections.Generic;
namespace VisitorPattern 
{
    /// <summary>
    /// 状态的抽象类，Visitor
    /// 定义了得到男人反应和女人反应的接口
    /// 为对象结构中的每一个ConcreteElement（如男人和女人）声明一个visit操作
    /// </summary>
    abstract class Action
    {
        // 得到男人反应
        public abstract void GetManConclusion(Man concreteElementA);

        // 得到女人反应
        public abstract void GetWomanConclusion(Woman concreteElementB);
    }
    
    /// <summary>
    /// 人的抽象类，对象结构中各元素的超类
    /// 定义一个accept操作，以一个访问者为参数
    /// </summary>
    abstract class Person 
    {
        public abstract void Accept(Action visitor);
    }

    class Man : Person
    {
        /// <summary>
        /// 接受一个访问者
        /// 双分派，执行的操作不仅取决于visitor的类型，还取决于this的类型
        /// </summary>
        /// <param name="visitor"></param>
        public override void Accept(Action visitor)
        {
            visitor.GetManConclusion(this);
        }
    }

    class Woman : Person
    {
        public override void Accept(Action visitor)
        {
            visitor.GetWomanConclusion(this);
        }
    }

    /// <summary>
    /// 具体状态类，成功
    /// 具体访问者，实现每个由Visitor声明的操作，每个操作实现算法的一部分，而该算法片段乃是对应于结构中对象的类
    /// </summary>
    class Success : Action
    {
        public override void GetManConclusion(Man concreteElementA)
        {
            Console.WriteLine("{0} {1}时，开心", concreteElementA.GetType().Name, this.GetType().Name);
        }

        public override void GetWomanConclusion(Woman concreteElementB)
        {
            Console.WriteLine("{0} {1}时，喜悦", concreteElementB.GetType().Name, this.GetType().Name);
        }
    }

    /// <summary>
    /// 具体状态类，失败
    /// </summary>
    class Failing : Action
    {
        public override void GetManConclusion(Man concreteElementA)
        {
            Console.WriteLine("{0} {1}时，忍住不哭", concreteElementA.GetType().Name, this.GetType().Name);
        }

        public override void GetWomanConclusion(Woman concreteElementB)
        {
            Console.WriteLine("{0} {1}时，流泪", concreteElementB.GetType().Name, this.GetType().Name);
        }
    }

    /// <summary>
    /// 对象结构类
    /// 能枚举它的元素，可以提供一个高层的接口以允许访问者访问它的元素
    /// </summary>
    class ObjectStructure
    {
        // 对象结构中元素的类型
        private List<Person> m_Elements = new List<Person>();

        public void Attach(Person element)
        {
            m_Elements.Add(element);
        }

        public void Detach(Person element)
        {
            m_Elements.Remove(element);
        }

        public void Display(Action visitor)
        {
            foreach (var element in m_Elements)
            {
                element.Accept(visitor);
            }
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            ObjectStructure o = new ObjectStructure();
            o.Attach(new Man());  // 对象结构中有Man
            o.Attach(new Woman()); // 对象结构中有Woman

            o.Display(new Success());
            o.Display(new Failing());
        }
    }
}