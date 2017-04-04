










































-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 04/04/2017 09:07:19

-- Generated from EDMX file: H:\Software\Proyectos\slnControlDeAsistencia\CapaDeDatos\mAsistencia.edmx
-- Target version: 3.0.0.0

-- --------------------------------------------------


DROP DATABASE IF EXISTS `bdasistencia`;
CREATE DATABASE `bdasistencia`;
USE `bdasistencia`;


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;

SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------


CREATE TABLE `TrabajadorSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Nombre` longtext NOT NULL, 
	`ApellidoPaterno` longtext NOT NULL, 
	`ApellidoMaterno` longtext NOT NULL, 
	`DNI` longtext NOT NULL, 
	`Oficina_Id` int NOT NULL, 
	`HorarioSemana_Id` int NOT NULL);

ALTER TABLE `TrabajadorSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `OficinaSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Nombre` longtext NOT NULL, 
	`Local_Id` int NOT NULL, 
	`Oficina2_Id` int NOT NULL);

ALTER TABLE `OficinaSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `AsistenciaSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`PicadoReloj` datetime NOT NULL, 
	`Trabajador_Id` int NOT NULL);

ALTER TABLE `AsistenciaSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `LocalSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Nombre` longtext NOT NULL, 
	`Empresa_Id` int NOT NULL);

ALTER TABLE `LocalSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `HorarioSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Entrada` datetime NOT NULL, 
	`Salida` datetime NOT NULL, 
	`Nombre` longtext NOT NULL, 
	`HorarioDia_Id` int NOT NULL, 
	`ReglasTardanza_Id` int NOT NULL);

ALTER TABLE `HorarioSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `HorarioDiaSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Dia` longtext NOT NULL, 
	`HorarioSemana_Id` int NOT NULL);

ALTER TABLE `HorarioDiaSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `HorarioSemanaSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Nombre` longtext NOT NULL);

ALTER TABLE `HorarioSemanaSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `ReglasTardanzaSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`TiempoTardanza` datetime NOT NULL);

ALTER TABLE `ReglasTardanzaSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `PermisosHorasSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Inicio` longtext NOT NULL, 
	`Trabajador_Id` int, 
	`TipoPermisos_Id` int NOT NULL);

ALTER TABLE `PermisosHorasSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `TipoPermisosSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Nombre` longtext NOT NULL, 
	`Computable` longtext NOT NULL);

ALTER TABLE `TipoPermisosSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `PermisosDiasSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Trabajador_Id` int, 
	`TipoPermisos_Id` int NOT NULL);

ALTER TABLE `PermisosDiasSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `DiaFestivoSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Dia` datetime NOT NULL, 
	`Nombre` longtext NOT NULL);

ALTER TABLE `DiaFestivoSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `CronogramaVacacionesSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Anio` smallint NOT NULL, 
	`Inicio` datetime NOT NULL, 
	`Fin` datetime NOT NULL, 
	`Trabajador_Id` int NOT NULL);

ALTER TABLE `CronogramaVacacionesSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `EmpresaSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Nombre` longtext NOT NULL);

ALTER TABLE `EmpresaSet` ADD PRIMARY KEY (`Id`);







-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------


-- Creating foreign key on `Oficina_Id` in table 'TrabajadorSet'

ALTER TABLE `TrabajadorSet`
ADD CONSTRAINT `FK_TrabajadorOficina`
    FOREIGN KEY (`Oficina_Id`)
    REFERENCES `OficinaSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_TrabajadorOficina'

CREATE INDEX `IX_FK_TrabajadorOficina`
    ON `TrabajadorSet`
    (`Oficina_Id`);



-- Creating foreign key on `Local_Id` in table 'OficinaSet'

ALTER TABLE `OficinaSet`
ADD CONSTRAINT `FK_LocalOficina`
    FOREIGN KEY (`Local_Id`)
    REFERENCES `LocalSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_LocalOficina'

CREATE INDEX `IX_FK_LocalOficina`
    ON `OficinaSet`
    (`Local_Id`);



-- Creating foreign key on `HorarioDia_Id` in table 'HorarioSet'

ALTER TABLE `HorarioSet`
ADD CONSTRAINT `FK_HorarioDiaHorario`
    FOREIGN KEY (`HorarioDia_Id`)
    REFERENCES `HorarioDiaSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_HorarioDiaHorario'

CREATE INDEX `IX_FK_HorarioDiaHorario`
    ON `HorarioSet`
    (`HorarioDia_Id`);



-- Creating foreign key on `HorarioSemana_Id` in table 'HorarioDiaSet'

ALTER TABLE `HorarioDiaSet`
ADD CONSTRAINT `FK_HorarioSemanaHorarioDia`
    FOREIGN KEY (`HorarioSemana_Id`)
    REFERENCES `HorarioSemanaSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_HorarioSemanaHorarioDia'

CREATE INDEX `IX_FK_HorarioSemanaHorarioDia`
    ON `HorarioDiaSet`
    (`HorarioSemana_Id`);



-- Creating foreign key on `HorarioSemana_Id` in table 'TrabajadorSet'

ALTER TABLE `TrabajadorSet`
ADD CONSTRAINT `FK_TrabajadorHorarioSemana`
    FOREIGN KEY (`HorarioSemana_Id`)
    REFERENCES `HorarioSemanaSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_TrabajadorHorarioSemana'

