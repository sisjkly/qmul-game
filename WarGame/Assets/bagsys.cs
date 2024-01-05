using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagsys : MonoBehaviour
{
    public enum ItemType
    {
        none = 0, helmet,aromor,gloves,boots,weapons,shield,misc,rings
    }

    public Camera currentPosition;

    public Transform BagParent;
    public Grids grid;
    public int gridAmount = 1;
    public List<ItemType> IAP=new List<ItemType>();//Item has been placed in bag//
    public List<int> IP = new List<int>();//item place in bag//
    public List<Grids> AllGrids = new List<Grids>();
    public void changeGridAmount(int _IAmouont)
    {
        for(int fFor=0; fFor< _IAmouont; fFor++)
        {
            AllGrids.Add(Instantiate(grid, BagParent));
            AllGrids[AllGrids.Count - 1].gameObject.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        changeGridAmount(gridAmount);
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
