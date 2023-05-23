CREATE TABLE `PENNY_USER` (
  `USER_ID` varchar(256) NOT NULL,/*GUID*/
  `PASS` varchar(256) NOT NULL,
  `USER_NAME` varchar(256) NOT NULL,
  `EMAIL` varchar(256) NOT NULL,
  `FIRST_NAME` varchar(256) NOT NULL,
  `LAST_NAME` varchar(256) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
--------------------------------------------
CREATE TABLE `PENNY_TRANSACTION_DETAIL` (
  `user_id` varchar(256) DEFAULT NULL,/*GUID*/
  `transaction_id` varchar(256) DEFAULT NUL,
  `fund_id` varchar(256) DEFAULT NULL,
  `description` longtext DEFAULT NULL,
  `value` decimal(10,2) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
--------------------------------------------
CREATE TABLE `PENNY_TRANSACTION` (
  `user_id` varchar(256) DEFAULT NULL,/*GUID*/
  `id` varchar(256) NOT NULL,
  `date` varchar(256) DEFAULT NULL,
  `account_id` varchar(256) DEFAULT NULL,
  `name` text DEFAULT NULL,
  `comment` longtext DEFAULT NULL,
  `is_active` varchar(10) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

ALTER TABLE `PENNY_TRANSACTION`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `PENNY_TRANSACTION_IDX` (`user_id`,`id`);
COMMIT;
-------------------------------------------
CREATE TABLE `PENNY_FUND` (
  `user_id` varchar(256) DEFAULT NULL,/*GUID*/
  `id` varchar(256) NOT NULL,
  `name` text DEFAULT NULL,
  `is_active` int(1) NOT NULL,
  `comment` longtext DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

ALTER TABLE `PENNY_FUND`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `PENNY_FUND_IDX` (`user_id`,`id`);
COMMIT;
-----------------------------------------
CREATE TABLE `PENNY_ACCOUNT` (
  `user_id` varchar(256) DEFAULT NULL,/*GUID*/
  `id` varchar(256) NOT NULL,
  `name` text DEFAULT NULL,
  `comment` longtext DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

ALTER TABLE `PENNY_ACCOUNT`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `PENNY_ACCOUNT_IDX` (`user_id`,`id`);
COMMIT;
--Generate sample data insert statements. User_ID is a GUID
INSERT INTO `PENNY_USER` (`USER_ID`, `PASS`, `user`, `EMAIL`, `FIRST_NAME`, `LAST_NAME`) VALUES
('a4b6c8d0-e1f2-43a4-b5c6-d7e8f9a0b1c2', 'penny123', 'penny', 'penny@gmail.com', 'Penny', 'Smith'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'leonard456', 'leonard', 'leonard@yahoo.com', 'Leonard', 'Hofstadter'),
('c6d8e0f2-g3h4-65c6-d7e8-f9a0b1c4e5d6', 'sheldon789', 'sheldon', 'sheldon@caltech.edu', 'Sheldon', 'Cooper'),
('d7e9f1g3-h4i5-76d7-e8f9-a0b1c5f6e7d8', 'amy1010', 'amy', 'amy@harvard.edu', 'Amy', 'Fowler'),
('e8f0g2h4-i5j6-87e8-f9a0-b1c6g7e8f9a0', 'raj1111', 'raj', 'raj@berkeley.edu', 'Rajesh', 'Koothrappali');
--Generate sample data insert statements for user Sheldon in PENNY_FUND. IDs are GUIDs. Use previous users.  Funds contain things like "Groceries", "Gas", "Rent", "Donations", "Medical", "Entertainment", "Clothing", "Utilities", "Car Payment", "Car Insurance", "Life Insurance", "Health Insurance", "Dental Insurance", "Vision Insurance", "Student Loan", "Investments", "Savings", "Emergency Fund", "Vacation", "Anniversary"
INSERT INTO `PENNY_FUND` (`user_id`, `id`, `name`, `is_active`, `comment`) VALUES
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'f0g1h2i3-j4k5-98f0-g1h2-i3j4k5l6m7n8', 'Groceries', 1, 'Monthly budget for food and household items'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'g1h2i3j4-k5l6-09g1-h2i3-j4k5l6m7n8o9', 'Gas', 1, 'Monthly budget for fuel and car maintenance'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'h2i3j4k5-l6m7-10h2-i3j4-k5l6m7n8o9p0', 'Rent', 1, 'Monthly rent for apartment'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'i3j4k5l6-m7n8-11i3-j4k5-l6m7n8o9p0q1', 'Donations', 1, 'Monthly donations to charity'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'j4k5l6m7-n8o9-12j4-k5l6-m7n8o9p0q1r2', 'Medical', 1, 'Budget for medical expenses not covered by insurance'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'k5l6m7n8-o9p0-13k5-l6m7-n8o9p0q1r2s3', 'Entertainment', 1, 'Monthly budget for movies, games, books, etc.'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'l6m7n8o9-p0q1-14l6-m7n8-o9p0q1r2s3t4', 'Clothing', 1, 'Budget for clothing and accessories'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'm7n8o9p0-q1r2-15m7-n8o9-p0q1r2s3t4u5', 'Utilities', 1, 'Monthly budget for electricity, water, internet, etc.'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'n8o9p0q1-r2s3-16n8-o9p0-q1r2s3t4u5v6', 'Car Payment', 1, 'Monthly payment for car loan'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'o9p0q1r2-s3t4-17o9-p0q1-r2s3t4u5v6w7', 'Car Insurance', 1, 'Monthly payment for car insurance'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'p0q1r2s3-t4u5-18p0-q1r2-s3t4u5v6w7x8', 'Life Insurance', 1, 'Monthly payment for life insurance policy'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'q1r2s3t4-u5v6-19q1-r2s3-t4u5v6w7x8y9', 'Health Insurance', 1, 'Monthly payment for health insurance plan'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'r2s3t4u5-v6w7-20r2-s3t4-u5v6w7x8y9z0', 'Dental Insurance', 1, 'Monthly payment for dental insurance plan'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 's3t4u5v6-w7x8-21s3-t4u5-v6w7x8y9z0a1', 'Vision Insurance', 0, 'Monthly payment for vision insurance plan'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 't4u5v6w7-x8y9-22t4-u5v6-w7x8y9z0a1b2', 'Student Loan', 1, 'Monthly payment for student loan debt'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'u5v6w7x8-y9z0-23u5-v6w7-x8y9z0a1b2c3', 'Investments', 1, 'Monthly budget for investing in stocks, bonds, etc.'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'v6w7x8y9-z0a1-24v6-w7x8-y9z0a1b2c3d4', 'Savings', 1, 'Monthly budget for saving money for future goals'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'w7x8y9z0-a1b2-25w7-x8y9-z0a1b2c3d4e5', 'Emergency Fund', 1, 'Budget for unexpected expenses or emergencies'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'x8y9z0a1-b2c3-26x8-y9z0-a1b2c3d4e5f6', 'Vacation', 1, 'Budget for travel and leisure activities'),
('b5c7d9e1-f2g3-54b5-c6d7-e8f9a0b1c3d4', 'y9z0a1b2-c3d4-27y9-z0a1-b2c3d4e5f6g7', 'Anniversary', 1, 'Budget for celebrating anniversary with partner');






