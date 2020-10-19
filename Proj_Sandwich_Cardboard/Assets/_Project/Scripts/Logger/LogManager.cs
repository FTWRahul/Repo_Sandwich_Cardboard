using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ExtensionMethods;
using TMPro;

namespace Logger
{
    public class LogManager : MonoBehaviour
    {
        [SerializeField] private Color highlightColor;
        [SerializeField] private float timeToFade;
        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private TextMeshProUGUI displayText;

        private void Start()
        {
            Log("Open some Drawers \n and make some sandwiches!");
        }

        public void Log(string message)
        {
            displayText.text = message;
            displayText.color = highlightColor;
            StopAllCoroutines();
            StartCoroutine(FadeText());
        }

        public IEnumerator FadeText()
        {
            float t = 0;
            while (t < timeToFade)
            {
                var color = displayText.color;
                color.a = Mathf.Lerp(color.a, 0, animationCurve.Evaluate(t.Remap(0, timeToFade, 0, 1)));
                displayText.color = color;
                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        [ContextMenu("Test Fade")]
        public void TestFade()
        {
            StartCoroutine(FadeText());
        }

        [ContextMenu("Send Log Message")]
        public void TestNewLog()
        {
            Log("This is a new Test, if everything works fine this should disappear soon");
        }
    }
}
