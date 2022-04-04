/*
命令模式[CommandPattern]

定义：
将一个请求封装为一个对象，从而使你可用不同的请求对其他对象进行参数化；对请求排队或记录请求日志，以及支持可撤销的操作

这里的参数化其他对象如何理解？（以服务员为调用者为例，参数化许多订单，服务员根本不在乎所拥有的命令是什么命令对象，只要该命令对象实现的Command接口即可）

命令模式可将“动作的请求者”从“动作的执行者”对象中解耦。利用命令对象，把请求封装成一个特定对象。之后就可以请命令对象做相关的工作，请求者并不需要知道具体的工作内容是什么，只要命令对象能把事情做好就可以了

宏命令：
可以制造一种新的命令，用来执行其他一堆命令，即宏命令
在宏命令中用命令数组存储一大堆命令，当这个宏命令被执行时，一次性执行数组里的每个命令

优点：
1. 能较容易地设计一个命令队列
2. 在需要的情况下，可以较容易地将命令记入日志
3. 允许接收请求的一方决定是否要否决请求
4. 可以容易地实现对请求的撤销和重做
5. 由于加进新的具体命令类不影响其他的类，因此增加新的具体命令类很容易
6. 命令模式把请求一个操作的对象与知道怎么执行一个操作的对象分割开
*/
using System;
using System.Collections.Generic;
namespace CommandPattern 
{
    /// <summary>
    /// 厨师
    /// 命令接收者
    /// </summary>
    public class Cook 
    {
        public void BakeMutton()
        {
            Console.WriteLine("烤羊肉串");
        }

        public void BakeChickenWing()
        {
            Console.WriteLine("烤鸡翅");
        }
    }
    
    /// <summary>
    /// 命令接口
    /// </summary>
    public interface Command 
    {
        void Execute();
        // 这里还可以定义Undo方法，用来实现撤销重做。比如Execute的动作是开灯，Undo的动作就是关灯
        // 这里还可以定义Store和Load方法，当命令被执行时，存储到硬盘，系统死机后，重新加载这些命令并以正确的次序执行
    }

    /// <summary>
    /// 具体命令，烤羊肉串
    /// m_Cook是接收者，它负责来执行具体的动作
    /// </summary>
    class BakeMuttonCommond : Command
    {
        private Cook m_Cook;
        public BakeMuttonCommond(Cook cook)
        {
            m_Cook = cook;
        }

        public void Execute()
        {
            // 接收者和动作在命令对象中被绑在一起
            m_Cook.BakeMutton();
        }
    }

    /// <summary>
    /// 具体命令，烤鸡翅
    /// </summary>
    class BakeChickenWingCommond : Command
    {
        private Cook m_Cook;
        public BakeChickenWingCommond(Cook cook)
        {
            m_Cook = cook;
        }

        public void Execute()
        {
            m_Cook.BakeChickenWing();
        }
    }

    /// <summary>
    /// 服务员
    /// 命令调用者，它和命令接收者（厨师）之间是解耦的
    /// </summary>
    class Waiter 
    {
        private List<Command> m_Commands = new List<Command>();

        public void SetOrder(Command command)
        {
            m_Commands.Add(command);
        }

        /// <summary>
        /// 通知开始制作
        /// </summary>
        public void Notify()
        {
            foreach (var command in m_Commands)
            {
                command.Execute();
            }
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            // 开店前的准备，找好厨师，服务员，和能制作的菜品
            Cook cook = new Cook();
            Command bakeMuttonCommand = new BakeMuttonCommond(cook);
            Command bakeChickenCommand = new BakeChickenWingCommond(cook);
            Waiter waiter = new Waiter();

            waiter.SetOrder(bakeMuttonCommand);
            waiter.SetOrder(bakeChickenCommand);
            waiter.Notify();
        }
    }
}