/*
适配器模式[AdapterPattern]

定义：
适配器模式讲一个类的接口，转换成客户期望的另一个接口。适配器模式使得原本由于接口不兼容而不能一起工作的那些类可以一起工作

通过适配器客户代码可以统一调用同一接口，客户和被适配者是解耦的，一个不知道另一个
在双方都不太容易修改的时候再使用适配器模式适配

客户使用适配的过程如下：
1. 客户通过目标接口调用适配器的方法对适配器发出请求
2. 适配器使用被适配者接口把请求转换成被适配者的一个或多个调用接口
3. 客户接收到调用的结果，但并未察觉这一切是适配器在起转换作用

有两种适配器：对象适配器和类适配器，类适配器是通过多重继承实现的，但C#不支持多继承，所以就不列出具体代码了
下面的是对象适配器示例代码
*/
using System;
using System.Collections.Generic;
namespace AdapterPattern 
{

    /// <summary>
    /// 鸭子接口
    /// </summary>
    public interface Duck
    {
        /// <summary>
        /// 呱呱叫
        /// </summary>
        public void Quack();
    }

    public class MallardDuck : Duck
    {
        public void Quack()
        {
            Console.WriteLine("呱呱叫");
        }
    }

    /// <summary>
    /// 火鸡类
    /// 不会呱呱叫，只会咯咯叫
    /// </summary>
    public class Turkey
    {
        /// <summary>
        /// 咯咯叫
        /// </summary>
        public void Gobble()
        {
            Console.WriteLine("呱呱叫");
        }
    }

    /// <summary>
    /// 火鸡适配器
    /// 实现了鸭子接口，可以使火鸡对象冒充鸭子
    /// 通过在内部包装一个火鸡对象，把咯咯叫接口转换成目标的呱呱叫接口
    /// </summary>
    public class TurkeyAdapter : Duck
    {
        Turkey m_Turkey;

        public TurkeyAdapter(Turkey turkey)
        {
            // 取得要适配对象的引用
            m_Turkey = turkey;
        }

        public void Quack()
        {
            m_Turkey.Gobble();
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            Duck duck = new MallardDuck();
            duck.Quack();

            Turkey turkey = new Turkey();
            // 将火鸡包装进一个火鸡适配器中，使它看起来像是一只鸭子
            Duck duck2 = new TurkeyAdapter(turkey);
            duck2.Quack();
        }
    }
}