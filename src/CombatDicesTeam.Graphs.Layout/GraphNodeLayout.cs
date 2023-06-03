namespace CombatDicesTeam.Graphs.Layout;

/// <summary>
/// Base implementation of node layout.
/// </summary>
public sealed record GraphNodeLayout<TNodePayload>
    (IGraphNode<TNodePayload> Node, Position Position, Size Size) : IGraphNodeLayout<TNodePayload>;