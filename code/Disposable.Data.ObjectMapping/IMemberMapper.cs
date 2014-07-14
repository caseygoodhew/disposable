namespace Disposable.Data.ObjectMapping
{
    internal interface IMemberMapper<TObject> where TObject : class
    {
        string MemberName { get; }

        void SetValue(TObject obj, object value);
    }
}
