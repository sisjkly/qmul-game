using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CtrlPanel : MonoBehaviour
{
    public static CtrlPanel instance;

    private void Awake()
    {
        instance = this;
    }
    public PawnCreater creater;
    /// Player Gold
    public int Gold = 5;
    
    /// Pawn price
    public int Price = 4;
  //  public TreeController controller;
  /// Defence base
    public void AllDefense()
    {
        var pawns = RayReason.Instance.controllers;
        foreach (var pawn in pawns)
        {
            pawn.mode = Mode.Defense;
        }


    }
    /// All Felling
    public void AllToTree()
    {
        var pawns = RayReason.Instance.controllers;
        foreach (var pawn in pawns)
        {
            pawn.mode = Mode.Gold;
        }
    }
    
    /// All Attack
    public void AllToAttack()
    {
        var pawns = RayReason.Instance.controllers;
        foreach (var pawn in pawns)
        {
            pawn.mode = Mode.Attack;
        }
    }

    /// Buy pawns
    public void Buy(int index )
    {
        if (creater.pawnControllers.Count>=100)
        {
            return;
        }
        if (Price+Price*index <= Gold) { 
            Gold = Gold-  (Price + Price * index);
           var pawn =  creater.CreatePawn(index);
            pawn.mode = Mode.Defense;
        }


    }

    public Text goldText;

    float goldtimer=0;


    /// Update gold coin information and
    /// automatically add a gold coin every 5 seconds
    public void UpdateGold() { 
    
        if (goldtimer <5)
        {
            goldtimer += Time.deltaTime;
        }
        else
        {
            Gold++;
            goldtimer = 0f;

        }
        goldText.text = "Gold:" + Gold;
    
    }

    public void AddGold()
    {
        Gold++;
    } 


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.K)) {AllToTree();}
        if (Input.GetKeyDown(KeyCode.D)) { AllDefense();}

        if (Input.GetKeyDown(KeyCode.A))
        {
            AllToAttack();
        }
     

    }
    // Update is called once per frame
    void Update()
    {
        UpdateGold();
        UpdateInput();
    }
}
