# SQL문은 SQL Server Studio를 사용했습니다.
```sql
create database portfolio1;

use fortfolio1;

create table register (
id int identity(1,1) not null,
name varchar(50) not null,
pw varchar(50) not null,
);

create table product(
id int identity(1,1) not null,
name varchar(50) not null,
description varchar(50) not null,
price varchar(50) not null,
number varchar(50) not null,
primary key(id)
);
