namespace CT.Core.Common
{
    public interface IMapTo<out TOut, in TIn>
    {
        TOut MapTo(TIn item);
    }

    public interface IMapTo<out TOut, in TIn1, in TIn2>
    {
        TOut MapTo(TIn1 item1, TIn2 item2);
    }

    public interface IMapTo<out TOut, in TIn1, in TIn2, in TIn3>
    {
        TOut MapTo(TIn1 item1, TIn2 item2, TIn3 item3);
    }

    public interface IMapTo<out TOut, in TIn1, in TIn2, in TIn3, in TIn4>
    {
        TOut MapTo(TIn1 item1, TIn2 item2, TIn3 item3, TIn4 item4);
    }

    public interface IMapTo<out TOut, in TIn1, in TIn2, in TIn3, in TIn4, in TIn5>
    {
        TOut MapTo(TIn1 item1, TIn2 item2, TIn3 item3, TIn4 item4, TIn5 item5);
    }
}
