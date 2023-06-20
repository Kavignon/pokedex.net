I'm focused on building an MVP for the Pokedex. As such, I'll be using the roadmap to showcase future incoming features, but for the MVP, I won't be focused on them.

The MVP will allow users of the web service to:
- Query existing Pokemon in the data source (SQL Lite),
- Change their stats,
- Delete a pokemon entry from the source.

Through SQL Lite, I'll be extracting the data contained in the provided [CSV file](https://gist.github.com/armgilles/194bcff35001e7eb53a2a8b441e8b2c6) and have a place where I can quickly manipulate the data as the Pokedex requires it. Without any requirements, I've decided to treat the configuration as in-memory for the moment. This will allow to have fast data manipulations since it'll be on RAM but it won't be persisted if the web API session's ends voluntirality* or not.

Due to the nature of the operations that will be run in the MVP, I've decided against setting up a [Repository](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design). That decision would be revisited as I'd moved from an MVP to supporting all features from the application depicted in the readme.

As I'm talking about data manipulations, something that I like to think about safe ways to manipulate the data from the application layer to avoid potential vulnerabilities on the persistence layer. I'll be parametrizing my commands to separate the SQL code from the user-provided data, preventing malicious input from being interpreted as SQL commands. Those commands will be perform through an ORM with Entity Framework.