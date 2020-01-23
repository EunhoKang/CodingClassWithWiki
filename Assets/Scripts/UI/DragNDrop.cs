using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    public static DragNDrop itemDragged;
    public OrderTable table;
    /*[HideInInspector]*/
    [HideInInspector]public Vector3 root;
    [HideInInspector]public Vector3 diff;
    [HideInInspector]public Transform startParent;
    [HideInInspector]public bool isOnTable=false;
    /*[HideInInspector]*/public bool isAlreadySpawned=false;
    /*[HideInInspector]*/public int indexInTable=-1;
    public bool isInvisible=false;
    public string commandName;
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragged=this;
        diff=gameObject.transform.position-Input.mousePosition;
        startParent=transform.parent;
        root=transform.position;
        table.SwapCard(table.invisible,transform);
        
        GetComponent<CanvasGroup>().blocksRaycasts=false;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=Input.mousePosition+diff;
        int invisibleIndex=table.invisible.transform.GetSiblingIndex();
        int targetIndex=table.GetIndex(itemDragged.transform,table.invisible.transform.GetSiblingIndex());
        if(table.ContainPos(table.transform as RectTransform,itemDragged.transform.position)){
            if(isAlreadySpawned && invisibleIndex!=targetIndex){
                table.SwapCardByIndex(invisibleIndex,targetIndex);
                indexInTable=targetIndex;
            }
            isOnTable=true;
        }else{
            isOnTable=false;
        }
        //table.MoveContent();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //table.SwapCard(table.invisible,transform);
        transform.position=root;
        GetComponent<CanvasGroup>().blocksRaycasts=true;
        GetComponent<Image>().raycastTarget = true;

        table.WhenDrop(this);
        itemDragged=null;
    }
}
