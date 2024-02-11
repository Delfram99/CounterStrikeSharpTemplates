# CounterStrikeSharpTemplates

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## Description

CounterStrikeSharpTemplates is a thoughtfully designed template set for kick-starting your plugin development for the great [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp). The templates offer foundational structures and useful functions, along with pre-configured GitHub workflows, making the initiation of plugin development a breeze. With adaptable templates and an automated version control system, it caters to both beginners and experienced developers alike.

## Installation

1. Install the [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).
2. Install the templates package:

```bash
dotnet new -i CSSharpTemplates
```

## Usage

Available templates:

- `default`: сreates a basic plugin template.
- `config`: сreates a plugin template that includes a configuration file.
- `lang`: сreates a plugin template that includes a language file.
- `configlang`: сreates a plugin template that includes both a configuration and a language file.
- `datamysql`: сreates a plugin template that includes a database (MySQL), language file, configuration file, and commands.

You can also add your own templates.

To specify a template type, use `--t`:

```bash
dotnet new cssharp -n MyPlugin --t config
```

To create a new basic project (using the `default` template), just:

```bash
dotnet new cssharp -n MyPlugin
```

For GitHub integration (which provides GitHub workflows, .gitignore, etc.), add `--g`:

```bash
dotnet new cssharp -n MyPlugin --g
```

Customize with `--np` (plugin name) and `--ap` (author):

```bash
dotnet new cssharp -n MyPlugin --t datamysql --g --np "My Plugin" --ap "Author Name"
```

## Versioning

Push commit messages with `#major`, `#minor`, or `#patch` to update the plugin's version (when `--g` is used):

```bash
git commit -m "add new feature #minor"
```

For manual version specification, use the `workflow_dispatch` option in the GitHub Actions tab.

## Environment Variables

In `dotnet.yml`, modify these variables as needed:

- `PLUGIN_NAME`: your plugin's name.
- `DOTNET_VERSION`: .NET version (default is `7.0`).
- `PATH_PLUGIN`: plugin path (default is `addons/counterstrikesharp/plugins/`).
- `START_VERSION`: starting version (default is `1.0.0`).
- `USE_V_VERSION`: prefix the version number with a 'v' (default is `true`).
  
## Credits

This project was inspired by [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp). A big thank you to the creators of this project for their contribution to the community and for providing the foundation for the development of these templates.❤️
