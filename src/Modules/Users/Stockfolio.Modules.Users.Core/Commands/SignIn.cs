using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Stockfolio.Modules.Users.Core.Commands;
internal record SignIn([Required] string Password, bool RememberMe = false) : ICommand
{
    private MailAddress _email;

    [Required]
    public string Email
    {
        get { return _email.ToString(); }
        set
        {
            try
            {
                _email = new MailAddress(value.ToLowerInvariant());
            }
            catch (FormatException)
            {
                throw new InvalidEmailAddressFormatException(value);
            }
        }
    }
}