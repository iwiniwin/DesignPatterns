/*
工厂方法模式[FactoryMethodPattern]

定义：
定义了一个创建对象的接口，但由子类决定要实例化的类时哪一个。工厂方法让类把实例化推迟到子类
是一个抽象方法起到了工厂的作用来封装具体类型的实例化，所以得名工厂方法模式
工厂方法让子类决定要实例化的类是哪一个，在编写创建者类时，不需要知道实际创建的产品是哪一个。选择了使用哪个子类，自然就决定了实际创建的产品是什么

工厂方法模式与简单工厂模式的区别：
本质上工厂方法模式是创建了一个框架，对实例化出来的类的一些处理都可以被封装在创建者类型内部，让子类决定如何要实现，同时又保持了一定的弹性。而不是像简单工厂模式直接把实例化工作交给工厂类
简单工厂模式实际是违背了开放-封闭原则的，每当增加一个新的实例化类型时，都需要去修改工厂类的分支条件。
工厂方法模式是对简单工厂模式的进一步抽象和推广，克服了简单工厂违背开放-封闭原则的缺点，又保持了封装对象创建过程的优点。当需要增加一个新的实例化类型时，只需要增加对应的类型和增加创建这个类型的子类即可。对于整个创建者类型和实例化类型体系没有修改的变化
当增加一个新的实例化类型时，简单工厂模式除了要增加的地方外，还需要修改工厂类的分支代码，和客户调用处的代码（传递新的类型参数以创建新类型），共2处地方
当增加一个新的实例化类型时，工厂方法模式除了要增加的地方外，只需要修改客户调用处的代码（new一个创建这个类型的子类来创建新类型），仅1处地方
*/
using System;
namespace FactoryMethodPattern 
{
    /// <summary>
    /// 披萨，抽象类
    /// </summary>
    public abstract class Pizza 
    {
        public abstract string name { get; }
        /// <summary>
        /// 烘焙
        /// </summary>
        public void Bake()
        {

        }
        /// <summary>
        /// 装盒
        /// </summary>
        public void Box()
        {
            Console.WriteLine("装盒：" + name);
        }
    }

    /// <summary>
    /// 披萨店，由此订购披萨
    /// 定义了订购披萨的框架，创建披萨、烘焙、装盒
    /// </summary>
    public abstract class PizzaStore
    {
        public Pizza OrderPizza(string type)
        {
            Pizza pizza = CreatePizza(type);
            pizza.Bake();
            pizza.Box();
            return pizza;
        }
        protected abstract Pizza CreatePizza(string type);  // 实例化的责任被移到一个“方法”中，此方法就如同是一个工厂。所以叫工厂方法模式
    }

    public class NYPizzaStore : PizzaStore
    {
        protected override Pizza CreatePizza(string type)  // 子类决定具体如何创建产品
        {
            if(type.Equals("cheese"))
            {
                return new NYStyleCheesePizza();
            }
            else if(type.Equals("veggie"))
            {
                return new NYStyleVeggiePizza();
            }
            return null;
        } 
    }

    public class NYStyleCheesePizza : Pizza
    {
        public override string name => "纽约芝士披萨";
    }

    public class NYStyleVeggiePizza : Pizza
    {
        public override string name => "纽约蔬菜披萨";
    }

    public class ChicagoPizzaStore : PizzaStore
    {
        protected override Pizza CreatePizza(string type)  // 子类决定具体如何创建产品
        {
            if(type.Equals("cheese"))
            {
                return new ChicagoStyleCheesePizza();
            }
            else if(type.Equals("veggie"))
            {
                return new ChicagoStyleVeggiePizza();
            }
            return null;
        } 
    }

    public class ChicagoStyleCheesePizza : Pizza
    {
        public override string name => "芝加哥芝士披萨";
    }

    public class ChicagoStyleVeggiePizza : Pizza
    {
        public override string name => "芝加哥蔬菜披萨";
    }

    public class Demo 
    {
        public static void Test()
        {
            PizzaStore nyStore = new NYPizzaStore();  // 创建纽约披萨店
            // 通过纽约店订购芝士披萨
            nyStore.OrderPizza("cheese");  // 装盒：纽约芝士披萨

            PizzaStore chicagoStore = new ChicagoPizzaStore();  
            // 通过芝加哥店订购蔬菜披萨
            chicagoStore.OrderPizza("veggie");  // 装盒：芝加哥蔬菜披萨

            // 对于工厂方法模式而言，如果要新增 北京杂酱披萨的制作
            // 1. 【创建】北京杂酱披萨类型 2. 【创建】北京披萨店  3. 【修改】这里的代码，new一个北京披萨店，然后订购

            // 对于简单工厂模式而言，如果要新增 北京杂酱披萨的制作
            // 1. 【创建】北京杂酱披萨类型 2. 【修改】工厂类的分支代码，支持创建北京杂酱披萨 3. 【修改】这里的代码，调用创建方法时，传入新的北京杂酱披萨类型
        }
    }
}