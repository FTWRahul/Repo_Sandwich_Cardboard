using UnityEngine;
using DG.Tweening;

namespace General
{
    public class ScaleObject : MonoBehaviour
    {
        [SerializeField] private float scaleFactor;
        [SerializeField] private Ease tweenEase;
        
        private bool _isScaledUp;
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void ScaleUp()
        {
            if(_isScaledUp) return;
            transform.DOScale(_originalScale * scaleFactor, .5f).SetEase(tweenEase);
            _isScaledUp = true;
        }

        public void ScaleDown()
        {
            if(!_isScaledUp) return;
            transform.DOScale(_originalScale, .5f).SetEase(tweenEase);
            _isScaledUp = false;
        }
    }
}
