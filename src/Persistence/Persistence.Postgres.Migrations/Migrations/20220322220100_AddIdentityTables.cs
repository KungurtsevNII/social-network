using FluentMigrator;

namespace Persistence.Postgres.Migrations.Migrations;

[Migration(20220322220100)]
public sealed class AddIdentityTables : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE IF NOT EXISTS users
            (
                id BIGSERIAL PRIMARY KEY,
                email TEXT NOT NULL,
                normalized_email TEXT NOT NULL,
                email_confirmed BOOL NOT NULL,
                password_hash TEXT NOT NULL,
                phone_number TEXT NULL,
                phone_number_confirmed BOOL NOT NULL,
                two_factor_enabled BOOL NOT NULL
            );
 
            CREATE UNIQUE INDEX IF NOT EXISTS users_normalized_email_uix ON users (normalized_email);
            CREATE UNIQUE INDEX IF NOT EXISTS users_phone_number_uix ON users (phone_number);

            CREATE TABLE IF NOT EXISTS roles
            (
                id BIGSERIAL PRIMARY KEY,
                name TEXT NOT NULL,
                normalized_name TEXT NOT NULL
            );
            
            CREATE UNIQUE INDEX IF NOT EXISTS roles_normalized_name_uix ON roles (normalized_name);

            CREATE TABLE IF NOT EXISTS users_roles
            (
	            user_id BIGINT NOT NULL,
	            role_id BIGINT NOT NULL,
                PRIMARY KEY (user_id, role_id),
                CONSTRAINT users_roles_user_id_fk FOREIGN KEY (user_id) REFERENCES users (id),
                CONSTRAINT users_roles_role_id_fk FOREIGN KEY (role_id) REFERENCES roles (id)
            )
        ");
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("roles");
        Delete.Table("users_roles");
    }
}
