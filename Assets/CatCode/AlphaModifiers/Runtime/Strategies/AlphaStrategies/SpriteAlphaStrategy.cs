using CatCode.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatCode.AlphaModifiers
{
    [Serializable]
    public sealed class SpriteAlphaStrategy : IAlphaModifierStrategy
    {
        [Serializable]
        public sealed class SpriteAlphaTarget
        {
            public SpriteRenderer renderer;
            public MinMaxFloat alpha;

            public SpriteAlphaTarget(SpriteRenderer renderer, MinMaxFloat alpha)
            {
                this.renderer = renderer;
                this.alpha = alpha;
            }
        }

        [SerializeField] private List<SpriteAlphaTarget> _targets = new();

        public SpriteAlphaStrategy() { }

        public SpriteAlphaStrategy(List<SpriteAlphaTarget> targets)
        {
            _targets = targets;
        }

        public void SetAlpha(float value)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];
                var renderer = target.renderer;
                var alpha = Mathf.Lerp(target.alpha.min, target.alpha.max, value);
                var color = renderer.color;
                color.a = alpha;
                renderer.color = color;
            }
        }
    }
}