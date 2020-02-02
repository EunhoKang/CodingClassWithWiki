using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public void StartButton()
    {
        UIManager.uimanager.RemoveCanvas(0);
        UIManager.uimanager.ShowCanvas(1);
    }

    public void MapButton(string mapName)
    {
        Debug.Log("Map : "+mapName.ToString());
        UIManager.uimanager.RemoveCanvas(1);
        MapManager.manager.Init(mapName,new Vector3(0,0,0));
        UIManager.uimanager.ShowCanvas(3);
        
    }

    public void OptionButton()
    {
        UIManager.uimanager.RemoveCanvas(1);
        UIManager.uimanager.ShowCanvas(2);
    }

    public void OptionToLobbyButton()
    {
        UIManager.uimanager.RemoveCanvas(2);
        UIManager.uimanager.ShowCanvas(1);
    }

    public void SkinButton()
    {
        UIManager.uimanager.RemoveCanvas(1);
        UIManager.uimanager.ShowCanvas(4);
    }

    public void BackSkinButton()
    {
        UIManager.uimanager.RemoveCanvas(4);
        UIManager.uimanager.ShowCanvas(1);
    }
}
