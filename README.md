# R6-D9 Bot Dashboard
This repository houses the dashboard of the Discord moderation bot called R6-D9.

## Prerequisites
1. Dotnet SDK
2. Dotnet Runtime
3. EntityFramework CLI Tools

```shell
sudo pacman -Sy dotnet-sdk
sudo pacman -Sy dotnet-runtime
dotnet tool install --global dotnet-ef
```

If you run `dotnet run` and it gives you an error that there are missing frameworks, you'll probably have to install the `aspnet-runtime` package as well.
```shell
sudo pacman -S aspnet-runtime
```

If you try to run `dotnet ef` and it prompts you with the following error:

Try re-adding `dotnet` to your path.
On Linux you can do so by:

```shell
export PATH=$PATH:~/.dotnet/tools
```

## Project setup

1. Clone the repository
2. Install frontend dependencies with `docker run -v "${PWD}/frontend":/home/node/app -w /home/node/app -u 1000:1000 node:21-alpine npm install;`
3. Run backend migrations `dotnet ef database update`

## Migrations
In order to create a new migration, you need the EntityFramework CLI Tools to be installed. (see prerequisites list)

Creating an actual migration:
```shell
dotnet ef migrations add
```

## TODO List
- [] Update the README
- [] Setup API Controllers
