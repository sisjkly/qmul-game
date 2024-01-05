using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnCanvas : MonoBehaviour
{
    IPawnBase controller;

    Slider slider;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<IPawnBase>();
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        slider.value = ((float)controller.Hp) / (controller.MaxHp);
       if (text!=null)
        {
            text.text = controller.Level.ToString();
            if (controller.Level>1)
            {
                text.color = Color.yellow;
            }
            if (controller.Level>2)
            {
                text.color = Color.red;
            }

        }
       
    }
}
