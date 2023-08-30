# Tatedrez-game

Create a tateDrez game using good coding practices, take into account the mantainance, scalability and readability of the code.
You should use the best practices at your disposal in order to guarantee the best and easier code re-usability.

Explain on a separated text document your implementation choices for the different systems or modules implemented.
Juicyness and attractiveness of the gameplay and UI will be valorated too.

The game must be buildable and runnable on iOS or Android at 60fps without crashes or errors.

---

# GAME DESCRIPTION AND RULES:
Here's a step-by-step description of how a game of Tateddrez would unfold:  

* **Board Setup:**
    An empty board is placed, consisting of a 3x3 grid, similar to a Tic Tac Toe game.
  <img width="320" alt="image" src="https://github.com/juanblasco/tatedrez-game/assets/129755869/69e58f89-c8e0-407c-9003-0ce5a6bb0beb">

* **Piece Placement:**
    Choose a random player to start.  
    Player 1 places one of their pieces in an empty square on the board.  
    Player 2 places one of their pieces in another empty square on the board.  
    They continue alternating until both players have placed their three pieces on the board.
  <img width="321" alt="image" src="https://github.com/juanblasco/tatedrez-game/assets/129755869/85ec3c00-6cd7-467e-b853-37f28698829a">
  

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
  <img width="321" alt="image" src="https://github.com/juanblasco/tatedrez-game/assets/129755869/2d3e69f8-89ae-4890-b19a-aadb9838cfda">


* **Game Conclusion:**
    The game of Tateddrez concludes when one of the players manages to achieve a TicTacToe with their three pieces, either during the initial placement phase or during dynamic mode.  
    The player who achieves the TicTacToe is declared the winner.

  <img width="317" alt="image" src="https://github.com/juanblasco/tatedrez-game/assets/129755869/9561dd1b-d760-47ec-8bc9-41086e1960db">


---
# Delivery
Fork this repository or clone this repository and create a new one into your github account to share it with us. Good luck!