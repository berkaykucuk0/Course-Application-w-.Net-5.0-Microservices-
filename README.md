# Course-Application-w-.Net-5.0-Microservices-
Course Application  with .Net 5.0 Microservice Architecture

Course Application microservices backend with basic UI template

![Ekran Alıntısı](https://user-images.githubusercontent.com/73891330/124637721-c5cbee80-de92-11eb-9985-6a31c64464a0.PNG)

Microservices:

Catalog Microservice:
.Responsible for keeping and presenting information about courses
.MongoDb (Veritabanı)
.One-To-Many/One-To-One relationship
 
Discount Microservice
.Responsible for define course discount coupons
.PostgreSQL(Database)
  
Order Microservice
.Responsible for order transactions.
.Domain Driven Design
.CQRS Design Pattern w/ Mediatr Library
.Sql Server(Db)

FakePayment Microservice
.Responsible for payment transactions(fake transactions for this project).

IdentityServer Microservice
.Responsible for keeping user datas,roles etc. and producing access token refresh token.
.Sql Server(Db)

PhotoStock Microservice
.Responsible for keeping courses photos.

API Gateway
.Ocelot Library

Message Broker
.Rabbitmq
.MassTransit library is used to communicate with Rabbitmq

