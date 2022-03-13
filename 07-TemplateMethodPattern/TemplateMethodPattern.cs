/*
模板方法模式[TemplateMethodPattern]

定义：
在一个方法中定义一个算法的骨架，而将一些步骤延迟到子类中。模板方法使得子类可以在不改变算法结构的情况下，重新定义算法中的某些步骤。
当我们要完成一个过程或一系列步骤，但其个别步骤在更详细的层次上的实现可能不同时，通常考虑用模板方法模式来处理

模板方法模式是通过把不变行为搬到超类，去除子类中的重复代码来体现它的优势，提供了一个很好的代码复用平台。

在模板方法中还可以使用钩子，钩子可以让子类实现算法中可选的部分，或是让子类能够有机会对模板方法中某些即将发生（或刚刚发生）的步骤作出反应

实际的例子：
C#中的Array.Sort方法可以对所有实现IComparable接口的类型的对象数组进行排序。Sort就是一个模板方法，它已经定义了对象数组的排序步骤，但其中如何比较两个对象的大小
是由IComparable接口的CompareTo决定的。虽然没有子类继承，但是思想就是模板方法模式的思想，不用拘泥于固有的定义
*/
using System;
namespace TemplateMethodPattern 
{
    /// <summary>
    /// 咖啡因类的饮料，比如有茶和咖啡
    /// </summary>
    public abstract class CoffeineBeverage 
    {
        /// <summary>
        /// 模板方法，用作算法模板，给出了逻辑的骨架，而逻辑的组成是一些相应的操作，其中的抽象操作推迟到子类实现
        /// 比如这里定义了咖啡因类饮料的制作过程
        /// 1. 把水煮沸
        /// 2. 冲泡
        /// 3. 把饮料倒进杯子
        /// 4. 添加调料
        /// 其中1和3是通用步骤，直接在超类中实现，这样重复的代码就都上升到父类中，将代码复用最大化
        /// 2和3不同饮料的冲泡与添加的调料是不同的由子类实现
        /// 
        /// 注意还有一个NeedCondiments方法，决定了要不要加调料，可以被子类覆写。可以称为“钩子”，让子类有能力对算法的不同点进行挂钩，要不要挂钩由子类决定
        /// </summary>
        public void PrepareRecipe()
        {
            BoilWater();

            Brew();

            PourInCup();

            if(NeedCondiments())
            {
                AddCondiments();
            }
        }

        protected abstract void Brew();  // 冲泡

        protected abstract void AddCondiments();  // 添加调料

        /// <summary>
        /// 把水煮沸
        /// </summary>
        void BoilWater()
        {
            Console.WriteLine("把水煮沸...");
        }

        /// <summary>
        /// 把饮料倒进杯子
        /// </summary>
        void PourInCup()
        {
            Console.WriteLine("把饮料倒进杯子...");
        }

        /// <summary>
        /// 钩子，子类可以覆写该方法得知已经把饮料倒进杯子里了，该决定要不要添加调料了
        /// 并可以通过返回值决定要不要添加调料
        /// </summary>
        protected virtual bool NeedCondiments()
        {
            return true;
        }
    }

    public class GreenTea : CoffeineBeverage
    {
        protected override void Brew()
        {
            Console.WriteLine("泡茶叶...");
        }

        protected override void AddCondiments()
        {
            // 空方法，什么也不做，因为不用加调料
        }

        /// <summary>
        /// 这里复写NeedCondiments，不需要加调料
        /// </summary>
        protected override bool NeedCondiments()
        {
            return false;
        }
    }

    public class Coffee : CoffeineBeverage
    {
        protected override void Brew()
        {
            Console.WriteLine("冲咖啡...");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("添加牛奶和糖...");
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            Console.WriteLine("开始制作绿茶......");
            CoffeineBeverage beverage = new GreenTea();
            beverage.PrepareRecipe();

            Console.WriteLine("开始制作咖啡......");
            CoffeineBeverage beverage2 = new Coffee();
            beverage2.PrepareRecipe();
        }
    }
}