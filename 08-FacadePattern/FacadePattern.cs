/*
外观模式[FacadePattern]

定义：
为子系统中的一组接口提供一个一致的界面，此模式定义了一个高层接口，这个接口使得这一子系统更加容易使用
之所以称呼为外观模式，是因为它将一个或数个类的复杂的一切都隐藏在背后，只显露出一个干净美好的外观
外观的意图是简化接口，但外观不只是简化了接口，也将客户从组件的子系统中解耦，遵守最少知识原则
*/
using System;
namespace FacadePattern 
{
    public class Stock1
    {
        public void Sell()
        {
            Console.WriteLine("股票1卖出");
        }
    }

    public class Stock2
    {
        public void Sell()
        {
            Console.WriteLine("股票2卖出");
        }
    }

    public class Stock3
    {
        public void Sell()
        {
            Console.WriteLine("股票3卖出");
        }
    }

    /// <summary>
    /// 基金经理人，外观类
    /// 它了解所有的子系统（这里指各种股票）的方法或属性，进行组合，以备外界调用
    /// </summary>
    public class FundManager
    {
        Stock1 stock1;
        Stock2 stock2;
        Stock3 stock3;

        public FundManager()
        {
            stock1 = new Stock1();
            stock2 = new Stock2();
            stock3 = new Stock3();
        }

        public void Sell()
        {
            stock1.Sell();
            stock2.Sell();
            stock3.Sell();
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            // 股票行情不好，需要卖出股票时，直接让基金经理人卖出即可
            FundManager manager = new FundManager();
            manager.Sell();

            // 对于客户而言，如果没有基金经理人提供的高层接口，想要卖出三只股票，需要依次调用三只股票的Sell方法
        }
    }
}