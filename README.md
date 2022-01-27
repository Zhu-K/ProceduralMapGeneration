# ProceduralMapGeneration
A template to generate random maps by procedurally connecting prefab rooms in Unity.

_This is a work in progress._

## Implemented Features:
* Easy setup of prefab rooms to spawn
* Randomly generates valid connections between rooms
* Adjustable generation parameters including boundary size, starting room type, room count

## Planned Features:
* Custom weights for room generation probability
* Static seed based generation
* Multiple-pass generation (eg. 1st pass: place rooms; 2nd pass: place corridors to connect rooms)
* Procedural locked door placement with validation check (ie. a locked door should be unlockable with a key existing in an already unlocked door)

## Installation:
* start unity project.
* copy paste all downloaded files into the Asset folder of the new project.

## Usage:
* Load TestScene from the Scenes directory.
* Edit grid size (width / height) of the map in the GameLogic/RoomSpawner object.
* Edit # of rooms in the GameLogic/RoomSpawner object.
* Run the game.
* Add, modify, or remove template prefabs from the Prefab directory, ensure dimensions of rooms and door placement are integer.
* Doorways must be named doorway.
* To use custom visual asset, overlay imported models over the template cubes in the prefabs, hide cubes if necessary.