using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PropMover : MonoBehaviour
{
    [SerializeField] private Transform p1, p2;
    [SerializeField] private Transform movingObject;
    [SerializeField] private float duration;

    void Start()
    {
        StartMoving();
    }

    public void StartMoving()
    {
        movingObject.DOMove(p2.position, duration).From(p1.position).SetLoops(-1, LoopType.Yoyo);
    }
    
}
