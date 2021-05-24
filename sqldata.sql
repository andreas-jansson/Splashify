DROP TABLE IF EXISTS eventclub;
DROP TABLE IF EXISTS roleapplication;
DROP TABLE IF EXISTS eventapplication;
DROP TABLE IF EXISTS clubapplication;
DROP TABLE IF EXISTS score;
DROP TABLE IF EXISTS jump;
DROP TABLE IF EXISTS eventcompetitor;
DROP TABLE IF EXISTS eventjudge;
DROP TABLE IF EXISTS organizer;
DROP TABLE IF EXISTS club;
DROP TABLE IF EXISTS judge;
DROP TABLE IF EXISTS competitor;
DROP TABLE IF EXISTS event;
DROP TABLE IF EXISTS user;
DROP TABLE IF EXISTS eventtype;
DROP TABLE IF EXISTS roletype;
DROP TABLE IF EXISTS gendertype;
DROP TABLE IF EXISTS jumptype;

CREATE TABLE jumptype(
    id INTEGER,
    groupnr INTEGER,
    description VARCHAR(75),
    style VARCHAR(3),
    height INTEGER,
    value REAL,
    PRIMARY KEY(id) 
);

CREATE TABLE gendertype(
    gender VARCHAR(10) PRIMARY KEY
);

CREATE TABLE roletype(
    role VARCHAR(15) PRIMARY KEY,
    ID INTEGER unique 
);

CREATE TABLE eventtype(
    eventtypeID REAL PRIMARY KEY
);

CREATE TABLE user(
    userID INTEGER PRIMARY KEY autoincrement,
    fname VARCHAR(15),
    lname VARCHAR(15),
    email VARCHAR(30),
    password VARCHAR(150) NOT NULL,
    salt VARCHAR(300) NOT NULL,
    birthdate VARCHAR(12),
    club INTEGER /*REFERENCES club(clubID)*/,
    gender VARCHAR(10) REFERENCES gendertype(gender),
    role VARCHAR(10)
);

CREATE TABLE event(
    eventID VARCHAR(50) PRIMARY KEY,
    startdate TEXT,
    gender VARCHAR(10) REFERENCES gendertype(gender),
    eventtype REAL REFERENCES eventtype(eventtypeID)
);

CREATE TABLE competitor(
    competitorID INTEGER PRIMARY KEY autoincrement,
    userID INTEGER REFERENCES user(userID)
);

CREATE TABLE judge(
    judgeID INTEGER PRIMARY KEY autoincrement,
    userID INTEGER REFERENCES user(userID)
);

CREATE TABLE club(
    clubID INTEGER PRIMARY KEY,
    userID INTEGER REFERENCES user(userID),
    clubname VARCHAR(30)
);

CREATE TABLE organizer(
    organizerID INTEGER PRIMARY KEY,
    userID INTEGER REFERENCES user(userID)
);

CREATE TABLE eventjudge(
    eventID VARCHAR(50) REFERENCES event(eventID),
    judgeID INTEGER REFERENCES judge(judgeID),
    PRIMARY KEY(eventID, judgeID)

);

CREATE TABLE eventcompetitor(
    eventID VARCHAR(50) REFERENCES event(eventID),
    competitorID INTEGER REFERENCES competitor(competitorID),
    PRIMARY KEY(eventID, competitorID)
);

CREATE TABLE jump(
    jumpID INTEGER PRIMARY KEY autoincrement,
    eventID VARCHAR(50) REFERENCES event(eventID),
    competitorID INTEGER REFERENCES competitor(competitorID),
    jumpnr INTEGER,
    jumptype INTEGER REFERENCES jumptype(ID),
    finalscore REAL
);

CREATE TABLE score(
    jumpID INTEGER  REFERENCES jump(jumpID),
    judgeID INTEGER REFERENCES judge(judgeID),
    score REAL,
    PRIMARY KEY(jumpID, judgeID)
);

CREATE TABLE clubapplication(
    userID INTEGER PRIMARY KEY REFERENCES user(userID),
    clubID INTEGER REFERENCES club(clubID)
);

