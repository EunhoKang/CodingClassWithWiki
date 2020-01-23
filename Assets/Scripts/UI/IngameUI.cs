using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    public OrderTable orderTable;
    public GameObject Commands;
    public RectTransform spot1;
    public RectTransform spot2;
    List<string> Queue=new List<string>();
    bool commandOpen=false;

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

    private bool isMoving=false;
    public void BackToLobby(){
        if(!isMoving){
            if(commandOpen){
                StartCoroutine(CommandsMove(spot2.position,spot1.position));
            }else{
                StartCoroutine(CommandsMove(spot1.position,spot2.position));
            }
            commandOpen=!commandOpen;
        }
    }

    IEnumerator CommandsMove(Vector3 a,Vector3 b){
        isMoving=true;
        for(float i=0;i<=1;i+=0.02f){
            Commands.transform.position=Vector3.Lerp(a,b,i*i);
            yield return new WaitForSeconds(0.005f);
        }
        isMoving=false;
    }
}
