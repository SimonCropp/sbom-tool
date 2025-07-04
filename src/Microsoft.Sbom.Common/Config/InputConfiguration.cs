// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Sbom.Common.Config.Attributes;
using Microsoft.Sbom.Contracts;
using Microsoft.Sbom.Contracts.Enums;
using Microsoft.Sbom.Extensions.Entities;
using Serilog.Events;

namespace Microsoft.Sbom.Common.Config;

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:Static readonly fields should begin with upper-case letter", Justification = "Private fields with the same name as public properties.")]
public class InputConfiguration : IConfiguration
{
    /// <inheritdoc cref="IConfiguration.BuildDropPath" />
    [DirectoryExists]
    [DirectoryPathIsWritable(ForAction = ManifestToolActions.Generate)]
    [ValueRequired]
    [Path]
    public ConfigurationSetting<string> BuildDropPath { get; set; }

    /// <inheritdoc cref="IConfiguration.BuildComponentPath" />
    [DirectoryExists]
    [Path]
    public ConfigurationSetting<string> BuildComponentPath { get; set; }

    /// <inheritdoc cref="IConfiguration.BuildListFile" />
    [FileExists]
    public ConfigurationSetting<string> BuildListFile { get; set; }

    /// <inheritdoc cref="IConfiguration.ManifestPath" />
    [Obsolete("This field is not provided by the user or configFile, set by system")]
    public ConfigurationSetting<string> ManifestPath { get; set; }

    /// <inheritdoc cref="IConfiguration.ManifestDirPath" />
    [DirectoryExists]
    [DirectoryPathIsWritable(ForAction = ManifestToolActions.Generate)]
    [Path]
    public ConfigurationSetting<string> ManifestDirPath { get; set; }

    /// <inheritdoc cref="IConfiguration.OutputPath" />
    [FilePathIsWritable]
    [ValueRequired(ForAction = ManifestToolActions.Validate)]
    [Path]
    public ConfigurationSetting<string> OutputPath { get; set; }

    /// <inheritdoc cref="IConfiguration.Parallelism" />
    [IntRange(minRange: Constants.MinParallelism, maxRange: Constants.MaxParallelism)]
    [DefaultValue(Constants.DefaultParallelism)]
    public ConfigurationSetting<int> Parallelism { get; set; }

    /// <inheritdoc cref="IConfiguration.Verbosity" />
    public ConfigurationSetting<LogEventLevel> Verbosity { get; set; }

    /// <inheritdoc cref="IConfiguration.ConfigFilePath" />
    [Path]
    public ConfigurationSetting<string> ConfigFilePath { get; set; }

    /// <inheritdoc cref="IConfiguration.ManifestInfo" />
    [ValidManifestInfo]
    public ConfigurationSetting<IList<ManifestInfo>> ManifestInfo { get; set; }

    /// <inheritdoc cref="IConfiguration.HashAlgorithm" />
    public ConfigurationSetting<AlgorithmName> HashAlgorithm { get; set; }

    /// <inheritdoc cref="IConfiguration.RootPathFilter" />
    [Path]
    public ConfigurationSetting<string> RootPathFilter { get; set; }

    /// <inheritdoc cref="IConfiguration.CatalogFilePath" />
    [Path]
    public ConfigurationSetting<string> CatalogFilePath { get; set; }

    /// <inheritdoc cref="IConfiguration.ValidateSignature" />
    [DefaultValue(false)]
    public ConfigurationSetting<bool> ValidateSignature { get; set; }

    /// <inheritdoc cref="IConfiguration.IgnoreMissing" />
    [DefaultValue(false)]
    public ConfigurationSetting<bool> IgnoreMissing { get; set; }

    /// <inheritdoc cref="IConfiguration.ManifestToolAction" />
    public ManifestToolActions ManifestToolAction { get; set; }

    /// <inheritdoc cref="IConfiguration.PackageName" />
    public ConfigurationSetting<string> PackageName { get; set; }

