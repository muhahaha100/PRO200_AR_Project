using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] float hp;
    public float HP { get { return hp; } set { hp = value; } }
    [SerializeField] float dmg;
    public float DMG { get { return dmg; } set { dmg = value; } }
    [SerializeField] float speed = 1;
    public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField] eElement element;
    public eElement Element { get { return element; } set { element = value; } }

    public void SubtractHP(float dmgTaken)
    {
        hp -= dmgTaken;
    }

    public void AddHP(float hpHealed)
    {
        hp += hpHealed;
    }
}
