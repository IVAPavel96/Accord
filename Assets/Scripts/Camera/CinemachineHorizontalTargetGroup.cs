using Cinemachine;
using Extensions;
using UnityEngine;

namespace Camera
{
    public class CinemachineHorizontalTargetGroup : CinemachineTargetGroup
    {
        void FixedUpdate()
        {
            float backup = transform.position.y;
            if (m_UpdateMethod == UpdateMethod.FixedUpdate)
                DoUpdate();
            transform.position = transform.position.WithY(backup);
        }

        void Update()
        {
            float backup = transform.position.y;
            if (!Application.isPlaying || m_UpdateMethod == UpdateMethod.Update)
                DoUpdate();
            transform.position = transform.position.WithY(backup);
        }

        void LateUpdate()
        {
            float backup = transform.position.y;
            if (m_UpdateMethod == UpdateMethod.LateUpdate)
                DoUpdate();
            transform.position = transform.position.WithY(backup);
        }
    }
}