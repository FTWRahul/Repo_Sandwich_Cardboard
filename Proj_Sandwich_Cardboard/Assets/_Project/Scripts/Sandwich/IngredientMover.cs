using Intractable;
using UnityEngine;

namespace Sandwich
{
    public class IngredientMover : MonoBehaviour
    {
        private IngredientDragSelectionResponse _sliceSelectionResponse;

        [SerializeField] private float lerpSpeed;
        
        private bool _isActive => SliceSelectionResponse != null;

        public IngredientDragSelectionResponse SliceSelectionResponse
        {
            get => _sliceSelectionResponse;
            set => _sliceSelectionResponse = value;
        }

        //TODO: lerp slice to position
        public void SmoothLerpTowardsTarget()
        {
            if(!_isActive) return;
            _sliceSelectionResponse.transform.position = Vector3.Lerp(_sliceSelectionResponse.transform.position, transform.position, Time.deltaTime * lerpSpeed);
            _sliceSelectionResponse.transform.localRotation =
                Quaternion.Slerp(_sliceSelectionResponse.transform.localRotation, Quaternion.Euler(new Vector3(0,transform.rotation.eulerAngles.y, 0)), Time.deltaTime * lerpSpeed);
            //_sliceIntractable.transform.position
        }
    }
}