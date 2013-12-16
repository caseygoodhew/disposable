using System.Collections.Generic;
using Disposable.DataAccess.StoredProcedures;
using MySql.Data.MySqlClient;

namespace Disposable.DataAccess.Security.Accounts.StoredProcedures
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticateUserProcedure : IStoredProcedureDefinition
    {
        private static readonly string PackageName = Packages.User;

        private static readonly string ProcedureName = "Authenticate";

        private static readonly string UsernameArgumentName = "in_username";

        private static readonly string PasswordArgumentName = "in_password";

        private static readonly string ResultArgumentName = "in_result";

        public AuthenticateUserProcedure()
        {
            Package = PackageName;
            Procedure = ProcedureName;
            
            InputParameters = new List<InputParameter>
            {
                new InputParameter(UsernameArgumentName, MySqlDbType.String),
                new InputParameter(PasswordArgumentName, MySqlDbType.String)
            };

            OutputParameter = new OutputParameter(ResultArgumentName, MySqlDbType.Bit);
        }

        /// <summary>
        /// Gets the package name where the procedure resides
        /// </summary>
        public string Package { get; private set; }

        /// <summary>
        /// Gets the name of the procedure
        /// </summary>
        public string Procedure { get; private set; }

        /// <summary>
        /// Gets the input parameters expected by the procedure
        /// </summary>
        public IEnumerable<InputParameter> InputParameters { get; private set; }

        /// <summary>
        /// Gets the output parameter provided by the procedure
        /// </summary>
        public OutputParameter OutputParameter { get; private set; }

        /// <summary>
        /// Generates the parameter list for the stored procedure arguments
        /// </summary>
        /// <param name="username">The username to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <returns>The parameterized list of arguments</returns>
        public static IDictionary<string, object> Parameterize(string username, string password)
        {
            return new Dictionary<string, object>
            {
                { UsernameArgumentName, username },
                { PasswordArgumentName, password }
            };
        }
    }
}
