using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatCode.AlphaModifiers
{
    [Serializable]
    public sealed class CanvasGroupAlphaStrategy : IAlphaModifierStrategy
    {
        [Serializable]
        public sealed class CanvasGroupTarget
        {
            public CanvasGroup canvasGroup;
            public MinMaxFloat alpha;

            public CanvasGroupTarget(CanvasGroup canvasGroup, MinMaxFloat alpha)
            {
                this.canvasGroup = canvasGroup;
                this.alpha = alpha;
            }
        }

        [SerializeField] private List<CanvasGroupTarget> _targets = new();

        public CanvasGroupAlphaStrategy() { }
        public CanvasGroupAlphaStrategy(List<CanvasGroupTarget> targets)
        {
            _targets = targets;
        }


        public void SetAlpha(float value)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];
                var canvasGroup = target.canvasGroup;
                var alpha = Mathf.Lerp(target.alpha.min, target.alpha.max, value);
                canvasGroup.alpha = alpha;
            }
        }
    }
}