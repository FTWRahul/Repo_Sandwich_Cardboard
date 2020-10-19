using System.Collections;
using ExtensionMethods;
using Interfaces;
using UnityEngine;

namespace Intractable
{
    //An Intractable that holds all the Ingredients for sandwich assembly
    public class MenuSelectionResponse : MonoBehaviour , IClickResponse
    {
        [SerializeField] private Vector3 drawerOffset;
        [SerializeField] private float animationDuration;
        [SerializeField] private AnimationCurve animationCurve;

        private bool _isActive;
        private bool _isAnimating;
        
        public void OnDown()
        {
             if(_isAnimating) return;
             
             _isActive = !_isActive;

            if (_isActive)
            {
                //transform.position += drawerOffset;
                StartCoroutine(AnimateDrawer(transform.position, transform.position += drawerOffset));
            }
            else
            {
                StartCoroutine(AnimateDrawer(transform.position, transform.position -= drawerOffset));
            }
        }

        public void OnUp()
        {
            //Debug.Log("Do Nothing");
        }

        private IEnumerator AnimateDrawer(Vector3 fromPosition, Vector3 toPosition)
        {
            _isAnimating = true;
            float t = 0;
            while (t < animationDuration)
            {
                transform.position = Vector3.Lerp(fromPosition, toPosition,
                    animationCurve.Evaluate(t.Remap(0, animationDuration, 0, 1)));
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _isAnimating = false;
        }
    }
}