CREATE TABLE eventapplication(
    ID INTEGER PRIMARY KEY autoincrement,
    clubID INTEGER REFERENCES club(clubID),
    eventID VARCHAR(50) REFERENCES event(eventID),
    userID INTEGER REFERENCES user(userID)
);

CREATE TABLE roleapplication(
    userID INTEGER PRIMARY KEY REFERENCES user(userID),
    role VARCHAR(15) REFERENCES roletype(role)
);

CREATE TABLE eventclub(
    eventID VARCHAR(50) REFERENCES event(eventID),
    clubID INTEGER REFERENCES club(clubID),
    PRIMARY KEY (eventID, clubID)
);

/*###########################               ###########################
###########################   Inserts     ###########################
###########################               ###########################*/

INSERT INTO roletype(role)
    VALUES('organizer');
INSERT INTO roletype(role)
    VALUES('club');
INSERT INTO roletype(role)
    VALUES('competitor');
INSERT INTO roletype(role)
    VALUES('judge');
INSERT INTO roletype(role)
    VALUES('default');

INSERT INTO gendertype(gender)
    VALUES('male');
INSERT INTO gendertype(gender)
    VALUES('female');

/*########################### club ###########################*/

INSERT INTO club(clubID, userID, clubname)
    VALUES(10000, NULL, 'Örebro simalliance');
INSERT INTO club(clubID, userID, clubname)
    VALUES(10001, NULL, 'Kumla divers');
INSERT INTO club(clubID, userID, clubname)
    VALUES(10002, NULL, 'sthlm finest');
INSERT INTO club(clubID, userID, clubname)
    VALUES(10003, NULL, 'Huddyk');

/*####################### user ###################################*/

INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Kenny', 'Starfinghter', 'kenny@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1977-08-03', NULL, 'male', 'competitor');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Elliot', 'Alderson', 'elliot@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1990-03-12', NULL, 'male', 'competitor');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Sara', 'Fridh', 'sara@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1991-05-17', NULL,'female', 'competitor');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Frida', 'Eriksson', 'frida@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1992-12-12', NULL,'female', 'competitor');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Sten', 'Gård', 'sten@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1988-01-22', NULL,'male', 'competitor');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Axel', 'Dag', 'axel@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1994-07-02', NULL,'male', 'competitor');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Anna', 'Jonsson', 'anna@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1997-01-01',NULL,'female', 'competitor');

/*########################### users roles: judge ###########################*/
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Gudrun', 'Skyman', 'gudrun@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1977-02-14',NULL,'female', 'judge');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Erik', 'Svärd', 'erik@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1980-07-17','male',NULL, 'judge');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Alfred', 'Gunnarsson', 'alfred@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1968-11-03',NULL,'male', 'judge');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Emma', 'Eriksson', 'emma@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1990-04-01',NULL,'female', 'judge');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Gustav', 'Frisk', 'gustav@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1972-03-12',NULL,'male', 'judge');

/*########################### users roles: organizer ###########################*/

INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Ricky', 'Martin', 'ricky@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1976-09-09',NULL,'male', 'oragnizer');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Brittney', 'Spears', 'brittney@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1981-10-24',NULL,'female', 'oragnizer');
/*########################### users roles: club ###########################*/

INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Anton', 'Jonsvens', 'anton@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1986-05-02', NULL, 'male', 'club');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Kalle', 'Öhman', 'kalle@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1982-09-12', NULL, 'male', 'club');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Sanna', 'Sjögren', 'sanna@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1989-04-20', NULL,'female', 'club');
INSERT INTO user(fname, lname, email, password, salt, birthdate, club, gender, role)
    VALUES('Julia', 'Andersson', 'julia@splashify.se', '88DE2B4947C14CD75F6CFCD89C1FC4661E88EF64F2F3675414AB521BD3D52655', '51-FE-AB-A6-06-24-F9-77-4D-73-5C-52-B1-F7-73-3D-DE-44-13-E7-3C-87-7D-2B-28-C3-9E-DF-87-C0-64-F1-20-38-A3-5D-F7-67-E8-D3-57-F2-EF-22-F2-48-85-9D-96-91-89-3C-92-61-2A-C6-F6-C9-6E-F1-8E-55-D3-40' , '1981-03-12', NULL,'female', 'club');

