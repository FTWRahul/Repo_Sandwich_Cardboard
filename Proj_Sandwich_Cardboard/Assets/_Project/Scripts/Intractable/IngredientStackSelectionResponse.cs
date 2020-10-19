using Sandwich;
using UnityEngine;
using DG.Tweening;
namespace Intractable
{
    public class IngredientStackSelectionResponse : BaseSelectionResponse
    {
        [SerializeField] private float timeBeforeConfirm;
        [SerializeField] private float shakeStrength;
        [SerializeField] private GameObject particlePrefab;

        private SandwichController _sandwichController;
        private bool _isActive;
        private float _activeTime;
        private Sequence _shakeSequence;

        private void Awake()
        {
            _sandwichController = FindObjectOfType<SandwichController>();
        }

        public override void OnDown()
        {
            if (_sandwichController.HasWon)
            {
                _sandwichController.TakeBite();
                return;
            }
            base.OnDown();
            _isActive = true;
            _shakeSequence.Prepend(transform.DOShakePosition(timeBeforeConfirm, shakeStrength).SetEase(Ease.InOutCubic));
        }

        private void Update()
        {
            if(!_isActive) return;

            if (_activeTime < timeBeforeConfirm)
            {
                _activeTime += Time.deltaTime;
            }
            else
            {
                _activeTime = 0;
                if (_sandwichController.TryRemoveSlice(this))
                {
                    _sandwichController.PopStack();
                    _shakeSequence.Kill();
                    Instantiate(particlePrefab, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }

        public override void OnUp()
        {
            base.OnUp();
            _isActive = false;
            _activeTime = 0;
            _shakeSequence.Kill();
        }
    }
}