CREATE INDEX `IX_FK_TrabajadorHorarioSemana`
    ON `TrabajadorSet`
    (`HorarioSemana_Id`);



-- Creating foreign key on `ReglasTardanza_Id` in table 'HorarioSet'

ALTER TABLE `HorarioSet`
ADD CONSTRAINT `FK_ReglasTardanzaHorario`
    FOREIGN KEY (`ReglasTardanza_Id`)
    REFERENCES `ReglasTardanzaSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_ReglasTardanzaHorario'

CREATE INDEX `IX_FK_ReglasTardanzaHorario`
    ON `HorarioSet`
    (`ReglasTardanza_Id`);



-- Creating foreign key on `Trabajador_Id` in table 'AsistenciaSet'

ALTER TABLE `AsistenciaSet`
ADD CONSTRAINT `FK_TrabajadorAsistencia`
    FOREIGN KEY (`Trabajador_Id`)
    REFERENCES `TrabajadorSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_TrabajadorAsistencia'

CREATE INDEX `IX_FK_TrabajadorAsistencia`
    ON `AsistenciaSet`
    (`Trabajador_Id`);



-- Creating foreign key on `Trabajador_Id` in table 'PermisosHorasSet'

ALTER TABLE `PermisosHorasSet`
ADD CONSTRAINT `FK_TrabajadorPermisosHoras`
    FOREIGN KEY (`Trabajador_Id`)
    REFERENCES `TrabajadorSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_TrabajadorPermisosHoras'

CREATE INDEX `IX_FK_TrabajadorPermisosHoras`
    ON `PermisosHorasSet`
    (`Trabajador_Id`);



-- Creating foreign key on `Trabajador_Id` in table 'PermisosDiasSet'

ALTER TABLE `PermisosDiasSet`
ADD CONSTRAINT `FK_TrabajadorPermisosDias`
    FOREIGN KEY (`Trabajador_Id`)
    REFERENCES `TrabajadorSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_TrabajadorPermisosDias'

CREATE INDEX `IX_FK_TrabajadorPermisosDias`
    ON `PermisosDiasSet`
    (`Trabajador_Id`);



-- Creating foreign key on `TipoPermisos_Id` in table 'PermisosHorasSet'

ALTER TABLE `PermisosHorasSet`
ADD CONSTRAINT `FK_PermisosHorasTipoPermisos`
    FOREIGN KEY (`TipoPermisos_Id`)
    REFERENCES `TipoPermisosSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_PermisosHorasTipoPermisos'

CREATE INDEX `IX_FK_PermisosHorasTipoPermisos`
    ON `PermisosHorasSet`
    (`TipoPermisos_Id`);



-- Creating foreign key on `TipoPermisos_Id` in table 'PermisosDiasSet'

ALTER TABLE `PermisosDiasSet`
ADD CONSTRAINT `FK_PermisosDiasTipoPermisos`
    FOREIGN KEY (`TipoPermisos_Id`)
    REFERENCES `TipoPermisosSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_PermisosDiasTipoPermisos'

CREATE INDEX `IX_FK_PermisosDiasTipoPermisos`
    ON `PermisosDiasSet`
    (`TipoPermisos_Id`);



-- Creating foreign key on `Trabajador_Id` in table 'CronogramaVacacionesSet'

ALTER TABLE `CronogramaVacacionesSet`
ADD CONSTRAINT `FK_TrabajadorCronogramaVacaciones`
    FOREIGN KEY (`Trabajador_Id`)
    REFERENCES `TrabajadorSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_TrabajadorCronogramaVacaciones'

CREATE INDEX `IX_FK_TrabajadorCronogramaVacaciones`
    ON `CronogramaVacacionesSet`
    (`Trabajador_Id`);



-- Creating foreign key on `Oficina2_Id` in table 'OficinaSet'

ALTER TABLE `OficinaSet`
ADD CONSTRAINT `FK_OficinaOficina`
    FOREIGN KEY (`Oficina2_Id`)
    REFERENCES `OficinaSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_OficinaOficina'

CREATE INDEX `IX_FK_OficinaOficina`
    ON `OficinaSet`
    (`Oficina2_Id`);



-- Creating foreign key on `Empresa_Id` in table 'LocalSet'

ALTER TABLE `LocalSet`
ADD CONSTRAINT `FK_EmpresaLocal`
    FOREIGN KEY (`Empresa_Id`)
    REFERENCES `EmpresaSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_EmpresaLocal'

CREATE INDEX `IX_FK_EmpresaLocal`
    ON `LocalSet`
    (`Empresa_Id`);



-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
