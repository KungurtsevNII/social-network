using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220327212000)]
public sealed class AddProfileTable : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS profiles
            (
                user_id BIGINT PRIMARY KEY REFERENCES users(id),
                first_name TEXT NOT NULL,
                last_name TEXT NOT NULL,
                middle_name TEXT NULL,
                age SMALLINT NOT NULL CONSTRAINT positive_age CHECK (age > 0),
                sex SMALLINT NOT NULL CONSTRAINT man_or_woman CHECK (sex = 1 OR sex = 2),
                interests TEXT NULL,
                city TEXT NOT NULL
            );
        ");
    }

    public override void Down()
    {
        Delete.Table("profiles");
    }
}