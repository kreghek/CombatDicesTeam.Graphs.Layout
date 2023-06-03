# Change Log
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/) and this project adheres to [Semantic Versioning](http://semver.org/).

## \[1.0.0\] - 2023-06-03
  
Initial version. Contains the simplest horizontal graph layout. And collection of post-processors to make some variety when drawing the graph.

Available post-processors:
*   `PushHorizontallyPostProcessor<>` to increase horizontal distance between node layouts on specified amount.
*   `RepeatPostProcessor<>` to repeat the collection of a post-processors.
*   `RetryTransformLayoutPostProcessor<>` to perform custom transformation and repeat in if validation was failed.
*   `RotatePostProcessor<>` to rotate graph around coordinates `(0, 0)` on specified angle in radians.

Available transformation validation:
*   `IntersectsGraphNodeLayoutValidator<>` fails the validation then specified layout intersects with other layouts.
