using ScriptableObjects;
using UnityEngine;

namespace Sandwich
{
    public class MenuDrawer : MonoBehaviour
    {
        private const string INGREDIENT_PATH = "Sandwich/IngredientSOs";
        [SerializeField] private GameObject ingredientPrefab;
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            var temp = Resources.LoadAll<IngredientSo>(INGREDIENT_PATH);
            for (int i = 0; i < temp.Length; i++)
            {
                IngredientSlice slice = Instantiate(ingredientPrefab, transform).GetComponent<IngredientSlice>();
                slice.Initialize(temp[i]);
            }
        }
    }
}