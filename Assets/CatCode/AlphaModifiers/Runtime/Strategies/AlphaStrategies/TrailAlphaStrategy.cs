using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatCode.AlphaModifiers
{
    [Serializable]
    public sealed class TrailAlphaStrategy : IAlphaModifierStrategy
    {
        private struct Data
        {
            public GradientAlphaKey[] alphaKeys;
            public Gradient gradient;
        }

        private bool _initialized;
        private int _count;
        private Data[] _data;

        [SerializeField] private List<TrailRenderer> _targets = new();

        public TrailAlphaStrategy() { }

        public TrailAlphaStrategy(List<TrailRenderer> targets)
        {
            _targets = targets;
        }

        private void Initialize()
        {
            if (_initialized)
                return;
            _initialized = true;

            _count = _targets.Count;
            _data = new Data[_count];

            for (int i = 0; i < _count; i++)
            {
                var target = _targets[i];
                _data[i] = new Data()
                {
                    alphaKeys = target.colorGradient.alphaKeys,
                    gradient = target.colorGradient
                };
            }
        }

        public void SetAlpha(float value)
        {
            Initialize();

            for (int i = 0; i < _count; i++)
            {
                var data = _data[i];
                GradientUtils.UpdateAlphaKeys(data.gradient, data.alphaKeys, value);
            }
        }
    }
}