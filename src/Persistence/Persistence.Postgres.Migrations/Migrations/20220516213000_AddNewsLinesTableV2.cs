using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220516213000)]
public sealed class AddNewsLinesTableV2 : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            DROP TABLE IF EXISTS news_lines;

            CREATE TABLE IF NOT EXISTS news_lines
            (
                id UUID PRIMARY KEY,
                post_id UUID NOT NULL,
                news_line_owner_user_id BIGINT NOT NULL,
                post_creater_user_id BIGINT NOT NULL                
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