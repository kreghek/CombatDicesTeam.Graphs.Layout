# CombatDicesTeam.Graphs.Layout

The simplest lightweight library to layout graphs to the plane.

## Usage

```c#

// 1. Create graph layouts. Layout is container of graph node with coordinates.
// HorizontalGraphVisualizer try to place every node in line.
// ConcreteLayoutConfig is your implementation of ILayoutConfig (see example below).

var visualizer = new HorizontalGraphVisualizer<int>();  // work with graph of integers.
var layouts = visualizer.Create(campaignGraph, new ConcreteLayoutConfig());

// 2. Optionally pass the graph layouts through the pipeline of a post-processors.

var random = new Random();

var postProcessors = new ILayoutPostProcessor<int>[]
{
    // Increase distance between every node in line.
    new PushHorizontallyPostProcessor<int>(16),
	// Rotate whole graph on specified angle in radians.
    new RotatePostProcessor<int>(random.NextDouble() * Math.PI),
	// Repeat list of transformations multiple times.
    new RepeatPostProcessor<int>(5,
	    // Try to perform specified transformation if it failed by validation.
		// RandomPositionLayoutTransformer is you own implementation of IGraphNodeLayoutTransformer<>.
		// IntersectsGraphNodeLayoutValidator<> checks a layouts do not intersects.
        new RetryTransformLayoutPostProcessor<int>(new RandomPositionLayoutTransformer(random),
            new IntersectsGraphNodeLayoutValidator<int>(), 10))
};

foreach (var postProcessor in postProcessors)
{
    layouts = postProcessor.Process(layouts);
}

// 3. Draw layouts
foreach (var layout in layouts)
{
    // Your code to draw.
	// Use:
	// - layout.Position.X and layout.Position.Y as coordinates to draw.
	// - layout.Size.Width and layout.Size.Height to get size of node layout.
	// - layout.Node.Payload to get you own data of the node.
}
```

Example of visualizer config:

```c#
private sealed class ConcreteLayoutConfig : ILayoutConfig
{
    public int NodeSize => LAYOUT_NODE_SIZE * 2 + CONTENT_MARGIN * 2;
}
```

Example of custom layout transformer:

```c#
private sealed class RandomPositionLayoutTransformer : IGraphNodeLayoutTransformer<int>
{
    private readonly Random _random;

    public RandomPositionLayoutTransformer(Random random)
    {
        _random = random;
    }

    /// <inheritdoc />
    public IGraphNodeLayout<int> Get(IGraphNodeLayout<int> layout)
    {
	    // Calculate new random position of layout.
        var offset = new Position(_random.Next(-20, 20), _random.Next(-20, 20));
        var position = new Position(layout.Position.X + offset.X, layout.Position.Y + offset.Y);

        // And create new layout.
		// If new generated position is not valid it will be failed by validation above.
        return new GraphNodeLayout<int>(layout.Node, position, layout.Size);
    }
}
```

## Motivation

The library was made for the indie game devs, so as not to pull monstrous enterprise solutions for working with graphs into small pet-games.

## Authors and acknowledgment

*    [KregHEk](https://github.com/kreghek)

## Contributing

Feel free to contribute into the project.

## License

You can use it in your free open-source and commercial projects with a link to this repository.
