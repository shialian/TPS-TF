using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool<T> where T : Component
{
    public T Prefab;
    private Queue<T> prefabQueue;
    private readonly int maxPoolCount = int.MaxValue;

    public PrefabPool(T prefab)
    {
        if (prefab == null)
        { 
            Debug.LogError("Error constructing " + this + ", " + prefab + " is null!");
        }
        Prefab = prefab;
    }

    public virtual T Rent(T prefab=null)
    {
        if (Prefab == null && prefab == null)
        {
            return null;
        }
        CheckQueueExistOrNot();

        T instance;
        if (prefabQueue.Count > 0)
        {
            instance = prefabQueue.Dequeue();
        }
        else
        {
            if (prefab == null)
                instance = CreateInstance();
            else
                instance = CreateInstance(prefab);
        }

        OnBeforeRent(instance);

        return instance;
    }

    private void CheckQueueExistOrNot()
    {
        if(prefabQueue == null)
            prefabQueue = new Queue<T>();
    }

    protected virtual T CreateInstance(T prefab=null)
    {
        if (prefab == null)
        {
            return UnityEngine.Object.Instantiate(Prefab);
        }
        else
        {
            return UnityEngine.Object.Instantiate(prefab);
        }
    }

    protected virtual void OnBeforeRent(T instance)
    {
        instance.gameObject.SetActive(true);
    }

    public void Return(T instance)
    {
        CheckException(instance);
        CheckQueueExistOrNot();

        OnBeforeReturn(instance);
        prefabQueue.Enqueue(instance);
    }

    private void CheckException(T instance)
    {
        if (instance == null)
            throw new ArgumentException("Return null instance");

        if (prefabQueue.Count + 1 == maxPoolCount)
            throw new InvalidOperationException("Reached Max PoolSize");
    }

    protected virtual void OnBeforeReturn(T instance)
    {
        instance.gameObject.SetActive(false);
    }
}