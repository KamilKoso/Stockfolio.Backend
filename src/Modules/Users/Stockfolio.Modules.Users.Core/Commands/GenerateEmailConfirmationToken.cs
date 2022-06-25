using Stockfolio.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace Stockfolio.Modules.Users.Core.Commands;

internal record GenerateEmailConfirmationToken([Required] Guid UserId) : ICommand;