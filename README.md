# Orbitality
<br>This is planetary artillery arcade in pseudo 2D (i.e. 2D game in 3D world).
<br>Player is in control of a planet, rotating around Sun. Planet can shoot rockets from it’s forward point, rockets obey gravity (of Sun and of other planets) meaning they fly in parabolic trajectories. On collision with Sun, rocket disappears, on collision with planet - does damage.
<br>You need to create singleplayer game mode with AI players (random number from configurable min/max values).
<br>Rest of features:
<ul>
  <li>● Planets should be visually different (and random)</li>
  <li>● Implement 3 types of rockets with different base stats (acceleration, weight, cooldown, etc), which are distributed randomly amongst planets</li>
  <li>● Pause/resume feature</li>
  <li>● UI: main menu, HUD, planet HUD (HP bar, shooting cooldown)</li>
</ul>
<hr>
<b>Additional information</b>
<br>By default rocket flies in normal(forward) direction, parabolic movement can be enabled in code - it can look strange coz it still needs correct selection of coefficients
<br>When the game starts the world generates player planet and 2-4 enemy planets.
<br>If one of planets is ready to shoot you'll see a little rocket icon on it's right top corner. Each planet has slider with it's healthbar.
<br>You will also see your planet hp and cooldown in details on the panel in the left bottom corner.
<br>Each of planets get it's values random - appearance, size, rotation speed, position, rocket type.
<br>There are 3 enemy planet attack strategies - to shoot random point, the closest planet or player planet.
<br>Rocket remembers it's native planet so will ignore collisions with them and move further.

