# Migration Common Commands
 - Migration Add: dotnet ef migrations add InitialCreate --project CleanArchitecture.Infrastructure --startup-project CleanArchitecture
 - Migration Update: dotnet ef database update --project CleanArchitecture.Infrastructure --startup-project CleanArchitecture
 - Migration Remove: dotnet ef migrations remove --project CleanArchitecture.Infrastructure --startup-project CleanArchitecture