using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : IPawnBase
{
    PawnCreater creater;
    /// BOSS injured
    /// <param name="value"></param>
    public override void Hurt(int value, IPawnBase pawnBase)
    {
       Hp -= value;

        if (Hp <=0)
        {
           var arr =  FindObjectsOfType<PawnController> ().Where(it=>it. creater == creater ).ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                Destroy(arr[i].gameObject);
            }
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        creater = GetComponent<PawnCreater>();
    }

    public float timer = 0;
    /// Time it take for the AI to create a soldier
    public float MaxTimer1 = 10f;

    public float Timer2 = 120f;

    /// The length of time between proactively launching strategic attacks
    /// and accumulating strength during this period
    public float MaxTimer2 = 120f;
    // Update is called once per frame
    void Update()
    {
        if (SettingPanel.instance.isShow)
        {
            return;
        }
        if (Timer2>0)
        {
            Timer2 -= Time.deltaTime;
        }

        else
        {
            Timer2 = MaxTimer2; 
            /// Ai's strength will gradually increase in the later stage,
            /// and the time to build troops will be shortened
            /// from the original 10 seconds to 5 seconds.
            if (MaxTimer1>5)
            {
                MaxTimer1--;
            }



            var p = FindObjectsOfType<PawnController>().Where(it=>it.creater==creater);
            foreach (var item in p)
            {
                item.mode = Mode.Attack;
            }
        }
        if (timer <MaxTimer1)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;

            for (int i = 0; i < 1+Menu.Level; i++)
            {
                int index = Random.Range(0, 5) < 4 ? 0 : 1;
                var p = creater.CreatePawn(index);
                var ind = Random.Range(0, 3);
                p.mode = (Mode)ind;
            }
         


        }
    }

    public override void Killed()
    {
       
    }
}
