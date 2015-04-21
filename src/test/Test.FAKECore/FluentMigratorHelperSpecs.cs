using Fake;
using Machine.Specifications;
using MigrationTask = Fake.FluentMigratorHelper.MigrationTask;
using Provider = Fake.FluentMigratorHelper.Provider;

namespace Test.FAKECore
{
    public class when_parsing_fluent_migrator_args
    {
        It should_append_correct_task_argument = () =>
        {
            BuildMigratorArgs(task: MigrationTask.Migrate).ShouldContain("--task=migrate");
            BuildMigratorArgs(task: MigrationTask.MigrateDown).ShouldContain("--task=migrate:down");
            BuildMigratorArgs(task: MigrationTask.ListMigrations).ShouldContain("--task=listmigrations");
            BuildMigratorArgs(task: MigrationTask.Rollback).ShouldContain("--task=rollback");
            BuildMigratorArgs(task: MigrationTask.RollbackAll).ShouldContain("--task=rollback:all");
            BuildMigratorArgs(task: MigrationTask.RollbackToVersion).ShouldContain("--task=rollback:toversion");
        };

        It should_append_assembly = () =>
        {
            BuildMigratorArgs().ShouldContain("--assembly=test.dll");                
        };

        It should_append_provider_when_specified = () =>
        {
            BuildMigratorArgs(provider: Provider.SqlServer2008).ShouldContain("--provider=SqlServer2008");
            BuildMigratorArgs().ShouldNotContain("--provider=");
        };
          
        It should_append_connection_when_specified = () =>
        {
            BuildMigratorArgs(connection: "Testing").ShouldContain("--connection=Testing");
            BuildMigratorArgs().ShouldNotContain("--connection=");
        };
            
        It should_append_connection_string_path_when_specified = () =>
        {
            BuildMigratorArgs(connectionStringConfigPath: @"C:\test.config")
                .ShouldContain(@"--connectionStringConfigPath=C:\test.config");
            
            BuildMigratorArgs().ShouldNotContain(@"--connectionStringConfigPath=");
        };
             
        It should_append_namespace_when_specified = () =>
        {
            BuildMigratorArgs(@namespace: "test.namespace").ShouldContain("--namespace=test.namespace");                                      
            BuildMigratorArgs().ShouldNotContain("--namespace=");
        };
            
        It should_append_nested_flag_when_specified = () =>
        {
            BuildMigratorArgs(nested: true).ShouldContain("--nested");
            BuildMigratorArgs().ShouldNotContain("--nested");
        };

        It should_append_output_flag_when_specified = () =>
        {
            BuildMigratorArgs(output: true).ShouldContain("--output");
            BuildMigratorArgs().ShouldNotContain("--output");                                      
        };

        It should_only_append_output_filename_when_output_flag_also_specified = () =>
        {
            BuildMigratorArgs(output: true, outputFileName: "test.sql").ShouldContain("--outputFilename=test.sql");
            BuildMigratorArgs(output: false, outputFileName: "test.sql").ShouldNotContain("--outputFilename=");
        };

        It should_append_preview_flag_when_specified = () =>
        {
            BuildMigratorArgs(preview: true).ShouldContain("--preview");
            BuildMigratorArgs().ShouldNotContain("--preview");
        };
   
        It should_only_append_steps_when_task_is_rollback = () =>
        {
            BuildMigratorArgs(task: MigrationTask.Rollback).ShouldContain("--steps=1");
            BuildMigratorArgs().ShouldNotContain("--steps=");                              
        };

        It should_only_append_version_when_specified = () =>
        {
            BuildMigratorArgs(version: 1).ShouldContain("--version=1");
            BuildMigratorArgs().ShouldNotContain("--version=");                            
        };

        It should_only_append_start_version_when_no_connection_flag_also_specified = () =>
        {
            BuildMigratorArgs().ShouldNotContain("--startVersion");
            BuildMigratorArgs(startVersion: 2).ShouldNotContain("--startVersion=");
            BuildMigratorArgs(startVersion: 2, noConnection: true).ShouldContain("--startVersion=");
        };

        It should_only_append_no_connection_flag_when_specified = () =>
        {
            BuildMigratorArgs().ShouldNotContain("--noConnection");
            BuildMigratorArgs(noConnection: true).ShouldContain("--noConnection");                                             
        };

        It should_only_append_profile_flag_when_specified = () =>
        {
            BuildMigratorArgs().ShouldNotContain("--profile=");
            BuildMigratorArgs(profile: "production").ShouldContain("--profile=production");
        };

        It should_only_append_timeout_when_timeout_specified_with_non_default_value = () =>
        {
            BuildMigratorArgs().ShouldNotContain("--timeout=");
            BuildMigratorArgs(timeout: 45).ShouldContain("--timeout=45");                                                                      
        };

        It should_append_multiple_tag_arguments_when_multiple_tags_specified = () =>
        {
            BuildMigratorArgs(tags: new[] { "tag1", "tag2" }).ShouldContain("--tag=tag1");
            BuildMigratorArgs(tags: new[] { "tag1", "tag2" }).ShouldContain("--tag=tag2");
        };

        It should_append_single_tag_argument_when_single_tag_specified = () =>
        {
            BuildMigratorArgs(tags: new [] { "tag3" }).ShouldContain("--tag=tag3");                                                                     
        };

        It should_not_append_tag_argument_when_no_tags_specified = () =>
        {
            BuildMigratorArgs(tags: new string[0]).ShouldNotContain("--tag=");                          
        };

        private static string BuildMigratorArgs(
            string assembly = "test.dll",
            Provider provider = Provider.None,
            string connection = null, 
            string connectionStringConfigPath = null, 
            string @namespace = null,
            bool nested = false, 
            bool output = false, 
            string outputFileName = null, 
            bool preview = false, 
            int steps = 1, 
            MigrationTask task = MigrationTask.Migrate, 
            int version = 0, 
            int startVersion = -1, 
            bool noConnection = false,
            string profile = null, 
            int timeout = 30, 
            string[] tags = null)
        {
            var args = new FluentMigratorHelper.FluentMigratorParams
            (
                string.Empty, // Toolpath
                provider,
                connection,
                connectionStringConfigPath,
                @namespace,
                nested,
                output,
                outputFileName,
                preview,
                steps,
                task,
                version,
                startVersion,
                noConnection,
                profile,
                timeout,
                tags ?? new string[0]
            );

            return FluentMigratorHelper.buildMigratorArgs(args, assembly);
        }
    }
}
