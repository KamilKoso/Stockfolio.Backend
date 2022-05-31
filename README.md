# Stockfolio

## About

**Stockfolio** is the app built as **Modular Monolith** which lets the investors to keep track on their investment  and measure the performance, written in **[.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)**. Each module is an independent **vertical slice** with its custom architecture, and the overall integration between the modules is mostly based on the **event-driven** approach to achieve **greater autonomy** between the modules. The shared components (such as cross-cutting concerns & abstractions) can be also found in a **[modular framework](https://github.com/devmentors/modular-framework)**.