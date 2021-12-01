using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T prefab;

    public static GenericObjectPool<T> Instance { get; private set; }

    [SerializeField]
    private Queue<T> objects = new Queue<T>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartingPool(int count)
    {
        AddObjects(count);
    }

    public T Get()
    {
        if (objects.Count == 0)
            AddObjects(1);
        return objects.Dequeue();
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    void AddObjects(int count)
    {
        T newObject = GameObject.Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        objects.Enqueue(newObject);
    }
}
