using DG.Tweening;
using Interfaces;
using SceneHandling;
using UnityEngine;

namespace Intractable
{
    //Diegetic Interactable to exit back to menu
    public class DoorClickSelectionResponse : MonoBehaviour , IClickResponse
    {
        private bool _isActive;
        private float _activeTime;
        private Sequence _shakeSequence;

        [SerializeField] private float timeBeforeConfirm;
        [SerializeField] private float shakeStrength;
        
        public void OnDown()
        {
            _isActive = true;
            _shakeSequence.Prepend(transform.DOShakePosition(timeBeforeConfirm, shakeStrength).SetEase(Ease.InOutCubic));

        }

        private void Update()
        {
            if(!_isActive) return;

            if (_activeTime <= timeBeforeConfirm)
            {
                _activeTime += Time.deltaTime;
            }
            else
            {
                _activeTime = 0;
                _shakeSequence.Kill();
                SceneController.LoadSceneSingle(0);
            }
        }

        public void OnUp()
        {
            _shakeSequence.Kill();
            _isActive = false;
            _activeTime = 0;
        }
    }
}
