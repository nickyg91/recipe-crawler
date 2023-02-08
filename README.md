# recipe-crawler
Crawler for recipe websites because I don't want to hear the life story I just want the goods.

# Dev Setup
## Docker pull commands to run:
- docker pull postgres
- docker pull redis
- docker pull dbck/mailtrap

## Docker container run commands:
- docker run -p 6379:6379  --restart unless-stopped  --name dev-redis -d redis
- docker run -p 5432:5432  --restart unless-stopped  --name dev-postgres -v dev-postgres -e POSTGRES_PASSWORD=<password> -d postgres
- docker run -p 9080:80 -p 2025:25  --restart unless-stopped  --name mailtrap -d dbck/mailtrap

## Dotnet commands to run
- dotnet tool install --global dotnet-ef
- dotnet ef migrations add <migration name>

## Set your environment variable if not on windows
- export ASPNETCORE_ENVIRONMENT='Development'