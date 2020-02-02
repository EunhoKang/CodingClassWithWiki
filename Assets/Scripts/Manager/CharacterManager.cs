using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //This script manages ONLY player's character.
    //And Commands player can order also can control ONLY player's character.

    public static CharacterManager manager = null;
    public void Awake() //Singletone Pattern
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
        DictionarySet();
    }
    //delegate for void,void
    [HideInInspector]public delegate void Command();
    //Gather all commands in here
    [HideInInspector]public Dictionary<string,Command> commandFinder;
    //Update new commands below here
    public void DictionarySet(){
        commandFinder=new Dictionary<string, Command>();
        Command temp;
        temp=new Command(MoveLeft);
        commandFinder.Add("MoveLeft",temp);
        temp=new Command(MoveRight);
        commandFinder.Add("MoveRight",temp);
        temp=new Command(MoveUp);
        commandFinder.Add("MoveUp",temp);
        temp=new Command(MoveDown);
        commandFinder.Add("MoveDown",temp);
    }

    //Commands. This is actions that character can do
    //Write all commands below here
    void MoveLeft(){
        direction=3;
        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.left));
    }
    void MoveRight(){
        direction=1;
        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.right));
    }
    void MoveUp(){
        direction=0;
        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.forward));
    }
    void MoveDown(){
        direction=2;
        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.back));
    }
    IEnumerator MovingCoroutine(Vector3 end){
        player.transform.rotation=Quaternion.Euler(new Vector3(0,90*direction,0));
        for(float i=0; i<=1; i+=0.05f){
            rb.MovePosition(Vector3.Lerp(player.transform.position,end,i));
            yield return moveDelay;
        }
    }


    //multiplier for moving
    public int pxPerTile;
    //player in Map
    [HideInInspector] public GameObject player;
    //show whether character is moving or not
    [HideInInspector] public bool isMoving=false;
    //place where player start moving. Use this for reset player's transform
    [HideInInspector] public Vector3 startingPlace;
    //show whether stage is ended
    bool isStageEnd=true;
    int direction=0;//0-앞/1-오른쪽/2-뒤/3-왼쪽
    Rigidbody rb;
    WaitForSeconds commandDelay=new WaitForSeconds(0.6f);
    WaitForSeconds moveDelay=new WaitForSeconds(0.015f);

    //Set Mapfile's references
    public void MapSet(GameObject target){
        player=target;
        rb=player.GetComponent<Rigidbody>();
        startingPlace=new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z);
        isStageEnd=false;
    }

    //Recieve the Command Queue to Coroutine.
    public void PlayerMove(List<string> queue,Command c){
        if(!isStageEnd){
            isMoving=true;
            IEnumerator Temp=Move(queue,c);
            StartCoroutine(Temp);
        }
    }

    //execute Commands in order, one by one.
    IEnumerator Move(List<string> queue,Command c){
        for(int i=0;i<queue.Count;i++){
            //Search the function matches with given string
            Command temp=commandFinder[queue[i]];
            //Execute
            temp();

            yield return commandDelay;
        }
        yield return commandDelay; 
        //Change 3 lines of codes below if you have some problems with it.
        //This didn't guarantee the complete end of all of moves.
        if(!isStageEnd){
            ResetPlace();
            //If all of command is finished, message that to IngameUI
            c();
        }
    }
    //If stage is finished, message that to IngameUI (Yet!)
    public void EndStage(bool clear){
        isStageEnd=true;
        StartCoroutine(MapManager.manager.gameUI.StageEnd(clear));
    }
    //Reset player's position & rotation
    public void ResetPlace(){
        StopAllCoroutines();
        isMoving=false;
        player.transform.position=startingPlace;
        player.transform.rotation=Quaternion.Euler(Vector3.zero);
    }

    public void ResetGame(){
        isStageEnd=false;
        ResetPlace();
    }
}
