﻿using System.Collections;
using UnityEngine;

namespace Crosstales.UI
{
    /// <summary>Controls a UI group (hint).</summary>
    public class UIHint : MonoBehaviour
    {

        #region Variables

        /// <summary>Group to fade.</summary>
        [Tooltip("Group to fade.")]
        public CanvasGroup Group;

        /// <summary>Delay in seconds before fading (default: 2).</summary>
        [Tooltip("Delay in seconds before fading (default: 2).")]
        public float Delay = 2f;

        /// <summary>Fade time in seconds (default: 2).</summary>
        [Tooltip("Fade time in seconds (default: 2).")]
        public float FadeTime = 2f;

        /*
        /// <summary>Destroy UI element after the fade (default: true).</summary>
        [Tooltip("Destroy UI element after the fade (default: true).")]
        public bool DestroyWhenFinished = true;
        */

        /// <summary>Disable UI element after the fade (default: true).</summary>
        [Tooltip("Disable UI element after the fade (default: true).")]
        public bool Disable = true;

        /// <summary>Fade at Start (default: true).</summary>
        [Tooltip("Fade at Start (default: true).")]
        public bool FadeAtStart = true;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            if (FadeAtStart)
                FadeDown();
        }

        #endregion


        #region Public methods

        public void FadeUp()
        {
            StartCoroutine(LerpAlphaUp(0f, 1f, FadeTime, Delay, Group));
        }

        public void FadeDown()
        {
            StartCoroutine(LerpAlphaDown(1f, 0f, FadeTime, Delay, Group));
        }

        /*
        public void Fade()
        {
            StartCoroutine(fadeTo(0f, Delay, FadeTime, DestroyWhenFinished));
        }
        */

        #endregion


        #region Private methods
        public IEnumerator LerpAlphaDown(float startAlphaValue, float endAlphaValue, float time, float delay, CanvasGroup gameObjectToFade)
        {
            gameObjectToFade.gameObject.SetActive(true);

            Group.alpha = Mathf.Clamp01(startAlphaValue);
            endAlphaValue = Mathf.Clamp01(endAlphaValue);

            yield return new WaitForSeconds(delay);

            while (Group.alpha >= endAlphaValue)
            {
                Group.alpha -= ((1f - endAlphaValue) / time) * Time.deltaTime;
                yield return null;
            }

            gameObjectToFade.gameObject.SetActive(!Disable);
        }

        public IEnumerator LerpAlphaUp(float startAlphaValue, float endAlphaValue, float time, float delay, CanvasGroup gameObjectToFade)
        {
            gameObjectToFade.gameObject.SetActive(true);

            Group.alpha = Mathf.Clamp01(startAlphaValue);
            endAlphaValue = Mathf.Clamp01(endAlphaValue);

            yield return new WaitForSeconds(delay);

            while (Group.alpha <= endAlphaValue)
            {
                Group.alpha += (endAlphaValue / time) * Time.deltaTime;
                yield return null;
            }

            gameObjectToFade.gameObject.SetActive(!Disable);
        }

        /*
        private IEnumerator fadeTo(float aValue, float delay, float aTime, bool destroy)
        {
            yield return new WaitForSeconds(delay);

            float alpha = Group.alpha;

            for (float t = 0f; t < 1f; t += Time.deltaTime / aTime)
            {
                //Debug.Log(Group.alpha + " - " + t);

                Group.alpha = Mathf.Lerp(alpha, aValue, t);
                yield return null;
            }

            if (destroy)
                Destroy(gameObject, .5f);
        }
        */

        #endregion
    }
}
// © 2018 crosstales LLC (https://www.crosstales.com)