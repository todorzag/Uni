INSERT INTO "Users" ("Username", "Email", "PasswordHash", "Role", "IsActive", "RegisteredOn") VALUES
('Тодор Загоров', 'admin@example.com', '$2a$11$VbFx.mTk59BrEGqsL.v1duiKQGbOhD9HwYBaFMJyI0TWbDwXvdaPe', 'Admin', TRUE, NOW()),
('Калоян Караиванов', 'kaloyan@example.com', '$2a$11$AkYJLVU8KMa4a9eUwFMIBeuHbHh2Qb/GlZ47AQh1n5D6Zc1kp0Ro6', 'User', TRUE, NOW()),
('Георги Дюлгеров', 'georgi@example.com', '$2a$11$dkaFvbeFOOcQhdD2PqPtTOLTr9C5kflOtFNEuUARq8OQ7qfCvLEYe', 'User', TRUE, NOW()),
('Таня Желева', 'tanya@example.com', '$2a$11$WXqTSn6JzUwFmlkFnNNO4e7z1CCkJYgeSnmM3dzLDCbbDn1bWQpze', 'User', FALSE, NOW()),
('Ивайло Петров', 'ivaylo@example.com', '$2a$11$fkk0Kx6kdl7p5DpVnOsODed3mNhwMWkaHpBW6WFTcg1eomEThVoMG', 'User', TRUE, NOW());

INSERT INTO "Computers" ("Name", "Description", "Price", "Stock", "Type", "Processor", "RAM", "Storage", "ScreenSize") VALUES
('Lenovo ThinkPad X1 Carbon', 'Бизнес лаптоп с Intel i7-1165G7, 16GB RAM, 512GB SSD', 1899.99, 10, 'Laptop', 'Intel i7-1165G7', '16GB DDR4', '512GB SSD', 14.0),
('Dell XPS 15', 'Мощен лаптоп с Intel i9-11900H, 32GB RAM, 1TB SSD', 2499.00, 5, 'Laptop', 'Intel i9-11900H', '32GB DDR4', '1TB SSD', 15.6),
('HP Pavilion Desktop', 'Десктоп с AMD Ryzen 5 5600X, 16GB RAM, 512GB SSD', 1199.00, 7, 'Desktop', 'AMD Ryzen 5 5600X', '16GB DDR4', '512GB SSD', NULL),
('Apple iMac 24"', 'Всичко в едно с Apple M1, 8GB RAM, 256GB SSD', 1799.99, 4, 'All-in-One', 'Apple M1', '8GB Unified', '256GB SSD', 24.0),
('Asus ROG Strix G15', 'Геймърски лаптоп с AMD Ryzen 7 5800H, 16GB RAM, 1TB SSD', 1599.99, 8, 'Laptop', 'AMD Ryzen 7 5800H', '16GB DDR4', '1TB SSD', 15.6);

INSERT INTO "Orders" ("UserId", "CreatedAt", "Total", "Status", "OrderAddress") VALUES
(2, NOW(), 1949.98, 'Paid', 'ул. Васил Левски 15, София'),
(3, NOW(), 699.00, 'Pending', 'жк. Младост 4, София'),
(2, NOW(), 89.99, 'Shipped', 'ул. Славейков 3, Варна');

INSERT INTO "OrderItems" ("OrderId", "ComputerId", "Quantity", "Price") VALUES
(1, 1, 1, 1899.99),
(1, 3, 1, 49.99),
(2, 5, 1, 699.00),
(3, 4, 1, 89.99);
