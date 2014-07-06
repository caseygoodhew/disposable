namespace Disposable.Data.Packages
{
    /// <summary>
    /// Loose collection of strings used in database connections to promote code consitency.
    /// </summary>
    internal static class PackageConstants
    {
        // schemas
        internal static readonly string Disposable = "Disposable";

        // packages
        internal static readonly string UserPkg = "user_pkg";

        // procedures
        internal static readonly string Authenticate = "Authenticate";
        internal static readonly string CreateUser = "CreateUser";

        // parameters
        internal static readonly string InApproved = "in_approved";
        
        internal static readonly string InEmail = "in_email";

        internal static readonly string InPassword = "in_password";

        internal static readonly string InUsername = "in_username";

        internal static readonly string OutConfirmationGuid = "out_confirmation_guid";

        internal static readonly string OutCursor = "out_cur";
        
        internal static readonly string OutResult = "out_result";

        internal static readonly string OutUserSid = "out_user_sid";
    }
}
