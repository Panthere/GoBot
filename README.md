# GoBot
A simple bot using the Rocket API for Pokemon Go.


# Warning
This is not a fully functioning bot. This was made in only a couple of hours. The API that this uses changes several hundred times a day.
If you build from source there may be some small issues to fix (as you will have to download the API too!)

# Screenshots
![Latest](http://i.imgur.com/exz22XW.gif)
![Main Settings](http://i.imgur.com/O5XTB2R.png)
![Statistics](http://i.imgur.com/fmS38kZ.png)

# API Link 
https://github.com/FeroxRev/Pokemon-Go-Rocket-API

# Features
- Walking (human)
- Custom speeds
- Lat/Lng positon (and walks back on reset)
- Evolve Selected (or unselected) pokemon - CP/IV filter
  - If both match it will evolve, otherwise it will not evolve.
- Catch Selected (or unselected) pokemon - CP/IV filter
  - If both match it will catch, otherwise it will TRANSFER them.
- Transfer Selected (or unselected) pokemon - CP/IV filter
  - If EITHER match (cp/iv) it will NOT transfer the pokemon.
- Use berries on certain pokemons if probability of capture is less than x (out of 100)
- Recycle certain items if you have more than the specified amount
- Basic statistics (thanks to: https://github.com/Spegeli/Pokemon-Go-Rocket-API)
- Perform tasks while idling in a location
- Evolve, or transfer selected pokemon from your inventory
- Show your pokemon inventory, pokeball inventory, or item inventory (detailed).
- Google Maps integration - live minimap!
- Google Direction API Integrated

# Donate
Bitcoin Address: 1LAPANo2N1jBBBHvBEqbMxG13pWqV1xCsY


# To Do
- Don't waste pokeballs on softbanned accounts
- Tutorial for retards
- Lured pokemons
- Deploy insense
- Catch pokemon while moving (hook walking function with event)
