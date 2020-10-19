using Sandwich;
using UnityEngine;

namespace Intractable
{
    public class IngredientStackSelectionResponse : BaseSelectionResponse
    {
        [SerializeField] private float timeBeforeConfirm;

        private SandwichController _sandwichController;
        private bool _isActive;
        private float _activeTime;

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
                    Destroy(this.gameObject);
                }
            }
        }

        public override void OnUp()
        {
            base.OnUp();
            _isActive = false;
            _activeTime = 0;
        }
    }
}