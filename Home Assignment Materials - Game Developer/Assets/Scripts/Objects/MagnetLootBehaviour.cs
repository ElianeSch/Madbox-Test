using System;
using UnityEngine;

public class MagnetLootBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float startDelay = 0.05f;
    [SerializeField] private float pickupDistance = 0.3f;

    private Transform target;
    private float currentSpeed;
    private float startTime;
    public static Action<MagnetLootBehaviour> OnLootPickedUp;

    private void Awake()
    {
        startTime = Time.time;
        GameObject player = GameManager.Instance.GetPlayer().gameObject;
        if (player != null)
            target = player.transform;
    }

    private void Update()
    {
        if (target == null)
            return;
        if (Time.time - startTime < startDelay)
            return;
        currentSpeed = Mathf.MoveTowards(currentSpeed, speed, acceleration * Time.deltaTime);
        Vector3 toTarget = target.position - transform.position;
        float dist = toTarget.magnitude;

        if (dist <= pickupDistance)
        {
            OnLootPickedUp?.Invoke(this);
            Destroy(gameObject);
            return;
        }
        Vector3 dir = toTarget / dist;
        transform.position += dir * currentSpeed * Time.deltaTime;
    }
}
