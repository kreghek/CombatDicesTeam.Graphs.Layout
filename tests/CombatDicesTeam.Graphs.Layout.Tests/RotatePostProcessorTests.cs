namespace CombatDicesTeam.Graphs.Layout.Tests;

public class RotatePostProcessorTests
{
    /// <summary>
    /// Test checks the processor rotates a left-right graph to right-left orientation.
    /// </summary>
    [Test]
    public void Process_180Degress_GraphBecomeMirrored()
    {
        // ARRANGE

        var processor = new RotatePostProcessor<object>(Math.PI);

        var sourceLayouts = new[]
        {
            Mock.Of<IGraphNodeLayout<object>>(x => x.Position == new Position(1, 0))
        };

        // ACT

        var layouts = processor.Process(sourceLayouts);

        // ASSERT

        layouts.First().Position.X.Should().Be(-1);
        layouts.First().Position.Y.Should().Be(0);
    }

    /// <summary>
    /// Test checks the processor rotates whole graph on 90 degress (pi/2) from top to down.
    /// </summary>
    [Test]
    public void Process_90Degrees_GraphBecomeOrientedVerically()
    {
        // ARRANGE

        var processor = new RotatePostProcessor<object>(Math.PI / 2);

        var sourceLayouts = new[]
        {
            Mock.Of<IGraphNodeLayout<object>>(x => x.Position == new Position(1, 0))
        };

        // ACT

        var layouts = processor.Process(sourceLayouts);

        // ASSERT

        layouts.First().Position.X.Should().Be(0);
        layouts.First().Position.Y.Should().Be(1);
    }

    /// <summary>
    /// Test checks the processor rotates whole graph on 90 degress (pi/2) from top-right square of coordinate system to
    /// bottom-right square.
    /// </summary>
    [Test]
    public void Process_DiagonalPositionOfNodeInTopRightSquareAnd90Degrees_NodeChangePositionToRightBottom()
    {
        // ARRANGE

        var processor = new RotatePostProcessor<object>(Math.PI / 2);

        var sourceLayouts = new[]
        {
            Mock.Of<IGraphNodeLayout<object>>(x => x.Position == new Position(1, 1))
        };

        // ACT

        var layouts = processor.Process(sourceLayouts);

        // ASSERT

        layouts.First().Position.X.Should().Be(-1);
        layouts.First().Position.Y.Should().Be(1);
    }
}