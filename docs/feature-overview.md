# Feature Overview

## Files Section

The files section of the SBOM is generated by traversing and listing all the directories and files found the on the `BuildDropPath` provided to the tool. The tool uses this list of files and generates an SPDXID, a SHA1 hash, and a SHA256 hash. Here's an example:

```json
"files": [
    {
      "fileName": "./sbom-tool-win-x64.exe",
      "SPDXID": "SPDXRef-File--sbom-tool-win-x64.exe-E55F25E239D8D3572D75D5CDC5CA24899FD4993F",
      "checksums": [
        {
          "algorithm": "SHA256",
          "checksumValue": "56624d8ab67ac0e323bcac0ae1ec0656f1721c6bb60640ecf9b30e861062aad5"
        },
        {
          "algorithm": "SHA1",
          "checksumValue": "e55f25e239d8d3572d75d5cdc5ca24899fd4993f"
        }
      ],
      "licenseConcluded": "NOASSERTION",
      "licenseInfoInFiles": [
        "NOASSERTION"
      ],
      "copyrightText": "NOASSERTION"
    }
  ]
```

## Packages Section

The packages section of the generated SBOM will use the [component detection](https://github.com/microsoft/component-detection) libraries to scan the project's `BuildComponentPath` and determine what packages are being used in the project. The sbom-tool will attempt to add additional information to this section wherever possible either through the [ClearlyDefinedApi](https://github.com/clearlydefined/clearlydefined) or via our own custom implementations (Note: At the time of writing, in order to enable populating these fields both the `-pm` and `-li` arguments must be set to `true`). This table displays the ecosystems and additional properties that the tool makes an attempt to populate.

Note: These are not the only entries found in the packages section. To view a full list of properties of an individual package please visit our sample document [here](https://github.com/microsoft/sbom-tool/blob/main/samples/manifest.spdx.json).

| Ecosystem | Detection Mechanism | Requirements | License Concluded (Via ClearlyDefinedApi) | License Declared | Supplier
| - | - | - | - | - | - |
| Cargo | <ul><li>Cargo.lock (v1, v2, v3)</li></ul> | - | ✔ | ✔ (Via RustCli detector) | ✔ |
| Ruby | <ul><li>gemfile.lock</li></ul> | - | ✔ | ✔ | ✔ |
| Pip (Python) | <ul><li>setup.py</li><li>requirements.txt</li><li>*setup=distutils.core.run_setup({setup.py}); setup.install_requires*</li><li>dist package METADATA file</li></ul> | <ul><li>Python 2 or Python 3</li><li>Internet connection</li></ul> | ✔ | ✔ | ✔ |
| Maven | <ul><li>pom.xml</li></ul> | <ul><li>Maven</li></ul> | - | - | - |
| NPM | <ul><li>package.json</li></ul> | - | ✔ | ✔ | ✔ |
| NuGet | <ul><li>project.assets.json</li><li>*.nupkg</li><li>*.nuspec</li><li>packages.config</li><li>nuget.config</li></ul> | - | ✔ | ✔ | ✔ |
| Linux (Debian, Alpine, Rhel, Centos, Fedora, Ubuntu)| <ul><li>(via [syft](https://github.com/anchore/syft))</li></ul> | - | ✔ | - | ✔ |
| CocoaPods | <ul><li>podfile.lock</li></ul> | - | ✔ | - | - |
| Conda (Python) | <ul><li>conda-lock.yml</li><li>*.conda-lock.yml</li></ul> | - | - | - | - |
| Gradle | <ul><li>*.lockfile</li></ul> | <ul><li>Gradle 7 or prior using [Single File lock](https://docs.gradle.org/6.8.1/userguide/dependency_locking.html#single_lock_file_per_project)</li></ul> | - | - | - |
| Go | <ul><li>*go list -m -json all*</li><li>*go mod graph* (edge information only)</li></ul>Fallback</br><ul><li>go.mod</li><li>go.sum</li></ul> | <ul><li>Go 1.11+ (will fallback if not present)</li></ul> | - | - | - |
| Yarn (v1, v2) | <ul><li>package.json</li><li>yarn.lock</li></ul> | - | - | - | - |
| Pnpm | <ul><li>shrinkwrap.yaml</li><li>pnpm-lock.yaml</li></ul> | - | - | - | - |
| Poetry (Python) | <ul><li>poetry.lock</li><ul> | - | - | - | - |

Example:

```json
"packages": [
    {
      "name": "Microsoft.VisualStudio.Threading.Analyzers",
      "SPDXID": "SPDXRef-Package-CCB741BD164B5B2F9771AD784B281D62BDB0E0707EE703E76AF22BFFB4503941",
      "downloadLocation": "NOASSERTION",
      "filesAnalyzed": false,
      "licenseConcluded": "MIT",
      "licenseDeclared": "MIT",
      "copyrightText": "NOASSERTION",
      "versionInfo": "17.7.30",
      "externalRefs": [
        {
          "referenceCategory": "PACKAGE-MANAGER",
          "referenceType": "purl",
          "referenceLocator": "pkg:nuget/Microsoft.VisualStudio.Threading.Analyzers@17.7.30"
        }
      ],
      "supplier": "Organization: Microsoft"
    }
]
```

## External References Section

When the sbom-tool detects an SPDX2.2 SBOM inside of the project it's currently trying to generate an SBOM for, it will list it in the files section as well as add an entry to the `ExternalDocumentRefs` section of the SBOM. Here's an example:

```json
 "files": [
    {
      "fileName": "./manifest.spdx.json",
      "SPDXID": "SPDXRef-File--manifest.spdx.json-9E563530C733C814FC04FF5C61D8DC9FB4FD29CB",
      "checksums": [
        {
          "algorithm": "SHA256",
          "checksumValue": "63789f7d621728cb197dcc5655d7b18ab35f825b596496e633b61291cfa00f4b"
        },
        {
          "algorithm": "SHA1",
          "checksumValue": "9e563530c733c814fc04ff5c61d8dc9fb4fd29cb"
        }
      ],
      "licenseConcluded": "NOASSERTION",
      "licenseInfoInFiles": [
        "NOASSERTION"
      ],
      "copyrightText": "NOASSERTION",
      "fileTypes": [
        "SPDX"
      ]
    },
 ]
```

```json
"externalDocumentRefs": [
    {
      "externalDocumentId": "DocumentRef-TEST.GIT-bd801c3b7553a08b6874d6bc129203c5a8a95a04-f3bca9cb-67f5-1320-d17e-188a244fc472-1.0-9e563530c733c814fc04ff5c61d8dc9fb4fd29cb",
      "spdxDocument": "https://sbom.microsoft/TEST.GIT/bd801c3b7553a08b6874d6bc129203c5a8a95a04/f3bca9cb-67f5-1320-d17e-188a244fc472/1.0/fa0d13e3-0136-4cc8-af85-622c04063a0f",
      "checksum": {
        "algorithm": "SHA1",
        "checksumValue": "9e563530c733c814fc04ff5c61d8dc9fb4fd29cb"
      }
    }
  ]
```

## Additional Properties

At the end of the SBOM you can find some additional properties that can be used as metadata to describe the SBOM itself. Here's an example of what these properties look like:

```json
 "spdxVersion": "SPDX-2.2",
  "dataLicense": "CC0-1.0",
  "SPDXID": "SPDXRef-DOCUMENT",
  "name": "test 1",
  "documentNamespace": "https://spdx.org/spdxdocs/sbom-tool-2.2.3-preview.0.1-d05da109-fdf8-479e-8a40-9efacd439647/test/1/c5lYxP0T1EyYvj3i5lru3w",
  "creationInfo": {
    "created": "2024-01-16T20:29:58Z",
    "creators": [
      "Organization: test",
      "Tool: Microsoft.SBOMTool-2.2.3-preview.0.1"
    ]
  },
  "documentDescribes": [
    "SPDXRef-RootPackage"
  ]
```

Properties such as the organization or name are supplied via arguments to the tool. The full list of arguments can be found [here](https://github.com/microsoft/sbom-tool/blob/main/docs/sbom-tool-arguments.md).
