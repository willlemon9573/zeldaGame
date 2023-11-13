# Team Enter Game Legend of Zelda - Sprint 4
# Team Members: Aaron Heishman (heishman.5), Muhammed Gheith (gheith.11), Zihe Wang (wang.14629), Will Lemon (lemon.285), Chih-Hsiang Tseng (tseng.253)
# Note: Code reviews will be under the CodeReviews folder in the project folder
  - Run SprintZero1.sln to play the game
# Controls
  - W, A, S, D, Up, Down, Left, Right - Link Movement Controls
  - Z  - Attack
  - Left Mouse Click: Move to previous room
  - Right Mouse Click: Move to next room
 

 # Unimplemented Features
	Collisions between enemies and the player has not been implemented
	Room screen transitions have not been implemented
	Most game states did not get their implementations
	Sound is not implemented
	Item Selection Menu transitioning down has not been implemented
	Heads up display has implementations, but unused as combat was not implemented
	Room events were not implemented (pushing blocks to open door for example)
	Link using secondary weapon not implemented
	Most of Link's state transitions did not get implemented for the same reason as combat not being implemented
	Hud doesn't display player weapons
	Inventory doesn't draw map / compass and other items

# Known Bugs 
	Pausing the game and opening the inventory can be accessed at the same time
	Enemies cannot be attacked properly
	No Collisions on sides of doors causing certain portions to be drawn above link


Boomerang Projectile's return state does not return to the player, but where it is currently thrown
Link is drawn on the layer below projectiles currently
Link's not drawn above closed doors


# Code Metrics for Sprint 4 (DO NOT DELETE)
Code Metrics ran 10/28/2023
	Project: SprintZero1
	Configuration: Debug
	Scope: Assembly
	Maintainability Index: 83
	Cyclomatic Complexity: 622
	Depth of Inheritance: 2
	Class Coupling: 143
	Lines of Source code: 4,542
	Lines of Executable code: 1,072
	During runtime the code analysis shows the game uses about 139mb of memory now

Code Metrics ran 11/13/2023
	Project: SprintZero1
	Configuration: Debug
	Scope: Assembly
	Maintainability Index: 86
	Cyclomatic Complexity: 1,091
	Depth of Inheritance: 3
	Class Coupling: 224
	Lines of Source code: 9,307
	Lines of Executable code: 2,126
	During runtime the code analysis shows the game uses about 181mb of memory, cpu usage was around 1%



# Sprint 4 Reflection:
	This sprint has been a journey of learning and growth for our team. Despite the hurdles we encountered, it brought us closer as a unit. The challenge of game development
	in the absence of effective communication became evident during this sprint. We strived to meet each deadline, but regrettably, we fell short of fulfilling all the
	prerequisites. We experimented with new task management strategies, which proved to be quite effective. However, as the sprint progressed, we discovered unanticipated
	object interactions that necessitated the creation of additional components to ensure seamless integration. Sprint 4 served as a reality check for our team, humbling us
	all. As we look forward to Sprint 5, our goal is to have all implementations functioning smoothly, in addition to incorporating the extra features we proposed.

# Extra Notes regarding commits:
- Just a note for the graders as suggested by Aaron Post. There's going to be a bunch of Commits from me (Aaron Heishman) that are going to include a bunch of changes made to the repo itself due to how I incorrectly uploaded my Sprint0 source
- All the changes were made to prevent any future conflictions with all of the team members own visual studio settings or to remove my old code that won't be used during the merge with main
- We added suppressions for messages about using "new()" instead of "new Type()"