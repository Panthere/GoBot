# GoBot
A simple bot using the Rocket API for Pokemon Go.


# Warning
This is not a fully functioning bot. This was made in only a couple of hours. The API that this uses changes several hundred times a day.
If you build from source there may be some small issues to fix (as you will have to download the API too!)

# API Link 
FeroxRev has sadly quit the scene. https://github.com/NecronomiconCoding/Pokemon-Go-Bot is the API that will work.

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

