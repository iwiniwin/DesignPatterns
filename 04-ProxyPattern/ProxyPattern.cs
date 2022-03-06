/*
代理模式[ProxyPattern]

定义：
为其他对象提供一种代理以控制对这个对象的访问。使用代理模式创建代表对象，让代表对象控制对某对象的访问，被代理的对象可以是远程的对象、创建开销大的对象或需要安全控制的对象

实现：
1. 以RealSubject表示被代表的真实实体，Proxy表示代表类，Proxy和RealSubject都要实现Subject接口，这样就在任何使用RealSubject的地方都可以使用Proxy
2. Proxy持有Subject的引用，所以必要时它可以把请求转发给Subject

代理模式的几种应用：
1. 远程代理，也就是为一个对象在不同的地址空间提供局部代表，这样可以隐藏一个对象存在于不同地址空间的事实
比如远程方法调用，调用本地代理对象的方法，会被代理利用网络转发到远程执行，并且结果会通过网络返回给代理，再由代理将结果转给客户
2. 虚拟代理，是根据需要创建开销很大的对象。通过它来存放实例化需要很长时间的真实对象
比如打开一张很大的图片时，可以通过代理替代真实的图片（代理存储了图片的真实信息，比如路径、尺寸），先显示“加载中”含义的默认图片，同时代理开启真实图片的加载，等到加载完成时再显示真实图片
3. 安全代理，用来控制真实对象访问时的权限
比如当代理发现被转发的请求是客户无权限的或非法的时候，可以直接中断或抛出异常
4. 智能指引，是当调用真实的对象时，代理处理另外一些事。
比如计数器，计算真实对象的引用次数。当对象没有引用时可以自动释放它
*/
using System;
namespace ProxyPattern 
{
    public class Girl {
        public string name { get; set; }
    }

    /// <summary>
    /// 送礼物接口，相当于Subject接口，RealSubject和Proxy都要实现它
    /// </summary>
    public interface IGiveGift
    {
        void GiveFlowers();
    }

    /// <summary>
    /// 追求者，相当于RealSubject
    /// </summary>
    public class Pursuit : IGiveGift
    {
        Girl m_Girl;

        public Pursuit(Girl girl)
        {
            m_Girl = girl;
        }

        /// <summary>
        /// 给一个菇凉送花
        /// </summary>
        public void GiveFlowers()
        {
            Console.WriteLine("Pursuit送花给" + m_Girl.name);
        }
    }

    /// <summary>
    /// 追求者的朋友，相当于Proxy
    /// </summary>
    public class Friend : IGiveGift
    {
        Pursuit m_Pursuit;
        public Friend(Girl girl)
        {
            m_Pursuit = new Pursuit(girl);
        }

        public void GiveFlowers()
        {
            m_Pursuit.GiveFlowers();  // 把请求转发给真正的追求者
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            Girl girl = new Girl{name = "girl"};
            // 真正的追求者不好意思露面，找了他的朋友帮忙送花
            Friend Friend = new Friend(girl);
            // 送花，看起来是追求者的朋友送的，实际是追求者送的
            Friend.GiveFlowers();  // Pursuit送花给girl
        }
    }
}