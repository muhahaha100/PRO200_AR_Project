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

    public void Move(Vector3 pointA, Vector3 pointB, float timer)
    {
        Vector3 peak = pointB - pointA;
        peak = new Vector3(peak.x, peak.y + 5, peak.z);

        Vector3 position = Vector3.Lerp(Vector3.Lerp(pointA, peak, timer), Vector3.Lerp(peak, pointB, timer), timer);
        gameObject.transform.position = position;
    }

    public void Attack(Entity targetEntity)
    {
        switch (element)
        {
            case eElement.FIRE:
                switch (targetEntity.element)
                {
                    case eElement.FIRE:
                        targetEntity.SubtractHP(dmg);
                        break;
                    case eElement.WATER:
                        targetEntity.SubtractHP(dmg / 2);
                        break;
                    case eElement.NATURE:
                        targetEntity.SubtractHP(dmg * 2);
                        break;
                }
                break;
            case eElement.WATER:
                switch (targetEntity.element)
                {
                    case eElement.FIRE:
                        targetEntity.SubtractHP(dmg * 2);
                        break;
                    case eElement.WATER:
                        targetEntity.SubtractHP(dmg);
                        break;
                    case eElement.NATURE:
                        targetEntity.SubtractHP(dmg / 2);
                        break;
                }
                break;
            case eElement.NATURE:
                switch (targetEntity.element)
                {
                    case eElement.FIRE:
                        targetEntity.SubtractHP(dmg / 2);
                        break;
                    case eElement.WATER:
                        targetEntity.SubtractHP(dmg * 2);
                        break;
                    case eElement.NATURE:
                        targetEntity.SubtractHP(dmg);
                        break;
                }
                break;
        }
    }
}
