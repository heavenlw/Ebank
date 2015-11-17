# Host: localhost  (Version: 5.6.26-log)
# Date: 2015-11-17 18:52:50
# Generator: MySQL-Front 5.3  (Build 4.224)

/*!40101 SET NAMES utf8 */;

#
# Structure for table "account_status"
#

DROP TABLE IF EXISTS `account_status`;
CREATE TABLE `account_status` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `status_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

#
# Data for table "account_status"
#

INSERT INTO `account_status` VALUES (1,'OK'),(2,'Freeze'),(3,'Expired');

#
# Structure for table "bank"
#

DROP TABLE IF EXISTS `bank`;
CREATE TABLE `bank` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

#
# Data for table "bank"
#

INSERT INTO `bank` VALUES (1,'ICE Bear'),(2,'ABC'),(3,'CCM'),(4,'ICBC'),(5,'CCB');

#
# Structure for table "bank_news"
#

DROP TABLE IF EXISTS `bank_news`;
CREATE TABLE `bank_news` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) DEFAULT NULL,
  `summary` varchar(255) DEFAULT NULL,
  `url` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

#
# Data for table "bank_news"
#

INSERT INTO `bank_news` VALUES (1,'test_title_1','test_summary_1','http://bbs.feng.com/thread-htm-fid-601.html'),(2,'test_2','test_summary_2','http://bbs.feng.com/thread-htm-fid-601.html'),(3,'test_3','test_summary_3','http://bbs.feng.com/thread-htm-fid-601.html');

#
# Structure for table "branch"
#

DROP TABLE IF EXISTS `branch`;
CREATE TABLE `branch` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `branch_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Data for table "branch"
#


#
# Structure for table "consumption_log"
#

DROP TABLE IF EXISTS `consumption_log`;
CREATE TABLE `consumption_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `account` varchar(255) DEFAULT NULL,
  `user_id` varchar(255) DEFAULT NULL,
  `to_account` varchar(255) DEFAULT NULL,
  `from_account` varchar(255) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `currency` varchar(255) DEFAULT NULL,
  `date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `status` int(11) DEFAULT NULL,
  `summary` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

#
# Data for table "consumption_log"
#

INSERT INTO `consumption_log` VALUES (1,'123456789012345','6','123456789012342',NULL,400,1,'2','2015-11-17 16:49:17',NULL,NULL),(2,'123456789012342','6',NULL,'123456789012345',400,2,'2','2015-11-17 16:49:18',NULL,NULL),(3,'123456789012342','6','1230000000000001',NULL,1000,4,'2','2015-11-17 16:50:29',NULL,NULL);

#
# Structure for table "credit_account"
#

DROP TABLE IF EXISTS `credit_account`;
CREATE TABLE `credit_account` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `card_num` varchar(45) DEFAULT NULL,
  `credit_amount` varchar(45) DEFAULT NULL,
  `expired_date` datetime DEFAULT NULL,
  `repayment_date` int(11) DEFAULT NULL,
  `branch_id` varchar(45) DEFAULT NULL,
  `cvv` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `signature` varchar(45) DEFAULT NULL,
  `currency_id` int(11) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `available_credit` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `card_num` (`card_num`),
  KEY `user_id_idx` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

#
# Data for table "credit_account"
#

INSERT INTO `credit_account` VALUES (1,6,'1230000000000001','10000','2019-07-10 00:00:00',20,NULL,NULL,NULL,NULL,1,1,1,7449),(2,6,'1230000000000002','50000','2020-08-10 00:00:00',13,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);

#
# Structure for table "credit_bill"
#

DROP TABLE IF EXISTS `credit_bill`;
CREATE TABLE `credit_bill` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `credit_account_num` varchar(255) DEFAULT NULL,
  `repayment_deadline` datetime DEFAULT NULL,
  `remain_repayment` int(11) DEFAULT NULL,
  `minimum_repayment` varchar(255) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL COMMENT '1:等待结账  2:结账完成',
  `month` int(11) DEFAULT NULL,
  `currency_id` int(11) DEFAULT NULL,
  `year` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

