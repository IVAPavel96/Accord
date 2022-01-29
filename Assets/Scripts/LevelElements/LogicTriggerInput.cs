using Sirenix.Utilities;
using UnityEngine;

namespace LevelElements
{
    public class LogicTriggerInput : MonoBehaviour
    {
        [SerializeField] private LogicTriggerOutput[] targets;

        public void TriggerEnable()
        {
            targets.ForEach(output => output.TriggerEnable());
        }

        public void TriggerDisable()
        {
            targets.ForEach(output => output.TriggerDisable());
        }
    }
}