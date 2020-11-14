using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] float dmg;
    [SerializeField] float speed = 1;
    [SerializeField] eElement element;

    public void SubtractHP(float dmgTaken)
    {
        hp -= dmgTaken;
    }

    public void AddHP(float hpHealed)
    {
        hp += hpHealed;
    }

    void Update()
    {
        
    }
}
