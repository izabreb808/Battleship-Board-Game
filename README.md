# Battleship-Board-Game
The algorithm randomly places the ships on the boards of two players so that they don't overlap or touch themselfs. Players make movements alternately.
If an empty square is hit, the next move will be randomly generated. If a ship segment is hit, there will be randomly selected a direction in which the player will try to shot.
Depending on whether it hits the segmant again or not, it will either continue firing in that direction or choose another one. Every time the segment is shoted, the four
nearest points diagonally are added to the list of forbidden fields. When all segments are shoted so the ship is sunk, two points at the end of the ship are also added to that list, 
so they won't be selected again. 
First success shot is always taken to a variable firstSuccessShot. After that, the algorithm finds the shooting direction. If it reaches the end of the board or an empty 
square, but the ship isn't sunk yet, algorithm goes back to the firstSuccessShot field and starts firing in the opposite direction. After the whole ship has sunk, another 
shot is randomly generated (not including forbidden fields).

<p align="center">
  <img src="[https://github.com/izabreb808/DoctorAppointmentSystem/blob/main/pliki%20do%20dokumentacji/schemat.png](https://github.com/izabreb808/Battleship-Board-Game/blob/master/game.png)"><br>
  <em>Rysunek 1: Schemat plików użytych do projektu</em>
</p>

<p align="center">
  <img src="[https://github.com/izabreb808/DoctorAppointmentSystem/blob/main/pliki%20do%20dokumentacji/schemat.png](https://github.com/izabreb808/Battleship-Board-Game/blob/master/game.png)"><br>
  <em>Rysunek 1: Schemat plików użytych do projektu</em>
</p>

  <p align="center">
  <img src="[https://github.com/izabreb808/DoctorAppointmentSystem/blob/main/pliki%20do%20dokumentacji/schemat.png](https://github.com/izabreb808/Battleship-Board-Game/blob/master/game.png)"><br>
  <em>Rysunek 1: Schemat plików użytych do projektu</em>
</p>


