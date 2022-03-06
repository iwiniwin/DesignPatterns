/*
装饰者模式[DecoratorPattern]

定义：
动态地将责任附加到对象上。若要扩展功能，装饰者提供了比继承更有弹性的替代方案
装饰者模式是为已有功能动态地添加更多功能的一种方式，它把每个要装饰的功能放在单独的类中，并让这个类包装它所要装饰的对象，因此，当需要执行特殊行为时，客户代码就可以在运行时根据需要有选择地，按顺序地使用装饰功能包装对象了
有效地把类的核心职责和装饰功能区分开了，而且可以去除相关类中重复的装饰逻辑

装饰者和被装饰者必须是一样的类型，也就是说有共同的超类，因为装饰者必须能取代被装饰者

Java中的IO Stream类是装饰者模式的实际应用

装饰者的一个缺点是可能会在设计中加入大量的小类，偶尔会使用起来不易理解，层层嵌套。比如Java IO的FileInputStream、BufferedInputStream
*/
using System;
namespace DecoratorPattern 
{
    /// <summary>
    /// 饮料抽象类
    /// </summary>
    public abstract class Beverage
    {
        public virtual string description { get; set; } = "未知饮料";
        public abstract double Cost();
    }

    /// <summary>
    /// 浓缩咖啡，具体的饮料类
    /// </summary>
    public class Espresso : Beverage
    {
        public override string description { get; set; } = "浓缩咖啡";
        public override double Cost()
        {
            return 1.99;
        }
    }

    /// <summary>
    /// 调料抽象类，是所有装饰者类的超类，用来装饰Beverage，必须让CondimentDecorator能够替代Beverage，所以CondimentDecorator继承自Beverage
    /// </summary>
    public abstract class CondimentDecorator : Beverage
    {
    }

    /// <summary>
    /// 奶泡，具体的调料装饰类
    /// </summary>
    public class Whip : CondimentDecorator
    {
        Beverage m_Beverage;  // 用一个实例记录饮料，也就是被装饰者
        public Whip(Beverage beverage)
        {
            m_Beverage = beverage;
        }

        public override string description
        {
            get 
            {
                return m_Beverage.description + ", 奶泡";  // 在被装饰者的描述上附加自己的描述 
            }
        }

        public override double Cost()
        {
            return m_Beverage.Cost() + 0.2;  // 在被装饰者的价格上附加自己的价格
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            Beverage beverage = new Espresso();  // 定一杯浓缩咖啡，原价1.99
            beverage = new Whip(beverage);  // 加入奶泡
            beverage = new Whip(beverage);  // 再加入一次奶泡，即双倍奶泡
            Console.WriteLine(beverage.description + " : " + beverage.Cost());
        }
    }
}