using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    public static DragNDrop itemDragged;
    [HideInInspector]public Vector3 root;
    [HideInInspector]public Vector3 diff;
    [HideInInspector]public Transform startParent;
    [HideInInspector]public bool isAlreadySpawned=false;
    [HideInInspector]public int indexInTable=-1;
    public string commandName;
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragged=this;
        diff=gameObject.transform.position-Input.mousePosition;
        startParent=transform.parent;
        root=transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts=false;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=Input.mousePosition+diff;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isAlreadySpawned){
            transform.parent=null;
            Destroy(this.gameObject);
        }
        else{
            transform.position=root;
        }
        GetComponent<CanvasGroup>().blocksRaycasts=true;
        GetComponent<Image>().raycastTarget = true;
        itemDragged=null;
    }
}
