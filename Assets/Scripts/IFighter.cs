using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFighter
{
    public void Attack(IFighter fighter);

    public void TakeDamage(float damage);
}
