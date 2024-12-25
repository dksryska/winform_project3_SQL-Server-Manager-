# 제품 내용 입력 프로그램
winform을 SQL Server Studio와 DB를 연결해서 만든 프로그램입니다. <br/>
회원가입, 로그인, 입력 내용 저장 후 수정 or 삭제 입니다.

# 실행방법
1."회원가입" 버튼을 클릭합니다. <br/>
2.회원가입을 합니다. <br/>
3.로그인을 합니다. <br/>
4.왼쪽에 "제품 내용 입력"을 클릭합니다. <br/>
5.내용을 입력 후 "저장하기"를 클릭합니다. <br/>
6.왼쪽에 "상세페이지"를 클릭합니다. <br/>
7.등록한 내용이 나옵니다. <br/>
8.등록한 내용을 클릭 후  수정 or 삭제를 할수가 있습니다.

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
```
