using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220516212000)]
public sealed class AddNewsLinesTable : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
           CREATE TABLE IF NOT EXISTS news_lines
            (
                post_id UUID NOT NULL,
                user_id BIGINT NOT NULL,
                PRIMARY KEY (post_id, user_id)                
            );
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"
           DROP TABLE IF EXISTS news_lines;
        ");
    }
}