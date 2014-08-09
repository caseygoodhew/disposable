namespace Disposable.Common.Conversion
{
    public interface IConvert<in TFrom, out TTo> where TFrom : class 
                                                 where TTo : class
    {
        TTo Convert(TFrom @from);
    }
}