    /// <inheritdoc cref="IConfiguration.PackageVersion" />
    public ConfigurationSetting<string> PackageVersion { get; set; }

    /// <inheritdoc cref="IConfiguration.PackageSupplier" />
    [ValueRequired(ForAction = ManifestToolActions.Generate)]
    public ConfigurationSetting<string> PackageSupplier { get; set; }

    /// <inheritdoc cref="IConfiguration.FilesList" />
    public ConfigurationSetting<IEnumerable<SbomFile>> FilesList { get; set; }

    /// <inheritdoc cref="IConfiguration.PackagesList" />
    public ConfigurationSetting<IEnumerable<SbomPackage>> PackagesList { get; set; }

    /// <inheritdoc cref="IConfiguration.TelemetryFilePath" />
    [Path]
    public ConfigurationSetting<string> TelemetryFilePath { get; set; }

    /// <inheritdoc cref="IConfiguration.DockerImagesToScan" />
    public ConfigurationSetting<string> DockerImagesToScan { get; set; }

    /// <inheritdoc cref="IConfiguration.ExternalDocumentReferenceListFile" />
    [FileExists]
    public ConfigurationSetting<string> ExternalDocumentReferenceListFile { get; set; }

    /// <inheritdoc cref="IConfiguration.AdditionalComponentDetectorArgs" />
    public ConfigurationSetting<string> AdditionalComponentDetectorArgs { get; set; }

    /// <inheritdoc cref="IConfiguration.NamespaceUriUniquePart" />
    public ConfigurationSetting<string> NamespaceUriUniquePart { get; set; }

    /// <inheritdoc cref="IConfiguration.NamespaceUriBase" />
    [ValidUri(ForAction = ManifestToolActions.Generate, UriKind = UriKind.Absolute)]
    [ValueRequired(ForAction = ManifestToolActions.Generate)]
    public ConfigurationSetting<string> NamespaceUriBase { get; set; }

    /// <inheritdoc cref="IConfiguration.GenerationTimestamp" />
    public ConfigurationSetting<string> GenerationTimestamp { get; set; }

    /// <inheritdoc cref="IConfiguration.FollowSymlinks" />
    [DefaultValue(true)]
    public ConfigurationSetting<bool> FollowSymlinks { get; set; }

    /// <inheritdoc cref="IConfiguration.DeleteManifestDirIfPresent" />
    [DefaultValue(false)]
    public ConfigurationSetting<bool> DeleteManifestDirIfPresent { get; set; }

    /// <inheritdoc cref="IConfiguration.FailIfNoPackages" />
    [DefaultValue(false)]
    public ConfigurationSetting<bool> FailIfNoPackages { get; set; }

    /// <inheritdoc cref="IConfiguration.FetchLicenseInformation" />
    [DefaultValue(false)]
    public ConfigurationSetting<bool> FetchLicenseInformation { get; set; }

    /// <inheritdoc cref="IConfiguration.LicenseInformationTimeoutInSeconds" />
    [DefaultValue(Constants.DefaultLicenseFetchTimeoutInSeconds)]
    public ConfigurationSetting<int> LicenseInformationTimeoutInSeconds { get; set; }

    [DefaultValue(false)]
    public ConfigurationSetting<bool> EnablePackageMetadataParsing { get; set; }

    /// <inheritdoc cref="IConfiguration.SbomDir" />
    [DirectoryExists]
    [Path]
    public ConfigurationSetting<string> SbomDir { get; set; }

    /// <inheritdoc cref="IConfiguration.SbomPath" />
    [Path]
    public ConfigurationSetting<string> SbomPath { get; set; }

    /// <inheritdoc cref="IConfiguration.Conformance" />
    public ConfigurationSetting<ConformanceType> Conformance { get; set; }

    /// <inheritdoc cref="IConfiguration.ArtifactInfoMap" />
    public ConfigurationSetting<Dictionary<string, ArtifactInfo>> ArtifactInfoMap { get; set; }
}
