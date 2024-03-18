using UnityEngine;

[DisallowMultipleComponent]
public abstract class SingleTonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance {  get; private set; }

    public void Init()
    {
        if (instance != null)
        {
            Debug.LogWarning("MonoSingleTon: Object of type already exsists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        instance = this as T;
    }   
}

