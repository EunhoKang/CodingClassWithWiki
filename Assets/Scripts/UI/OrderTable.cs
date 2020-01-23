using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderTable : MonoBehaviour, IDropHandler
{
    public GameObject Content;
    public Transform invisible;
    List<GameObject> Queue;
    RectTransform tablePos;

    void Start(){
        Queue=new List<GameObject>();
        tablePos=Content.transform as RectTransform;
        UpdateQueue();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(DragNDrop.itemDragged!=null){
            DragNDrop temp=Instantiate(DragNDrop.itemDragged) as DragNDrop;
            RectTransform tempPos=temp.transform as RectTransform;
            temp.gameObject.GetComponent<CanvasGroup>().blocksRaycasts=true;
            temp.gameObject.GetComponent<Image>().raycastTarget = true;
            
            if(!DragNDrop.itemDragged.isAlreadySpawned){
                temp.isAlreadySpawned=true;
                temp.transform.SetParent(Content.transform);
                Debug.Log(Input.mousePosition);
                UpdateQueue();
                if(Queue.Count%5==0){
                    tablePos.sizeDelta=new Vector2(tablePos.sizeDelta.x,tablePos.sizeDelta.y+1000);
                }
            }
            else{
                temp.transform.SetParent(Content.transform);
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
    
}
