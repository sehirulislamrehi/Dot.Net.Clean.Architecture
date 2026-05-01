# Migration Common Commands
 - Migration Add: dotnet ef migrations add InitialCreate --project CleanArchitecture.Infrastructure --startup-project CleanArchitecture
 - Migration Update: dotnet ef database update --project CleanArchitecture.Infrastructure --startup-project CleanArchitecture
 - Migration Remove: dotnet ef migrations remove --project CleanArchitecture.Infrastructure --startup-project CleanArchitecture
 - Migration Rollback: dotnet ef database update 20260428054138_Alter_Role_280420261141 --project CleanArchitecture.Infrastructure --startup-project CleanArchitecture