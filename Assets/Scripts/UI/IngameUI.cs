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
    public GameEndUI stageEndLayer;
    List<string> Queue=new List<string>();
    bool commandOpen=false;
    public void OnEnable(){
        MapManager.manager.GetIngameUI(this);
    }

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
        orderTable.SendQueue();
    }

    public void ResetQueue(){
        foreach(Transform t in orderTable.Content.transform){
            Destroy(t.gameObject);
        }
        Queue.Clear();
        CharacterManager.manager.ResetPlace();
    }

    private bool isMoving=false;
    public void CommandMoves(){
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

    public IEnumerator StageEnd(bool clear){
        yield return new WaitForSeconds(0.5f);
        stageEndLayer.gameObject.SetActive(true);
        stageEndLayer.ChecksPoint();
        stageEndLayer.PassOrFail(clear);
        //player dance
    }

    public void BackToMenu(){
        ResetQueue();
        if(stageEndLayer.isGameClear){
            stageEndLayer.gameObject.SetActive(false);
            MapManager.manager.EndMap();
            UIManager.uimanager.RemoveCanvas(3);
            UIManager.uimanager.ShowCanvas(1);
        }else{
            stageEndLayer.gameObject.SetActive(false);
            CharacterManager.manager.ResetGame();
        }
    }

    public void BackToMenuBeforeGameEnd(){
        stageEndLayer.gameObject.SetActive(false);
        MapManager.manager.EndMap();
        UIManager.uimanager.RemoveCanvas(3);
        UIManager.uimanager.ShowCanvas(1);
    }
}
