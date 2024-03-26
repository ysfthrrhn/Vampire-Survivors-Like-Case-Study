using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IPoolObject
{
    protected Transform _target;

    [SerializeField]
    protected float moveSpeed;

    protected virtual void MoveToTarget() { }
    

    public void SetTarget(Transform target)
    {
        _target = target;
        GetComponent<TrailRenderer>().emitting = true;
    }

    public void ReturnToPool()
    {
        _target = null;
        GetComponent<TrailRenderer>().Clear();
        gameObject.SetActive(false);
    }

}
