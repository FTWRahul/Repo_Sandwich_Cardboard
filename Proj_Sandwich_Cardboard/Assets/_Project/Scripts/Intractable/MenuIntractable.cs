using Sandwich;
using ScriptableObjects;
using UnityEngine;

namespace Intractable
{
    //An Intractable that holds all the Ingredients for sandwich assembly
    public class MenuIntractable : BaseIntractable
    {
        [SerializeField] private Vector3 drawerOffset;

        private bool _isActive;
        
        public override void OnClick()
        {
            _isActive = !_isActive;

            if (_isActive)
            {
                transform.position += drawerOffset;
            }
            else
            {
                transform.position -= drawerOffset;
            }
            base.OnClick();
        }
    }
}