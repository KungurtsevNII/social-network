using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220510212000)]
public sealed class AddPostsTable : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
           CREATE TABLE IF NOT EXISTS posts
            (
                id BIGSERIAL,
                user_id UUID NOT NULL,
                text TEXT NOT NULL,
                PRIMARY KEY (id, user_id)                
            );
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"
           DROP TABLE IF EXISTS posts;
        ");
    }
}