/*################## gööööb ################*/

UPDATE club SET userID = 15 WHERE clubID = 10000;
UPDATE club SET userID = 16 WHERE clubID = 10001;
UPDATE club SET userID = 17 WHERE clubID = 10002;
UPDATE club SET userID = 18 WHERE clubID = 10003;

UPDATE user SET club = 10001 WHERE email = 'kenny@splashify.se';
UPDATE user SET club = 10001 WHERE email = 'elliot@splashify.se';
UPDATE user SET club = 10001 WHERE email = 'sara@splashify.se';
UPDATE user SET club = 10003 WHERE email = 'frida@splashify.se';
UPDATE user SET club = 10002 WHERE email = 'sten@splashify.se';
UPDATE user SET club = 10002 WHERE email = 'axel@splashify.se';
UPDATE user SET club = 10002 WHERE email = 'anna@splashify.se';
UPDATE user SET club = 10000 WHERE email = 'anton@splashify.se';
UPDATE user SET club = 10003 WHERE email = 'kalle@splashify.se';
UPDATE user SET club = 10001 WHERE email = 'sanna@splashify.se';
UPDATE user SET club = 10002 WHERE email = 'julia@splashify.se';

/* ###########################   eventtype ###########################*/
INSERT INTO eventtype(eventtypeID)
    VALUES(1);
INSERT INTO eventtype(eventtypeID)
    VALUES(3);
INSERT INTO eventtype(eventtypeID)
    VALUES(5);
INSERT INTO eventtype(eventtypeID)
    VALUES(7.5);
INSERT INTO eventtype(eventtypeID)
    VALUES(10);
/*###########################   event     ###########################*/

INSERT INTO event(eventID, startdate, gender, eventtype)
    VALUES('vm2021', '2021-05-26', 'male', 5);
INSERT INTO event(eventID, startdate, gender, eventtype)
    VALUES('os2021', '2021-06-12', 'male', 10);
INSERT INTO event(eventID, startdate, gender, eventtype)
    VALUES('em2021', '2021-07-10', 'female', 3);

/*########################### competitor ###########################*/

INSERT INTO competitor(competitorID, userID)
    VALUES(1000, 1);
INSERT INTO competitor(competitorID, userID)
    VALUES(1001, 2);
INSERT INTO competitor(competitorID, userID)
    VALUES(1002, 3);
INSERT INTO competitor(competitorID, userID)
    VALUES(1003, 4);
INSERT INTO competitor(competitorID, userID)
    VALUES(1004, 5);
INSERT INTO competitor(competitorID, userID)
    VALUES(1005, 6);
INSERT INTO competitor(competitorID, userID)
    VALUES(1006, 7);

/*########################### judge ###########################*/

INSERT INTO judge(judgeID, userID)
    VALUES(5000, 8);
INSERT INTO judge(judgeID, userID)
    VALUES(5001, 9);
INSERT INTO judge(judgeID, userID)
    VALUES(5002, 10);
INSERT INTO judge(judgeID, userID)
    VALUES(5003, 11);
INSERT INTO judge(judgeID, userID)
    VALUES(5004, 12);

/*########################### organizer ###########################*/

INSERT INTO organizer(organizerID, userID)
    VALUES(20000, 13);
INSERT INTO organizer(organizerID, userID)
    VALUES(20001, 14);





/*######################### eventcompetitor #########################*/

INSERT INTO eventcompetitor(eventID, competitorID)
    VALUES('vm2021', 1000);
INSERT INTO eventcompetitor(eventID, competitorID)
    VALUES('vm2021', 1001);
INSERT INTO eventcompetitor(eventID, competitorID)
    VALUES('os2021', 1001);
INSERT INTO eventcompetitor(eventID, competitorID)
    VALUES('os2021', 1005);
INSERT INTO eventcompetitor(eventID, competitorID)
    VALUES('em2021', 1003);
INSERT INTO eventcompetitor(eventID, competitorID)
    VALUES('em2021', 1004);

/*########################### eventjudge ###########################*/

