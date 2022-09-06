== GaragePI

C# program to control a Raspberry PI for opening and closing garage doors

Based on this project: https://github.com/shrocky2/SiriGarage

Installed on the Raspberry PI using "servicectl".   See file "GaragePi.service" for the directives used by the executable.

=== NOTES

Had to change the pin for reading the "open" door status from 18 to 15, as for some reason pin 18 wasn't readable.