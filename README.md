# Tatedrez-Game: Development Documentation

Since the base of the game is the board, I decided to create a main **"board" system/module** to manage the core gameplay mechanics and behaviors. So the full gameplay cycle (board creation, piece placement and win condition) are handled in this Board system.

For other aspects of the game that are not actual core gameplay (Input, Audio, Frame Rate, Settings...) I decided to create separate modules or scripts to manage them independantly.

On the following sections I explain how I implemented each of them.

---

## Board

For the main system of the game, I decided to use the **MVC architecture** with the Mediator pattern to keep it clean and organised, since this one is the biggest system in the game.
So I created three main classes to manage the board: Model, View and Mediator. I also created separated classes to store board-related data, such as Cell, Piece (and de derived piece classes: Knight, Rook and Bishop), and Score.

* As the MVC architecture establishes, the **BoardModel** stores all the board's data, the **BoardView** updates the screen according to that model and the **BoardMediator** handles the player's input to manage the board's behaviour and notifies the model and the view.

* **Cell** stores the data related to a cell, so the BoardModel can contain an array with all the board cells.

* **Piece** stores the data related to a piece, and also updates the piece's view (position and overlay color) when the BoardMediator notifies it. I could separate the Piece class into another MVC system to keep everything independent like in the Board system, but I though that in this case it was unnecessary due to the simplicity of the Piece class. Having another MVC system just for the Piece would result in a too complex code (with a lot of small scripts) and I prefered to apply the MVC architecture only for the main system, instead of creating other subsystems and applying it to every single one of them too.
In addition, I decided to make Piece an abstract class and create child classes for each piece type, so that with this approach it is easy to create new piece types in case that it is required. Each piece subclass contains the method to check if a movement is valid, since it is different for every type. The Board system always references the abstract Piece class, so it will not need to change in case that the piece types change or that the behaviour of any of them is modified.

* **Score** contains two variables to store each player's score, so it can be displayed on the screen during gameplay by the BoardView. It is initialized as 0-0 at the start of the game and it is updated on every game win. It restarts when the game is closed.

---

## Game Manager

I created a simple class to initialize the game, which is the **GameManager**. Its responsibility is to load the game by creating the board model and mediator and calling the Init method in the mediator. It also initializes the input and the Score class, and responds to the restart button.

---

## Utils

I decided to create a separate module to contain the classes that are used as utils or tools for the game, which are the input and the frame rate handling.
So Utils contains two classes: InputHandler and FrameRateHandler.

* **InputHandler** contains an event that is invoked whenever input is received (screen touch on Android and iOS, and mouse button on Editor). I implemented a singleton pattern for this class so other classes can access it to listen to that event without needed to store a reference to the InputHandler class.

* **FrameRateHandler** sets the target frame rate to 60 and checks the frame rate continuosly to display it on screen for debug purposes. It is updated every 0.5 seconds.

---

## Libraries

The purpose of this module is to have a clean way of storing some data that is likely to change in the future, such as the game texts, audio clips or some textures. I created three different Library classes in addition to a LibrariesHandler to easily provide each of those libraries to other classes when they need it.

* **LibrariesHandler** contains references to all three mentioned libraries and contains getter methods for each of them. In case the reference hasn't been initialized yet when it is required, it loads it from the Resources folder.

* **BoardLibrary** contains board-related data, which in this case is only the tiles used for the board creation.

* **AudioLibrary** contains a list of the audio clips that are used in the game.

* **TextsLibrary** contains the string values for each text that is displayed in the game.

---

## Audio

I decided to create a separate module to manage the audio. This module contains two classes: AudioManager and AudioPool.

* **AudioManager** contains an enum with every sound type and a method to play a sound. I made it a static class so other classes can access it easily to play a specific sound by just calling the method and passing it a value from the mentioned enum.

* **AudioPool**'s purpose is to have a fixed amount of AudioSource gameobjects in the scene and avoid instantiating a new gameobject for every sound that is played. I didn't want to use a single one AudioSource since this could create issues if multiple sounds need to be played simultaneously. So I created the pool to instantiate a specific number of AudioSource gameobjects at start and them recycle them during gameplay.

The AudioManager could have managed the pool itself, but I prefered to have two separate classes for this in order to follow the Single Responsibility principle and keep these classes small and simple.

---

## Unit Tests

I added unit tests for the game to check that the implementation works as expected, and also to help in creating a clean and maintainable code.

I created both PlayMode and TestMode tests, and I separated them in three categories: Cell, PieceMovement and TicTacToe.

* **Cell** tests check that cell-related operations work as expected. Those operations include selection, selection clearing, moving and checking whether a cell is valid or not.

* **PieceMovement** tests check that the validation of a movement for every type of piece works as expected. For each piece, there are tests for each posible outcome (valid movement, invalid movement, obstacle).

* **TicTacToe** tests check that the win condition works as expected. There is a test for each posible win (horizontal, vertical and diagonal) and also for the no-win outcome.

---

# EXTRAS

Besides the basic gameplay mechanics and requirements for the game, I decided to add some extras to make the game more attractive and complete.

Those extras include the already mentioned **Score** and **Audio**, and I also added some **visual effects** like an overlay for the pieces to give some feedback to the players when they select and move.

I tried to use 2D assets and colors that could make an attractive UI, and I also tried to find and use a nice font.

Every asset (textures, fonts, sounds, etc.) that is used in the game has been obtained from free and open source sites.

---

# BUILD

 You can find the Android .apk file included in the repository, inside the root folder. It is called **Tatedrez.apk**.

---

# GAME DESCRIPTION AND RULES
Here's a step-by-step description of how a game of Tateddrez would unfold:  

* **Pieces:**
    The game has only 3 pieces. Knight, Bishop and Rook:
    * Knight (Horse): The knight moves in an L-shape: two squares in one direction (either horizontally or vertically), followed by one square perpendicular to the previous direction. Knights can jump over other pieces on the board, making their movement unique. Knights can move to any square on the board that follows this L-shaped pattern, regardless of the color of the squares.
    * Rook: The rook moves in straight lines either horizontally or vertically. It can move any number of squares in the chosen direction, as long as there are no pieces blocking its path.
    * Bishop: The bishop moves diagonally on the board. It can move any number of squares diagonally in a single move, as long as there are no pieces obstructing its path.

* **Board Setup:**
    An empty board is placed, consisting of a 3x3 grid, similar to a Tic Tac Toe game.

* **Piece Placement:**
    Choose a random player to start.  
    Player 1 places one of their pieces in an empty square on the board.  
    Player 2 places one of their pieces in another empty square on the board.  
    They continue alternating until both players have placed their three pieces on the board.

* **Checking for TicTacToe:**
    After all players have placed their three pieces on the board, it's checked whether anyone has managed to create a line of three pieces in a row, column, or diagonal – a TicTacToe.

* **Dynamic Mode:**
    If neither player has achieved a TicTacToe with the placed pieces, the game enters the dynamic mode of Tateddrez.
    If X player can't move, the other player move twice.
    In this mode, players take turns to move one of their pieces following chess rules.
    **Capturing opponent's pieces is not allowed.**

* **Seeking TicTacToe:**
    In dynamic mode, players strategically move their pieces to form a TicTacToe.  
    They continue moving their pieces in turns until one of them achieves a TicTacToe with their three pieces.

* **Game Conclusion:**
    The game of Tateddrez concludes when one of the players manages to achieve a TicTacToe with their three pieces, either during the initial placement phase or during dynamic mode.  
    The player who achieves the TicTacToe is declared the winner.
