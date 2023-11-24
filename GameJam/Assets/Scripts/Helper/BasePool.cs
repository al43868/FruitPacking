using UnityEngine;
using UnityEngine.Pool;

public class BasePool<T> where T : Component
{
    [SerializeField] protected T prefab;
    [SerializeField] int defaultSize = 100;
    [SerializeField] int maxSize = 500;

    ObjectPool<T> pool;

    public int ActiveCount => pool.CountActive;
    public int InactiveCount => pool.CountInactive;
    public int TotalCount => pool.CountAll;

    protected void Initialize(bool collectionCheck = true) =>
        pool = new ObjectPool<T>(OnCreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, collectionCheck, defaultSize, maxSize);

    protected virtual T OnCreatePoolItem() => GameObject.Instantiate(prefab);
    protected virtual void OnGetPoolItem(T obj) => obj.gameObject.SetActive(true);
    protected virtual void OnReleasePoolItem(T obj) => obj.gameObject.SetActive(false);
    protected virtual void OnDestroyPoolItem(T obj) => GameObject.Destroy(obj.gameObject);

    public T Get() => pool.Get();
    public void Release(T obj) => pool.Release(obj);
    public void Clear() => pool.Clear();
}