using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeController : IPawnBase
{

    public UnityEvent hurtE;
    public override void Hurt(int value, IPawnBase pawnBase)
    {
        print("gold");
        hurtE?.Invoke();
    }
    public override void Killed()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
