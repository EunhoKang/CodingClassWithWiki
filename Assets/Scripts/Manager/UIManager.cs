using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uimanager = null;
    public void Awake()
    {
        if (uimanager == null)
        {
            uimanager = this;
        }
        else if (uimanager != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public List<GameObject> canvasPrefabs;
    private List<GameObject> canvases = new List<GameObject>();

    private void Start()
    {
        float height = Screen.height;
        Screen.SetResolution((int)(height * 16 / 9), (int)height, false);
        StartCoroutine(Init());   
    }
    
    IEnumerator Init()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        for (int i = 0; i < canvasPrefabs.Count; i++)
        {
            GameObject temp = Instantiate(canvasPrefabs[i]);
            canvases.Add(temp);
            temp.SetActive(false);
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        ShowCanvas(0);
    }

    public void RemoveCanvas(int index)
    {
        canvases[index].SetActive(false);
    }
    public void ShowCanvas(int index)
    {
        canvases[index].SetActive(true);
    }

}
