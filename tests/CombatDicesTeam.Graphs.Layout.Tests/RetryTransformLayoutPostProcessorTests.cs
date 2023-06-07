namespace CombatDicesTeam.Graphs.Layout.Tests;

public class RetryTransformLayoutPostProcessorTests
{
    /// <summary>
    /// Test checks the post-processor retry transformation every time a validation failed.
    /// </summary>
    [Test]
    public void Process_FirstAttemptIsInvalid_CallTransformation2Times()
    {
        // ARRANGE

        var transformerMock = new Mock<IGraphNodeLayoutTransformer<object>>();
        transformerMock.Setup(x => x.Get(It.IsAny<IGraphNodeLayout<object>>()))
            .Returns(Mock.Of<IGraphNodeLayout<object>>());

        var attemptNumber = 0;
        var validatorMock = new Mock<IGraphNodeLayoutValidator<object>>();
        validatorMock.Setup(x => x.Validate(It.IsAny<IGraphNodeLayout<object>>(),
            It.IsAny<IReadOnlyCollection<IGraphNodeLayout<object>>>())).Returns(() =>
        {
            attemptNumber++;
            // Pass validation only for last retry.
            return attemptNumber - 1 == 0;
        });

        const int ATTEMPT_COUNT = 2;
        var processor =
            new RetryTransformLayoutPostProcessor<object>(transformerMock.Object, validatorMock.Object, ATTEMPT_COUNT);

        var sourceLayouts = new[]
        {
            Mock.Of<IGraphNodeLayout<object>>(x => x.Position == new Position(0, 0))
        };

        // ACT

        processor.Process(sourceLayouts);

        // ASSERT

        const int EXPECTED_RETRY_COUNT = ATTEMPT_COUNT - 1;
        transformerMock.Verify(x => x.Get(It.IsAny<IGraphNodeLayout<object>>()), Times.Exactly(EXPECTED_RETRY_COUNT));
    }
}