using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace API.Middleware
{
    public class MultilanguageIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = "DuplicateUserName", Description = $"\"{ userName }\" kullanıcı adı kullanılmaktadır." };
        public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = "InvalidUserName", Description = "Geçersiz kullanıcı adı." };
        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = "DuplicateEmail", Description = $"\"{ email }\" başka bir kullanıcı tarafından kullanılmaktadır." };
        public override IdentityError InvalidEmail(string email) => new IdentityError { Code = "InvalidEmail", Description = "Geçersiz email." };
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new IdentityError { Code = "InvalidPassword", Description = $"En az {uniqueChars} özel karakter içermeli" };
        public override IdentityError PasswordTooShort(int length) => new IdentityError { Code = "InvalidPassword", Description = $"Şifre en az {length} karakter olmalı" };
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = "InvalidPassword", Description = $"Şifre en az 1 rakam içermeli" };
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = "InvalidPassword", Description = $"Şifre en az 1 büyük harf içermeli" };
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = "InvalidPassword", Description = $"Şifre en az 1 küçük harf içermeli" };
    }
}
