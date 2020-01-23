using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    //public List<Image> imageFrame;
    //public List<Sprite> commandImages;
    public OrderTable orderTable;
    List<string> Queue=new List<string>();

    public void SendQueue(){//it will take some time because of GetComponent(Maybe?)
        if(CharacterManager.manager.isMoving){
            return;
        }
        foreach(Transform t in orderTable.Content.transform){
            DragNDrop temp=t.gameObject.GetComponent<DragNDrop>();
            Queue.Add(temp.commandName);
        }
        CharacterManager.Command reset=ResetQueue;
        CharacterManager.manager.PlayerMove(new List<string>(Queue),reset);
        Queue.Clear();
    }

    public void ResetQueue(){
        foreach(Transform t in orderTable.Content.transform){
            Destroy(t.gameObject);
        }
        Queue.Clear();
        CharacterManager.manager.ResetPlace();
    }

    public void GameEnd(){
        //
    }
}
