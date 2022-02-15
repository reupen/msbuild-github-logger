# MSBuild logger for GitHub Actions

This is a custom MSBuild logger that emits GitHub Actions annotations for warnings and errors.

These appear in the PR diff:

![PR diff example](https://user-images.githubusercontent.com/12693549/154147610-603c3ebd-50af-4a8c-b9c2-573be3c3eb68.png)

And in the run summary:

![Run summary](https://user-images.githubusercontent.com/12693549/154338298-8023848b-19a9-4494-b581-4be99da2f188.png)

## Requirements

The logger has been tested with Visual Studio 2022 17.1 and 2019 16.11. Similar versions should be compatible.

## Usage

`Reupen.MSBuild.GitHubLogger.dll` needs to be available to the GitHub Actions workflow. This can be achieved either by
downloading it during the workflow run, or commiting it to your repo.

Then, `/logger:path\to\Reupen.MSBuild.GitHubLogger.dll` should be passed to the logger.

For a complete example, see https://github.com/reupen/columns_ui/tree/master/.github/workflows/build.yml.

## Notes

The logger currently logs all warnings and errors. This may not be suitable if your project has a large number of warnings.