using System;
using Basic.Healths;

namespace Interfaces
{
    public interface IChange<out T>
    {
        event Action<T> ChangeEvent;
    }
}