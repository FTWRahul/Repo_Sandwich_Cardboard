using System;
using ScriptableObjects;
using UnityEngine;

namespace Sandwich
{
    //Ingame representation of an Ingredient, takes a SO and populates the prefab with its data.
    //Dynamically create ingredients based on passed Data.
    public class IngredientSlice : MonoBehaviour
    {
        //[SerializeField] private IngredientSo testSo; 
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private IngredientSo _ingredientSo;

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
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void Initialize(IngredientSo data)
        {
            _ingredientSo = data;
            gameObject.name = data.IngredientName;
            Mesh = data.IngredientMesh;
            Material = data.IngredientMaterial;
        }

        // [ContextMenu( "Test Initialization")]
        // public void InitTest()
        // {
        //     Initialize(testSo);
        // }
    }
}
