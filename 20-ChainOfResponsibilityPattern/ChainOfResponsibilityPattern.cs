/*
责任链模式[ChainOfResponsibilityPattern]

定义：
使多个对象都有机会处理请求，从而避免请求的发送者和接收者之间的耦合关系。将这些对象连成一条链，并沿着这条链传递该请求，直到有一个对象处理它为止。

链中的每个对象扮演处理器，并且有一个后继对象。如果它可以处理请求，就进行处理；否则把请求转发给后继者

接收者和发送者都没有对方的明确信息，且链中的对象自己也不知道链的结构。结果是职责链可简化对象的相互连接，它们仅需保持一个指向其后继者的引用，而不需保持它所有的候选接收者的引用

经常被使用在窗口系统中，处理鼠标和键盘之类的事件
并不保证请求一定会被处理；一个请求极有可能到了链的末端都得不到处理，或者因为没有正确配置而得不到处理。所以需要考虑全面
可能不容易观察运行时的特征，有碍于除错

优点：
1. 将请求的发送者和接收者解耦
2. 可以简化你的接收者对象，因为它不需要知道链的结构
3. 通过改变链内的成员或调动它们的次序，允许你动态地新增或者删除责任。因为请求的客户端并不知道这当中的哪一个对象最终处理请求
*/
using System;
using System.Collections.Generic;
namespace ChainOfResponsibilityPattern 
{
    /// <summary>
    /// 管理者类
    /// 请求的接收者或是处理者
    /// </summary>
    abstract class Manager
    {
        protected string m_Name;
        public Manager superior { get; set; }  // 管理者的上级

        public Manager(string name)
        {
            m_Name = name;
        }

        // 处理申请
        public abstract void HandleRequest(string request);
    }

    /// <summary>
    /// 经理类
    /// 具体处理者类，处理它所处理的请求。如果可处理该请求，就处理之，否则就该请求转发给它的后继者
    /// </summary>
    class CommonManager : Manager
    {
        public CommonManager(string name) : base(name) {}

        public override void HandleRequest(string request)
        {
            if(request == "请假")
            {
                Console.WriteLine("{0}批准：{1}", m_Name, request);
            }
            else
            {
                // 经理只能处理请假请求，其他请求转发给它的上级处理
                superior.HandleRequest(request);
            }
        }
    }

    /// <summary>
    /// 总监类
    /// 具体处理者类
    /// </summary>
    class Majordomo : Manager
    {
        public Majordomo(string name) : base(name) {}

        public override void HandleRequest(string request)
        {
            if(request == "加薪")
            {
                Console.WriteLine("{0}批准：{1}", m_Name, request);
            }
            else
            {
                // 总监只能处理加薪请求，其他请求转发给它的上级处理
                superior.HandleRequest(request);
            }
        }
    }

    /// <summary>
    /// 总经理类
    /// 具体处理者类
    /// </summary>
    class GeneralManager : Manager
    {
        public GeneralManager(string name) : base(name) {}

        public override void HandleRequest(string request)
        {
            // 总经理可以处理一切请求。这里简单处理就全批准了
            Console.WriteLine("{0}批准：{1}", m_Name, request);
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            CommonManager commonManager = new CommonManager("王经理");
            Majordomo majordomo = new Majordomo("李总监");
            GeneralManager generalManager = new GeneralManager("张总经理");

            commonManager.superior = majordomo;  // 设置经理的上级是总监
            majordomo.superior = generalManager;  // 设置总监的上级是总经理

            // 客户端的申请都是由经理发起，但实际谁来决策由具体管理类来处理，客户端不知道
            commonManager.HandleRequest("请假");
            commonManager.HandleRequest("加薪");
            commonManager.HandleRequest("升职");
        }
    }
}