﻿using System;
using System.Collections.Generic;

namespace AiurObserver
{
    public class Subscription<T> : IDisposable
    {
        private readonly List<IAsyncObserver<T>> _observers;
        private readonly IAsyncObserver<T> _observer;

        internal Subscription(List<IAsyncObserver<T>> observers, IAsyncObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}
