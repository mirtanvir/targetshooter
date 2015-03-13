# Software Requirement Specifications #
## Target Shooter ##

CS-4310 : Software Engineering I <br>
Mir Hossain,       Shailaja Raju,       Leon Choi,       Hieu Nguyen <br>
California State University East Bay<br>
<br>
<h2>1. INTRODUCTION</h2>

<h3>1.1 Purpose of this Document</h3>

This Software Requirement Specification document describes the performance requirements of our class project for CS-4310, Software Engineering -I. These include an overview of the project description, functional requirements of systems the project will run on, and characteristics of end users.<br>
<br>
<h3>1.2 Scope of the Development Project</h3>

The name for the game is not been finalized yet, but is currently referred to as 'Target Shooter,' which will be used through out the development process. Target Shooter is real time game in which the player aims and attacks a random target with keyboard controls at different levels of difficulty.<br>
<br>
<h3>Limitations of the game:</h3>

<ul><li>The game is limited to single player<br>
</li><li>XNA Game Studio 3.0 is required<br>
</li><li>Works only on Windows platform, Windows XP requires Service Pack 2 or later<br>
</li><li>Windows Vista Service Pack 1 (SP1) is supported, but is not required, for XNA Game Studio<br>
</li><li>Graphics card that supports Shader Model 1.1 or greater and Direct X 9.0c is also required<br>
</li><li>Input or game controls is only through keyboard keys</li></ul>

<h3>Distinct features of the game:</h3>

<ul><li>It is a scored single player action game<br>
</li><li>It has simple graphical user interface that is self explanatory<br>
</li><li>Intensity of the game increases with the increase in levels of the game<br>
</li><li>The competitiveness and the speed of action increases with the increase in game levels</li></ul>

<h3>Benefits of the game:</h3>

<ul><li>It is fun filled action game for a player</li></ul>

We reserve the right to add or change any features to the project, for example to incorporate the clock option into the game which will allow the user to play against the clock to increase the level of intensity.<br>
<br>
<h3>1.3 Definitions, Acronyms, and Abbreviations:</h3>

<ul><li>Project Name - Target Shooter</li></ul>

<ul><li>Level - the difficulty represented in the game</li></ul>

<ul><li>Single player - only one player per game</li></ul>

<ul><li>Target - the object at which the player aims and shoots at to play and win the game</li></ul>

<ul><li>Lives/chances - used interchangeably and means the same. It is the number of survival chances for the player tank</li></ul>

<ul><li>player - player tank (grey colored tanks)</li></ul>

<ul><li>enemy - enemy tanks (different colored tanks, not grey)</li></ul>

<ul><li>health - current health of the player tank</li></ul>

<h2>1.4 References</h2>

<h3>1.4.1 Books</h3>

Aaron Reed, Game Development for the PC, Xbox 360, and Zune, Learning XNA 3.0, O'Reilly Media, Inc., CA, 2008.<br>
<br>
<h3>1.4.2 Websites</h3>

<ul><li>Microsoft Visual Studio<br>
<blockquote><a href='http://msdn.microsoft.com/en-us/vcsharp/default.aspx'>http://msdn.microsoft.com/en-us/vcsharp/default.aspx</a></blockquote></li></ul>

<ul><li>XNA<br>
</li></ul><blockquote><a href='http://creators.xna.com/en-US/'><a href='http://creators.xna.com/en-US/'>http://creators.xna.com/en-US/</a></a></blockquote>


<h2>1.5 Overview of Document</h2>

The rest of this document lists the general description, functional, data, specific and other requirements of the game Target shooter.<br>
<br>
<h2>2. GENERAL DESCRIPTION</h2>

This section of SRS lists an overview of user characteristics with respect to game, product perspective, an overview of functional performed by the software and the data requirements from the user.<br>
<br>
<h3>2.1 User Characteristics</h3>

The anticipated users of Target Shooter game are game savvies of ages 8 and up who are familiar with the use of Windows and keyboard. Those who are interested in playing video games or online games.<br>
<br>
<h3>2.2 Product Perspective</h3>

Target shooter is a stand-alone code module, which displays the game on to the screen and the player uses keyboard as controls to play the game. The game is played on Windows using a standard keyboard. The game uses XNA to interface with the users input and output of the game. We also might incorporate the game into X-box360.<br>
<br>
<h3>2.3 Overview of Functional Requirements</h3>

<ul><li>The game can be played on a PC using just the existing keyboard as controls.<br>
</li><li>The player/user tank is provided with keyboard controls to shoot at a random target.<br>
</li><li>The player tank has to aim and shoot at the enemy tank to proceed to the next level.<br>
</li><li>The enemy tanks also can shoot randomly at the player tank, which  will result in player loosing a chance/life if the player is hit.<br>
</li><li>The player tank is provided with limited number of chances/lives for the entire game.<br>
</li><li>The intensity of the game increases with each level<br>
</li><li>The game keep tracks of player score through out the game<br>
</li><li>The player looses the game if he/she is hit by the target and looses all his/her chances.<br>
</li><li>The player wins the game if he/she survives with at least one life and finishes the last level of the game.</li></ul>

<h3>2.4 Overview of Data Requirements</h3>

<ul><li>The user needs to have enough space on the hard drive to load the game.<br>
</li><li>Current level in the game the player is at. (total 2 levels)<br>
</li><li>The current score for a player which increments in 1000 for every enemy tank shot.<br>
</li><li>The number of lives remaining  which depends on the health of the player.<br>
</li><li>Current health of the player, which decrements in % for every shot to the player tank from the enemy tank.</li></ul>

All the above data needs to be maintained and manipulated accordingly.<br>
<br>
<br>
<h3>2.5 General Constraints, Assumptions, Dependencies, Guidelines</h3>

