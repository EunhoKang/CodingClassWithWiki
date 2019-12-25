using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager manager = null;
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
        DictionarySet();
    }

    [HideInInspector]public delegate void Command();
    [HideInInspector]public Command s;
    [HideInInspector]public Dictionary<string,Command> commandFinder;
    
    public void DictionarySet(){//Update New Commands Here
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

    //Commands
    void MoveLeft(){
        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.left));
    }
    void MoveRight(){

        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.right));
    }
    void MoveUp(){
        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.forward));
    }
    void MoveDown(){
        StartCoroutine(MovingCoroutine(player.transform.position+pxPerTile*Vector3.back));
    }
    IEnumerator MovingCoroutine(Vector3 end){
        for(float i=0; i<=1; i+=0.05f){
            rb.MovePosition(Vector3.Lerp(player.transform.position,end,i));
            yield return moveDelay;
        }
    }


    //CharacterMoves
    public int pxPerTile;
    [HideInInspector] public GameObject player;
    [HideInInspector] public bool isMoving=false;
    [HideInInspector] public Vector3 startingPlace;
    bool isStageEnd=true;
    Rigidbody rb;

    WaitForSeconds commandDelay=new WaitForSeconds(0.6f);
    WaitForSeconds moveDelay=new WaitForSeconds(0.015f);
    
    public void MapSet(GameObject target){
        player=target;
        rb=player.GetComponent<Rigidbody>();
        startingPlace=new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z);
        isStageEnd=false;
    }

    public void PlayerMove(List<CommandNode> queue){
        if(!isStageEnd){
            isMoving=true;
            IEnumerator Temp=Move(queue);
            StartCoroutine(Temp);
        }
    }

    IEnumerator Move(List<CommandNode> queue){
        for(int i=0;i<queue.Count;i++){
            Command temp=commandFinder[queue[i].GetName()];
            temp();

            yield return commandDelay;
        }
        yield return commandDelay;
        //This didn't guarantee the complete end of all of moves. Change 3 lines of codes below this if you have some problems with it.
        isMoving=false;
        if(!isStageEnd){
            player.transform.position=startingPlace;
        }
    }

    public void EndStage(){
        isStageEnd=true;
        Debug.Log("finish!");
    }
    public void ResetPlace(){
        StopAllCoroutines();
        isMoving=false;
        player.transform.position=startingPlace;
        player.transform.rotation=Quaternion.Euler(Vector3.zero);
    }
}
