using Sandwich;
using UnityEngine;
using DG.Tweening;
using Interfaces;

namespace Intractable
{
    public class IngredientStackSelectionResponse : MonoBehaviour , IClickResponse
    {
        [SerializeField] private float timeBeforeConfirm;
        [SerializeField] private float shakeStrength;
        [SerializeField] private GameObject particlePrefab;

        private SandwichController _sandwichController;
        private IngredientSlice _ingredientSlice;
        private bool _isActive;
        private float _activeTime;
        private Sequence _shakeSequence;

        private void Awake()
        {
            _sandwichController = FindObjectOfType<SandwichController>();
            _ingredientSlice = GetComponent<IngredientSlice>();
        }

        public void OnDown()
        {
            if (_sandwichController.HasWon)
            {
                _sandwichController.TakeBite();
                return;
            }
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
                if (_sandwichController.TryRemoveSlice(_ingredientSlice))
                {
                    _sandwichController.PopStack();
                    _shakeSequence.Kill();
                    Instantiate(particlePrefab, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }

        public void OnUp()
        {
            _isActive = false;
            _activeTime = 0;
            _shakeSequence.Kill();
        }
    }
}