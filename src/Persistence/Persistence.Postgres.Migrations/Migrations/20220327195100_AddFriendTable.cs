using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220327195100)]
public sealed class AddFriendTable : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS friends
            (
                user_id BIGINT NOT NULL,
                friend_id BIGINT NOT NULL,
                PRIMARY KEY (user_id, friend_id),
                CONSTRAINT friends_user_id_fk FOREIGN KEY (user_id) REFERENCES users (id),
                CONSTRAINT friends_friend_id_fk FOREIGN KEY (friend_id) REFERENCES users (id)
            );

            ALTER TABLE users 
                ADD CONSTRAINT users_phone_email_uix UNIQUE (normalized_email, phone_number);
        ");
    }

    public override void Down()
    {
        Delete.Table("friends");
        Delete.UniqueConstraint("users_phone_email_uix");
    }
}
