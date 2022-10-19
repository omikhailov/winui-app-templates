using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.ModelLayer
{
    public static class INotifyPropertyChangedExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static bool Set<T>(this INotifyPropertyChanged bindableObject, ref T field, T value, PropertyChangedEventHandler propertyChangedEventHandler, [CallerMemberName] string propertyName = "")
        {
            return Set(bindableObject, ref field, value, propertyChangedEventHandler, EqualityComparer<T>.Default, propertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static bool Set<T>(this INotifyPropertyChanged bindableObject, ref T field, T value, 
            PropertyChangedEventHandler propertyChangedEventHandler, IEqualityComparer<T> equalityComparer, [CallerMemberName] string propertyName = "")
        {
            var areEquals = equalityComparer.Equals(field, value);

            if (!areEquals)
            {
                field = value;

                propertyChangedEventHandler?.Invoke(bindableObject, new PropertyChangedEventArgs(propertyName));
            }

            return areEquals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static bool Set<T>(this INotifyPropertyChanged bindableObject, ref T field, T value, Action<string> notifyPropertyChangedDelegate, [CallerMemberName] string propertyName = "")
        {
            return Set(bindableObject, ref field, value, notifyPropertyChangedDelegate, EqualityComparer<T>.Default, propertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static bool Set<T>(this INotifyPropertyChanged bindableObject, ref T field, T value,
            Action<string> notifyPropertyChangedDelegate, IEqualityComparer<T> equalityComparer, [CallerMemberName] string propertyName = "")
        {
            var areEquals = equalityComparer.Equals(field, value);

            if (!areEquals)
            {
                field = value;

                notifyPropertyChangedDelegate(propertyName);
            }

            return areEquals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static void Raise(this INotifyPropertyChanged bindableObject, PropertyChangedEventHandler propertyChangedEventHandler, [CallerMemberName] string propertyName = "")
        {
            propertyChangedEventHandler?.Invoke(bindableObject, new PropertyChangedEventArgs(propertyName));
        }
    }
}
