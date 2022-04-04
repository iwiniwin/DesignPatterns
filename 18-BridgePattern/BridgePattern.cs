/*
桥接模式[BridgePattern]

定义：
将抽象部分与它的实现部分分离，使它们都可以独立地变化

抽象的实现方式可能有多种，桥接的核心意图就是把这些实现独立出来，让它们各自地变化。这就使得每种实现的变化不会影响其他实现，从而达到应对变化的目的
实现系统可能有多角度分类，每一种分类都有可能发生变化，那么就把这种多角度分离出来让它们独立变化，减少它们之间的耦合
桥接模式不仅改变了实现，也改变了抽象
*/
using System;
using System.Collections.Generic;
namespace BridgePattern 
{

    /// <summary>
    /// 手机软件
    /// </summary>
    abstract class HandsetSoft
    {
        public abstract void Run();
    }

    class HandsetGame : HandsetSoft
    {
        public override void Run()
        {
            Console.WriteLine("运行手机游戏");
        }
    }

    class HandsetAddressList : HandsetSoft
    {
        public override void Run()
        {
            Console.WriteLine("运行手机通讯录");
        }
    }

    /// <summary>
    /// 手机品牌
    /// </summary>
    abstract class HandsetBrand
    {
        // 品牌需要关注软件，所以可在机器中安装软件以备运行
        public HandsetSoft soft { get; set; }

        public abstract void Run();
    }

    class Huawei : HandsetBrand
    {
        public override void Run()
        {
            soft.Run();
        }
    }

    class Xiaomi : HandsetBrand
    {
        public override void Run()
        {
            soft.Run();
        }
    }
    
    public class Demo 
    {
        public static void Test()
        {
            HandsetBrand handsetBrand;
            handsetBrand = new Huawei();
            handsetBrand.soft = new HandsetGame();
            handsetBrand.Run();

            handsetBrand = new Xiaomi();
            handsetBrand.soft = new HandsetAddressList();
            handsetBrand.Run();

            /*
            在这个例子中，手机品牌和手机软件之间是聚合关系，采用的是桥接模式
            当需要增加一个手机软件，比如MP3，或增加一个手机品牌，比如OPPO，时，都只需增加一个类即可，不会影响到其他已有类

            对手机可以按品牌分类也可以按软件功能分类，当不采用桥接模式，完全使用继承来实现时可能有如下的继承层次
            1. 
                                        手机品牌
                        Huawei                          Xiaomi
                Huawei游戏   Huawei通讯录        Xiaomi游戏   Xiaomi通讯录

            2.
                                        手机软件
                        游戏                              通讯录
                Huawei游戏   Xiaomi游戏       Huawei通讯录     Xiaomi通讯录 

            当需要增加一个手机软件，比如Mp3时，需要增加至少两个类，HuaweiMp3，XiaomiMp3
            当需要增加一个手机品牌，比如Oppo时，需要增加至少三个类，Oppo游戏，Oppo通讯录，OppoMp3

            主要的问题就是采用了继承这种紧密的依赖关系，当需要复用子类时，如果继承下来的实现不适合解决新的问题，则父类必须重写或被其他更合适的类替换
            */
        }
    }
}