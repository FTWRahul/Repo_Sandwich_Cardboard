using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewIngredient", menuName = "Sandwich/Create Ingredient", order = 0)]
    //Contains Data about the 3D model and material of any given ingredient.
    public class IngredientSo : ScriptableObject
    {
        //exposed fields
        [SerializeField] private string ingredientName;
        [SerializeField] private Mesh[] ingredientMesh; // Stores 3 different meshes for depicting different bite states
        [SerializeField] private Material ingredientMaterial;

        //public properties
        public string IngredientName => ingredientName;
        public Mesh[] IngredientMesh => ingredientMesh;
        public Material IngredientMaterial => ingredientMaterial;
    }
}