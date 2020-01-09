using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderTable : MonoBehaviour, IDropHandler
{
    public GameObject Content;

    List<string> Queue=new List<string>();
    public void OnDrop(PointerEventData eventData)
    {
        if(DragNDrop.itemDragged!=null){
            DragNDrop temp=Instantiate(DragNDrop.itemDragged) as DragNDrop;
            temp.gameObject.GetComponent<CanvasGroup>().blocksRaycasts=true;
            temp.gameObject.GetComponent<Image>().raycastTarget = true;
            temp.gameObject.transform.localScale=Vector3.one;
            
            if(!DragNDrop.itemDragged.isAlreadySpawned){
                temp.isAlreadySpawned=true;
                temp.transform.SetParent(Content.transform);
            }
            else{
                temp.transform.SetParent(Content.transform);
            }
        }
                
    }

    public void ResetTable(){
        Queue.Clear();
    }
    
}
