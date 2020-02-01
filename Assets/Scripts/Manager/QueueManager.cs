using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static QueueManager manager = null;
    public void Awake() //Singietone Pattern
    {
        if (manager == null)
        {
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    [HideInInspector]public List<GameObject> Queue;
    public void Init(List<GameObject> q){
        Queue=new List<GameObject>(q);
    }
}
