using System;
using UnityEngine;

namespace CatCode.AlphaModifiers
{
    public interface IStrategyBuilder : IDisposable
    {
        void CollectComponentsFrom(Transform t);
        bool HasComponents { get; }
        IAlphaModifierStrategy BuildStrategy();
    }
}