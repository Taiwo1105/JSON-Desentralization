# 🏋 Workout Ball Spawner (Unity + JSON)

A Unity-based interactive system that dynamically loads workout data from a JSON file, generates UI buttons for each workout, and spawns balls with physics-based motion when selected by the user.

---

## 📁 Project Setup

### Requirements
- **Unity Version**: 2021.3 or newer recommended
- **Dependencies**: 
  - [TextMeshPro (TMP)](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest)

### Folder Structure
Assets/
├── Prefabs/
│ └── BallPrefab.prefab
├── Resources/
│ └── workouts.json
├── Scripts/
│ ├── WorkoutLoader.cs
│ └── WorkoutData.cs
└── Scenes/
└── MainScene.unity

markdown
Copy
Edit

### Scene Setup
- `Canvas`: Contains UI elements
  - **Title Text** (`TextMeshProUGUI`)
  - **Description Text** (`TextMeshProUGUI`)
  - **Workout Buttons Panel** (e.g., `VerticalLayoutGroup`)
  - **Play/Pause Button** (`Button`)
- `BallSpawnPoint`: An empty `GameObject` placed at the ball spawn location (e.g., `(0, 1, 2)`)
- `WorkoutLoader`: A `GameObject` with `WorkoutLoader.cs` attached

### Prefab
- `BallPrefab`:
  - A 3D sphere with:
    - `Rigidbody`
    - `SphereCollider`
    - Optional: Custom `Material` or color

---

##  JSON File Structure

Located at: `Assets/Resources/workouts.json`

```json
{
  "ProjectName": "TaiwoChimdalu JSON Project",
  "workoutInfo": [
    {
      "workoutName": "Agility Drill",
      "description": "Improves speed and footwork.",
      "ballCount": 5,
      "ballDirection": "right"
    },
    {
      "workoutName": "Balance Practice",
      "description": "Enhances core stability.",
      "ballCount": 3,
      "ballDirection": "center"
    }
  ]
}
JSON Field Reference
Field	Type	Description
ProjectName	string	Title of the project (displayed on screen)
workoutInfo	array	List of workouts
workoutName	string	Name shown on each generated button
description	string	Details shown when a workout is selected
ballCount	int	Number of balls to spawn
ballDirection	string	"left", "center", or "right"

⚙ Key Functionalities
. Dynamic JSON Loading

Loads workout info from workouts.json at runtime

. UI Button Generation

Automatically generates buttons for each workout entry

. Workout Selection & Display

Shows the workout description when selected

. Play/Pause Ball Spawning

Balls spawn based on ballCount and direction

Spawning continues from where it paused

. Physics-Based Movement

Balls spawn from BallSpawnPoint and move using Rigidbody.AddForce()

 Force Mapping
ballDirection	X Force Value
"left"	-0.5f
"center"	0f
"right"	0.5f

. Tips
Ensure the BallSpawnPoint is in camera view and placed above ground (e.g., Y = 1–2)

Check that the ball prefab is scaled and visible (Vector3.one * 1.5f recommended)

Use Debug.Log() to confirm ball positions during testing

Reimport the JSON file if Unity doesn't reflect changes

💡 Optional Enhancements
Add "Reset" button to clear existing balls

Add animations or sound on spawn

Customize ball color or behavior per workout

Add a workout progress tracker or timer

 Authors
Taiwo and Chimdalu
Built with Unity for interactive training and fun!

