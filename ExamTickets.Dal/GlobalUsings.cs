// dotnet ef --startup-project ../ExamTickets/ migrations add init --context 
// dotnet ef --startup-project ../ExamTickets/ migrations add init --context IdentityContext

global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using ExamTickets.Domain.Entities;
global using ExamTickets.Domain.Identity;

