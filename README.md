# pokedex.net

## Overview
This project serves to act as a live Pokedex that can be queried to retrieve information on Pokemon that you might encounter in the wild.

Here's the data from where the Pokedex serves its data: https://gist.github.com/armgilles/194bcff35001e7eb53a2a8b441e8b2c6.

## Features

- Allows to get the data on a specific Pokemon when providing its name.
- Allows to retrieve all legendary Pokemon data in the Pokedex.
- Allows to retrieve the most search Pokemon in the past 24 hours.
- Allows users to search for Pokemons with typos.
- Allows for users to update the stats of a given Pokemon.
- Allows for users to remove Pokemon from the Pokedex.

## High-level approach

Please take a look at [design.md](docs/design.md) for more information.

## Installation

To run the ASP.NET Core Web API locally, follow these steps:

### Prerequisites
- [.NET 7.X SDK](https://dotnet.microsoft.com/en-us/download)

- [SQL Lite](https://www.sqlite.org/download.html)

- [Docker](https://www.docker.com)

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

4. Apply database migrations:

`dotnet ef database update`

5. Run the API locally:

`dotnet run`

### Executing the container

1. Ensure Docker is running on your machine.

2. Build the Docker image:

`docker build -t pokedex-api`

3. Run the Docker container:

`docker run -p 8080:80 pokedex-api`

4. The API will be available at http://localhost:8080.


### Running Tests

1. Change the current directory to the cloned repository:

`cd pokedex.net`

2. Run the tests:

`dotnet test`

## Roadmap

Please take a look at [roadmap.md](docs/roadmap.md) for more information.