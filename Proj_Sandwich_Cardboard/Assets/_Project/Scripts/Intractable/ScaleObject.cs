using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Intractable
{
    public class ScaleObject : MonoBehaviour , ISelectionResponse
    {
        [SerializeField] private float scaleFactor;
        [SerializeField] private Ease tweenEase;
        
        private bool _isScaledUp;
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void OnEnter()
        {
            if(_isScaledUp) return;
            transform.DOScale(_originalScale * scaleFactor, .5f).SetEase(tweenEase);
            _isScaledUp = true;
        }

        public void OnExit()
        {
            if(!_isScaledUp) return;
            transform.DOScale(_originalScale, .5f).SetEase(tweenEase);
            _isScaledUp = false;
        }
    }
}
