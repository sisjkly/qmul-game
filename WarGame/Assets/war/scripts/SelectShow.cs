using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectShow : MonoBehaviour
{

    public GameObject Obj;
    PawnController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RayReason.Instance.controllers.Contains(controller))
        {
            Obj.SetActive(true);
        }
        else { Obj.SetActive(false); }
    }
}