INSERT INTO eventjudge(eventID, judgeID)
    VALUES('vm2021', 5000);
INSERT INTO eventjudge(eventID, judgeID)
    VALUES('vm2021', 5001);
INSERT INTO eventjudge(eventID, judgeID)
    VALUES('vm2021', 5002);

INSERT INTO eventjudge(eventID, judgeID)
    VALUES('os2021', 5000);
INSERT INTO eventjudge(eventID, judgeID)
    VALUES('os2021', 5002);
INSERT INTO eventjudge(eventID, judgeID)
    VALUES('os2021', 5004);

INSERT INTO eventjudge(eventID, judgeID)
    VALUES('em2021', 5002);
INSERT INTO eventjudge(eventID, judgeID)
    VALUES('em2021', 5003);
INSERT INTO eventjudge(eventID, judgeID)
    VALUES('em2021', 5004);

/*########################### jump ###########################*/
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('vm2021', 1000, 1);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('vm2021', 1000, 2);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('vm2021', 1001, 1);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('vm2021', 1001, 2);

INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('os2021', 1000, 1);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('os2021', 1000, 2);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('os2021', 1005, 1);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('os2021', 1005, 2);

INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('em2021', 1003, 1);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('em2021', 1003, 2);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('em2021', 1004, 1);
INSERT INTO jump(eventID, competitorID, jumpnr)
    VALUES('em2021', 1004, 2);

/*########################### score ###########################*/
INSERT INTO score(jumpID, judgeID, score)
    VALUES(1, 5000, 8);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(1, 5001, 7);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(1, 5002, 8);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(2, 5000, 6);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(2, 5001, 7);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(2, 5002, 8);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(3, 5000, 9);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(3, 5001, 8);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(3, 5002, 9);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(4, 5000, 6);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(4, 5001, 6);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(4, 5002, 7);


INSERT INTO score(jumpID, judgeID, score)
    VALUES(5, 5000, 10);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(5, 5002, 10);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(5, 5004, 10);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(6, 5000, 10);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(6, 5002, 8);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(6, 5004, 9);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(7, 5000, 8);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(7, 5002, 8);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(7, 5004, 8);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(8, 5000, 7);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(8, 5002, 7);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(8, 5004, 8);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(9, 5002, 6);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(9, 5003, 6);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(9, 5004, 6);

INSERT INTO score(jumpID, judgeID, score)
    VALUES(10, 5002, 5);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(10, 5003, 6);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(10, 5004, 6);


INSERT INTO score(jumpID, judgeID, score)
    VALUES(11, 5002, 9);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(11, 5003, 8);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(11, 5004, 9);


INSERT INTO score(jumpID, judgeID, score)
    VALUES(12, 5002, 10);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(12, 5003, 10);
INSERT INTO score(jumpID, judgeID, score)
    VALUES(12, 5004, 10);


/*########################### club application ###########################*/
INSERT INTO clubapplication(userID, clubID)
    VALUES(6, 10000);
INSERT INTO clubapplication(userID, clubID)
    VALUES(7, 10000);

/*########################### role application ###########################*/
INSERT INTO roleapplication(userID, role)
    VALUES(6, 'competitor');
INSERT INTO roleapplication(userID, role)
    VALUES(7, 'competitor');
INSERT INTO roleapplication(userID, role)
    VALUES(11, 'judge');

/*########################### event application ###########################*/
INSERT INTO eventapplication(clubID, eventID)
    VALUES(10000, 'vm2021');
INSERT INTO eventapplication(clubID, eventID)
    VALUES(10002, 'os2021');

select j.eventID, j.competitorID, u.fname, u.lname,  j.jumpnr, j.jumptype, us.fname as JudgeFirstName , us.lname as JudgeLastName, s.score, j.finalscore 
from jump as j 
left join score as s on j.jumpID = s.jumpID 
 inner join competitor as c on c.competitorID = j.competitorID 
inner join user as u on u.userID = c.userID 
inner join judge as ju on ju.judgeID = s.judgeID 
inner join user as us on us.userID = ju.userID 
where j.eventID = "VM 2021"
order by j.competitorID and j.jumpnr;