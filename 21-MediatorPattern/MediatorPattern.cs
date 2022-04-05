/*
中介者模式[MediatorPattern]

定义：
用一个中介者对象来封装一系列的对象交互，中介者使个对象不需要显式地相互引用，从而使其耦合松散，而且可以独立地改变它们之间的交互

在没有中介者的情况下，所有的对象都需要认识其他对象，对象之间是紧耦合的。有了中介者以后，对象之间彻底解耦
Mediator的出现减少了各个Colleague的耦合，使得可以独立地改变和复用各个Colleague类
由于把对象如何协作进行了抽象，将中介作为一个独立的概念并将其封装在一个对象中，这个关注的对象就从对象各自本身的行为转移到它们之间的交互上来，也就是站在一个更宏观的角度去看待系统

使用中介者模式来集中相关对象之间复杂的沟通和控制方式
中介者内包含了整个系统的控制逻辑。当某装置需要一个新的规则时，或者是一个新的装置被加入系统内，其所有需要用到的逻辑也都被加进了中介者内
由于ConcreteMediator控制了集中化，于是就把交互复杂性变为了中介者的复杂性，这就使得中介者会变得比任何一个ConcreteMediator都复杂
中介者模式的缺点是，如果设计不当，中介者对象本身会变得过于复杂
*/
using System;
using System.Collections.Generic;
namespace MediatorPattern 
{
    /// <summary>
    /// 抽象中介者类
    /// 定义了同事对象到中介者对象的接口
    /// </summary>
    abstract class Mediator 
    {
        public abstract void Send(string message);
    }

    /// <summary>
    /// 抽象同事类
    /// 持有中介者对象
    /// </summary>
    abstract class Colleague 
    {
        protected Mediator m_Mediator;

        public Colleague(Mediator mediator)
        {
            m_Mediator = mediator;
        }
    }

    /// <summary>
    /// 具体中介者类
    /// 它需要知道所有具体同事类，并从具体同事接收消息，向具体同事对象发出命令
    /// </summary>
    class ConcreteMediator : Mediator
    {
        public ConcreteColleague1 colleague1 { get; set; }
        public ConcreteColleague2 colleague2 { get; set; }

        /// <summary>
        /// 从具体同事接收消息，向具体同事对象发出通知
        /// </summary>
        public override void Send(string message)
        {
            if(message == "通知colleague1")
            {
                colleague1.Notify(message);
            }
            else if(message == "通知colleague2")
            {
                colleague2.Notify(message);
            }
        }
    }

    /// <summary>
    /// 具体同事类
    /// 每个具体同事类只知道自己的行为，而不了解其他同事类的情况，但它们却都认识中介者对象
    /// </summary>
    class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(Mediator mediator) : base(mediator) {}

        public void Send(string message)
        {
            m_Mediator.Send(message);  // 发送信息通常是通过中介者发送出去的
        }

        public void Notify(string message)
        {
            Console.WriteLine("ConcreteColleague1收到：" + message);
        }
    }

    /// <summary>
    /// 具体同事类
    /// 每个具体同事类只知道自己的行为，而不了解其他同事类的情况，但它们却都认识中介者对象
    /// </summary>
    class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator) : base(mediator) {}

        public void Send(string message)
        {
            m_Mediator.Send(message);
        }

        public void Notify(string message)
        {
            Console.WriteLine("ConcreteColleague2收到：" + message);
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            ConcreteMediator mediator = new ConcreteMediator();
            ConcreteColleague1 colleague1 = new ConcreteColleague1(mediator);  // 让具体同事类认识中介者对象
            ConcreteColleague2 colleague2 = new ConcreteColleague2(mediator);  // 让具体同事类认识中介者对象

            // 让中介者对象认识各个具体同事类
            mediator.colleague1 = colleague1;
            mediator.colleague2 = colleague2;

            // 具体同事类发送信息都是通过中介者转发
            colleague1.Send("通知colleague2");
            colleague2.Send("通知colleague1");
        }
    }
}