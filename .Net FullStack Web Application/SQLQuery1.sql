CREATE TABLE clients (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	name VARCHAR (100) NOT NULL,
	email VARCHAR (150) NOT NULL UNIQUE,
	phone VARCHAR (20) NULL,
	address VARCHAR (100) NULL,
	created_At DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO clients (name, email, phone, address) 
VALUES
('Rohan Yohan', 'Rohan.Yohan@company.com', '052-1234567', 'Jerusalem, Israel'),
('Rohan Gohan', 'Rohan.Gohan@company.com', '052-1234567', 'Jerusalem, Israel'),
('Rohan Bohan', 'Rohan.Bohan@company.com', '052-1234567', 'Jerusalem, Israel'),
('Rohan Aohan', 'Rohan.Aohan@company.com', '052-1234567', 'Jerusalem, Israel'),
('Rohan Sohan', 'Rohan.Sohan@company.com', '052-1234567', 'Jerusalem, Israel')
