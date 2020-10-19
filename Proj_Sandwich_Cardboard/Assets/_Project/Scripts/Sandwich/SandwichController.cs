using System;
using System.Collections.Generic;
using Intractable;
using ScriptableObjects;
using UnityEngine;

namespace Sandwich
{
    public class SandwichController : MonoBehaviour
    {
        [SerializeField] private int maxStackSize;
        [SerializeField] private GameObject stackSlicePrefab;
        [SerializeField] private float sliceHeightOffset;
        [SerializeField] private float stackDistanceThreshold;
        
        private Stack<IngredientStackSelectionResponse> _stackedIngredients = new Stack<IngredientStackSelectionResponse>();

        public bool TryPlaceSlice(IngredientSo data, Vector3 position)
        {
            if (Vector3.Distance(transform.position, position) > stackDistanceThreshold) return false;
            
            if (_stackedIngredients.Count > 1 && _stackedIngredients.Count < maxStackSize)
            {
                var stackPosition = new Vector3(transform.position.x,
                    transform.position.y + _stackedIngredients.Count * sliceHeightOffset, transform.position.z);
                IngredientSlice spawnedSlice = Instantiate(stackSlicePrefab, stackPosition, Quaternion.identity).GetComponent<IngredientSlice>();
                spawnedSlice.Initialize(data);
                _stackedIngredients.Push(spawnedSlice.GetComponent<IngredientStackSelectionResponse>());
                return true;
            }
            //TODO: In game log, too many ingredients!!
            return false;
        }

        public bool TryRemoveSlice(IngredientStackSelectionResponse checkAgainst)
        {
            return _stackedIngredients.Count < 2 && _stackedIngredients.Peek() == checkAgainst;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position, stackDistanceThreshold);
        }
    }
}