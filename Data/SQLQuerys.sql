/*
	Create table example
*/
CREATE TABLE Weapons 
(
	Id INT NOT NULL,
	Name VARCHAR(25),
	Damage INT,
	CharacterId INT NOT NULL,
	PRIMARY KEY (Id),
	CONSTRAINT FK_WEAPONS FOREIGN KEY (CharacterId)
	REFERENCES Characters(Id)
);

/*
	Shows constriants in the database
*/
SELECT * FROM sys.objects

WHERE type_desc LIKE '%CONSTRAINT'

/*
	Removes a constraint from a database
*/
ALTER TABLE Skill DROP CONSTRAINT FK_CharacterSkill_Skill_SkillId;