<ul><li>The game runs only on Windows platform<br>
</li><li>The input for the game is only through the keyboard<br>
</li><li>Generally arrow keys are used to move the player tank on the screen,<br>
<ul><li>up arrow - move front<br>
</li><li>down arrow - move back<br>
</li><li>right arrow - turn anticlockwise<br>
</li><li>left arrow - turn anticlockwise<br>
</li><li>space bar - to shoot bullets<br>
</li><li>'A' - to turn turret left<br>
</li><li>'D' - to turn turret right<br>
</li></ul></li><li>The user's interaction to the game is critical to play the game effectively<br>
</li><li>The player is provided with the help screen which gives detailed description of the game itself and the keyboard keys used to control the game.</li></ul>

<h3>2.6 User View of Product Use</h3>

Once the game is started, the user sees the welcome screen for Target Shooter, which includes a brief description of the game. Then the user is provided with the following options to select from;<br>
<br>
<ul><li>Help - which gives the description of the keys associated with the game controls<br>
</li><li>Play - which will start the game and<br>
</li><li>Quit - which will exit the game</li></ul>

The game screen has a info bar at the bottom of the screen which displays the following information:<br>
<br>
<ul><li>current level in the game<br>
</li><li>current score<br>
</li><li>number of chances/lives left<br>
</li><li>current health of the player which decreases in %, which when zero, decreases a life for the player tank<br>
</li><li>pause option to pause the game which will toggle between pause and play<br>
</li><li>option to quit the game at any stage in the game<br>
</li><li>help option which will brief the user about the keys used to control different aspects of the game</li></ul>

The game has 2 levels, 1 and 2. The player is supposed to aim and shoot with the keyboard controls specified at the randomly moving enemy tanks. The number of enemy tanks, the randomness and the speed of the tanks increases with the increase in levels. As levels increase, the enemy tanks also shoots back at the player tank, which the player tank needs to survive to continue the game. However the player tank is provided with 3 survival chances after which the player looses the game. If the player survives from the enemy tanks, the player proceeds to the next level hitting the targets. If the player survives through all the levels hitting all the enemy tanks, then he wins the game.<br>
<br>
The game is over either when the Quit game option is selected or game is won or lost by the player. Then final screen is displayed with the scores and the appropriate results and is also provided with the options to either "Exit the game" or "Play one more game"<br>
<br>
<h2>3. SPECIFIC REQUIREMENTS</h2>

<h3>3.1 External Interface Requirements</h3>

The Target Shooter Game is designed for only one player and is operated by using keyboard.<br>
<h3>3.2 Detailed Description of Functional Requirements</h3>

<h3>3.2.1 Template for describing functional requirements</h3>

Component Name:<br>
<br>
Purpose:<br>
<br>
Inputs to the Components:<br>
<br>
Processing:<br>
<br>
Outputs:<br>
<br>
<h3>3.2.2 Interface</h3>

Component Name: Interface<br>
<br>
Purpose: Uses Graphical User Interface to display and control game state.<br>
<br>
Inputs to the Components:<br>
<br>
<blockquote>From user: choose How to Play? / Play / Quit commands through keyboard shortcuts</blockquote>

<blockquote>From Game Core: Display state parameters</blockquote>

Processing: Determine and control game movements to display on the screen<br>
<br>
Outputs: Showing the result after processing the commands<br>
<br>
<h3>3.2.3 Game Core</h3>

Component Name: Game Core<br>
<br>
Purpose: Receives commands from the inputs by which the game state is calculated and the interface behaves accordingly<br>
<br>
Inputs to the Components:keystrokes from the keyboard<br>
<br>
Processing: Calculates the score according to the enemy tanks killed by the player tank<br>
<br>
Outputs: Message is sent to the interface and the Score is displayed to the player<br>
<br>
<h3>3.2.4 Player Engine</h3>

Component Name: Player Engine<br>
<br>
Purpose: Receives commands from the inputs by which the game state is calculated and the interface behaves accordingly<br>
<br>
Inputs to the Components: Receives inputs from the keyboard, firing of bullets to the targets etc<br>
<br>
Processing: Collision detection of bullets to the targets, calculation of score accordingly, concluding the game with win or loose status and also be able to quit the game<br>
<br>
Outputs: Message is displayed according to the options selected by the user<br>
<br>
<h3>3.2.5 Input</h3>

Component Name: Input<br>
<br>
Purpose: Actions are determined according to the keys input by the user<br>
<br>
Inputs to the Components: Receives inputs from the keyboard, firing of bullets to the targets etc<br>
<br>
Processing: Actions are performed accordingly<br>
<br>
Outputs: the player will be able to shoot the target if he selected to fire the bullets etc<br>
<br>
<h3>3.3 Performance Requirements</h3>

<ul><li>Game requires that only one player plays the game at a time. It is a single player game<br>
</li><li>It requires graphics card that supports Shader Model 1.1 or greater and DirectX9.0c, XNA Game Studio 3.0 installed<br>
</li><li>It requires Windows Vista Service Pack 1 or Windows XP Service Pack 2 or up<br>
</li><li>Every action is run instantly after player chooses the command. No delay</li></ul>

<h3>3.4 Quality Attributes</h3>

<blockquote>Availability and Reliability:</blockquote>

<blockquote>No internet required. Players can run the game on thier personal computer. All the required software is provided with the game</blockquote>

<blockquote>Maintainability:</blockquote>

<blockquote>Updates to the project can be made depending if there are added requirements to the game.</blockquote>

<h3>4. Other requirements</h3>

It is expected that the user uses a computer with a typical English language, QWERTY layout keyboard