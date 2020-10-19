using Intractable;
using UnityEngine;

namespace Sandwich
{
    //Class in-charge of linking an ingredient to the sandwich. takes care of movement, conditions and calls.
    public class IngredientMover : MonoBehaviour
    {
        //private members
        private IngredientDragSelectionResponse _sliceSelectionResponse;
        private SandwichController _sandwichController;
        
        //public fields
        [SerializeField] private float lerpSpeed;
        
        //properties
        private bool _isActive => SliceSelectionResponse != null;
        public IngredientDragSelectionResponse SliceSelectionResponse
        {
            get => _sliceSelectionResponse;
            set => _sliceSelectionResponse = value;
        }

        private void Awake()
        {
            _sandwichController = FindObjectOfType<SandwichController>();
        }

        //Moves the provided object towards the assigned in the inspector
        public void SmoothLerpTowardsTarget()
        {
            if(!_isActive) return;
            _sliceSelectionResponse.transform.position = Vector3.Lerp(_sliceSelectionResponse.transform.position, transform.position, Time.deltaTime * lerpSpeed);
            _sliceSelectionResponse.transform.localRotation =
                Quaternion.Slerp(_sliceSelectionResponse.transform.localRotation, Quaternion.Euler(new Vector3(0,transform.rotation.eulerAngles.y, 0)), Time.deltaTime * lerpSpeed);

            //checks to see if ingredient can be placed down.
            if (_sandwichController.TryPlaceSlice(_sliceSelectionResponse.SliceIngredientSo, _sliceSelectionResponse.transform.position))
            {
                //resolve ingredient in hand
                _sliceSelectionResponse.SnapBackToOrigin();
            }
        }
    }
}