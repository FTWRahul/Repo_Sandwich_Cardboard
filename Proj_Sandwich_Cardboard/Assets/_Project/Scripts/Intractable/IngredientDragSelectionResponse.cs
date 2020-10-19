using Interfaces;
using Sandwich;
using ScriptableObjects;
using UnityEngine;

namespace Intractable
{
    public class IngredientDragSelectionResponse : MonoBehaviour, IClickResponse
    {
        //private members
        private IngredientMover _ingredientMover;
        private MenuDrawer _menuDrawer;
        private IngredientSlice _ingredientSlice;
        
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private bool _isActive;

        //exposed fields
        [SerializeField] private float lerpSpeed;

        //properties
        public IngredientSo SliceIngredientSo => _ingredientSlice.IngredientSo; 
        
        private void Awake()
        {
            _ingredientMover = FindObjectOfType<IngredientMover>();
            _menuDrawer = GetComponentInParent<MenuDrawer>();
            _ingredientSlice = GetComponent<IngredientSlice>();
            
            // is called after menuDrawer is initialized since it is the one assigning the start position.
            _menuDrawer.OnInitialized += LogStartTransform; 
        }

        //Track start position and rotation
        private void LogStartTransform()
        {
            var thisTransform = transform;
            _originalPosition = thisTransform.localPosition;
            _originalRotation = thisTransform.localRotation;
        }

        //Assigns reference to the IngredientMover
        public void OnDown()
        {
            _ingredientMover.SliceSelectionResponse = this;
            _isActive = true;
        }
        
        private void Update()
        {
            if (_isActive)
            {
                //While being held used the ingredientMover's logic
                _ingredientMover.SmoothLerpTowardsTarget();
            }
            else //go back to origin
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _originalPosition, Time.deltaTime * lerpSpeed);
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, _originalRotation, Time.deltaTime * lerpSpeed);
            }
        }

        //Un-assign references
        public void OnUp()
        {
            _ingredientMover.SliceSelectionResponse = null;
            _isActive = false;
        }

        //Reset position instantly (User Experience)
        public void SnapBackToOrigin()
        {
            OnUp(); // needs to resolve dereferencing 
            transform.localPosition = _originalPosition;
            transform.localRotation = _originalRotation;
        }

        private void OnDisable()
        {
            _menuDrawer.OnInitialized -= LogStartTransform;
        }
    }
}