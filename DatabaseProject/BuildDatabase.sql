create database condominio
go
use condominio
go

create table funcionarios 
(Id int primary key,
Nome varchar(100),
Endereco varchar(100),
CEP varchar(10),
Bairro varchar(50),
Cidade varchar(50),
UF varchar(2),
Telefone varchar(20))