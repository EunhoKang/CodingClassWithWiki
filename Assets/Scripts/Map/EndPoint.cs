using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        CharacterManager.manager.EndStage(); 
    }
}
