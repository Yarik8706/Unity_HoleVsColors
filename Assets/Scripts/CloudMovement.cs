using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class CloudMovement : MonoBehaviour
    {
        private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        
        private void Start()
        {
            var _duration = Random.Range(20f, 25f);
            _startPosition = transform.position;
            _endPosition += transform.position;
            DOTween.Sequence()
                .Append(transform.DOMove(_endPosition, _duration))
                .Append(transform.DOMove(_startPosition, _duration))
                .SetEase(Ease.InOutSine)
                .SetLoops(-1);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_endPosition+transform.position, 0.1f);
        }
    }
}