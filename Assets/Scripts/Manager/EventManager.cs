using System;
using System.Collections.Generic;
using UnityEngine.Events;

public static class EventType
{
    public static class UI
    {
        public const string OpenVolumeSetting = "OpenVolumeSetting";
    }
}

public static class EventManager
{
    private static Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

    // 訂閱事件，支持無參數
    public static void Subscribe(string eventName, Action listener)
    {
        SubscribeEvent(eventName, listener);
    }

    // 訂閱事件，支持一個參數
    public static void Subscribe<T>(string eventName, Action<T> listener)
    {
        SubscribeEvent(eventName, listener);
    }

    // 訂閱事件，支持兩個參數
    public static void Subscribe<T1, T2>(string eventName, Action<T1, T2> listener)
    {
        SubscribeEvent(eventName, listener);
    }

    private static void SubscribeEvent(string eventName, Delegate listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] = Delegate.Combine(thisEvent, listener);
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    // 取消訂閱事件，支持無參數
    public static void Unsubscribe(string eventName, Action listener)
    {
        UnsubscribeEvent(eventName, listener);
    }

    // 取消訂閱事件，支持一個參數
    public static void Unsubscribe<T>(string eventName, Action<T> listener)
    {
        UnsubscribeEvent(eventName, listener);
    }

    // 取消訂閱事件，支持兩個參數
    public static void Unsubscribe<T1, T2>(string eventName, Action<T1, T2> listener)
    {
        UnsubscribeEvent(eventName, listener);
    }

    private static void UnsubscribeEvent(string eventName, Delegate listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] = Delegate.Remove(thisEvent, listener);
        }
    }

    // 觸發事件，無參數
    public static void Invoke(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            (thisEvent as Action)?.Invoke();
        }
    }

    // 觸發事件，一個參數
    public static void Invoke<T>(string eventName, T parameter)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            (thisEvent as Action<T>)?.Invoke(parameter);
        }
    }

    // 觸發事件，兩個參數
    public static void Invoke<T1, T2>(string eventName, T1 param1, T2 param2)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            (thisEvent as Action<T1, T2>)?.Invoke(param1, param2);
        }
    }
}
