// -----------------------------------------------------------------------
//  <copyright file="AppState.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.State
{
    using System.Runtime.CompilerServices;
    using Website.Services.GitHub;

    public class AppState
    {
        private Repository[] _repos = Array.Empty<Repository>();

        public event Action? OnChanged;

        public Repository[] Repos
        {
            get => _repos;
            set => SetProperty(ref _repos, value);
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? property = default)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;

            OnChanged?.Invoke();

            return true;
        }
    }
}