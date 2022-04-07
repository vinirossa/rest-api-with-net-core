CREATE TABLE main.users (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`username` VARCHAR(50) NOT NULL DEFAULT '0',
	`password` VARCHAR(130) NOT NULL DEFAULT '0',
	`fullname` VARCHAR(120) NOT NULL,
	`refresh_token` VARCHAR(500) NULL DEFAULT '0',
	`refresh_token_expiry_time` DATETIME NULL DEFAULT NULL,
	PRIMARY KEY (`id`),
	UNIQUE `username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;