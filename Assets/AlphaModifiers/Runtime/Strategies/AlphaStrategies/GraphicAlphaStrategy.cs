using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatCode.AlphaModifiers
{
    [Serializable]
    public sealed class GraphicAlphaStrategy : IAlphaModifierStrategy
    {
        [Serializable]
        public sealed class GraphicAlphaTarget
        {
            public Graphic graphic;
            public MinMaxFloat alpha;

            public GraphicAlphaTarget(Graphic graphic, MinMaxFloat alpha)
            {
                this.graphic = graphic;
                this.alpha = alpha;
            }
        }

        [SerializeField] private List<GraphicAlphaTarget> _targets = new();

        public GraphicAlphaStrategy() { }

        public GraphicAlphaStrategy(List<GraphicAlphaTarget> targets)
        {
            _targets = targets;
        }

        public void SetAlpha(float value)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];
                var graphic = target.graphic;
                var alpha = Mathf.Lerp(target.alpha.min, target.alpha.max, value);
                var color = graphic.color;
                color.a = alpha;
                graphic.color = color;
            }
        }
    }
}