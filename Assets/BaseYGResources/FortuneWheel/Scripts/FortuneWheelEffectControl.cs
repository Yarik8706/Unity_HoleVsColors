using System;
using DG.Tweening;
using UnityEngine;

namespace FortuneWheel.Scripts
{
    public class FortuneWheelEffectControl : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 6f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}