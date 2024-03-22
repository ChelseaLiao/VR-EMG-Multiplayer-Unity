# VR-EMG-Multiplayer-Unity
This project was developed with Unity3D 2021.3.6f1 and set to basic 3D-usage by default
<br>Run <b>Assets/Scenes/SampleScene.unity</b> to test
<br>Detailed description and video about this demo please check https://www.artstation.com/artwork/1xw9O3
## For VR-EMG-usage
Currently 3 player skills can be only released in VR environment, as well as 3 moving circles.  
### Required Software
  1. SteamVR
### Required Hardware
  1. HTC Vive 
  2. EMG device. reference from https://labstreaminglayer.readthedocs.io/info/supported_devices.html
### LSL Framework for Unity (already inculded in this project)
https://github.com/labstreaminglayer/LSL4Unity
### In Unity Editor
![image](https://github.com/ChelseaLiao/VR-EMG-Multiplayer-Unity/assets/92916469/65037248-c9c9-43b1-bae0-f3ac0864899d)
<br>Open <b>Assets/Scenes/SampleScene.unity</b>
<br>Show <b>SteamVRObjects</b> and hide <b>NoSteamVRFallbackObjects</b>
## For Basic 3D-usage
For testing this demo without VR environment and EMG device, I added damage image and gameover UI animation for basic 3D scene based on canvas, and a hand model follows the mouse. You can make a quick punch by clicking the space key.
### In Unity Editor
![image](https://github.com/ChelseaLiao/VR-EMG-Multiplayer-Unity/assets/92916469/da904b6b-e8a5-49cd-a542-2d3dc9904a97)
<br>Show <b>NoSteamVRFallbackObjects</b> and hide <b>SteamVRObjects</b>
<br><br>![image](https://github.com/ChelseaLiao/VR-EMG-Multiplayer-Unity/assets/92916469/ccce0b68-2fb8-496f-b7d9-62c9a87c040f)
<br> Make sure the following variables of <b>PlayerController</b> under game object <b>Player</b> are from <b>NoSteamVRFallbackObjects</b>:
<br><b>HP_Player</b>
<br><b>HP_Text</b>
<br><b>Damage_Image</b>
<br><b>Gameover Cube</b>

## To-Do List
To provide an entire single player version for testing without VR and EMG device:
  1. Animate the punch with 2 hand models.
  2. Change the UIs of health bar for zombies and player.
  3. Add 3 player skills, and 3 moving circles on the right hand model get hide during skill cool down.
  4. Add Opening animation with game introduction and a menu


