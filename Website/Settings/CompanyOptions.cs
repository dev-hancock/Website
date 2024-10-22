// -----------------------------------------------------------------------
//  <copyright file="CompanyOptions.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Settings
{
    public class CompanyOptions
    {
        public string Name { get; set; } = default!;

        public string Number { get; set; } = default!;

        public int Registered { get; set; } = default!;
    }
}