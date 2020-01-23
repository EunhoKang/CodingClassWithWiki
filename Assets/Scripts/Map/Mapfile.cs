using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class customArray
{
    public List<int> data;
}
public class Mapfile : MonoBehaviour
{
    public List<customArray> map;
    public float gridLength;
    public List<GameObject> filePrefabs;
    
}
