
# 🐍 Serpent's Second Chance

> **Every roll changes your fate. Every challenge is a chance to fight back.**

A magical twist on the classic **Snakes & Ladders** experience, *Serpent's Second Chance* transforms the familiar board game into an interactive adventure filled with unexpected challenges, mini-games, and second chances.

🎮 **Play the Game:** https://ojaswi.itch.io/serpents-second-chance

---

## 🎮 About the Game

*Serpent's Second Chance* is a 2D board game inspired by the classic Snakes & Ladders, but with an added layer of adventure.

Instead of simply moving across the board, players encounter unexpected events that can trigger **mini-games**. Your performance in these challenges can determine whether you stay on your tile or get sent back by a snake.

The goal is simple:

**Roll. Move. Survive the challenges. Reach the final tile. 🐍🎲**

---

## ✨ Key Features

- 🎲 Interactive Snakes & Ladders gameplay
- 🐍 Snake-triggered events
- 🎮 Three different mini-games
  - 🃏 Card Game
  - 👻 Avoid Objects
  - 🔤 Hangman
- 🎬 Story-driven introduction
- ⏭️ Skipable story sequence
- 🔊 Interactive sound effects and background music
- 🏆 Win screen and game progression
- 🎨 Custom UI and visual assets
- 🔄 Scene-based game flow
- 💾 Persistent game-state management between scenes

## Screenshots
<img width="1037" height="828" alt="Screenshot 2026-08-25 182955" src="https://github.com/user-attachments/assets/f3ab84fd-e8f6-4115-9205-0a83f6f30062" />
<img width="1164" height="653" alt="IntroScene" src="https://github.com/user-attachments/assets/a33392bc-777e-41e0-9f98-27a3502d0728" />
<img width="817" height="460" alt="Welcome Scene" src="https://github.com/user-attachments/assets/ad27e99f-7019-4e99-b2a7-0f2eddb9170b" />
<img width="1192" height="678" alt="BoardScene" src="https://github.com/user-attachments/assets/1254db22-e58d-4773-96ee-e3e66d013a96" />
<img width="952" height="528" alt="AvoidFOScene" src="https://github.com/user-attachments/assets/2210f3ff-6ae3-4755-a6c5-023c31b68620" />
<img width="812" height="455" alt="MemoryCardScene" src="https://github.com/user-attachments/assets/cf55b7d0-1459-4d76-97cc-65bc21b8a1a2" />
<img width="818" height="455" alt="HangmanScene" src="https://github.com/user-attachments/assets/21227acc-2849-46a9-9de2-577b748b0c82" />

## 🕹️ Gameplay Flow

```text
Story Scene
     ↓
UI Splash
     ↓
Main Menu
     ↓
Board Game
     ↓
Snake Event
     ↓
Mini-Game
   ↙     ↘
 Win     Lose
 ↓        ↓
Stay    Snake Tail
 ↓        ↓
    Board Game
        ↓
   Final Tile
        ↓
     🏆 Win


🛠️ Tech Stack
Technology	Usage
Unity	Game Engine
C#	Game Logic & Scripting
Unity UI	User Interface
Unity Scene Management	Scene Transitions
Unity Video Player	Story / Intro Sequence
Unity Audio	Music & Sound Effects


🧩 Game Architecture
The project uses a scene-based architecture to separate different parts of the game.
Main Scenes
- StoryScene — Introduction and storyline
- UISplashScene — Splash / transition screen
- UIMainMenuScene — Main menu
- BoardScene — Main Snakes & Ladders gameplay
- HangmanScene — Mini-game
- AvoidObjectsScene — Mini-game
- CardScene — Mini-game
- WinScene — Final victory screen
A persistent MainController manages important game-state information and handles transitions between scenes.


💻 Project Structure
Serpents S.Chance/
│
├── Assets/
│   ├── Scenes/
│   ├── Scripts/
│   ├── UIImages/
│   ├── UIAudio/
│   └── ...
│
├── Packages/
│
├── ProjectSettings/
│
└── README.md


🚀 How to Run the Project
Requirements
- Unity
- Git
Steps
1. Clone the repository:
git clone https://github.com/ojaswigupta06-cyber/serpents-second-chance.git
2. Open Unity Hub.
3. Select Add Project.
4. Select the cloned Serpents S.Chance folder.
5. Open the project using the compatible Unity version.
6. Open the starting scene and press Play.


🎯 What I Worked On
This project involved implementing:
- Game scene management
- Board-game mechanics
- Snake and ladder interactions
- Mini-game integration
- Scene transitions
- UI interactions
- Story / intro sequence
- Audio integration
- Win and lose states
- Persistent game-state handling


🔮 Future Improvements
- Multiplayer support
- Additional mini-games
- More interactive board events
- Player customization
- Difficulty levels
- Online leaderboards
- Improved animations and visual effects

🎮 Play Online
Want to try the game without opening Unity?
👉 Play Serpent's Second Chance on itch.io:
https://ojaswi.itch.io/serpents-second-chance

👩‍💻 Developers
Ojaswi Gupta
Computer Science Engineering Student
Interested in Game Development, Software Development & AI

⭐ If you like the project, consider giving the repository a star!
