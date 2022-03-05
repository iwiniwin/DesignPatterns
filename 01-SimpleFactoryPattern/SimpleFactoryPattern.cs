/*
简单工厂模式

定义：
严格意义上来说不是一个真正的模式，但经常被用于封装创建对象的代码
到底要实例化谁，将来会不会增加实例化的对象，这是很容易变化的部分，应该考虑用一个单独的类来做这个创造实例的过程，这就是工厂
*/
using System;
namespace SimpleFactoryPattern
{
    /// <summary>
    /// 运算类
    /// </summary>
    class Operation
    {
        public double numberA { get; set; }
        public double numberB { get; set; }

        public virtual double GetResult()
        {
            return 0;
        }
    }

    /// <summary>
    /// 加法类，继承运算类
    /// </summary>
    class OperationAdd : Operation
    {
        public override double GetResult()
        {
            return numberA + numberB;
        }
    }

    /// <summary>
    /// 减法类，继承运算类
    /// </summary>
    class OperationSub : Operation
    {
        public override double GetResult()
        {
            return numberA - numberB;
        }
    }
    
    /// <summary>
    /// 简单运算工厂类
    /// 只需要输入运算符号，工厂实例化出合适的对象，通过多态，返回父类的方式实现了计算的结果
    /// 加减乘除等运算被分离，修改其中一个不影响另外的一个
    /// 如果以后想再增加乘法运算，只需要添加一个OperationMul乘法类，然后在工厂类的switch中增加分支
    /// </summary>
    class OperationFactory
    {
        public static Operation createOperate(string operate)
        {
            Operation oper = null;
            switch(operate)
            {
                case "+":
                    oper = new OperationAdd();
                    break;
                case "-":
                    oper = new OperationSub();
                    break;
            }
            return oper;
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            Operation oper;
            oper = OperationFactory.createOperate("+");
            oper.numberA = 3;
            oper.numberB = 4;
            Console.WriteLine(oper.GetResult());  // 7
        }
    }
}