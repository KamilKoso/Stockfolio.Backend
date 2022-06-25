using Stockfolio.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace Stockfolio.Modules.Users.Core.Commands;

internal record ConfirmEmail([Required] Guid UserId, [Required] string EmailConfirmationToken) : ICommand;