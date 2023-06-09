﻿using JetBrains.Annotations;

namespace CombatDicesTeam.Graphs.Layout;

[PublicAPI]
public sealed class RepeatPostProcessor<TNodePayload> : ILayoutPostProcessor<TNodePayload>
{
    private readonly ILayoutPostProcessor<TNodePayload>[] _layoutPostProcessors;
    private readonly int _repeatCount;

    public RepeatPostProcessor(int repeatCount, params ILayoutPostProcessor<TNodePayload>[] layoutPostProcessors)
    {
        _repeatCount = repeatCount;
        _layoutPostProcessors = layoutPostProcessors;
    }

    public IReadOnlyCollection<IGraphNodeLayout<TNodePayload>> Process(
        IReadOnlyCollection<IGraphNodeLayout<TNodePayload>> sourceLayouts)
    {
        for (var i = 0; i < _repeatCount; i++)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var postProcessor in _layoutPostProcessors)
            {
                sourceLayouts = postProcessor.Process(sourceLayouts);
            }
        }

        return sourceLayouts;
    }
}
