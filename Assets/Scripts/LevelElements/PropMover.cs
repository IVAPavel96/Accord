using System;
using DG.Tweening;
using Extensions;
using UnityEngine;

namespace LevelElements
{
    public class PropMover : MonoBehaviour
    {
        [SerializeField] private Transform p1, p2;
        [SerializeField] private Transform movingObject;
        [SerializeField] private float duration;

        private Rigidbody2D playerRigidbody2D;
        private Vector3 lastFramePosition;
        private LogicTriggerOutput trigger;
        private Tweener moving;

        void Start()
        {
            moving = movingObject.DOMove(p2.position, duration).From(p1.position).SetLoops(-1, LoopType.Yoyo);
            trigger = GetComponent<LogicTriggerOutput>();
            if (trigger != null)
            {
                trigger.onTriggerEnable.AddListener(StartMoving);
                trigger.onTriggerDisable.AddListener(StopMoving);
            }
            else
            {
                StartMoving();
            }
        }

        private void OnDestroy()
        {
            moving.Kill();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("entered: " + other.name);
            if (other.CompareTag("Player"))
            {
                playerRigidbody2D = other.GetComponent<Rigidbody2D>();
                lastFramePosition = transform.position;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var delta = transform.position - lastFramePosition;
                playerRigidbody2D.position += delta.xy();
                lastFramePosition = transform.position;
            }
        }

        public void StartMoving()
        {
            moving.Play();
        }

        public void StopMoving()
        {
            moving.Pause();
        }
    }
}