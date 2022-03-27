/*
迭代器模式[IteratorPattern]

定义：
提供一种方法顺序访问一个聚合对象中的各个元素，而又不暴露其内部的表示

迭代器模式把在元素之间游走责任交给迭代器，而不是聚合对象。这不仅让聚合的接口和实现变得更简洁，也可以让聚合更专注在它所应该专注的事情上面（也就是管理对象集合），而不必去理会遍历的事情
迭代器模式就是分离了集合对象的遍历行为，抽象出一个迭代器类来负责，这样既可以做到不暴露集合的内部结构，又可以让外部代码透明地访问集合内部的数据
*/
using System;
using System.Collections.Generic;
namespace IteratorPattern 
{

    /// <summary>
    /// 迭代器接口
    /// 等价于C#原生IEnumerator接口的作用
    /// </summary>
    interface Iterator 
    {
        object Next();  // 得到下一个对象
        bool IsDone();  // 是否到结尾
        object CurrentItem();  // 当前对象
    }

    /// <summary>
    /// 聚集抽象类
    /// 定义所有的聚集都有一个创建迭代器的接口，将客户代码从集合对象的实现解耦了
    /// 等价于C#原生IEnumerable接口的作用
    /// </summary>
    abstract class Aggregate 
    {
        public abstract Iterator CreateIterator();
    }

    /// <summary>
    /// 具体迭代器类
    /// 针对一种特定聚集ConcreteAggregate，负责它的遍历
    /// </summary>
    class ConcreteIterator : Iterator
    {
        // 具体的聚集对象
        private ConcreteAggregate m_Aggregate; 
        private int m_Current = 0;

        /// <summary>
        /// 初始化时将具体的聚集对象传入
        /// </summary>
        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            m_Aggregate = aggregate;
        }

        public object Next()
        {
            object ret = null;
            m_Current ++;
            if(m_Current < m_Aggregate.count)
            {
                ret = m_Aggregate[m_Current];
            }
            return ret;
        }

        public bool IsDone()
        {
            return m_Current >= m_Aggregate.count ? true : false;
        }

        public object CurrentItem()
        {
            return m_Aggregate[m_Current];
        }
    }

    /// <summary>
    /// 具体聚集类
    /// 需要负责实例化一个具体迭代器，此迭代器能够遍历对象集合
    /// </summary>
    class ConcreteAggregate : Aggregate
    {
        // 对象集合
        private IList<object> m_Items = new List<object>();

        public override Iterator CreateIterator()
        {
            // 返回对应的迭代器
            return new ConcreteIterator(this);
        }

        public int count 
        {
            get
            {
                return m_Items.Count;
            }
        }

        public object this[int index]
        {
            get
            {
                return m_Items[index];
            }
            set
            {
                m_Items.Insert(index, value);
            }
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            ConcreteAggregate aggregate = new ConcreteAggregate();
            aggregate[0] = "AA";
            aggregate[1] = "BB";
            aggregate[2] = "CC";
            aggregate[3] = "DD";

            Iterator iterator = aggregate.CreateIterator();
            while(!iterator.IsDone())
            {
                Console.WriteLine(iterator.CurrentItem());
                iterator.Next();
            }

            // C#原生迭代器示例

            List<string> list = new List<string>(){"11", "22", "33", "44"};
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            // foreach的背后，是编译器利用迭代器进行的遍历
            // 等价于如下代码
            IEnumerator<string> enumerator = list.GetEnumerator();
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
    }
}