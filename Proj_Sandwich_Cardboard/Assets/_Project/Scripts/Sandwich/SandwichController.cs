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
        [SerializeField] private IngredientSo startingBread;
        
        private Stack<IngredientStackSelectionResponse> _stackedIngredients = new Stack<IngredientStackSelectionResponse>();

        private void Awake()
        {
            SpawnStackSlice(startingBread);
        }

        public bool TryPlaceSlice(IngredientSo data, Vector3 position)
        {
            Debug.Log("Distance " + Vector3.Distance(transform.position, position));
            if (Vector3.Distance(transform.position, position) > stackDistanceThreshold) return false;
            
            if (_stackedIngredients.Count >= 0 && _stackedIngredients.Count < maxStackSize)
            {
                SpawnStackSlice(data);
                return true;
            }
            //TODO: In game log, too many ingredients!!
            return false;
        }

        private void SpawnStackSlice(IngredientSo data)
        {
            var stackPosition = new Vector3(transform.position.x,
                transform.position.y + _stackedIngredients.Count * sliceHeightOffset, transform.position.z);
            IngredientSlice spawnedSlice = Instantiate(stackSlicePrefab, stackPosition, Quaternion.identity)
                .GetComponent<IngredientSlice>();
            spawnedSlice.Initialize(data);
            _stackedIngredients.Push(spawnedSlice.GetComponent<IngredientStackSelectionResponse>());
        }

        public bool TryRemoveSlice(IngredientStackSelectionResponse checkAgainst)
        {
            return _stackedIngredients.Count > 1 && _stackedIngredients.Peek() == checkAgainst;
        }

        public void PopStack()
        {
            _stackedIngredients.Pop();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, stackDistanceThreshold);
        }
    }
}