using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int Level = 0;

    public static float AudioValue = 1.0f;
    public void LevelSelected(int value)
    {
        Level = value;
    }
    public static int MapIndex = 1;
    public  void MapSelected(int value) {
        MapIndex = value+1;
    }
    public void SetAudio(float value)
    {
        AudioValue = value;
    }
    /// When click the start game button
    public void OnStartBtn()
    {
        SceneManager.LoadScene(MapIndex);
    }


    public void OnQuitBtn()
    {
       Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        Level = 0;
        MapIndex = 1;
        AudioValue = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
