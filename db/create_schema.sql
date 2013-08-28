--
-- ER/Studio 8.0 SQL Code Generation
-- Company :      c
-- Project :      disposable.dm1
-- Author :       Casey
--
-- Date Created : Thursday, August 29, 2013 00:29:33
-- Target DBMS : MySQL 5.x
--

-- 
-- TABLE: DISPOSABLE.ACT 
--

CREATE TABLE DISPOSABLE.ACT(
    TEST_APP_ID    INT UNSIGNED  NOT NULL,
    ACT_ID         INT         NOT NULL,
    APP_ID         INT UNSIGNED  NOT NULL,
    USER_ID        INT UNSIGNED  NOT NULL,
    ACT_TYPE_ID    INT         NOT NULL,
    ISSUED_DTM     DATETIME    NOT NULL,
    PRIMARY KEY (TEST_APP_ID, ACT_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.ACT_TYPE 
--

CREATE TABLE DISPOSABLE.ACT_TYPE(
    ACT_TYPE_ID          INT         NOT NULL,
    EXPIRATION_OFFSET    DATETIME    NOT NULL,
    PRIMARY KEY (ACT_TYPE_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.APP 
--

CREATE TABLE DISPOSABLE.APP(
    APP_ID    INT UNSIGNED  AUTO_INCREMENT,
    PRIMARY KEY (APP_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.DEVICE 
--

CREATE TABLE DISPOSABLE.DEVICE(
    APP_ID            INT UNSIGNED   NOT NULL,
    DEVICE_ID         INT UNSIGNED   NOT NULL,
    DEVICE_TYPE_ID    INT            NOT NULL,
    USER_ID           INT UNSIGNED   NOT NULL,
    NAME              VARCHAR(18)    NOT NULL,
    PRIMARY KEY (APP_ID, DEVICE_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.DEVICE_SEED 
--

CREATE TABLE DISPOSABLE.DEVICE_SEED(
    DEVICE_ID    INT UNSIGNED  AUTO_INCREMENT,
    PRIMARY KEY (DEVICE_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.DEVICE_TYPE 
--

CREATE TABLE DISPOSABLE.DEVICE_TYPE(
    DEVICE_TYPE_ID    INT    NOT NULL,
    PRIMARY KEY (DEVICE_TYPE_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.USER 
--

CREATE TABLE DISPOSABLE.USER(
    APP_ID     INT UNSIGNED  NOT NULL,
    USER_ID    INT UNSIGNED  NOT NULL,
    PRIMARY KEY (APP_ID, USER_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.USER_SEED 
--

CREATE TABLE DISPOSABLE.USER_SEED(
    USER_ID    INT UNSIGNED  AUTO_INCREMENT,
    PRIMARY KEY (USER_ID)
)ENGINE=INNODB
;



-- 
-- TABLE: DISPOSABLE.ACT 
--

ALTER TABLE DISPOSABLE.ACT ADD CONSTRAINT FK_ACT_TYPE_ACT 
    FOREIGN KEY (ACT_TYPE_ID)
    REFERENCES DISPOSABLE.ACT_TYPE(ACT_TYPE_ID)
;

ALTER TABLE DISPOSABLE.ACT ADD CONSTRAINT FK_APP_ACT 
    FOREIGN KEY (TEST_APP_ID)
    REFERENCES DISPOSABLE.APP(APP_ID)
;

ALTER TABLE DISPOSABLE.ACT ADD CONSTRAINT FK_USER_ACT 
    FOREIGN KEY (APP_ID, USER_ID)
    REFERENCES DISPOSABLE.USER(APP_ID, USER_ID)
;


-- 
-- TABLE: DISPOSABLE.DEVICE 
--

ALTER TABLE DISPOSABLE.DEVICE ADD CONSTRAINT FK_APP_DEVICE 
    FOREIGN KEY (APP_ID)
    REFERENCES DISPOSABLE.APP(APP_ID)
;

ALTER TABLE DISPOSABLE.DEVICE ADD CONSTRAINT FK_DEVICE_SEED_DEVICE 
    FOREIGN KEY (DEVICE_ID)
    REFERENCES DISPOSABLE.DEVICE_SEED(DEVICE_ID)
;

ALTER TABLE DISPOSABLE.DEVICE ADD CONSTRAINT FK_DEVICE_TYPE_DEVICE 
    FOREIGN KEY (DEVICE_TYPE_ID)
    REFERENCES DISPOSABLE.DEVICE_TYPE(DEVICE_TYPE_ID)
;

ALTER TABLE DISPOSABLE.DEVICE ADD CONSTRAINT FK_USER_DEVICE 
    FOREIGN KEY (APP_ID, USER_ID)
    REFERENCES DISPOSABLE.USER(APP_ID, USER_ID)
;


-- 
-- TABLE: DISPOSABLE.USER 
--

ALTER TABLE DISPOSABLE.USER ADD CONSTRAINT FK_APP_USER 
    FOREIGN KEY (APP_ID)
    REFERENCES DISPOSABLE.APP(APP_ID)
;

ALTER TABLE DISPOSABLE.USER ADD CONSTRAINT FK_USER_SEED_USER 
    FOREIGN KEY (USER_ID)
    REFERENCES DISPOSABLE.USER_SEED(USER_ID)
;


