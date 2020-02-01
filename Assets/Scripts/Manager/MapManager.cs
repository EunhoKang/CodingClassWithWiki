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
    [HideInInspector]public Mapfile stagefile;
    [HideInInspector]public IngameUI gameUI;
    public string mapName;
    private Transform mapHolder;
    void Start()
    {
        Init();
    }

    void Init(){//추후 csv방식으로 바꿀 것
        stagefile=(Resources.Load(mapName) as GameObject).GetComponent<Mapfile>();
        MissionManager.manager.Init(stagefile.missions,stagefile.missionTargetCount);
        List<customArray> grid=stagefile.map;
        List<GameObject> array=stagefile.filePrefabs;
        Vector3 pointer=Vector3.zero;
        mapHolder=new GameObject("holder").transform;
        GameObject temp;
        for(int i=-1;i<=grid.Count;i++){
            for(int j=-1;j<=grid.Count;j++){
                temp=Instantiate(array[0],pointer,Quaternion.identity);
                temp.transform.SetParent(mapHolder);
                if(i==-1 || j==-1 || i==grid.Count || j==grid.Count){
                    temp=Instantiate(array[1],pointer+stagefile.gridLength*Vector3.up,Quaternion.identity);
                    temp.transform.SetParent(mapHolder);
                }
                else if(grid[i].data[j]>0){
                    temp=Instantiate(array[grid[i].data[j]],pointer+stagefile.gridLength*Vector3.up,Quaternion.identity);
                    temp.transform.SetParent(mapHolder);
                    if(grid[i].data[j]==2){
                        CharacterManager.manager.MapSet(temp);
                    }
                }
                pointer+=stagefile.gridLength*Vector3.right;
            }
            pointer.x=0;
            pointer+=stagefile.gridLength*Vector3.forward;
        }

        //CharacterManager.manager.MapSet(stagefile.GetComponent<Mapfile>().player);
    }

    public void GetIngameUI(IngameUI ui){
        gameUI=ui;
    }
}
