using Sandwich;
using UnityEngine;
using DG.Tweening;
using Interfaces;

namespace Intractable
{
    //Logic for maintaining ingredients that are already on the stack
    public class IngredientStackSelectionResponse : MonoBehaviour , IClickResponse
    {
        //private members
        private SandwichController _sandwichController;
        private IngredientSlice _ingredientSlice;
        private bool _isActive;
        private float _activeTime;
        private Sequence _shakeSequence;
        
        //exposed fields
        [SerializeField] private float timeBeforeConfirm;
        [SerializeField] private float shakeStrength;
        [SerializeField] private GameObject particlePrefab;
        
        private void Awake()
        {
            _sandwichController = FindObjectOfType<SandwichController>();
            _ingredientSlice = GetComponent<IngredientSlice>();
        }

        public void OnDown()
        {
            //change response based on win state
            if (_sandwichController.HasWon)
            {
                _sandwichController.TakeBite();
                return;
            }
            _isActive = true;
            _shakeSequence.Prepend(transform.DOShakePosition(timeBeforeConfirm, shakeStrength).SetEase(Ease.InOutCubic)); // shake ingredient as confirmation
        }

        private void Update()
        {
            if(!_isActive) return;

            //Add time until threshold 
            if (_activeTime < timeBeforeConfirm)
            {
                _activeTime += Time.deltaTime;
            }
            else
            {
                _activeTime = 0;
                if (_sandwichController.TryRemoveSlice(_ingredientSlice)) // Remove the slice
                {
                    _sandwichController.PopStack();
                    _shakeSequence.Kill();
                    Instantiate(particlePrefab, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }

        //Dereference 
        public void OnUp()
        {
            _isActive = false;
            _activeTime = 0;
            _shakeSequence.Kill();
        }
    }
}