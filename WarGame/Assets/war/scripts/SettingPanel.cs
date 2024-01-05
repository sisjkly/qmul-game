using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public static SettingPanel instance;
    public bool isShow => root.activeSelf;

    private void Awake()
    {
        instance = this;
    }


    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

   


    public void SetAudio(float value)
    {
      Menu.AudioValue = value;
    }
    public GameObject root;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider .value = Menu.AudioValue;
        Close();
    }
    public void Close()
    {
        root.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Open()
    {
        if (root.activeSelf)
        {
            return;
        }

        root.SetActive(true);
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          Open();
        }
    }
}
