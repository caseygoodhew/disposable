namespace Disposable.Data.Database.Exceptions
{
    public static class ProgrammaticDatabaseExceptions
    {
        static ProgrammaticDatabaseExceptions()
        {
            var type = typeof(ProgrammaticDatabaseExceptions);
            
            foreach (var field in type.GetFields())
            {
                field.SetValue(null, new ExceptionDescription(field.Name));
            }
        }
        
        // ReSharper disable UnassignedReadonlyField
        public static readonly ExceptionDescription DuplicateEmail;
        

        public static readonly ExceptionDescription Unhandled;
        // ReSharper restore UnassignedReadonlyField
    }
}
