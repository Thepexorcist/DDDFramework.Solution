# DDDFramework

DDDFramework is an application framework for C# applications built with _DDD_

## Features
This solution provides a framework for building _DDD_ applications. Includes building blocks for implementing _aggregates_, _entities_, _value objects_, _repositories_ and _domain events_.
The framework also includes the support for handling multi-tenancy and support for eventual consistency using the Outbox pattern.

## Example
The framework includes an example using all parts of the building blocks.
Multi-tenancy and the eventual consistency between two bounded contexts.
The example uses an in memory event bus. This won't work as an event bus if both contexts runs in seperate processes.

This example uses a Blazor application that will set up both contexts and running them in one process instead of two.
Both contexts can be setup as running in seperate processes but then another implementation of the event bus must be present.

## Example features
- Aggregates, Multi tenant aggregates
- Entities
- Value objects
- Domain services
- Domain Events
- Repositories, Multi tenant repositories
- Domain event handling (within same bounded context)
- Domain notification handling (external communication outside bounded context using eventual consistency)
- Integration event handling
