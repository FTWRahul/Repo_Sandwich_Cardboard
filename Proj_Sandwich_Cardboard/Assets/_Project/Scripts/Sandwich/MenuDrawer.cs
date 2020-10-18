﻿using ScriptableObjects;
using UnityEngine;

namespace Sandwich
{
    public class MenuDrawer : MonoBehaviour
    {
        private const string INGREDIENT_PATH = "Sandwich/IngredientSOs";
        
        [SerializeField] private GameObject ingredientPrefab;
        [SerializeField] private Transform parentTransform;
        
        [Tooltip("In the drawer how many rows exist?")]
        [SerializeField] private int width;
        [SerializeField] private float horizontalPadding, verticalPadding;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            var temp = Resources.LoadAll<IngredientSo>(INGREDIENT_PATH);
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
        }
    }
}