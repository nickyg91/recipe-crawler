# recipe-crawler
Crawler for recipe websites because I don't want to hear the life story I just want the goods.

# Dev Setup
## Docker pull commands to run:
- docker pull postgres
- docker pull redis
- docker pull dbck/mailtrap

## Docker container run commands:
- docker run -p 6379:6379  --restart unless-stopped  --name dev-redis -d redis
- docker run -p 5432:5432  --restart unless-stopped  --name dev-postgres -v dev-postgres -e POSTGRES_PASSWORD=<password> -e POSTGRES_INITDB_ARGS="--auth-host=scram-sha-256 --auth-local=scram-sha-256" -d postgres

- docker run -p 9080:80 -p 2025:25  --restart unless-stopped  --name mailtrap -d dbck/mailtrap

## Dotnet commands to run
- dotnet tool install --global dotnet-ef
- dotnet ef migrations add <migration name>
- dotnet tool install -g dotnet-aspnet-codegenerator

## Set your environment variable if not on windows and it isnt already set
- export ASPNETCORE_ENVIRONMENT='Development'