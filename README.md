# Yelper.Microservices
Yelper is a Twitter clone, with basic features. This project is a **proof of concept**, **an exercise** on **Microservices Architectures** and best pratices. It's *intentionally over-engineered* sometime, as I said, it's an exercise. This project is ispired from [eShopContainer](https://github.com/dotnet/eShop), my goal is to apply **Clean Architecture** and **DDD patterns** and understand how they fit on a real project. 

Yelper will be improved over the time, with new features and principle applications. So please feel free to give me some feedbacks.

## Current release
1.0.0

## Overview
Yelper is a Twitter clone with **3 Microservices** and a **Message Broker (RabbitMq)**.

- **Identity**, to create and auth users.
- **Writer**, to create new Yelps (tweet).
- **Reader**, to retrieve Yelps from users for your profile.
