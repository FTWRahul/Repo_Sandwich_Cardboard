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
        [SerializeField] private GameObject displayGameObject; 
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
            _meshRenderer = displayGameObject.GetComponent<MeshRenderer>();
            _meshFilter = displayGameObject.GetComponent<MeshFilter>();
        }

        public void Initialize(IngredientSo data)
        {
            _ingredientSo = data;
            gameObject.name = data.IngredientName;
            UpdateMesh(0);
            Material = data.IngredientMaterial;
        }

        public void UpdateMesh(int index)
        {
            if (index >= _ingredientSo.IngredientMesh.Length)
            {
                Mesh = null;
                return;
            }
            index = Mathf.Clamp(index, 0, _ingredientSo.IngredientMesh.Length - 1);
            Mesh = _ingredientSo.IngredientMesh[index];
        }
        

        // [ContextMenu( "Test Initialization")]
        // public void InitTest()
        // {
        //     Initialize(testSo);
        // }
    }
}
