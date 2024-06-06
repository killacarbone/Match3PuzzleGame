# Game Design Document (GDD)

## 1. Overview

### Game Title
**Match-3 Puzzle Game** (Working Title)

### Game Concept
A captivating Match-3 puzzle game with a home and garden renovation theme, designed to appeal to female millennials aged 28-43. The game combines engaging gameplay mechanics with customization features, allowing players to renovate and decorate their virtual spaces.

### Target Audience
Female millennials aged 28-43, who are tech-savvy, value inclusivity, and engage deeply with social and educational content. They are likely to enjoy casual and puzzle games that provide relaxation, mental stimulation, and opportunities for social interaction.

### Platform
Mobile (iOS and Android)

### Monetization Model
Freemium model with in-app purchases (IAP) and rewarded ads.

## 2. Core Gameplay Mechanics

### Match-3 Mechanics
- **Basic Gameplay:** Players swap adjacent tiles on a grid to create matches of three or more identical tiles in a row or column.
- **Special Matches:** 
  - **Match-4:** Creates a striped tile that clears a row or column.
  - **Match-5 (T or L Shape):** Creates a wrapped tile that clears a 3x3 area.
  - **Match-5 (Straight Line):** Creates a color bomb that clears all tiles of the matched color.
- **Power-Ups and Boosters:**
  - **Hammer:** Clears a single tile.
  - **Shuffle:** Shuffles all tiles on the board.
  - **Extra Moves:** Provides additional moves for the current level.
  - **Reference:** [Behavioral Game Design](https://www.gamasutra.com/view/feature/131494/behavioral_game_design.php)

### Levels and Progression
- **Level Structure:** 
  - **Linear Progression:** Players progress through levels sequentially, each increasing in difficulty.
  - **Goal-Oriented Levels:** Different level goals, such as clearing all blocks, reaching a target score, or collecting specific items within a set number of moves.
- **Obstacles and Challenges:**
  - **Blocked Tiles:** Tiles that cannot be moved until a match is made adjacent to them.
  - **Locked Tiles:** Require matches or power-ups to unlock.
  - **Reference:** [Flow Theory in Games](https://www.gamedeveloper.com/design/creating-flow-in-game-design)

### Customization Features
- **Home and Garden Renovation:**
  - **Rooms and Gardens:** Players can renovate various rooms and garden areas.
  - **Customization Options:** Different styles, colors, and items to choose from, unlocked through gameplay or purchases.
- **Avatars and Characters:**
  - **Avatar Customization:** Players can customize their avatars with different outfits and accessories.
  - **Character Interaction:** NPCs that provide tasks, rewards, and story elements.
  - **Reference:** [Ownership Psychology](https://www.psychologytoday.com/us/articles/200402/ownership-why-we-value-things)

## 3. User Interface (UI) and User Experience (UX) Design

### UI Design Concepts
- **Main Menu:**
  - **Home Button:** Takes players to their customizable home screen.
  - **Play Button:** Starts the next available level.
  - **Store Button:** Opens the in-game store for purchases.
  - **Settings Button:** Accesses game settings.
- **In-Game HUD:**
  - **Move Counter:** Displays the number of moves remaining.
  - **Score Tracker:** Shows the current score.
  - **Power-Ups Panel:** Quick access to available power-ups.
- **Customization Menus:**
  - **Home/Garden:** Options for decorating and renovating.
  - **Avatar:** Options for customizing the player's avatar.
  - **Reference:** [UI/UX Best Practices](https://www.smashingmagazine.com/2018/02/guide-user-experience-design/)

### UX Design Principles
- **Intuitive Controls:**
  - Simple tap and swipe gestures for easy navigation and gameplay.
- **Accessible Design:**
  - High-contrast colors and readable fonts for better accessibility.
- **Feedback and Rewards:**
  - Visual and audio feedback for matches, power-ups, and level completions.
  - Regular rewards and progress updates to keep players motivated.
- **Onboarding:**
  - A guided tutorial for new players to explain core mechanics and features.
  - Contextual tips and hints to assist players as they progress.
  - **Reference:** [User Experience Design](https://www.nngroup.com/articles/ten-usability-heuristics/)

## 4. Art and Aesthetics

### Visual Style
- **Art Style:** 
  - Bright, vibrant colors with a clean and modern aesthetic.
  - Character designs that are relatable and appealing to the target demographic.
- **Theme:** 
  - Home and garden environments with a variety of decor styles.
  - Seasonal and event-based themes to keep the game fresh and engaging.
- **Reference:** [Color Psychology](https://www.colormatters.com/color-and-design/basic-color-theory)

### Concept Art
- **Characters:**
  - Create diverse and inclusive characters that resonate with the target audience.
- **Environments:**
  - Design multiple environments reflecting different home and garden styles.
- **UI Elements:**
  - Develop buttons, icons, and menus that are visually appealing and easy to navigate.
  - **Reference:** [Visual Design Principles](https://www.canva.com/learn/visual-design-principles/)

## 5. Technical Specifications

### Game Engine
- **Unity:** Chosen for its flexibility and support for mobile platforms.

### Platforms
- **iOS:** Optimized for iPhone and iPad.
- **Android:** Optimized for a wide range of Android devices.

### Performance Considerations
- **Optimization:** Ensure smooth performance across all supported devices.
- **Testing:** Regular testing on different devices to identify and fix performance issues.
- **Reference:** [Mobile Game Optimization](https://developer.android.com/topic/performance)

## 6. Development Roadmap

### Milestones
- **Prototype Development:**
  - Basic Match-3 mechanics and initial level designs.
- **Alpha Version:**
  - Expanded levels, basic customization features, and initial UI/UX design.
- **Beta Version:**
  - Full level set, advanced customization, social features, and monetization elements.
- **Release Version:**
  - Polished gameplay, complete customization options, and optimized performance.

### Timeline
- **Planning and Research:** 2 weeks
- **Concept Development:** 2 weeks
- **Prototyping:** 4 weeks
- **Alpha Development:** 6 weeks
- **Beta Development:** 6 weeks
- **Pre-Launch:** 4 weeks
- **Launch:** 2 weeks
- **Post-Launch:** Ongoing updates and improvements.

## 7. References

1. [Behavioral Game Design](https://www.gamasutra.com/view/feature/131494/behavioral_game_design.php)
2. [Flow Theory in Games](https://www.gamedeveloper.com/design/creating-flow-in-game-design)
3. [Ownership Psychology](https://www.psychologytoday.com/us/articles/200402/ownership-why-we-value-things)
4. [UI/UX Best Practices](https://www.smashingmagazine.com/2018/02/guide-user-experience-design/)
5. [User Experience Design](https://www.nngroup.com/articles/ten-usability-heuristics/)
6. [Color Psychology](https://www.colormatters.com/color-and-design/basic-color-theory)
7. [Visual Design Principles](https://www.canva.com/learn/visual-design-principles/)
8. [Mobile Game Optimization](https://developer.android.com/topic/performance)
