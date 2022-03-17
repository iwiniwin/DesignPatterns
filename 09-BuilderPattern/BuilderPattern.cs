/*
建造者模式[BuilderPattern]

定义：
又叫生成器模式，将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示

允许对象通过多个步骤来创建，并且可以改变过程（这和只有一个步骤的工厂模式不同）
向客户隐藏产品内部的表现。产品的实现可以被替换，因为客户只看到一个抽象的接口

客户看到的只是一组可以构建产品的抽象接口，具体如何构建以及产品内部如何实现，都是向客户隐藏的

建造者模式是在当创建复杂对象的算法应该独立于该对象的组成部分以及它们的装配方式时适用的模式
*/
using System;
using System.Collections.Generic;
namespace BuilderPattern 
{
    /// <summary>
    /// 产品类，由多个部件组成
    /// </summary>
    public class Product
    {
        List<string> parts = new List<string>();

        /// <summary>
        /// 添加产品部件
        /// </summary>
        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {
            string str = "";
            foreach(var part in parts) 
            {
                str += (part + " ");
            }
            Console.WriteLine("产品组成部件：" + str);
        }
    }

    /// <summary>
    /// 抽象建造者类，确定产品由两个部件PartA和PartB组成
    /// 利用BuildPartA和BuildPartB向客户隐藏产品内部的表现
    /// 客户只会看到BuildPartA这个抽象接口，具体部件A是如何被创建的，是向客户隐藏的（比如PartA是怎样被装配到产品上的这样的细节，客户是不知道的）
    /// 甚至使用指挥者指挥创建过程，连BuildPartA这个接口客户也是看不到的
    /// </summary>
    public abstract class ProductBuilder
    {
        public abstract void BuildPartA(); 
        public abstract void BuildPartB();
        public abstract Product GetProduct();
    }

    /// <summary>
    /// 具体生成器创建真正的产品
    /// </summary>
    public class ConcreteBuilder1 : ProductBuilder
    {
        private Product product = new Product();

        /// <summary>
        /// 建造和装配具体的两个部件
        /// </summary>

        public override void BuildPartA()
        {
            product.Add("大型部件");
        }

        public override void BuildPartB()
        {
            product.Add("小型部件");
        }

        public override Product GetProduct()
        {
            return product;
        }
    }

    public class ConcreteBuilder2 : ProductBuilder
    {
        private Product product = new Product();

        /// <summary>
        /// 建造和装备具体的两个部件
        /// </summary>

        public override void BuildPartA()
        {
            product.Add("中型部件");
        }

        public override void BuildPartB()
        {
            product.Add("小型部件");
        }

        public override Product GetProduct()
        {
            return product;
        }
    }

    /// <summary>
    /// 指挥者类，用来指挥建造过程
    /// 通过传入不同的建造者，使得同样的构建过程可以创建不同的产品
    /// 也可以不使用指挥者，由客户直接控制产品的构建步骤
    /// </summary>
    public class Director
    {
        public void Construct(ProductBuilder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            Director director = new Director();

            ConcreteBuilder1 builder1 = new ConcreteBuilder1();
            director.Construct(builder1);
            builder1.GetProduct().Show();

            ConcreteBuilder2 builder2 = new ConcreteBuilder2();
            director.Construct(builder2);
            builder2.GetProduct().Show();

            // 或者不使用指挥者，由客户直接指示生成器依照一定步骤生成产品
            // 此时隐藏的是产品内部的表现，而不是产品构建步骤
            ConcreteBuilder1 builder1_1 = new ConcreteBuilder1();
            builder1_1.BuildPartB();
            builder1_1.BuildPartA();
            builder1_1.GetProduct().Show();
        }
    }
}