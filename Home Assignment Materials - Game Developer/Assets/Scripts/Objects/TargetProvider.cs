using System.Collections.Generic;
using UnityEngine;

public class TargetProvider : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float range;
    private Transform currentTarget;
    public Transform CurrentTarget => currentTarget;

    public void SetRange(float newRange)
    {
        range = newRange;
    }

    public Transform GetClosestTarget()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            range,
            targetLayer
        );

        Transform closest = null;
        float closestSqrDistance = range * range;

        foreach (Collider hit in hits)
        {
            Transform target = hit.transform;
            if (target == transform)
                continue;
            if (IsTargetAlive(target) == false)
                continue;
            float sqrDistance =
                (target.position - transform.position).sqrMagnitude;
            if (sqrDistance > closestSqrDistance)
                continue;
            closestSqrDistance = sqrDistance;
            closest = target;
        }
        currentTarget = closest;
        return closest;
    }

    public bool IsTargetAlive(Transform target)
    {
        if (target == null) return false;
        if (target.GetComponent<Health>() == null) return false;
        return !target.GetComponent<Health>().IsDead;
    }

    public void SetCurrentTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}
