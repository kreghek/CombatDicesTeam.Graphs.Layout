﻿namespace CombatDicesTeam.Graphs.Layout;

/// <summary>
/// Layout of graph node.
/// </summary>
/// <typeparam name="TNodePayload">Type of node's data.</typeparam>
public interface IGraphNodeLayout<out TNodePayload>
{
    /// <summary>
    /// Node which visualized.
    /// </summary>
    public IGraphNode<TNodePayload> Node { get; }

    /// <summary>
    /// Position of node.
    /// </summary>
    public Position Position { get; }

    /// <summary>
    /// Size of layout.
    /// </summary>
    public Size Size { get; }
}