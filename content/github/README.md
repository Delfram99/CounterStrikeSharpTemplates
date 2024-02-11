# CSSharpTemplates

## Useful Links

- [CounterStrikeSharp API Documentation](https://docs.cssharp.dev/api/CounterStrikeSharp.API.html): The API documentation for writing plugins in CounterStrikeSharp.
- [CounterStrikeSharp Discord Channel](https://discord.gg/tfPyCqyCPv): Join the CounterStrikeSharp community on Discord for help and discussion about plugin development.

## Quick Start with Git

Before you start, make sure you have created a new repository on GitHub.

1. **Initialize a Git repository**

   Start by initializing a new Git repository with the following command:

   ```bash
   git init
   ```

2. **Add a remote repository**

   Next, add a remote repository. Replace `https://github.com/username/repository.git` with the URL of your own repository:

   ```bash
   git remote add origin https://github.com/username/repository.git
   ```

3. **Create a new branch**

   Create a new branch and switch to it. The `-M` option will force Git to create the branch if it doesn't exist:

   ```bash
   git branch -M main
   ```

4. **Stage your changes**

   Stage all changes in the directory:

   ```bash
   git add .
   ```

5. **Commit your changes**

   Commit your staged changes with a descriptive message:

   ```bash
   git commit -m "your commit message"
   ```

6. **Push your changes to GitHub**

   Finally, push your commits to the remote repository:

   ```bash
   git push -u origin main
   ```

### Working with Plugin Versions

Your plugin's version is automatically updated when you push commit messages containing `#major`, `#minor`, or `#patch`. These keywords trigger a version bump:

- `#major`: Triggers a major version bump (e.g., 1.0.0 to 2.0.0).
- `#minor`: Triggers a minor version bump (e.g., 1.0.0 to 1.1.0).
- `#patch`: Triggers a patch version bump (e.g., 1.0.0 to 1.0.1).

For example, if you want to increase the minor version, your commit message might look like this:

```bash
git commit -m "add new feature #minor"
```

Upon pushing a commit with one of these keywords, a GitHub Action is triggered. This action builds your plugin with the specified version and creates a new release.

In addition to automatic version bumps through commit messages, this workflow also supports manual versioning through `workflow_dispatch`.

You can manually specify a version by triggering the `workflow_dispatch` event from the GitHub Actions tab. After setting the version, the workflow will build your plugin with the specified version and create a new release, similar to the process that occurs when you push a commit with `#minor`, `#major`, or `#patch` in the message.

For example, to manually trigger a workflow with a specific version:

1. Go to the 'Actions' tab in your GitHub repository.
2. From the left menu, select the 'Build & Release' workflow you want to run.
3. Click 'Run workflow'.
4. Enter the version you want to use in the 'Plugin Version' field.
5. Click 'Run workflow'.

This will trigger the workflow with the specified version, build your plugin, and create a new release.

### GitHub Actions Workflow Environment Variables

In the `dotnet.yml` file, several environment variables are defined under the `env` section:

- `PLUGIN_NAME`: The name of your plugin.
- `DOTNET_VERSION`: The version of .NET you are using in your project. Currently, it's set to `7.0`.
- `PATH_PLUGIN`: The path where your plugin is located. Here, it's `addons/counterstrikesharp/plugins/`.
- `START_VERSION`: The starting version of your project. It's set to `1.0.0`.
- `USE_V_VERSION`: A boolean value indicating whether to prefix the version number with a 'v'. If set to `true`, your version will look like `v1.0.0`.
  
You can modify these variables according to your project's needs.

## Conclusion

That's all you need to know about managing your plugin's version with GitHub. Remember, you can either push a commit with `#major`, `#minor`, or `#patch` in the message for automatic versioning, or manually set a version using `workflow_dispatch`.

**Good luck with your plugin development!**
