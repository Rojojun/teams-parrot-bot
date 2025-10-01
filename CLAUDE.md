# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Microsoft Bot Framework v4 echo bot application built with .NET 6. The bot receives messages from users and echoes them back, with Korean language support in the welcome message. The application follows standard Bot Framework architecture patterns.

## Development Commands

### Build and Run
```bash
# Build the project
dotnet build

# Run the application
dotnet run
```

The bot will start on `http://localhost:3978` by default.

### Testing with Bot Framework Emulator
- Use Bot Framework Emulator v4.9.0+
- Connect to: `http://localhost:3978/api/messages`

## Architecture

### Core Components

- **Program.cs** - Application entry point with ASP.NET Core host configuration
- **Startup.cs** - Dependency injection and middleware configuration
- **AdapterWithErrorHandler.cs** - Bot adapter with centralized error handling
- **Bots/EchoBot.cs** - Main bot logic implementing `ActivityHandler`
- **Controllers/BotController.cs** - ASP.NET Core controller handling HTTP requests at `/api/messages`
- **Command/Command.cs** - Command pattern implementation for bot commands (Help, Show, Store)

### Key Dependencies
- Microsoft.Bot.Builder.Integration.AspNet.Core (v4.22.0)
- Microsoft.AspNetCore.Mvc.NewtonsoftJson (v3.1.1)

### Configuration
- **appsettings.json** - Bot authentication settings (MicrosoftAppId, MicrosoftAppPassword, etc.)
- **appsettings.Development.json** - Development-specific overrides

### Bot Logic Flow
1. HTTP requests hit `BotController` at `/api/messages`
2. Controller delegates to `AdapterWithErrorHandler`
3. Adapter processes request and invokes `EchoBot`
4. `EchoBot.OnMessageActivityAsync` handles user messages
5. Commands are processed via `Command.FindCommand()` and executed

### Error Handling
Centralized error handling in `AdapterWithErrorHandler` logs exceptions and sends user-friendly error messages.

### Deployment
Azure deployment templates are available in `DeploymentTemplates/` for both new and existing resource groups.
