/*
原型模式[PrototypePattern]

定义：
用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象
原型模式其实就是通过拷贝的方式从一个对象再创建另外一个可定制的对象，而且不需知道任何创建的细节。拷贝可以是浅拷贝也可以是深拷贝
原型类一般都有一个克隆自身的接口

一般在初始化的信息不发生变化的情况下，克隆是最好的办法，这既隐藏了对象创建的细节，又对性能是大大的提高
（内存级别直接复制对象，不需要走重新创建对象的过程，不需要重新初始化对象。在某些情况下，复制对象比创建新对象更有效。比如构造函数的执行时间很长）

原型模式的缺点：对象的复制有时相当复杂
*/
using System;
namespace PrototypePattern 
{
    public class Resume : ICloneable
    {
        private string name;
        private int age;

        public Resume(string name)
        {
            Console.WriteLine("构造函数：" + name);
            this.name = name;
        }

        public void SetAge(int age)
        {
            this.age = age;
        }

        public void Display()
        {
            Console.WriteLine("姓名：" + name + ", 年龄：" + age);
        }

        /// <summary>
        /// 原型模式的关键就在于这个拷贝方法
        /// 内部是调用了MemberwiseClone，它仅仅是在内存中创建一个新对象，然后将原有对象的所有字段复制过去。与new这种调用构造函数构建对象的过程完全不同
        /// 如果字段是值类型，则复制该值类型，如果是引用类型，则复制引用但不复制引用的对象（浅拷贝）
        /// 利用MemberwiseClone创建新对象的时候，
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            Resume resume = new Resume("iwiniwin");  // 构造函数：iwiniwin
            resume.SetAge(16);
            
            Resume resumeCopy = (Resume)resume.Clone();  // 此处不会触发构造函数的调用
            resumeCopy.SetAge(18);

            resume.Display();  // 姓名：iwiniwin, 年龄：16
            resumeCopy.Display();  // 姓名：iwiniwin, 年龄：18
        }
    }
}