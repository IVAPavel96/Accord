using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace LevelElements
{
    public class LogicTriggerOutput : MonoBehaviour
    {
        public UnityEvent onTriggerEnable = new UnityEvent();
        public UnityEvent onTriggerUpdate = new UnityEvent();
        public UnityEvent onTriggerDisable = new UnityEvent();

        private bool isTriggered;
        public bool IsTriggered => isTriggered;

        public void TriggerEnable()
        {
            onTriggerEnable.Invoke();
            isTriggered = true;
        }

        public void TriggerDisable()
        {
            onTriggerDisable.Invoke();
            isTriggered = false;
        }
        
        private void Update()
        {
            if (isTriggered)
            {
                onTriggerUpdate.Invoke();
            }
        }
    }
}