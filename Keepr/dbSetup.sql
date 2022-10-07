CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(25) NOT NULL,
  img VARCHAR(500) NOT NULL,
  description TEXT NOT NULL,
  views INT NOT NULL DEFAULT 0,
  kept INT NOT NULL DEFAULT 0,
  shares INT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  vaultKeepId INT,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
  -- FOREIGN kEY (vaultKeepId) REFERENCES vaultKeeps(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';
-- vaultKeepId INT NOTE dont know if I really need this should ask

ALTER TABLE vaultKeeps DROP FOREIGN KEY vaultKeeps_ibfk_3;

ALTER TABLE keeps ADD FOREIGN KEY (vaultKeepId) REFERENCES vaultKeeps(id) ON DELETE CASCADE;
SELECT * FROM keeps;
SELECT * FROM keeps WHERE id = 705;
SELECT * FROM vaults;
SELECT * FROM vaultKeeps;
DROP TABLE keeps;
DROP TABLE vaults;
DROP TABLE vaultKeeps;
DELETE FROM keeps;
DELETE FROM vaults;
DELETE FROM vaultKeeps;

CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(25) NOT NULL,
  description TEXT NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT false,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN kEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

INSERT INTO keeps
(name, img, description, creatorId)
VALUES
("Camping", "https://images.unsplash.com/photo-1504851149312-7a075b496cc7?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8M3x8Y2FtcGluZ3xlbnwwfHwwfHw%3D&auto=format&fit=crop&w=600&q=60", "Camping time", "63059889588984525e6be97d");

INSERT INTO keeps
(name, img, description, creatorId)
VALUES
("Fair Vibes", "https://images.unsplash.com/photo-1610659606489-77967e40fa35?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8ZmFpcnxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=600&q=60", "Ferris Wheel", "630598d573bee231866dd930");