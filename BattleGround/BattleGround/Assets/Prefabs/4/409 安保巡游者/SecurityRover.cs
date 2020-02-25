using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityRover : BaseCard
{
    public BaseCard prefab;

    public override IEnumerator TakeDamage()
    {
        yield return StartCoroutine(minions.SummonMinion(prefab, GetPosition() + 1));
    }
}
