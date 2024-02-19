insert into Users values (gen_random_uuid(), 'test-user1', '1234','testemail1@gmail.com'); 
insert into Users values (gen_random_uuid(), 'test-user2', '1234','testemail2@gmail.com'); 

insert into Tests
select gen_random_uuid(), 'test1', 'small', id from Users where right(login,1) = '1';
insert into Tests
select gen_random_uuid(), 'test2', 'small', id from Users where right(login,1) = '2';

select * from Tests

call create_table_from_test('666ec453-9bd0-4283-b45c-1a9b43415cee'::uuid, 2::smallint);
call create_table_from_test('236b13de-73e5-4dc7-ae1a-7a7e9f9cc67e', 10::smallint);

select * from information_schema.tables
where table_schema = 'main_schema'