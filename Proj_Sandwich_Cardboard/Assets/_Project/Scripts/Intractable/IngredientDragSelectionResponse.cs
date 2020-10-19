using System;
using Interfaces;
using Sandwich;
using ScriptableObjects;
using UnityEngine;

namespace Intractable
{
    public class IngredientDragSelectionResponse : BaseSelectionResponse
    {
        private IngredientMover _ingredientMover;
        private MenuDrawer _menuDrawer;
        private IngredientSlice _ingredientSlice;
        
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private bool _isActive;

        [SerializeField] private float lerpSpeed;

        public IngredientSo SliceIngredientSo => _ingredientSlice.IngredientSo; 
        
        private void Awake()
        {
            _ingredientMover = FindObjectOfType<IngredientMover>();
            _menuDrawer = GetComponentInParent<MenuDrawer>();
            _ingredientSlice = GetComponent<IngredientSlice>();
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
            _isActive = false;
        }

        public void SnapBackToOrigin()
        {
            OnUp();
            transform.localPosition = _originalPosition;
            transform.localRotation = _originalRotation;
        }

        private void OnDisable()
        {
            _menuDrawer.OnInitialized -= LogStartTransform;
        }
    }
}