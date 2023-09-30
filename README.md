# Team Enter Game Legend of Zelda - Sprint 2
# Team Members: Aaron Heishman (heishman.5), Muhammed Gheith (gheith.11), Zihe Wang (wang.14629), Will Lemon (lemon.285), Chih-Hsiang Tseng (tseng.253)
# Note: Code reviews will be under the CodeReviews folder in the project folder
  - Run SprintZero1.sln to play the game
# Controls
  - W, A, S, D, Up, Down, Left, Right - Link Movement Controls
  - Z  - Attack
  - E - Hurt Link
  - T, Y - Change Block sprite
  - O, P - Change enemy sprite
  - U, I - Change Item sprite

# Known bugs and unimplemented features
  - System for changing Links items developed, just not implemented into the Sprint yet
  - Code does not detect held keys- Each frame, a held key is executed as a new key press
  - Sprite sheets were examined and edited using Microsoft Paint (ole reliable) and Pixly.com
  - Controls are developed using ICommand design- Dictionary is developed with Keys linked to Codes that fire Execute().
  - Sprites are handled by Sprite Animator classes. Each object has a base class and an Animator as a child object. Child Object fires Draw()
  - Plan on refactoring heavily. 
	  - List of planned refactoring
      - ISprite and implementing classes for better management of updating Sprite Location (remove control from sprite)
      - Factories for better management of the types of sprites (Static, Animated, etc)
      - Overhaul of unused code (Code we started, but did not finish)

# Code Metrics 
  - Calculating Code Metrics: Our end code metrics for sprint 2 are: maintainability index = 85, Cyclomatic Complexity = 259, Depth of Inheritance = 2, class coupling = 70, lines of source code = 2090, and lines of executable code = 465 


# Sprint 2 Reflection:
	Sprint 2 has revealed the downfalls of not having a clear design plan ready. We completed the requirements of the Sprint, however certain features are missing with several bugs. Links movement is too freeform and the game does not have a method for only allowing one command sent per key press (if held, the game interprets this as a new keypress every frame).
	Our team made sparse use of the Issues chart in our Github, our pseudo-Trello board. Work was primarily discussed and assigned in person, so precise planning was difficult. Most work was primarily complete thanks to the spirited efforts of the team.
	For our next Sprint, we are considering a major refactoring of the code to make design more consistent. Considerations are; switching to an Entity-Component System, standardizing Sprite locations, using Managers for Animation. We are also planning to more concretely plan out the next Sprint.
	All in all, however, our team seems pretty confident in our ability to make a unique product. Several members seem passionate in designing a modular game and believe that with careful planning we can come out with something great.

# Extra Notes regarding commits:
- Just a note for the graders as suggested by Aaron Post. There's going to be a bunch of Commits from me (Aaron Heishman) that are going to include a bunch of changes made to the repo itself due to how I incorrectly uploaded my Sprint0 source
- All the changes were made to prevent any future conflictions with all of the team members own visual studio settings or to remove my old code that won't be used during the merge with main
