using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMain : IPawnBase
{
    public override void Hurt(int value, IPawnBase pawnBase)
    {
        Hp -= value;
        if (Hp <= 0)
        {
            SceneManager.LoadScene("Fail");
        }
    }

    float timer = 0;
    private void Update()
    {
        if (timer<10)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            Hp++;
        }
    }

    public override void Killed()
    {
       // throw new System.NotImplementedException();
    }
}
