# Team Enter Game Legend of Zelda - Sprint 2
# Team Members: Aaron Heishman (heishman.5), Muhammed Gheith (gheith.11), Zihe Wang (wang.14629), Will Lemon (lemon.285), Chih-Hsiang Tseng (tseng.253)
# Note: Code reviews will be under the CodeReviews folder in the project folder
  - Run SprintZero1.sln to play the game
# Controls
  - W, A, S, D, Up, Down, Left, Right - Link Movement Controls
  - Z  - Attack
  - Numerical keys (1, 2,3) for using items
 

# Known bugs, unimplemented features and future updates

For enemy Entity, the way we implement the ChangeDirection is not great enough, because it is using the switch statement for now and we need to change it to use the state pattern for the next sprint.
Enemy Entity needs refactored to move away from switch state

Enemy State pattern not implemented yet, but plan to implement in Sprint 4

The relationship between the player and the weapon/projectile is under reconsideration. Will be handled in Sprint 4 once we have an inventory set up

Classes are missing comments, but plan to have these update 
There are magic numbers that we will remove in sprint 4

PlayerEntity will need extra functionality to handle using both the main weapon (sword) and items (bombs, arrows, etc)

Player currently has an infinite amount of each item, but will be changed in the following sprint

Door's don't transition to next stage currently as we mainly use the mouse to transition. Door collision will be implemented in Sprint 4 alongside Stage transition

# Known Bugs 

Boomerang Projectile's return state does not return to the player, but where it is currently thrown
Link is drawn on the layer below projectiles currently
Link's not drawn above closed doors


# Code Metrics 
Project: SprintZero1
Configuration: Debug
Scope: Assembly
Maintainability Index: 83
Cyclomatic Complexity: 595
Depth of Inheritance: 2
Class Coupling: 128
Lines of Source code: 4,330
Lines of Executable code: 1,039


# Sprint 2 Reflection:
	In Sprint 3, we revised our design plan and initiated code refactoring. The new plan improved task distribution and prioritization, but didn’t resolve all issues.
	The refactoring process was more labor-intensive than anticipated, limiting some team members’ availability for implementing collision and level loader features.
	Despite this, our plan enabled us to meet the sprint’s objectives. Communication was another challenge due to differing schedules, midterms, and fall break. 
	We are still confident with our team’s ability to create a complete and finished product. We we recognize that we need to make some more changes in order to properly meet 
	deadlines so we don’t miss points again this sprint.

# Extra Notes regarding commits:
- Just a note for the graders as suggested by Aaron Post. There's going to be a bunch of Commits from me (Aaron Heishman) that are going to include a bunch of changes made to the repo itself due to how I incorrectly uploaded my Sprint0 source
- All the changes were made to prevent any future conflictions with all of the team members own visual studio settings or to remove my old code that won't be used during the merge with main
