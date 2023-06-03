namespace CombatDicesTeam.Graphs.Layout.Tests;

public class RepeatPostProcessorTests
{
    /// <summary>
    /// Test checks the inner processor used multiple times.
    /// </summary>
    [Test]
    public void Process_MultipleIterations_BaseProcessorsCalledMultipleTimes()
    {
        var baseProcessorMock = new Mock<ILayoutPostProcessor<object>>();
        baseProcessorMock.Setup(x => x.Process(It.IsAny<IReadOnlyCollection<IGraphNodeLayout<object>>>()))
            .Returns(new[]
            {
                Mock.Of<IGraphNodeLayout<object>>()
            });

        const int REPEAT_COUNT = 2;
        var processor = new RepeatPostProcessor<object>(REPEAT_COUNT, baseProcessorMock.Object);

        var sourceLayouts = new[]
        {
            Mock.Of<IGraphNodeLayout<object>>()
        };

        // ACT

        processor.Process(sourceLayouts);

        // ASSERT

        baseProcessorMock.Verify(x => x.Process(It.IsAny<IReadOnlyCollection<IGraphNodeLayout<object>>>()),
            Times.Exactly(REPEAT_COUNT));
    }
}