#
# Data for table "credit_bill"
#

INSERT INTO `credit_bill` VALUES (1,'1230000000000001','2015-11-26 00:00:00',2055,'1200','0',NULL,1,NULL);

#
# Structure for table "currency"
#

DROP TABLE IF EXISTS `currency`;
CREATE TABLE `currency` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `currency_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

#
# Data for table "currency"
#

INSERT INTO `currency` VALUES (1,'HKD'),(2,'RMB'),(3,'USD');

#
# Structure for table "log_type"
#

DROP TABLE IF EXISTS `log_type`;
CREATE TABLE `log_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

#
# Data for table "log_type"
#

INSERT INTO `log_type` VALUES (1,'Inner-bank transfer OUT'),(2,'Inner-bank transfer IN'),(3,'Consumption'),(4,'Repayment'),(5,'Oversea Transfer'),(6,'Interbank Transfer OUT'),(7,'Interbank Transfer IN');

#
# Structure for table "oversea_trans"
#

DROP TABLE IF EXISTS `oversea_trans`;
CREATE TABLE `oversea_trans` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `swift_code` varchar(255) DEFAULT NULL,
  `payee_address` varchar(255) DEFAULT NULL,
  `payee_name` varchar(255) DEFAULT NULL,
  `payee_account_num` varchar(255) DEFAULT NULL,
  `payee_account_bank` varchar(255) DEFAULT NULL,
  `payee_account_address` varchar(255) DEFAULT NULL,
  `payer_account_num` varchar(255) DEFAULT NULL,
  `payer_name` varchar(255) DEFAULT NULL,
  `payer_hk_id` varchar(255) DEFAULT NULL,
  `payer_amount` int(11) DEFAULT NULL,
  `payer_currency` int(11) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Data for table "oversea_trans"
#


#
# Structure for table "question_list"
#

DROP TABLE IF EXISTS `question_list`;
CREATE TABLE `question_list` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `question` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

#
# Data for table "question_list"
#

INSERT INTO `question_list` VALUES (1,'what\'s your name?'),(2,'what\'s your mother\'s name?');

#
# Structure for table "repayment_log"
#

DROP TABLE IF EXISTS `repayment_log`;
CREATE TABLE `repayment_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bill_id` int(11) DEFAULT NULL,
  `amount` varchar(255) DEFAULT NULL,
  `repay_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `repay_account` varchar(255) DEFAULT NULL,
  `type` varchar(255) DEFAULT NULL COMMENT '1:ebank 2:ATM',
  `status` varchar(255) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

#
# Data for table "repayment_log"
#

INSERT INTO `repayment_log` VALUES (1,1,'1000','2015-11-17 16:50:29','123456789012342','4','0');

#
# Structure for table "saving_account"
#

DROP TABLE IF EXISTS `saving_account`;
CREATE TABLE `saving_account` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `card_num` varchar(45) DEFAULT NULL,
  `balance` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `branch_id` varchar(45) DEFAULT NULL,
  `expired_date` datetime DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `currency_id` int(11) DEFAULT NULL,
  `open_date` varchar(255) DEFAULT NULL,
  `common_use` int(11) DEFAULT NULL COMMENT '1为常用',
  `error_count` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `card_num` (`card_num`),
  KEY `user_id_idx` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

#
# Data for table "saving_account"
#

INSERT INTO `saving_account` VALUES (1,6,'123456789012341','1000','202cb962ac59075b964b07152d234b70',NULL,NULL,1,1,NULL,1,1),(2,6,'123456789012342','400','202cb962ac59075b964b07152d234b70',NULL,NULL,1,2,NULL,1,0),(3,6,'123456789012343','1000','202cb962ac59075b964b07152d234b70',NULL,NULL,1,2,NULL,1,4),(4,6,'123456789012344','1000','202cb962ac59075b964b07152d234b70',NULL,NULL,1,3,NULL,0,0),(5,6,'123456789012345','600','202cb962ac59075b964b07152d234b70',NULL,NULL,1,2,NULL,0,0);

#
# Structure for table "trans_log"
#

DROP TABLE IF EXISTS `trans_log`;
CREATE TABLE `trans_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tr_from` varchar(255) DEFAULT NULL,
  `tr_to` varchar(255) DEFAULT NULL,
  `user_id` varchar(255) DEFAULT NULL,
  `insert_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `amount` varchar(255) DEFAULT NULL,
  `handle_time` datetime DEFAULT NULL,
  `finish_time` datetime DEFAULT NULL,
  `kind` int(11) DEFAULT NULL,
  `type` int(11) DEFAULT NULL COMMENT '1:转账 2:还款',
  `currency_id` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT '0' COMMENT '0:等待处理 1:处理完成',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

