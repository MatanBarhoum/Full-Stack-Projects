CREATE TABLE reports (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	category VARCHAR (100) NOT NULL,
	fullname VARCHAR (150) NOT NULL UNIQUE,
	fee VARCHAR (20) NULL,
	payment_status VARCHAR (100) NULL,
	created_At DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO reports (category, fullname, fee, payment_status) 
VALUES
('speeding', 'Rohan Yohan', '532', 'paid up'),
('speeding', 'Rohan Gohan', '462', 'not paid'),
('speeding', 'Rohan Bohan', '721', 'paid up'),
('speeding', 'Rohan Aohan', '672', 'not paid'),
('speeding', 'Rohan Sohan', '941', 'paid up')
