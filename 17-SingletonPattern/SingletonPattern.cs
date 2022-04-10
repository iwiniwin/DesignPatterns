/*
单例模式[SingletonPattern]

定义：
保证一个类仅有一个实例，并提供一个访问它的全局访问点

让类自身负责保护它的唯一实例，这个类可以保证没有其他实例可以被创建，并且它可以提供一个访问该实例的方法
*/
using System;
using System.Collections.Generic;
namespace SingletonPattern 
{
    /// <summary>
    /// 懒汉式单例类【线程安全的】
    /// 要在第一次被引用时（即第一次调用GetInstance时），才会将自己实例化，所以被称为懒汉式单例类
    /// </summary>
    class Singleton1 
    {
        private static Singleton1 m_Instance;  // 声明一个静态的类变量

        // 程序运行时创建一个静态只读的进程辅助对象
        private static readonly object syncObj = new object();  // 这里再创建一个object来lock，而不是直接lock(m_Instance)，是因为加锁时，m_Instance可能都还没有被创建

        // 构造方法私有，外部代码不能直接new来实例化它
        private Singleton1()
        {

        }

        // 此方法是获得本类实例的唯一全局访问点
        public static Singleton1 GetInstance()
        {
            // 【双重检查锁定】
            if(m_Instance == null)  // 先判断实例是否存在，不存在再加锁处理，避免每次访问都加锁。【第一次检查】
            {
                lock(syncObj)  // 在同一时刻，加了锁的那部分代码程序只有一个线程可以进入
                {
                    // 这里再检查一次，是因为当m_Instance为null时，两个线程都可以通过第一次检查
                    // 然后由于lock机制，第一个线程先进入，第二个线程在外排队等待。如果此时没有第二次检查，则第一个线程创建了实例后，第二个线程进入时会再创建一个实例
                    if(m_Instance == null)  // 【第二次检查】
                    {
                        m_Instance = new Singleton1();
                    }
                }
            }
            return m_Instance;
        }
    }

    /// <summary>
    /// 饿汉式单例类【线程安全的】
    /// 采用了静态初始化的方式，在自己被加载时就将自己实例化，所以被形象地称为饿汉式单例类
    /// </summary>
    sealed class Singleton2  // sealed阻止派生
    {
        // 静态初始化，在第一次引用类的任何成员时创建实例。公共语言运行库负责处理变量初始化。可以保证在任何线程访问m_Instance前，一定先创建此实例
        private static readonly Singleton2 m_Instance = new Singleton2();

        // 构造方法私有，外部代码不能直接new来实例化它
        private Singleton2()
        {

        }
        
        // 此方法是获得本类实例的唯一全局访问点
        public static Singleton2 GetInstance()
        {
            return m_Instance;
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            // 两次获得的实例是相同的
            Console.WriteLine(Singleton1.GetInstance() == Singleton1.GetInstance());  // true

            // 两次获得的实例是相同的
            Console.WriteLine(Singleton2.GetInstance() == Singleton2.GetInstance());  // true
        }
    }
}