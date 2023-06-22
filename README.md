# pokedex.net

## Overview
This project serves to act as a live Pokedex that can be queried to retrieve information on Pokemon that you might encounter in the wild.

Here's the data from where the Pokedex serves its data: https://gist.github.com/armgilles/194bcff35001e7eb53a2a8b441e8b2c6.

## Features

- Allows getting the data on a specific Pokemon when providing its name.
- Allows retrieving all legendary Pokemon data in the Pokedex.
- Allows retrieving the most searched Pokemon in the past 24 hours.
- Allows users to search for Pokemon with typos.
- Allows users to update the stats of a given Pokemon.
- Allows users to remove Pokemon from the Pokedex.

## High-level approach

Please look at [design.md](docs/design.md) for more information.

## Installation

To run the ASP.NET Core Web API locally, follow these steps:

### Prerequisites
- [.NET 7.X SDK](https://dotnet.microsoft.com/en-us/download)
- [Postman](https://www.postman.com/downloads/)

### Clone the repository

1. Open a terminal or command prompt.

2. Change the current directory to where you want to clone the repository.

3. Run the following command to clone the repository:

`git clone https://github.com/Kavignon/pokedex.net`

## Usage

### Build and Run the API

1. Change the current directory to the cloned repository:

`cd pokedex.net`

2. Restore the project dependencies:

`dotnet restore`

3. Build the project:

`dotnet build`

4. Run the API locally:

`dotnet run`
