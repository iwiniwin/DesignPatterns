/*
策略模式[StrategyPattern]

定义：
定义了算法家族，分别封装起来，让它们之间可以互相替换，此模式让算法的变化，不会影响到使用算法的客户。
通过策略模式可以以相同的方式调用所有的算法，减少了算法类与使用算法类之间的耦合
策略模式另一个优点是简化了单元测试，因为每个算法都有自己的类，可以通过自己的接口单独测试，修改其中一个也不会影响其他的算法
具体实现是
    1. 先申明一个Strategy基类或接口，定义所有支持的算法的公共接口
    2. 再定义多个具体的策略类，封装了具体的算法或行为，继承于Strategy基类
    3. 声明一个Context上下文，维护一个对Strategy的引用。起到一个委托的作用，对算法的调用都是通过它执行的
    4. 对算法的调用统一通过Context的ContentInterface接口，ContentInterface接口内部通过引用的策略类执行对应的算法
对于整个算法家族的调用，都是通过Context的ContentInterface接口，对Strategy类及其子类都是无感知的，因此算法的变化不会影响到使用算法的客户

策略模式封装可互换的行为，然后使用委托来决定要采用哪一个行为

策略模式与简单工厂模式的区别：
策略模式由于定义了算法家族，每个算法类的实例化工作可能会利用简单工厂模式。但策略模式的核心是封装算法的变化，让客户对算法类，算法类工厂都无感知。从始至终都只认识上下文类就可以了
*/
using System;
namespace StrategyPattern
{
    /// <summary>
    /// 现金收取超类
    /// 定义了现金收取算法的接口，参数为原价，返回的是当前价
    /// </summary>
    abstract class CashSuper
    {
        public abstract double accpetCash(double money);
    }

    /// <summary>
    /// 折扣收费 实现了具体收费策略的类
    /// </summary>
    class CashRebate : CashSuper
    {
        private double m_Rebate;  // 折扣
        public CashRebate(double rebate)
        {
            m_Rebate = rebate;
        }

        public override double accpetCash(double money)
        {
            return m_Rebate * money;
        }
    }

    /// <summary>
    /// 返利收费 实现了具体收费策略的类
    /// 若大于moneyCondition返利条件，就减去moneyReturn返利值。满多少减多少
    /// </summary>
    class CashReturn : CashSuper
    {
        private double m_MoneyCondition = 0.0d;
        private double m_MoneyReturn = 1.0d;
        public CashReturn(double moneyCondition, double moneyReturn)
        {
            m_MoneyCondition = moneyCondition;
            m_MoneyReturn = moneyReturn;
        }

        public override double accpetCash(double money)
        {
            double result = money;
            if(money >= m_MoneyCondition)
            {
                result = money - Math.Floor(money / m_MoneyCondition) * m_MoneyReturn;
            }
            return result;
        }
    }

    /// <summary>
    /// 现金收费上下文
    /// </summary>
    class CashContext
    {
        private CashSuper m_CashSuper;  // 引用策略类

        /// <summary>
        /// 根据传入的收费类型，实例化对应的策略类。是简单工厂模式的应用
        /// </summary>
        /// <param name="type">收费类型</param>
        public CashContext(string type)
        {
            switch(type)
            {
                case "打8折":
                    m_CashSuper = new CashRebate(0.8);
                    break;
                case "满300减100":
                    m_CashSuper = new CashReturn(300, 100);
                    break;
            }
        }

        public double GetResult(double money)
        {
            return m_CashSuper.accpetCash(money);
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            // 客户在调用不同的收费策略时，只认识CashContext类，也只调用了CashContext.GetResult方法。对策略类CashSuper完全是无耦合的，也不关心策略的具体实现细节，策略的变化自然也不会影响到客户
            CashContext context = new CashContext("打8折");
            Console.WriteLine(context.GetResult(100));  // 80
        }
    }
}