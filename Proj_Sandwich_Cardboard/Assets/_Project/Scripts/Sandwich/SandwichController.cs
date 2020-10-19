using System.Collections.Generic;
using Audio;
using Logger;
using ScriptableObjects;
using UnityEngine;

namespace Sandwich
{
    /// <summary>
    /// Maintains the state of the sandwich and provides checks of operations made on and by it. 
    /// </summary>
    public class SandwichController : MonoBehaviour
    {
        //private members
        private AudioManager _audioManager;
        private LogManager _logManager;
        //a stack best represents our use case!
        private Stack<IngredientSlice> _stackedIngredients = new Stack<IngredientSlice>();
        private int _meshIndex = 1;

        //exposed fields
        [SerializeField] private int maxStackSize;
        [SerializeField] private GameObject stackSlicePrefab;
        [SerializeField] private float sliceHeightOffset;
        [SerializeField] private float stackDistanceThreshold;
        [SerializeField] private IngredientSo startingBread;
        [SerializeField] private GameObject particlePrefab;
        
        //public properties
        public bool HasWon => CheckWin();

        private void Awake()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _logManager = FindObjectOfType<LogManager>();
        }

        private void Start()
        {
            //Place initial bread lice
            SpawnStackSlice(startingBread);
        }

        //Returns a bool based on if the bread can and was placed successfully
        public bool TryPlaceSlice(IngredientSo data, Vector3 position)
        {
            if (HasWon)
            {
                _logManager.Log("Finish what you started! :D");
                return true; // returns true to trigger the object to go back.
            }
            // Distance check to see if slice can be stacked
            if (Vector3.Distance(transform.position, position) > stackDistanceThreshold) return false;
            
            if (_stackedIngredients.Count >= 0 && _stackedIngredients.Count < maxStackSize) //Error handling and limiting stack height
            {
                SpawnStackSlice(data);
                _audioManager.PlayPopSound();
                return true;
            }
            _logManager.Log("Error : Sandwich Overflow \n Remove the top slice and seal the deal \n with another piece of bread!");
            return false;
        }

        //Instantiation logic and reference assignment
        private void SpawnStackSlice(IngredientSo data)
        {
            var stackPosition = new Vector3(
                   transform.position.x, 
                transform.position.y + _stackedIngredients.Count * sliceHeightOffset,
                   transform.position.z);
            IngredientSlice spawnedSlice = Instantiate(stackSlicePrefab, stackPosition, Quaternion.Euler(0, 270, 0))
                .GetComponent<IngredientSlice>();
            spawnedSlice.Initialize(data);
            Instantiate(particlePrefab, spawnedSlice.transform.position, Quaternion.identity);
            _stackedIngredients.Push(spawnedSlice);
        }

        // makes sure that the bottom bread does not get removed (User Experience) and only the top slice can be removed
        public bool TryRemoveSlice(IngredientSlice checkAgainst)
        {
            return _stackedIngredients.Count > 1 && _stackedIngredients.Peek() == checkAgainst;
        }

        public void PopStack()
        {
            _stackedIngredients.Pop();
        }

        //Returns true if the top slice is a bread and its at least 2 pieces of bread
        public bool CheckWin()
        {
            return _stackedIngredients.Count >= 2 && _stackedIngredients.Peek().IngredientSo == startingBread;
        }
        
        //Upon winning, the response of stacked ingredients points to this.
        //Updates the mesh of all our slices in the stack and resets upon completion
        public void TakeBite()
        {
            _audioManager.PlayBiteSound();
            
            if (_meshIndex >= 3)
            {
                foreach (var slice in _stackedIngredients)
                {
                    Destroy(slice.gameObject);
                }
                //TODO: extract functions?
                _meshIndex = 1;
                _stackedIngredients.Clear();
                SpawnStackSlice(startingBread);
                _logManager.Log("Hungry for more?");
            }
            else
            {
                foreach (var slice in _stackedIngredients)
                {
                    slice.UpdateMesh(_meshIndex);
                }
                _logManager.Log("Nom!");
                _meshIndex++;
            }
            
        }
        //Visual representation in editor of how far an objects need to be to trigger the place function
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, stackDistanceThreshold);
        }
    }
}