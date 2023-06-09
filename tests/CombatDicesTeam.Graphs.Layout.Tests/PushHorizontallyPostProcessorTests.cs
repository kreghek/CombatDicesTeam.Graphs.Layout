﻿namespace CombatDicesTeam.Graphs.Layout.Tests;

public class PushHorizontallyPostProcessorTests
{
    /// <summary>
    /// Test checks the next node pushed from previous node on specified distance.
    /// </summary>
    [Test]
    public void Process_TwoLayoutsOneNextToAnother_IncreaseDistanceBetweenNodesBySpecifiedAmount()
    {
        // ARRANGE

        var processor = new PushHorizontallyPostProcessor<int>(1);

        var sourceLayouts = new[]
        {
            Mock.Of<IGraphNodeLayout<int>>(x =>
                x.Position == new Position(0, 0) && x.Size == new Size(1, 1) &&
                x.Node == Mock.Of<IGraphNode<int>>(n => n.Payload == 0)),
            Mock.Of<IGraphNodeLayout<int>>(x =>
                x.Position == new Position(1, 0) && x.Size == new Size(1, 1) &&
                x.Node == Mock.Of<IGraphNode<int>>(n => n.Payload == 1))
        };

        // ACT

        var layouts = processor.Process(sourceLayouts);

        // ASSERT

        layouts.Single(x => x.Node.Payload == 0).Position.X.Should().Be(0);
        layouts.Single(x => x.Node.Payload == 1).Position.X.Should().Be(2);
    }
}
