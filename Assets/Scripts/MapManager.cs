using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager manager = null;
    public void Awake()
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
        //DontDestroyOnLoad(this); 
    }
    
    public GameObject targetPrefab; //This is variable for Test ver. Delete this after UI Scene is completed. 

    GameObject map;

    void Start()
    {
        Init();
    }

    void Init(){
        map=Instantiate(targetPrefab);
        Camera.main.transform.position=map.transform.position+15*Vector3.up;
        CharacterManager.manager.MapSet(map.GetComponent<Mapfile>().player);
    }
}
