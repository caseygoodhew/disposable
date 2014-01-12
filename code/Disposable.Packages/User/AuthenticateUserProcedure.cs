using System.Collections.Generic;
using System.Data;
using Disposable.Packages.Core;

namespace Disposable.Packages.User
{
    internal class AuthenticateUserProcedure : StoredProcedureDefinition
    {
        private static readonly string ProcedureName = "Authenticate";

        private static readonly string UsernameArgumentName = "in_username";

        private static readonly string PasswordArgumentName = "in_password";

        private static readonly string ResultArgumentName = "in_result";

        public AuthenticateUserProcedure() 
            : base(ProcedureName, 
                new List<InputParameter>
                {
                    new InputParameter(UsernameArgumentName, DbType.String),
                    new InputParameter(PasswordArgumentName, DbType.String)
                },
                new OutputParameter(ResultArgumentName, DbType.Boolean)
            )
        {
        }

        /// <summary>
        /// Generates the parameter list for the stored procedure arguments
        /// </summary>
        /// <param name="username">The username to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <returns>The parameterized list of arguments</returns>
        internal void SetParameters(string username, string password)
        {
            SetParameters(new Dictionary<string, object>
            {
                {UsernameArgumentName, username},
                {PasswordArgumentName, password}
            });
        }
    }
}
