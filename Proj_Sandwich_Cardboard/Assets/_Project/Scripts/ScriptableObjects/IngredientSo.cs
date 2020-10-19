using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewIngredient", menuName = "Sandwich/Create Ingredient", order = 0)]
    public class IngredientSo : ScriptableObject
    {
        [SerializeField] private string ingredientName;
        [SerializeField] private Mesh[] ingredientMesh;
        [SerializeField] private Material ingredientMaterial;

        public string IngredientName => ingredientName;

        public Mesh[] IngredientMesh => ingredientMesh;

        public Material IngredientMaterial => ingredientMaterial;
    }
}