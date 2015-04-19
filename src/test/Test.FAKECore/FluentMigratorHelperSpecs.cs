using System;
using Machine.Specifications;

namespace Test.FAKECore
{
    public class when_parsing_fluent_migrator_args
    {
        It should_append_provider_when_specified = () =>
        {
            BuildMigratorArgsProvider().ShouldContain("--provider=SqlServer2008");
            BuildMigratorArgsNoProvider().ShouldNotContain("--provider=SqlServer2008");
        };
          
        It should_append_connection_when_specified = () =>
        {
            BuildMigratorArgsProvider().ShouldContain("--connection=Testing");
            BuildMigratorArgsNoProvider().ShouldNotContain("--connection=Testing");
        };
            
        It should_append_connection_string_path_when_specified = () =>
        {
            BuildMigratorArgsProvider().ShouldContain(@"--connectionStringConfigPath=C:\test.config");
            BuildMigratorArgsNoProvider().ShouldNotContain(@"--connectionStringConfigPath=C:\test.config");
        };
             
        It should_append_namespace_when_specified = () =>
        {
            BuildMigratorArgsProvider().ShouldContain("--namespace=test.namespace");                                      
            BuildMigratorArgsNoProvider().ShouldNotContain("--namespace=test.namespace");
        };
            
        It should_append_nested_flag_when_specified = () =>
        {
            BuildMigratorArgsProvider().ShouldContain("--nested");
            BuildMigratorArgsNoProvider().ShouldNotContain("--nested");
        };

        It should_append_output_flag_when_specified = () =>
        {
            BuildMigratorArgsProvider().ShouldContain("--output");
            BuildMigratorArgsNoProvider().ShouldNotContain("--output");                                      
        };

        It should_only_append_output_filename_when_output_flag_also_specified = () =>
        {
            BuildMigratorArgsProvider(true).ShouldContain("--outputFilename=test.sql");
            BuildMigratorArgsProvider(false).ShouldNotContain("--outputFilename=test.sql");
        };

        It should_append_preview_flag_when_specified = () =>
        {
            BuildMigratorArgsProvider().ShouldContain("--preview");
            BuildMigratorArgsNoProvider().ShouldNotContain("--preview");
        };
   
        It should_only_append_steps_when_task_is_rollback = () =>
        {
            BuildMigratorArgsTask("rollback").ShouldContain("--steps=1");
            BuildMigratorArgsTask("migrate").ShouldNotContain("--steps=1");                              
        };

        It should_only_append_version_when_specified = () =>
        {
            BuildMigratorArgsVersion(1).ShouldContain("--version=1");
            BuildMigratorArgsVersion(0).ShouldNotContain("--version=1");                            
        };

        private static string BuildMigratorArgsVersion(int version)
        {
            var args = new Fake.FluentMigratorHelper.FluentMigratorParams
                (
                string.Empty, // Toolpath
                Fake.FluentMigratorHelper.Provider.SqlServer2008, // Provider
                "Testing", // Connection
                @"C:\test.config", // Connection String Config Path
                "test.namespace", // Namespace
                true, // Nested
                true, // Output
                null, // OutputFileName
                true, // Preview
                1, // Steps
                "migrate", // Task
                version, // Version
                -1, // Start Version
                false, // NoConnection
                null, // Profile
                30, // Timeout
                new String[0] // Tags
            );

            return Fake.FluentMigratorHelper.buildMigratorArgs(args, "test.dll");
        }

        private static string BuildMigratorArgsTask(string task)
        {
            var args = new Fake.FluentMigratorHelper.FluentMigratorParams
                (
                string.Empty, // Toolpath
                Fake.FluentMigratorHelper.Provider.SqlServer2008, // Provider
                "Testing", // Connection
                @"C:\test.config", // Connection String Config Path
                "test.namespace", // Namespace
                true, // Nested
                true, // Output
                null, // OutputFileName
                true, // Preview
                1, // Steps
                task, // Task
                0, // Version
                -1, // Start Version
                false, // NoConnection
                null, // Profile
                30, // Timeout
                new String[0] // Tags
            );

            return Fake.FluentMigratorHelper.buildMigratorArgs(args, "test.dll");
        }

        private static string BuildMigratorArgsProvider()
        {
            var args = new Fake.FluentMigratorHelper.FluentMigratorParams
                (
                string.Empty, // Toolpath
                Fake.FluentMigratorHelper.Provider.SqlServer2008, // Provider
                "Testing", // Connection
                @"C:\test.config", // Connection String Config Path
                "test.namespace", // Namespace
                true, // Nested
                true, // Output
                null, // OutputFileName
                true, // Preview
                1, // Steps
                "migrate", // Task
                0, // Version
                -1, // Start Version
                false, // NoConnection
                null, // Profile
                30, // Timeout
                new String[0] // Tags
            );

            return Fake.FluentMigratorHelper.buildMigratorArgs(args, "test.dll");
        }

        private static string BuildMigratorArgsProvider(bool output)
        {
            var args = new Fake.FluentMigratorHelper.FluentMigratorParams
                (
                string.Empty, // Toolpath
                Fake.FluentMigratorHelper.Provider.SqlServer2008, // Provider
                "Testing", // Connection
                @"C:\test.config", // Connection String Config Path
                "test.namespace", // Namespace
                true, // Nested
                output, // Output
                "test.sql", // OutputFileName
                false, // Preview
                1, // Steps
                "migrate", // Task
                0, // Version
                -1, // Start Version
                false, // NoConnection
                null, // Profile
                30, // Timeout
                new String[0] // Tags
            );

            return Fake.FluentMigratorHelper.buildMigratorArgs(args, "test.dll");
        }

        private static string BuildMigratorArgsNoProvider()
        {
            var args = new Fake.FluentMigratorHelper.FluentMigratorParams
                (
                string.Empty, // Toolpath
                Fake.FluentMigratorHelper.Provider.None, // Provider
                null, // Connection
                null, // Connection String Config Path
                null, // Namespace
                false, // Nested
                false, // Output
                null, // OutputFileName
                false, // Preview
                1, // Steps
                "migrate", // Task
                0, // Version
                -1, // Start Version
                false, // NoConnection
                null, // Profile
                30, // Timeout
                new String[0] // Tags
            );

            return Fake.FluentMigratorHelper.buildMigratorArgs(args, "test.dll");
        }
    }
}
