# CounterStrikeSharpTemplates

[![NuGet Version](https://img.shields.io/nuget/v/CounterStrikeSharpTemplates)](https://www.nuget.org/packages/CounterStrikeSharpTemplates)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## Description

Project templates for [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) plugins: a ready-made project structure, a VS Code build task and an optional GitHub workflow with automatic versioning and releases.

## Installation

1. Install the [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).
2. Install the templates package:

```bash
dotnet new install CounterStrikeSharpTemplates
```

## Usage

```bash
dotnet new cssharp -n MyPlugin
```

### Templates (`--t`)

| Template | Contents |
| --- | --- |
| `default` | basic plugin, used when `--t` is omitted |
| `config` | + JSON configuration |
| `lang` | + translations and commands |
| `configlang` | + configuration, translations and commands |
| `datamysql` | + MySQL database, configuration, translations and commands |

[You can also add your own templates](#adding-your-own-template).

### Options

| Option | Description |
| --- | --- |
| `--t` | template type (see the table above) |
| `--g` | add GitHub files: build & release workflow, release notes config, `.gitignore` |
| `--np` | plugin name used in code (default `MyPlugin`) |
| `--ap` | author name (default `AuthorName`) |
| `--skipRestore` | do not run `dotnet restore` after the project is created |

```bash
dotnet new cssharp -n MyPlugin --t datamysql --g --np "My Plugin" --ap "Author Name"
```

## Local Compilation

`Ctrl + Shift + B` in Visual Studio Code runs the build task from `.vscode/tasks.json`. To compile the plugin straight into your server folder, replace the output path in that file:

```json
"compiled/CSSharpTemplates"
```

with your own, for example:

```json
"D:/cs2-server/game/csgo/addons/counterstrikesharp/plugins/CSSharpTemplates"
```

## Versioning

Available when the project is created with `--g`. The version is bumped by a keyword in the commit message:

```bash
git commit -m "add new feature #minor"
```

`#major` → `2.0.0`, `#minor` → `1.1.0`, `#patch` → `1.0.1`. A version can also be set manually through `Run workflow` on the Actions tab.

The workflow builds the plugin, creates a release with automatically generated notes and attaches the zip archive. It only starts when source files change - editing the README does not trigger a build.

## Environment Variables

In `dotnet.yml`, modify these variables as needed:

| Variable | Default | Description |
| --- | --- | --- |
| `PLUGIN_NAME` | plugin name | used for the archive and the folder inside it, do not use spaces |
| `DOTNET_VERSION` | `10.0.x` | .NET SDK version used to build |
| `TARGET_FRAMEWORK` | `net10.0` | must match `TargetFramework` in the `.csproj` |
| `PATH_PLUGIN` | `addons/counterstrikesharp/plugins/` | plugin path inside the archive |
| `START_VERSION` | `1.0.0` | version used for the first release |
| `USE_V_VERSION` | `true` | prefix the version number with a `v` |

## Adding Your Own Template

1. Clone the repository:

    ```bash
    git clone https://github.com/Delfram99/CounterStrikeSharpTemplates.git
    ```

2. In the `content/templates` directory, create a new directory for your template. For example, if your template is named "example", create a directory named `example` and place all the necessary template files in it.

3. In the `content/.template.config/template.json` file, find the `"choices"` section under `"t"`. Add a new choice for your template. For example:

    ```json
    {
        "choice": "example",
        "description": "example description."
    }
    ```

4. In the `"sources"` section, add a new source with a condition for your template. For example:

    ```json
    {
        "source": "templates/example",
        "target": "./",
        "condition": "(t == 'example')"
    }
    ```

5. Before installing the new template, you need to uninstall the existing one to avoid conflicts:

    ```bash
    dotnet new uninstall CounterStrikeSharpTemplates
    ```

6. Install the new template:

    ```bash
    dotnet new install . --force
    ```

## Contributing

New templates are added as needed. If you have an idea, found a problem or want to improve something - open an issue or a pull request, feedback is always welcome.

## Credits

This project was inspired by [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp). A big thank you to the creators of this project for their contribution to the community and for providing the foundation for the development of these templates. ❤️
