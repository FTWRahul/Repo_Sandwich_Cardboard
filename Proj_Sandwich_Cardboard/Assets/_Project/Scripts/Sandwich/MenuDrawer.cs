using System;
using ScriptableObjects;
using UnityEngine;

namespace Sandwich
{
    //initializes a drawer of ingredients based on resources loaded
    public class MenuDrawer : MonoBehaviour
    {
        //Constant path in resource folder
        private const string INGREDIENT_PATH = "Sandwich/IngredientSOs";
        
        //exposed fields
        [SerializeField] private GameObject ingredientPrefab;
        [SerializeField] private Transform parentTransform;
        [Tooltip("Don't forget to prepend with a / !!")]
        [SerializeField] private string specificFolderName;
        [Tooltip("In the drawer how many rows exist?")]
        [SerializeField] private int width;
        [SerializeField] private float horizontalPadding, verticalPadding;
        
        //exposed event
        public event Action OnInitialized;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            var temp = Resources.LoadAll<IngredientSo>(INGREDIENT_PATH + specificFolderName);
            
            //Offsets the elements to form a grid pattern.
            int height = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                IngredientSlice slice = Instantiate(ingredientPrefab, parentTransform).GetComponent<IngredientSlice>();
                slice.Initialize(temp[i]);
                int tempWidth = (i % width);
                slice.transform.localPosition = new Vector3( tempWidth * horizontalPadding,0 , height * verticalPadding);
                if (tempWidth == width - 1)
                {
                    height++;
                }
            }
            //Invocation for ingredient response to update.
            OnInitialized?.Invoke();
        }
    }
}