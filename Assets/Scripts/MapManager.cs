using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager manager = null;
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
    //This is variable for Test ver. Delete this after UI Scene is completed. 
    public GameObject targetPrefab; 
    GameObject map;

    void Start()
    {
        Init();
    }

    void Init(){
        //Instantiate target Map.
        map=Instantiate(targetPrefab);
        //Delete this when using ARCore
        Camera.main.transform.position=map.transform.position+15*Vector3.up;
        //Give CharacterManager references(player, endpoint, etc.) from Mapfile targeted.
        CharacterManager.manager.MapSet(map.GetComponent<Mapfile>().player);
    }
}
