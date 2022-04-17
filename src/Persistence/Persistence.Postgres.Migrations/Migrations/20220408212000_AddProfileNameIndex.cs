using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220418212000)]
public sealed class AddProfileNameIndex : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
           CREATE INDEX IF NOT EXISTS lower_last_and_first_name_inx ON profiles (lower(last_name) text_pattern_ops, lower(first_name) text_pattern_ops);
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"
           DROP INDEX lower_last_and_first_name_inx;
        ");
    }
}