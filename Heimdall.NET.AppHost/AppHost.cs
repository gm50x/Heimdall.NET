var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Heimdall_WebAPI>("heimdall-webapi");

builder.Build().Run();
