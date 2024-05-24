# Checkers Game - WPF Application

## Overview
This project is a Checkers game developed in C# using a WPF graphical user interface. The application follows the MVVM design pattern, ensuring clean code and separation of concerns. The game includes two sets of pieces (white and black) on an 8x8 board, with black pieces making the first move. The application supports various types of moves, including single moves, jumps, and multiple jumps, and allows for saving and loading game states.

## Features
### General
- Two-player game with alternating turns.
- Visual indication of the current player.
- Display of the remaining pieces for each player.
- Endgame detection and winner announcement.

### Moves
- **Simple Move:** Move a piece diagonally forward by one square. If a piece reaches the opposite end, it becomes a "king" and can move both forward and backward.
- **Jump Over Opponent:** Capture an opponent's piece by jumping over it.
- **Multiple Jumps:** Chain multiple jumps if possible. This feature can be toggled at the start of the game and will persist in subsequent runs.

### Game Management
- Save and load game state, including the current player.
- Display statistics of games won by white and black players.
- Option to start a new game with standard or custom configurations.

## Project Structure
- **Model:** Contains the data models for the game.
- **ViewModel:** Contains the logic for the application's views.
- **View:** Contains the user interface components.

## Technologies Used
- C#
- WPF
- MVVM Architecture
- File I/O for saving and loading game states

## Setup and Installation
### Prerequisites
- Visual Studio
- .NET Framework

### Steps
1. Clone the repository.
2. Open the solution in Visual Studio.
3. Build and run the application.

## Usage
### Menus
- **File Menu:**
  - **NewGame:** Start a new game with the standard configuration.
  - **SaveGame:** Save the current game state.
  - **LoadGame:** Load a previously saved game state.
  - **AllowMultipleJump:** Toggle the option for multiple jumps.
  - **SwitchTurn:** Manually switch over to the next player's turn.
  - **Statistics:** View the number of games won by white and black players.
  - **Exit:** Exit the game.

- **Help Menu:**
  - **About:** View the creator's name, institutional email address, group, and a short description of the game.

### Gameplay
1. Start a new game or load an existing game.
2. Move pieces according to the rules.
3. The game ends when one player has no remaining pieces.
4. The application announces the winner and updates the statistics.

## Commands
The application implements ICommand for handling user actions and data binding, ensuring responsiveness and maintainability.

## License
This project is for educational purposes and is not licensed for commercial use.
