using Cinemachine;
using UnityEngine;

public class CameraSizeCalculator : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float sizeCoefficient;

    private void Update()
    {
        float distance = (target1.position - target2.position).magnitude;
        virtualCamera.m_Lens.OrthographicSize = distance * sizeCoefficient;
    }
}
