/*
抽象工厂模式[AbstractFactoryPattern]

定义：
提供一个创建一系列相关或相互依赖对象的接口，而无需指定它们具体的类

抽象工厂允许客户使用抽象的接口来创建一组相关的产品，而不需要知道实际产出的具体产品是什么。这样依赖，客户就从具体的产品中被解耦
抽象工厂为产品家族提供接口
通过改变具体工厂即可使用不同的产品配置

工厂方法模式和抽象工厂模式的区别：
工厂方法模式创建对象的方式是继承，通过子类来创建对象。抽象工厂是利用组合来创建对象，通过将具体的工厂传递给客户来使用
工厂方法潜伏在抽象工厂里，抽象工厂的任务是定义一个负责创建一组产品的接口。这个接口内的每个方法都负责创建一个具体产品，而每个方法都被声明成抽象，然后子类实现这些方法来创建对象，这正是工厂方法
*/
using System;
using System.Collections.Generic;
namespace AbstractFactoryPattern 
{
    /// <summary>
    /// 原料工厂接口
    /// 这个工厂负责创建原料家族中的每一种原料
    /// 抽象工厂为原料家族提供接口，制作披萨需要的东西，例如面团，酱料和芝士
    /// </summary>
    public interface PizzaIngredientFactory
    {
        public string CreateDough();  // 面团
        public string CreateSauce();  // 酱料
        public string CreateCheese();  // 芝士
    }

    /// <summary>
    /// 纽约原料工厂，具体工厂，实现不同的产品家族
    /// 对于原料家族的每一种原料，提供了纽约的版本
    /// </summary>
    public class NYPizzaIngredientFactory : PizzaIngredientFactory
    {
        public string CreateDough()
        {
            return "薄饼";
        }

        public string CreateSauce()
        {
            return "番茄酱料";
        }

        public string CreateCheese()
        {
            return "意大利白干酪";
        }
    }

    /// <summary>
    /// 芝加哥原料工厂，具体工厂
    /// 对于原料家族的每一种原料，提供了芝加哥的版本
    /// </summary>
    public class ChicagoPizzaIngredientFactory : PizzaIngredientFactory
    {
        public string CreateDough()
        {
            return "薄饼";
        }

        public string CreateSauce()
        {
            return "大蒜番茄酱料";
        }

        public string CreateCheese()
        {
            return "Reggiano干酪";
        }
    }

    /// <summary>
    /// 披萨
    /// 要制作披萨，需要工厂提供原料
    /// </summary>
    public class Pizza
    {
        string m_Name;
        PizzaIngredientFactory m_IngredientFactory1;

        /// <summary>
        /// 将工厂传递给披萨，以便披萨能从工厂取得原料
        /// 代码从实际的工厂解耦，以便在不同上下文中实现各式各样的工厂，制造出各种不同的产品
        /// </summary>
        public Pizza(string name, PizzaIngredientFactory ingredientFactory)
        {
            m_Name = name;
            m_IngredientFactory1 = ingredientFactory;
        }
        
        public void Prepare()
        {
            Console.WriteLine("准备 " + m_Name);
            string dough = m_IngredientFactory1.CreateDough();
            string sauce = m_IngredientFactory1.CreateSauce();
            string cheese = m_IngredientFactory1.CreateCheese();
            Console.WriteLine("从工厂取得的原料：" + dough + " " + sauce + " " + cheese);
        }

        public void Bake()
        {
            Console.WriteLine("烘焙");
        }

        public void Box()
        {
            Console.WriteLine("装盒");
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            PizzaIngredientFactory ingredientFactory = new NYPizzaIngredientFactory();
            Pizza pizza = new Pizza("采用纽约工厂原料的披萨", ingredientFactory);
            pizza.Prepare();
            pizza.Bake();
            pizza.Box();

            PizzaIngredientFactory ingredientFactory2 = new ChicagoPizzaIngredientFactory();
            Pizza pizza2 = new Pizza("采用芝加哥工厂原料的披萨", ingredientFactory2);
            pizza2.Prepare();
            pizza2.Bake();
            pizza2.Box();
        }
    }
}