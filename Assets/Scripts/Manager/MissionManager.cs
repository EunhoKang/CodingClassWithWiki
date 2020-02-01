using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager manager = null;
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
        DictionaryInit();
    }
    [HideInInspector]public List<mission> missions=new List<mission>();
    [HideInInspector]public List<string> missionStrings=new List<string>();
    [HideInInspector]public List<int> missionTargetCount=new List<int>();
    public delegate bool mission(int a);
    private Dictionary<string,mission> MissionDictionary=new Dictionary<string, mission>();
    public void DictionaryInit(){
        MissionDictionary.Add("QueueCount",CountQueue);
    }
    public void Init(List<string> mission, List<int> Count){
        missionStrings=new List<string>(mission);
        for(int i=0;i<mission.Count;i++){
            missions.Add(MissionDictionary[mission[i]]);
            missionTargetCount.Add(Count[i]);
        }
        
    }

    public List<bool> AtGameEnd(){
        List<bool> tp=new List<bool>();
        for(int i=0;i<missions.Count;i++){
            mission temp=missions[i];
            tp.Add(temp(missionTargetCount[i]));
        }
        return tp;
    }

    public List<string> missionString(){
        List<string> tp=new List<string>();
        for(int i=0;i<missionStrings.Count;i++){
            if(missionStrings[i]=="QueueCount"){
                tp.Add(missionTargetCount[i].ToString()+"번 움직이기");
            }
        }
        return tp;
    }
    //----------------------------------------------------
    public bool CountQueue(int MaxCount){
        return MaxCount<=QueueManager.manager.Queue.Count;
    }
}
