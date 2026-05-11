using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CatCode.AlphaModifiers
{
    public sealed class StrategyBuilder<TComponent, TStrategy> : IStrategyBuilder
        where TComponent : Component
        where TStrategy : IAlphaModifierStrategy
    {
        private List<TComponent> _components;
        private readonly Func<List<TComponent>, TStrategy> _strategyFactory;

        public bool HasComponents => _components == null ? false : _components.Count > 0;

        public StrategyBuilder(Func<List<TComponent>, TStrategy> strategyFactory)
        {
            _strategyFactory = strategyFactory;
            _components = ListPool<TComponent>.Get();
        }

        public void CollectComponentsFrom(Transform t)
            => _components.AddRange(t.GetComponents<TComponent>());

        public IAlphaModifierStrategy BuildStrategy()
            => _strategyFactory(_components);

        public void Dispose()
        {
            if (_components == null)
                return;
            ListPool<TComponent>.Release(_components);
            _components = null;
        }
    }
}