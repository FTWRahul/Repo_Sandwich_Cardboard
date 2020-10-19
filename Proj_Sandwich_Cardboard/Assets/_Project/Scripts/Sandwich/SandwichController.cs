using System;
using System.Collections.Generic;
using Intractable;
using Logger;
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
        private int _meshIndex = 1;

        public bool HasWon => CheckWin();

        private void Start()
        {
            SpawnStackSlice(startingBread);
        }

        public bool TryPlaceSlice(IngredientSo data, Vector3 position)
        {
            if (HasWon)
            {
                LogManager.Instance.Log("Enjoy your meal :D");
                return true;
            }
            if (Vector3.Distance(transform.position, position) > stackDistanceThreshold) return false;
            
            if (_stackedIngredients.Count >= 0 && _stackedIngredients.Count < maxStackSize)
            {
                SpawnStackSlice(data);
                return true;
            }
            LogManager.Instance.Log("That's too big of a sandwich \n Remove a slice and seal the deal with another bread slice!");
            return false;
        }

        private void SpawnStackSlice(IngredientSo data)
        {
            var stackPosition = new Vector3(transform.position.x,
                transform.position.y + _stackedIngredients.Count * sliceHeightOffset, transform.position.z);
            IngredientSlice spawnedSlice = Instantiate(stackSlicePrefab, stackPosition, Quaternion.Euler(0, 180, 0))
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

        public bool CheckWin()
        {
            return _stackedIngredients.Count > 2 &&
                   _stackedIngredients.Peek().GetComponent<IngredientSlice>().IngredientSo == startingBread;
        }
        
        public void TakeBite()
        {
            if (_meshIndex >= 3)
            {
                foreach (var slice in _stackedIngredients)
                {
                    Destroy(slice.gameObject);
                }
                _meshIndex = 1;
                _stackedIngredients.Clear();
                SpawnStackSlice(startingBread);
                LogManager.Instance.Log("Hungry for more?");
                return;
            }
            foreach (var slice in _stackedIngredients)
            {
                slice.GetComponent<IngredientSlice>().UpdateMesh(_meshIndex);
            }
            LogManager.Instance.Log("Nom!");

            _meshIndex++;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, stackDistanceThreshold);
        }
    }
}