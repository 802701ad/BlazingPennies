CREATE TABLE `PENNY_FUND` (
 `user_id` varchar(256) DEFAULT NULL,
 `id` varchar(256) NOT NULL,
 `name` text DEFAULT NULL,
 `is_active` int(1) DEFAULT NULL,
 `comment` longtext DEFAULT NULL,
 PRIMARY KEY (`id`),
 UNIQUE KEY `PENNY_FUND_IDX` (`user_id`,`id`)
);


PENNY_TRANSACTION	CREATE TABLE `PENNY_TRANSACTION` (
 `user_id` varchar(256) DEFAULT NULL,
 `id` varchar(256) NOT NULL,
 `date` varchar(256) DEFAULT NULL,
 `seq` varchar(100) NOT NULL,
 `account_id` varchar(256) DEFAULT NULL,
 `name` text DEFAULT NULL,
 `comment` longtext DEFAULT NULL,
 `is_active` varchar(10) DEFAULT NULL,
 `amount` decimal(10,2) DEFAULT NULL,
 PRIMARY KEY (`id`),
 UNIQUE KEY `PENNY_TRANSACTION_IDX` (`user_id`,`id`),
 KEY `idx_account_id` (`account_id`)
);


CREATE TABLE `PENNY_TRANSACTION_DETAIL` (
 `user_id` varchar(256) DEFAULT NULL,
 `transaction_id` varchar(256) DEFAULT NULL,
 `account_id` varchar(256) DEFAULT NULL,
 `fund_id` varchar(256) DEFAULT NULL,
 `description` longtext DEFAULT NULL,
 `value` decimal(10,2) DEFAULT NULL,
 KEY `PENNY_TRANSACTION_DETAIL_IDX` (`user_id`,`transaction_id`),
 KEY `PENNY_TRANSACTION_DETAIL_BY_TRANS_IDX` (`transaction_id`)
);


PENNY_USER	CREATE TABLE `PENNY_USER` (
 `user_id` varchar(256) NOT NULL,
 `pass` varchar(256) DEFAULT NULL,
 `user_name` varchar(256) DEFAULT NULL,
 `email` varchar(256) DEFAULT NULL,
 `first_name` varchar(256) DEFAULT NULL,
 `last_name` varchar(256) DEFAULT NULL
);