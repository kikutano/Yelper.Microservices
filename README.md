[![.NET](https://github.com/kikutano/Yelper.Microservices/actions/workflows/dotnet.yml/badge.svg)](https://github.com/kikutano/Yelper.Microservices/actions/workflows/dotnet.yml)

# Yelper.Microservices
**Yelper** is a "Twitter Clone **Playground**" (yeah, of course!). Ad I said, this project is **my playground to experiment** patterns, best practices, technologies and more. So, a lot of over-engineering here, fancy solutions and experimentations.

## Overview
There are 3 services that comunicate using RabbitMq and a Docker composite to run everything in local. You must install Docker to run the entire solution and tests!

## Services
- **Identity**, to create and auth users.
- **Writer**, to create new Yelps (tweet).
- **Reader**, to retrieve Yelps from users for your profile.

## Automated Tests
Yelper.Microservices use a real database to run the tests. So install Docker if you want run the test correctly.

# Automated Tests with TestContainers
- SqlServer
- Redis

## More
More updates are coming..
