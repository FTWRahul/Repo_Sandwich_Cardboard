using System;
using ScriptableObjects;
using UnityEngine;

namespace Sandwich
{
    //In-game representation of an Ingredient, takes a SO and populates the prefab with its data.
    //Dynamically create ingredients based on passed Data.
    public class IngredientSlice : MonoBehaviour
    {
        //private memebers
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private IngredientSo _ingredientSo;
        
        //exposed fields
        [SerializeField] private GameObject displayGameObject; 
        
        //public properties
        public IngredientSo IngredientSo => _ingredientSo;
        
        private Material Material
        {
            set => _meshRenderer.material = value;
        }

        private Mesh Mesh
        {
            set => _meshFilter.mesh = value;
        }

        private void Awake()
        {
            _meshRenderer = displayGameObject.GetComponent<MeshRenderer>();
            _meshFilter = displayGameObject.GetComponent<MeshFilter>();
        }

        //Initial setup
        public void Initialize(IngredientSo data)
        {
            _ingredientSo = data;
            gameObject.name = data.IngredientName;
            UpdateMesh(0);
            Material = data.IngredientMaterial;
        }

        //Change the mesh being displayed based on passed index
        public void UpdateMesh(int index)
        {
            if (index >= _ingredientSo.IngredientMesh.Length) // error handling
            {
                Mesh = null;
                return;
            }
            //index = Mathf.Clamp(index, 0, _ingredientSo.IngredientMesh.Length - 1);
            Mesh = _ingredientSo.IngredientMesh[index];
        }
        
    }
}
