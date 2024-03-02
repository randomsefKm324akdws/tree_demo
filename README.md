1. load backend solution: backend.sln
2. update all "appsettings.json"
3. create database:
	cd C:\Repos\tree\tree
	dotnet ef migrations add InitialCreate -s api -p da_ef_model
	dotnet ef database update -s api -p da_ef_model
4. start "api" project