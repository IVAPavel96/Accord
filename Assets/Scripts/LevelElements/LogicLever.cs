using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelElements
{
    public class LogicLever : LogicTriggerInput
    {
        [SerializeField, BoxGroup()] private Transform movingPart;
        [SerializeField, BoxGroup()] private float rotationMin;
        [SerializeField, BoxGroup()] private float rotationMax;
        [SerializeField, BoxGroup()] private float rotationTime;
        
        private bool inUsableRange;
        private bool triggerEnabled;

        private void Update()
        {
            if (inUsableRange && Input.GetButtonDown("Use"))
            {
                triggerEnabled = !triggerEnabled;

                if (triggerEnabled)
                {
                    movingPart?.DOLocalRotate(Vector3.forward * rotationMax, rotationTime, RotateMode.FastBeyond360);
                    TriggerEnable();
                }
                else
                {
                    movingPart?.DOLocalRotate(Vector3.forward * rotationMin, rotationTime, RotateMode.FastBeyond360);
                    TriggerDisable();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                inUsableRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                inUsableRange = false;
            }
        }
    }
}