using System;
using Interfaces;
using Sandwich;
using UnityEngine;

namespace Intractable
{
    public class IngredientDragSelectionResponse : BaseSelectionResponse
    {
        private IngredientMover _ingredientMover;
        private MenuDrawer _menuDrawer;
        
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private bool _isActive;

        [SerializeField] private float lerpSpeed;
        private void Awake()
        {
            _ingredientMover = FindObjectOfType<IngredientMover>();
            _menuDrawer = GetComponentInParent<MenuDrawer>();
            _menuDrawer.OnInitialized += LogStartTransform;
        }

        private void LogStartTransform()
        {
            var thisTransform = transform;
            _originalPosition = thisTransform.localPosition;
            _originalRotation = thisTransform.localRotation;
        }

        public override void OnDown()
        {
            base.OnDown();
            _ingredientMover.SliceSelectionResponse = this;
            _isActive = true;
        }
        
        private void Update()
        {
            if (_isActive)
            {
                _ingredientMover.SmoothLerpTowardsTarget();
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _originalPosition, Time.deltaTime * lerpSpeed);
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, _originalRotation, Time.deltaTime * lerpSpeed);
            }
        }

        public override void OnUp()
        {
            base.OnUp();
            _ingredientMover.SliceSelectionResponse = null;
            //TODO: lerp back to original position and rotation.
            _isActive = false;
        }

        private void OnDisable()
        {
            _menuDrawer.OnInitialized -= LogStartTransform;
        }
    }
}