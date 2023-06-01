CREATE TABLE `PENNY_USER` (
  `USER_ID` varchar(256) NOT NULL,/*GUID*/
  `PASS` varchar(256) NOT NULL,
  `USER_NAME` varchar(256) NOT NULL,
  `EMAIL` varchar(256) NOT NULL,
  `FIRST_NAME` varchar(256) NOT NULL,
  `LAST_NAME` varchar(256) NOT NULL
);
---
CREATE TABLE `PENNY_TRANSACTION_DETAIL` (
  `user_id` varchar(256) ,/*GUID*/
  `transaction_id` varchar(256) DEFAULT NUL,
  `fund_id` varchar(256) ,
  `description` longtext ,
  `value` decimal(10,2) 
);
---
CREATE TABLE `PENNY_TRANSACTION` (
  `user_id` varchar(256) ,/*GUID*/
  `id` varchar(256) NOT NULL,
  `date` varchar(256) ,
  `account_id` varchar(256) ,
  `name` text ,
  `comment` longtext ,
  `is_active` varchar(10) ,
  `amount` decimal(10,2) 
);
---
CREATE TABLE `PENNY_FUND` (
  `user_id` varchar(256) ,/*GUID*/
  `id` varchar(256) NOT NULL,
  `name` text ,
  `is_active` int(1) NOT NULL,
  `comment` longtext 
);
---
CREATE TABLE `PENNY_ACCOUNT` (
  `user_id` varchar(256) ,/*GUID*/
  `id` varchar(256) NOT NULL,
  `name` text ,
  `comment` longtext 
);