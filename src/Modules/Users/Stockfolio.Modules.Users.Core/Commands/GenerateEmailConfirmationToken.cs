using Stockfolio.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace Stockfolio.Modules.Users.Core.Queries;

internal record GenerateEmailConfirmationToken([Required] Guid UserId) : ICommand;