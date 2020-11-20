﻿using AiurEventSyncer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiurEventSyncer.Abstract
{
    public interface IRemote<T>
    {
        public string Name { get; set; }
        public bool AutoPush { get; set; }
        public bool AutoPull { get; set; }
        public Func<Task> OnRemoteChanged { get; set; }
        public Commit<T> LocalPointer { get; set; }
        Task<IReadOnlyList<Commit<T>>> DownloadFromAsync(string localPointerPosition);
        Task<string> UploadFromAsync(string startPosition, IReadOnlyList<Commit<T>> commitsToPush);
    }
}
