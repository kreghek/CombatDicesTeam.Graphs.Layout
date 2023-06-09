﻿namespace CombatDicesTeam.Graphs.Layout.Tests;

public sealed class HorizontalGraphVisualizerSizeTests
{
    /// <summary>
    /// Test checks the visualizer layouts node between two related.
    /// </summary>
    [Test]
    public void Create_ForkGraphAndSize2_ReturnsRootBetweenChildren()
    {
        // ARRANGE

        const int NODE_SIZE = 2;

        var visualizer = new HorizontalGraphVisualizer<int>();

        var graphMock = new Mock<IGraph<int>>();

        var root = Mock.Of<IGraphNode<int>>(n => n.Payload == 0);
        var child1 = Mock.Of<IGraphNode<int>>(n => n.Payload == 1);
        var child2 = Mock.Of<IGraphNode<int>>(n => n.Payload == 2);

        graphMock.Setup(x => x.GetAllNodes()).Returns(new[] { root, child1, child2 });
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == root)))
            .Returns(new[] { child1, child2 });
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == child1 || n == child2)))
            .Returns(ArraySegment<IGraphNode<int>>.Empty);

        var graph = graphMock.Object;

        var layoutConfig = Mock.Of<ILayoutConfig>(x => x.NodeSize == NODE_SIZE);

        // ACT

        var layouts = visualizer.Create(graph, layoutConfig);

        // ASSERT

        const int MIDDLE = NODE_SIZE / 2;

        layouts.Should().Satisfy(
            layout => layout.Node.Payload == 0 && layout.Position.X == 0 && layout.Position.Y == MIDDLE,
            layout => (layout.Node.Payload == 1 || layout.Node.Payload == 2) && layout.Position.X == NODE_SIZE,
            layout => (layout.Node.Payload == 1 || layout.Node.Payload == 2) && layout.Position.X == NODE_SIZE);
    }

    /// <summary>
    /// Test checks the visualizer layouts node horizontally using node size from config.
    /// </summary>
    [Test]
    public void Create_GraphWithTwoNodes_ReturnsLayoutPlacedHorizontallyUsingSize()
    {
        // ARRANGE

        const int NODE_SIZE = 3;

        var visualizer = new HorizontalGraphVisualizer<int>();

        var graphMock = new Mock<IGraph<int>>();

        var root = Mock.Of<IGraphNode<int>>(n => n.Payload == 0);
        var child1 = Mock.Of<IGraphNode<int>>(n => n.Payload == 1);

        graphMock.Setup(x => x.GetAllNodes()).Returns(new[] { root, child1 });
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == root)))
            .Returns(new[] { child1 });
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == child1)))
            .Returns(ArraySegment<IGraphNode<int>>.Empty);

        var graph = graphMock.Object;

        var layoutConfig = Mock.Of<ILayoutConfig>(x => x.NodeSize == NODE_SIZE);

        // ACT

        var layouts = visualizer.Create(graph, layoutConfig);

        // ASSERT

        var xOrderedLayouts = layouts.OrderBy(x => x.Position.X).Select(x => x.Position).ToArray();
        xOrderedLayouts.Should().BeEquivalentTo(new[] { new Position(0, 0), new Position(NODE_SIZE, 0) });
    }

    /// <summary>
    /// Test checks the visualizer layouts node horizontally using node size from config.
    /// </summary>
    [Test]
    public void Create_GraphWithTwoRoots_ReturnsLayoutsPlacedVerticallyUsingSize()
    {
        // ARRANGE

        const int NODE_SIZE = 3;

        var visualizer = new HorizontalGraphVisualizer<int>();

        var graphMock = new Mock<IGraph<int>>();

        var root1 = Mock.Of<IGraphNode<int>>(n => n.Payload == 0);
        var root2 = Mock.Of<IGraphNode<int>>(n => n.Payload == 1);

        graphMock.Setup(x => x.GetAllNodes()).Returns(new[] { root1, root2 });
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == root1)))
            .Returns(ArraySegment<IGraphNode<int>>.Empty);
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == root2)))
            .Returns(ArraySegment<IGraphNode<int>>.Empty);

        var graph = graphMock.Object;

        var layoutConfig = Mock.Of<ILayoutConfig>(x => x.NodeSize == NODE_SIZE);

        // ACT

        var layouts = visualizer.Create(graph, layoutConfig);

        // ASSERT

        var yOrderedLayouts = layouts.OrderBy(x => x.Position.Y).Select(x => x.Position).ToArray();
        yOrderedLayouts.Should().BeEquivalentTo(new[] { new Position(0, 0), new Position(0, NODE_SIZE) });
    }

    /// <summary>
    /// Test checks the visualizer layouts node between two related.
    /// </summary>
    [Test]
    public void Create_MergeGraphAndSize2_ReturnsChildBetweenRoots()
    {
        // ARRANGE

        const int NODE_SIZE = 2;

        var layoutConfig = Mock.Of<ILayoutConfig>(x => x.NodeSize == NODE_SIZE);

        var graphMock = new Mock<IGraph<int>>();

        var root1 = Mock.Of<IGraphNode<int>>(n => n.Payload == 0);
        var root2 = Mock.Of<IGraphNode<int>>(n => n.Payload == 1);
        var child = Mock.Of<IGraphNode<int>>(n => n.Payload == 2);

        graphMock.Setup(x => x.GetAllNodes()).Returns(new[] { root1, root2, child });
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == root1 || n == root2)))
            .Returns(new[] { child });
        graphMock.Setup(x => x.GetNext(It.Is<IGraphNode<int>>(n => n == child)))
            .Returns(ArraySegment<IGraphNode<int>>.Empty);

        var graph = graphMock.Object;

        var visualizer = new HorizontalGraphVisualizer<int>();

        // ACT

        var layouts = visualizer.Create(graph, layoutConfig);

        // ASSERT

        const int MIDDLE = NODE_SIZE / 2;

        layouts.Should().Satisfy(
            layout => (layout.Node.Payload == 0 || layout.Node.Payload == 1) && layout.Position.X == 0,
            layout => (layout.Node.Payload == 0 || layout.Node.Payload == 1) && layout.Position.X == 0,
            layout => layout.Node.Payload == 2 && layout.Position.X == NODE_SIZE && layout.Position.Y == MIDDLE);
    }
}
