using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelElements
{
    public class LogicLever : LogicTriggerInput, IInteractableObject
    {
        [SerializeField, BoxGroup()] private Transform movingPart;
        [SerializeField, BoxGroup()] private float rotationMin;
        [SerializeField, BoxGroup()] private float rotationMax;
        [SerializeField, BoxGroup()] private float rotationTime;
        [SerializeField] private AudioSource audioOnTrigger;
        [SerializeField] private AudioSource audioOffTrigger;
        
        private bool inUsableRange;
        private bool triggerEnabled;

        public void StartUse()
        {
            ToggleLeverState();
        }

        public void ToggleLeverState()
        {
            triggerEnabled = !triggerEnabled;

            if (triggerEnabled)
            {
                movingPart?.DOLocalRotate(Vector3.forward * rotationMax, rotationTime, RotateMode.FastBeyond360);
                TriggerEnable();
                audioOnTrigger?.Play();
            }
            else
            {
                movingPart?.DOLocalRotate(Vector3.forward * rotationMin, rotationTime, RotateMode.FastBeyond360);
                TriggerDisable();
                audioOffTrigger?.Play();
            }
        }
    }
}