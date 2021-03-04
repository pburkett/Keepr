use pburkett;

CREATE TABLE vaults 
(
  id INT NOT NULL AUTO_INCREMENT,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255),
  isPrivate TINYINT NOT NULL,
  img VARCHAR(65535),


  PRIMARY KEY (id),

FOREIGN KEY (creatorId)
   REFERENCES profiles (id)
   ON DELETE CASCADE

);
-- -- CREATE TABLE profiles
-- -- (
-- --   id VARCHAR(255) NOT NULL,
-- --   email VARCHAR(255) NOT NULL,
-- --   name VARCHAR(255),
-- --   picture VARCHAR(255),
-- --   PRIMARY KEY (id)
-- -- );

CREATE TABLE keeps
(
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255),
  img VARCHAR(65535) NOT NULL,
  views INT NOT NULL DEFAULT 0,
  shares INT NOT NULL DEFAULT 0,
  keeps INT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  id int NOT NULL AUTO_INCREMENT,

PRIMARY KEY (id),

FOREIGN KEY (creatorId)
   REFERENCES profiles (id)
   ON DELETE CASCADE
);
CREATE TABLE vaultKeeps
(
  id INT NOT NULL AUTO_INCREMENT,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,

  PRIMARY KEY (id),

  FOREIGN KEY (creatorId)
   REFERENCES profiles (id)
   ON DELETE CASCADE,

   FOREIGN KEY (vaultId)
   REFERENCES vaults (id)
   ON DELETE CASCADE,

   FOREIGN KEY (keepId)
   REFERENCES keeps (id)
   ON DELETE CASCADE

);


-- -- SELECT 
-- --       vault.*,
-- --       profile.*
-- --        FROM vaults vault
-- --        JOIN profiles profile ON vault.creatorId = profile.id
-- --        WHERE creatorId = profile.id;

-- use pburkett;
-- DROP TABLE vaultkeeps;
-- DROP TABLE keeps;
-- DROP TABLE vaults;