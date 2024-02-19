/* script from initializing schema and entities, from my project */

create schema if not exists main_schema;

set search_path to main_schema;

drop table if exists Users;

drop table if exists Tests;

create table Users(
    id uuid not null unique primary key,
    login varchar(18) not null unique,
    password varchar(18) not null,
    email text
);

create table Tests(
    id uuid not null unique primary key,
    title text not null,
    description text null,
    user_id uuid not null,
    Foreign Key (user_id) REFERENCES Users (id) on delete cascade
)

create or replace procedure create_table_from_test(id_test uuid, possibles_numbers smallint)
    language plpgsql
as 
$$
    declare
        count int := 0;
        loop_count int; 
        query text;
    begin
        query := 'drop table if exists ' || quote_ident(id_test) || ';
                  create table ' || quote_ident(id_test) || ' ( question text not null, ';

        for loop_count in 1 .. possibles_numbers 
        loop
            count := loop_count;
            query := query || 'possible' || count || ' text null, ';
        end loop;

        query := query || 'answer text not null )';

        execute query;
    end;
$$;

create or replace procedure drop_all_tables_from_test()
    language plpgsql
as 
$$
    declare 
        rec record;
    begin
        for rec in (
                select table_name from information_schema.tables
                where table_schema = 'main_schema' and table_name != 'users' and table_name != 'tests'
            )

        loop
            execute 'drop table ' || quote_ident(rec.table_name);
        end loop;
    end;
$$;
