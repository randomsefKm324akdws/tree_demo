Task description:
https://test.vmarmysh.com/user/description/backend

1. load backend solution: backend.sln
2. update all "appsettings.json"
3. create database:
	dotnet ef database update -s api -p da_ef_model
4. start "api" project
