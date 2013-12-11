namespace Disposable.Security.Policies
{
    public interface IPasswordPolicy
    {
        int MaxInvalidPasswordAttempts { get; }
        
        int MinRequiredNonAlphanumericCharacters { get; }
        
        int MinRequiredPasswordLength { get; }
        
        int PasswordAttemptWindow { get; }
        
        string PasswordStrengthRegularExpression { get; }
    }
}
