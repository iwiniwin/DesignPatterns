/*
状态模式[StatePattern]

定义：
允许对象在内部状态改变时改变它的行为，对象看起来好像修改了它的类

状态模式的好处是将与特定状态相关的行为局部化（将每个状态的行为局部化到自己的类中），并且将不同状态的行为分割开来
状态模式允许一个对象基于内部状态而拥有不同的行为
有助于消除庞大的条件分支语句
状态模式通过把各种状态转移逻辑分布到State的子类之间，来减少相互间的依赖

每个状态类是"对修改关闭"的，状态类的持有者是"对扩展开放"的，因为可以加入新的状态类

当状态转换是固定的时候，就适合放在Context中，然而当转换是更动态的时候，通常就会放在状态类中，但缺点就是状态之间会产生依赖
*/
using System;
using System.Collections.Generic;
namespace StatePattern 
{

    /// <summary>
    /// 状态类
    /// 可以有多个方法，映射状态类可以执行的多个动作
    /// </summary>
    public abstract class State
    {
        public abstract void Handle(Work work);
    }

    /// <summary>
    /// 上午工作状态类
    /// </summary>
    public class ForenoonState : State
    {
        /// <summary>
        /// 在当前状态下，根据时间所作出的行为
        /// </summary>
        public override void Handle(Work work)
        {
            if(work.hour < 12)
            {
                Console.WriteLine("当前时间{0}点，上午工作，精神百倍", work.hour);
            }
            else
            {
                // 超过12点，转入中午工作状态
                work.SetState(new NoonState());  // 这里也可以不每次new，让Work持有NoonState实例，设置的时候直接使用work.noonState
                work.Program();
            }
        }
    }

    /// <summary>
    /// 中午工作状态类
    /// </summary>
    public class NoonState : State
    {
        /// <summary>
        /// 在当前状态下，根据时间所作出的行为
        /// </summary>
        public override void Handle(Work work)
        {
            if(work.hour < 13)
            {
                Console.WriteLine("当前时间{0}点，犯困，午休", work.hour);
            }
            else
            {
                // 超过13点转入下午工作状态 
                work.SetState(new AfternoonState());
                work.Program();
            }
        }
    }

    /// <summary>
    /// 下午工作状态类
    /// </summary>
    public class AfternoonState : State
    {
        public override void Handle(Work work)
        {
            if(work.hour < 17)
            {
                Console.WriteLine("当前时间{0}点，下午状态还不错，继续努力", work.hour);
            }
            else
            {
                Console.WriteLine("当前时间{0}点，下班回家了", work.hour);
            }
        }
    }

    public class Work 
    {
        private State m_Current;
        public int hour { get; set; }

        public Work()
        {
            m_Current = new ForenoonState();
        }

        /// <summary>
        /// 这个方法允许其他的对象将当前状态转换到不同的状态
        /// </summary>
        /// <param name="state"></param>
        public void SetState(State state)
        {
            m_Current = state;
        }

        /// <summary>
        /// 编程
        /// 用户调用这个方法来请求执行动作，内部会委托给状态来处理
        /// </summary>
        public void Program()
        {
            // 委托当前状态去做具体的动作，这样当内部状态改变时，行为也会改变
            m_Current.Handle(this);
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            Work work = new Work();
            work.hour = 9;
            work.Program();

            // 时间改变，会改变work内部状态，使work.Program的行为发生改变
            work.hour = 12;
            work.Program();

            work.hour = 14;
            work.Program();

            work.hour = 18;
            work.Program();
        }
    }
}