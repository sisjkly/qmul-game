using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
public class npcsys2 : MonoBehaviour
{
    public GameObject d_template;
    public GameObject canvas;

    bool playerdetection = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(playerdetection && Input.GetKeyDown(KeyCode.F)   && !ThirdPersonController.dialogue)
        {
            canvas.SetActive(true);
            ThirdPersonController.dialogue = true;
            newdialogue("Greetings, traveler.");
            newdialogue("I've heard rumors of a hidden treasure.");
            newdialogue("Are you interested in joining the quest?");
            newdialogue("You'll face many challenges ahead.");
            newdialogue("Monsters, traps, and riddles await.");
            newdialogue("What say you?");
            canvas.transform.GetChild(1).gameObject.SetActive(true); // use 'transform' to access GetChild

        }
    }

    void newdialogue(string text)
    {
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canvas.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PlayerArmature")

        playerdetection = true;
    }
    private void OnTriggerExit(Collider other)
    {
        playerdetection = false;            
    }
}
