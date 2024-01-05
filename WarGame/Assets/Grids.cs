using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grids : MonoBehaviour
{
    public Image Item;
    public bool collectItem;
    public bagsys.ItemType ITOB;//Item type in this grid//
    public int PlaceAmount = 0;
    public Sprite[] itemImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Item.gameObject.SetActive(collectItem);
        Item.sprite = itemImage[(int)ITOB];

    }
}
