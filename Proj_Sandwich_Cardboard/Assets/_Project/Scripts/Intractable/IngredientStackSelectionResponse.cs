using System;
using UnityEngine;

namespace Intractable
{
    public class IngredientStackSelectionResponse : BaseSelectionResponse
    {
        [SerializeField] private float timeBeforeConfirm;

        private bool _isActive;
        
        public override void OnDown()
        {
            base.OnDown();
            _isActive = true;
        }

        private void Update()
        {
            throw new NotImplementedException();
        }

        public override void OnUp()
        {
            base.OnUp();
            _isActive = false;
        }
    }
}