using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class nextDialogue : MonoBehaviour
{

    int index = 2;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && transform.childCount > 1)
        {
            if (ThirdPersonController.dialogue)
            { 
            transform.GetChild(index).gameObject.SetActive(true);
                index += 1;
                if (transform.childCount == index)
                {

                    index = 2;
                    ThirdPersonController.dialogue = false;
                }
            }
            
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
