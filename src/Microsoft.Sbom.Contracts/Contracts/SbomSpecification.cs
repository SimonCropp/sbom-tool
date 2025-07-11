// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Sbom.Contracts;

/// <summary>
/// Represents the specification of the SBOM.
/// For ex. SPDX 2.2.
/// </summary>
public class SbomSpecification : IEquatable<SbomSpecification>
{
    /// <summary>
    /// Gets the name of the SBOM specification.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the version of the SBOM specification.
    /// </summary>
    public string Version { get; private set; }

    public SbomSpecification(string name, string version)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(version))
        {
            throw new ArgumentException($"'{nameof(version)}' cannot be null or empty.", nameof(version));
        }

        Name = name;
        Version = version;
    }

    /// <summary>
    /// Parse the given string into a <see cref="SbomSpecification"/> object.
    /// </summary>
    /// <param name="value">The string representation of the SBOM.</param>
    /// <returns>A SbomSpecification object.</returns>
    /// <example>spdx:2.2.</example>
    public static SbomSpecification Parse(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("The SBOM specification string is empty");
        }

        var values = value.Split(':');
        if (values is not { Length: 2 } || values.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("The SBOM specification string is not formatted correctly. The correct format is <name>:<version>.");
        }

        return new SbomSpecification(values[0], values[1]);
    }

    public override string ToString()
    {
        return $"{Name}:{Version}";
    }

    public override bool Equals(object obj) => this.Equals(obj as SbomSpecification);

    public bool Equals(SbomSpecification other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != other.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        return Name.ToLowerInvariant() == other.Name.ToLowerInvariant() &&
               Version.ToLowerInvariant() == other.Version.ToLowerInvariant();
    }

    public override int GetHashCode()
    {
        var hashCode = 2112831277;
        hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Version);
        return hashCode;
    }

    public static bool operator ==(SbomSpecification lhs, SbomSpecification rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }

        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(SbomSpecification lhs, SbomSpecification rhs) => !(lhs == rhs);
}
