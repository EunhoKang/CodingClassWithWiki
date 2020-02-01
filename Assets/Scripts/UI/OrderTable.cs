using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderTable : MonoBehaviour
{
    public GameObject Content;
    public RectTransform MoveUp;
    public RectTransform MoveDown;
    public float moveSpeed=5;
    public Transform invisible;
    List<GameObject> Queue;
    RectTransform tablePos;

    void Start(){
        Queue=new List<GameObject>();
        tablePos=Content.transform as RectTransform;
        UpdateQueue();
    }
    public void WhenDrop(DragNDrop d)
    {
        SwapCard(invisible,d.transform);
        if(d!=null && !d.isInvisible){
            DragNDrop temp=Instantiate(d) as DragNDrop;
            RectTransform tempPos=temp.transform as RectTransform;
            if(!d.isAlreadySpawned){
                if(d.isOnTable){
                    temp.isAlreadySpawned=true;
                    temp.transform.SetParent(Content.transform);
                    temp.indexInTable=temp.transform.GetSiblingIndex();
                    UpdateQueue();
                    if(Queue.Count%5==0){
                        tablePos.sizeDelta=new Vector2(tablePos.sizeDelta.x,tablePos.sizeDelta.y+1000);
                    }
                }else{
                    temp.transform.SetParent(null);
                    Destroy(temp.gameObject);
                }
            }
            else {
                if(d.isOnTable){
                    d.transform.SetParent(Content.transform);
                    if(d.indexInTable>=0){
                        d.transform.SetSiblingIndex(d.indexInTable);
                    }
                }
                else{
                    temp.transform.SetParent(null);
                    Destroy(temp.gameObject);
                    d.transform.SetParent(null);
                    Destroy(d.gameObject);
                }
            }
            temp.transform.localScale=new Vector3(1,1,1);
        }
                
    }

    public void UpdateQueue(){
        for(int i=0; i<Content.transform.childCount;i++){
            if(i>=Queue.Count){
                Queue.Add(null);
            }
            GameObject child=Content.transform.GetChild(i).gameObject;
            if(child!=Queue[i]){
                Queue[i]=child;
                DragNDrop tp=Queue[i].GetComponent<DragNDrop>();
                if(!tp.isInvisible)
                    Queue[i].GetComponent<DragNDrop>().indexInTable=i;
            }
        }
        Queue.RemoveRange(Content.transform.childCount,Queue.Count-Content.transform.childCount);
    }

    public int GetIndex(Transform card, int skipIndex=-1){
        int res=0;
        for(int i=0; i<Queue.Count; i++){
            if(card.position.y>Queue[i].transform.position.y){
                break;
            }else if(skipIndex!=i){
                res++;
            }
        }
        return res;
    }

    public void SwapCardByIndex(int a, int b){
        SwapCard(Queue[a].transform,Queue[b].transform);
    }

    public bool ContainPos(RectTransform rectTransform, Vector2 pos){
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform,pos);
    }

    public void SwapCard(Transform a, Transform b){
        Transform aParent=a.parent;
        Transform bParent=b.parent;
        int aindex=a.GetSiblingIndex();
        int bindex=b.GetSiblingIndex();
        a.SetParent(bParent);
        a.SetSiblingIndex(bindex);
        b.SetParent(aParent);
        b.SetSiblingIndex(aindex);
        UpdateQueue();
    }

    public void SendQueue(){
        QueueManager.manager.Init(Queue);
    }
    /*
    public void MoveContent(){
        Debug.Log(1);
        RectTransform tableMax=this.transform as RectTransform;
        RectTransform TT=Content.transform as RectTransform;
        if(DragNDrop.itemDragged==null){
            return;
        }

        if(0<TT.anchoredPosition.y && TT.anchoredPosition.y<TT.sizeDelta.y){
            if(ContainPos(MoveUp,DragNDrop.itemDragged.transform.position)){
                Debug.Log(1);
                TT.anchoredPosition+=Vector2.up*moveSpeed;
            }else if(ContainPos(MoveDown,DragNDrop.itemDragged.transform.position)){
                Debug.Log(2);
                TT.anchoredPosition+=Vector2.down*moveSpeed;
            }
        }
    }
    */
    
}
