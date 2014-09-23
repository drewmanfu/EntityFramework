﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Model;
using Microsoft.Data.Entity.Migrations.Utilities;

namespace Microsoft.Data.Entity.Migrations.Infrastructure
{
    public class MigrationMetadata : IMigrationMetadata
    {
        internal static readonly string CurrentProductVersion = typeof(HistoryRepository).GetTypeInfo().Assembly.GetInformationalVersion();

        private readonly string _migrationId;
        private readonly string _productVersion;

        public MigrationMetadata([NotNull] string migrationId)
            : this(migrationId, CurrentProductVersion)
        {
        }

        public MigrationMetadata([NotNull] string migrationId, [NotNull] string productVersion)
        {
            Check.NotEmpty(migrationId, "migrationId");
            Check.NotEmpty(productVersion, "productVersion");

            if (!MigrationMetadataExtensions.IsValidMigrationId(migrationId))
            {
                throw new ArgumentException(Strings.FormatInvalidMigrationId(migrationId));
            }

            _migrationId = migrationId;
            _productVersion = productVersion;
        }

        public virtual string MigrationId
        {
            get { return _migrationId; }
        }

        public virtual string ProductVersion
        {
            get { return _productVersion; }
        }

        public virtual IModel TargetModel { get; [param: NotNull] set; }

        public virtual IReadOnlyList<MigrationOperation> UpgradeOperations { get; [param: NotNull] set; }

        public virtual IReadOnlyList<MigrationOperation> DowngradeOperations { get; [param: NotNull] set; }
    }
}
