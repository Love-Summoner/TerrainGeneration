# TerrainGeneration
A Unity project made to practice programming endless procedural world generation 

# Noise Generation
Unity has a function called mathf.PerlinNoise() that takes in two values: an X value and a y value.  Then returns a value between 0 and 1 based on fractal perlin noise generation.  In the map_noise_generation class there is a function called get_noise_point, which similar to PerlinNoise() takes in an x and y float and returns a value, but gets a point using set variables for octaves, frequency, and lacunarity allowing for more control over how the map generates.
(incomplete feature)The value returned in get_noise_point will also have a value from a texture map called, "blurred_water_map" subtracted from it using x and y as coordinates.

# Chunk Generation
The code starts by filling a 2d array with the locations of every vertice.
The x-z global location(added the position of the gameobject to get this) is passed into a map_noise_generation object and the returned value is stored at y value.  It is then multiplied by a set height value to get the exact y location of the vertice.  After this the triangles are set in an array of integers called, "triangles".  Then the vertices and triangle arrays are passed into a mesh and the chunks mesh is generated.

# World generation
A gameobject with the chunk generation code is saved as a prefab and using a gameobject with the chunk_generator script attached to it will take in a value called render_distance and then using the player objects location will instantiate a square array of chunks around the player.

# Optimizations
The first optimization is deactivating the collision of chunks who are far from the player.  Every chunk has an if statement in the Update function that will calculate the distance between it and the player and will change itself depending on that distance, whith collision disabling being the first way.
The chunks will delete themself if the player is too far.
