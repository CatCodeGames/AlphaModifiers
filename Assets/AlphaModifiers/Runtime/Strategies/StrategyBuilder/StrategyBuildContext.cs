using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CatCode.AlphaModifiers
{
    public sealed class StrategyBuildContext : IDisposable
    {
        private readonly IStrategyBuilder[] _builders;

        public StrategyBuildContext()
        {
            _builders = new IStrategyBuilder[]
            {
                    new StrategyBuilder<SpriteRenderer, SpriteAlphaStrategy>((sprites) =>
                    {
                        var targets = sprites.Select(sprite => new SpriteAlphaStrategy.SpriteAlphaTarget(sprite, MinMaxFloat.ZeroToOne)).ToList();
                        return new SpriteAlphaStrategy(targets);
                    }),
                    new StrategyBuilder<ParticleSystem, ParticleSystemAlphaStrategy>((particles) =>
                        new ParticleSystemAlphaStrategy(particles.ToList())),
                    new StrategyBuilder<TrailRenderer, TrailAlphaStrategy>((trails) =>
                        new TrailAlphaStrategy(trails.ToList())),
                    new StrategyBuilder<CanvasGroup, CanvasGroupAlphaStrategy>((groups) =>
                    {
                        var targets = groups.Select(group => new CanvasGroupAlphaStrategy.CanvasGroupTarget(group, MinMaxFloat.ZeroToOne)).ToList();
                        return new CanvasGroupAlphaStrategy(targets);
                    }),
                    new StrategyBuilder<Graphic, GraphicAlphaStrategy>((graphics) =>
                    {
                        var targets = graphics.Select(graphic => new GraphicAlphaStrategy.GraphicAlphaTarget(graphic, MinMaxFloat.ZeroToOne)).ToList();
                        return new GraphicAlphaStrategy(targets);
                    })
            };
        }
                
        public void CollectComponentsFrom(Transform t)
        {
            foreach (var builder in _builders)
                builder.CollectComponentsFrom(t);
        }

        public List<IAlphaModifierStrategy> BuildStrategies()
        {
            var result = new List<IAlphaModifierStrategy>(_builders.Length);

            foreach (var builder in _builders)
            {
                if (!builder.HasComponents)
                    continue;
                var strategy = builder.BuildStrategy();
                result.Add(strategy);
            }

            return result;
        }

        public void Dispose()
        {
            foreach (var builder in _builders)
                builder.Dispose();
        }
    }
}
