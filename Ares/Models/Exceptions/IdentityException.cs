using Microsoft.AspNetCore.Identity;

namespace API.Models.Exceptions
{
    public class IdentityException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }
        public IdentityException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }
    }
}
