using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPawnBase : MonoBehaviour {
    public int Hp = 100;
    public int MaxHp = 100;
    public int AttackValue = 10;
    public int Level = 1;
    public abstract void Hurt(int value,IPawnBase pawnBase);

    public abstract void Killed();
}
public enum Mode
{
    Defense=0,
    Attack=1,
    Gold=2,
    Idle=3
}
