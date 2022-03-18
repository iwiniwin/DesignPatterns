/*
观察者模式[ObserverPattern]

定义：
又叫做发布-订阅（Publish/Subscribe）模式，定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象。这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己

当一个对象的改变需要同时改变其他对象，，而且它不知道具体有多少对象有待改变时，应该考虑使用观察者模式。有助于维护相关对象间的一致性

主题和观察者之间是松耦合的，主题只知道观察者实现了观察者接口，不需要知道观察者具体是谁，做了些什么（利用C#的委托，主题连观察者接口都不要知道，只需要知道通知函数的声明即可）
任何时候都可以增加或删除观察者，主题不会受到任何影响

注意有多个观察者时，最好不要依赖特定的通知顺序
*/
using System;
using System.Collections.Generic;
namespace ObserverPattern 
{
    /// <summary>
    /// 主题接口
    /// </summary>
    public interface Subject 
    {
        public void RegisterObserver(Observer o);  // 添加观察者接口
        public void RemoveObserver(Observer o);  // 移除观察者接口
        public void NotifyObservers();  // 当主题状态改变时，这个方法会被调用以通知所有的观察者
    }

    /// <summary>
    /// 观察者接口
    /// 所有的观察者都实现此接口，这样，主题在需要通知观察者时可以调用这个共同的接口
    /// </summary>
    public interface Observer
    {
        public void Update(string info);
    }

    /// <summary>
    /// 气象站
    /// 可以发布天气信息
    /// </summary>
    public class WeatherStation : Subject
    {
        private List<Observer> m_Observers;
        public string info;
        public WeatherStation()
        {
            m_Observers = new List<Observer>();
        }

        public void RegisterObserver(Observer observer)
        {
            m_Observers.Add(observer);
        }

        public void RemoveObserver(Observer observer)
        {
            m_Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(var observer in m_Observers)
            {
                observer.Update(info);
            }
        }
    }

    /// <summary>
    /// 天气应用
    /// 可以用来订阅天气信息
    /// </summary>
    public class WeatherApp : Observer
    {
        public void Update(string info) {
            Console.WriteLine("天气情况：" + info);
        }
    }

    public class Demo 
    {
        public static void Test()
        {
            WeatherStation weatherStation = new WeatherStation();
            WeatherApp weatherApp = new WeatherApp();

            // 添加观察者，使天气App订阅天气信息
            weatherStation.RegisterObserver(weatherApp);
            // 设置天气信息
            weatherStation.info = "晴";
            // 通知观察者
            weatherStation.NotifyObservers();  // 天气情况：晴

            weatherStation.RemoveObserver(weatherApp);
            weatherStation.info = "雨";
            weatherStation.NotifyObservers();  // 由于前面已经移除了观察者weatherApp，所以weatherApp不会收到雨天的通知
        }
    }
}