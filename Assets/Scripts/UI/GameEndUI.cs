using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    public List<Image> checkList;
    public List<Text> textList;
    public Image PassFail;
    public Sprite pass;
    public Sprite fail;
    public Sprite missionChecked;

    public void ChecksPoint(){
        List<bool> tp=MissionManager.manager.AtGameEnd();
        List<string> tp2=MissionManager.manager.missionString();
        for(int i=0;i<checkList.Count;i++){
            textList[i].text=tp2[i];
            if(tp[i]){
                checkList[i].sprite=missionChecked;
            }
        }
    }

    public void PassOrFail(bool clear){
        if(clear){
            PassFail.sprite=pass;
        }else{
            PassFail.sprite=fail;
        }
    }
}
