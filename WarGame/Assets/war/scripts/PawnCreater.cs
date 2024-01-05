using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnCreater : MonoBehaviour
{
    public TreeController tree;
    public List< GameObject> prefab =new List<GameObject>();
    public List<PawnController> pawnControllers = new List<PawnController>();
    public PawnController CreatePawn( int index )
    {
        prefab[index].GetComponent<PawnController>().transform.position = transform.position;
        var obj = Instantiate (prefab[index]);

        obj.transform.position = transform.position;
        var  p = obj.GetComponent<PawnController>();
        pawnControllers.Add(p);
        p.creater = this;
        return p;
    }


    // Start is called before the first frame update
    void Start()
    {
        pawnControllers = FindObjectsOfType<PawnController>().Where(it=>it.creater==this).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
