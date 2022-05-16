using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220516214000)]
public sealed class AddCreatedAtColumn : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            ALTER TABLE posts
                ADD COLUMN IF NOT EXISTS created_at timestamptz NULL;
            
            UPDATE posts
            SET created_at = timezone('utc', now());
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"
           ALTER TABLE IF EXISTS posts
                DROP COLUMN IF EXISTS created_at;
        ");
    }
}