#
# Data for table "trans_log"
#

INSERT INTO `trans_log` VALUES (1,'123456789012344','123456789012342','6','2015-11-17 15:26:00','23',NULL,NULL,NULL,1,NULL,2),(2,'123456789012341','123456789012342','6','2015-11-17 15:32:38','500',NULL,NULL,NULL,1,NULL,2),(3,'123456789012341','123456789012342','6','2015-11-17 15:43:33','500',NULL,NULL,NULL,1,NULL,2),(4,'123456789012345','123456789012341','6','2015-11-17 16:18:10','400',NULL,NULL,NULL,1,NULL,2),(5,'123456789012345','123456789012341','6','2015-11-17 16:21:38','400',NULL,NULL,NULL,1,NULL,2),(6,'123456789012345','123456789012341','6','2015-11-17 16:23:38','400',NULL,NULL,NULL,1,NULL,2),(7,'123456789012345','123456789012342','6','2015-11-17 16:49:18','400',NULL,NULL,NULL,1,NULL,2);

#
# Structure for table "user"
#

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `hk_id` varchar(45) DEFAULT NULL,
  `sign_up_date` datetime DEFAULT NULL,
  `login_date` datetime DEFAULT NULL,
  `login_count` int(11) DEFAULT NULL,
  `question_id` int(11) DEFAULT NULL,
  `question_answer` varchar(45) DEFAULT NULL,
  `status` int(11) DEFAULT NULL COMMENT '-1:0:1',
  `hk_id_type` varchar(255) DEFAULT NULL,
  `service_code` varchar(255) DEFAULT NULL,
  `real_name` varchar(255) DEFAULT NULL,
  `login_ip` varchar(255) DEFAULT NULL,
  `try_time` int(255) DEFAULT NULL,
  `session` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`),
  UNIQUE KEY `hk_id_UNIQUE` (`hk_id`),
  KEY `question_id_idx` (`question_id`),
  KEY `status` (`status`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

#
# Data for table "user"
#

INSERT INTO `user` VALUES (1,'lw','123456','11111',NULL,'1899-12-30 01:00:00',0,2,'luowei',0,'1',NULL,NULL,NULL,NULL,'asdfghjkl123'),(5,'awol','ec7424afdfb0693a3403a2528dc30f7f','11','2015-11-10 00:00:00',NULL,0,1,'202cb962ac59075b964b07152d234b70',1,'1',NULL,NULL,'::1',NULL,NULL),(6,'du','202cb962ac59075b964b07152d234b70','2222','2015-11-09 00:00:00',NULL,3,1,'202cb962ac59075b964b07152d234b70',1,'1',NULL,NULL,'::1',NULL,'12312312312312312'),(7,NULL,NULL,'123',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);

#
# Structure for table "user_info"
#

DROP TABLE IF EXISTS `user_info`;
CREATE TABLE `user_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `birthday` datetime DEFAULT NULL,
  `marital_status` varchar(45) DEFAULT NULL,
  `address` varchar(45) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `state` varchar(45) DEFAULT NULL,
  `country` varchar(45) DEFAULT NULL,
  `phone_num` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Data for table "user_info"
#

