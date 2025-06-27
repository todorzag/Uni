CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Username" VARCHAR(50) NOT NULL UNIQUE,
    "Email" VARCHAR(100) NOT NULL UNIQUE,
    "PasswordHash" VARCHAR(200) NOT NULL,
    "Role" VARCHAR(20) NOT NULL DEFAULT 'User',
    "RegisteredOn" TIMESTAMP NOT NULL DEFAULT NOW(),
	"IsActive" BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE "Computers" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(500),
    "ImageUrl" VARCHAR(255) NOT NULL,
    "Price" DECIMAL(10,2) NOT NULL CHECK ("Price" >= 0),
    "Stock" INT NOT NULL CHECK ("Stock" >= 0),
    "Type" VARCHAR(50),
    "Processor" VARCHAR(100),
    "RAM" VARCHAR(50),
    "Storage" VARCHAR(100),
    "ScreenSize" DECIMAL(4,2),
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE "Orders" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL REFERENCES "Users"("Id") ON DELETE CASCADE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "Total" DECIMAL(10, 2) NOT NULL CHECK ("Total" >= 0),
    "Status" VARCHAR(20) NOT NULL DEFAULT 'Pending',
    "OrderAddress" VARCHAR(200) NOT NULL
);

CREATE TABLE "OrderItems" (
    "Id" SERIAL PRIMARY KEY,
    "OrderId" INT NOT NULL REFERENCES "Orders"("Id") ON DELETE CASCADE,
    "ComputerId" INT NOT NULL REFERENCES "Computers"("Id") ON DELETE CASCADE,
    "Quantity" INT NOT NULL CHECK ("Quantity" > 0),
    "Price" DECIMAL(10, 2) NOT NULL CHECK ("Price" >= 0)
);
