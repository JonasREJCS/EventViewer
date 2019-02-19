SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `Event_Viewer` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `Event_Viewer` ;

-- -----------------------------------------------------
-- Table `Event_Viewer`.`evento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`evento` (
  `id_evento` INT NOT NULL AUTO_INCREMENT,
  `nome_evento` VARCHAR(45) NOT NULL,
  `descricao_evento` VARCHAR(255) NULL,
  `logotipo_evento` MEDIUMBLOB NULL,
  `status_evento` CHAR NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_evento`),
  UNIQUE INDEX `nome_UNIQUE` (`nome_evento` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`pais`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`pais` (
  `id_pais` INT NOT NULL AUTO_INCREMENT,
  `nome_pais` VARCHAR(100) NOT NULL,
  `status_pais` CHAR NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_pais`),
  UNIQUE INDEX `nome_pais_UNIQUE` (`nome_pais` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`estado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`estado` (
  `id_estado` INT NOT NULL AUTO_INCREMENT,
  `nome_estado` VARCHAR(100) NOT NULL,
  `status_estado` CHAR NOT NULL DEFAULT '1',
  `id_do_pais` INT NOT NULL,
  PRIMARY KEY (`id_estado`),
  INDEX `fk_estado_pais1_idx` (`id_do_pais` ASC),
  UNIQUE INDEX `nome_estado_UNIQUE` (`nome_estado` ASC),
  CONSTRAINT `fk_estado_pais1`
    FOREIGN KEY (`id_do_pais`)
    REFERENCES `Event_Viewer`.`pais` (`id_pais`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`municipio`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`municipio` (
  `id_municipio` INT NOT NULL AUTO_INCREMENT,
  `nome_municipio` VARCHAR(100) NOT NULL,
  `status_municipio` CHAR NOT NULL DEFAULT '1',
  `id_do_estado` INT NOT NULL,
  PRIMARY KEY (`id_municipio`),
  INDEX `fk_municipio_estado1_idx` (`id_do_estado` ASC),
  CONSTRAINT `fk_municipio_estado1`
    FOREIGN KEY (`id_do_estado`)
    REFERENCES `Event_Viewer`.`estado` (`id_estado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`bairro`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`bairro` (
  `id_bairro` INT NOT NULL AUTO_INCREMENT,
  `nome_bairro` VARCHAR(100) NOT NULL,
  `status_bairro` CHAR NOT NULL DEFAULT '1',
  `id_do_municipio` INT NOT NULL,
  PRIMARY KEY (`id_bairro`),
  INDEX `fk_bairro_municipio1_idx` (`id_do_municipio` ASC),
  CONSTRAINT `fk_bairro_municipio1`
    FOREIGN KEY (`id_do_municipio`)
    REFERENCES `Event_Viewer`.`municipio` (`id_municipio`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`endereco`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`endereco` (
  `id_endereco` INT NOT NULL AUTO_INCREMENT,
  `cep` VARCHAR(45) NOT NULL,
  `logradouro` VARCHAR(100) NOT NULL,
  `status_endereco` CHAR NOT NULL DEFAULT '1',
  `id_do_bairro` INT NOT NULL,
  PRIMARY KEY (`id_endereco`),
  UNIQUE INDEX `cep_UNIQUE` (`cep` ASC),
  INDEX `fk_endereco_bairro1_idx` (`id_do_bairro` ASC),
  CONSTRAINT `fk_endereco_bairro1`
    FOREIGN KEY (`id_do_bairro`)
    REFERENCES `Event_Viewer`.`bairro` (`id_bairro`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`local_de_evento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`local_de_evento` (
  `id_local_de_evento` INT NOT NULL AUTO_INCREMENT,
  `nome_do_local` VARCHAR(45) NOT NULL,
  `numero_local` VARCHAR(6) NULL,
  `endereco_virtual` VARCHAR(255) NULL,
  `complemento_do_local` VARCHAR(255) NULL,
  `status_local_evento` CHAR NOT NULL DEFAULT '1',
  `id_do_endereco` INT NULL,
  PRIMARY KEY (`id_local_de_evento`),
  INDEX `fk_local_de_evento_endereco1_idx` (`id_do_endereco` ASC),
  CONSTRAINT `fk_local_de_evento_endereco1`
    FOREIGN KEY (`id_do_endereco`)
    REFERENCES `Event_Viewer`.`endereco` (`id_endereco`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`usuario` (
  `id_usuario` INT NOT NULL AUTO_INCREMENT,
  `usuario_login` VARCHAR(16) NOT NULL,
  `senha` VARCHAR(32) NOT NULL,
  `status_usuario` CHAR NOT NULL,
  `nome_usuario` VARCHAR(100) NOT NULL,
  `tipo_usuario` CHAR NOT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE INDEX `usuario_UNIQUE` (`usuario_login` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`evento_agendado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`evento_agendado` (
  `id_evento_agendado` INT NOT NULL AUTO_INCREMENT,
  `descricao_evento_agendado` VARCHAR(255) NULL,
  `capacidade_participante` INT NULL,
  `data` DATETIME NOT NULL,
  `horario_encontro` DATETIME NULL,
  `horario_inicio` DATETIME NULL,
  `horario_termino` DATETIME NULL,
  `data_publicacao` DATETIME NULL,
  `status_evento_agendado` CHAR NOT NULL DEFAULT '1',
  `id_do_evento` INT NOT NULL,
  `id_do_local_de_evento` INT NOT NULL,
  `id_do_usuario_organizador` INT NOT NULL,
  PRIMARY KEY (`id_evento_agendado`),
  INDEX `fk_evento_agendado_evento1_idx` (`id_do_evento` ASC),
  INDEX `fk_evento_agendado_local_de_evento1_idx` (`id_do_local_de_evento` ASC),
  INDEX `fk_evento_agendado_usuario1_idx` (`id_do_usuario_organizador` ASC),
  CONSTRAINT `fk_evento_agendado_evento1`
    FOREIGN KEY (`id_do_evento`)
    REFERENCES `Event_Viewer`.`evento` (`id_evento`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_evento_agendado_local_de_evento1`
    FOREIGN KEY (`id_do_local_de_evento`)
    REFERENCES `Event_Viewer`.`local_de_evento` (`id_local_de_evento`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_evento_agendado_usuario1`
    FOREIGN KEY (`id_do_usuario_organizador`)
    REFERENCES `Event_Viewer`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`pessoa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`pessoa` (
  `id_pessoa` INT NOT NULL AUTO_INCREMENT,
  `nome_pessoa` VARCHAR(100) NOT NULL,
  `identidade` VARCHAR(45) NULL,
  `cpf` VARCHAR(11) NULL,
  `telefone_residencial` VARCHAR(20) NULL,
  `telefone_movel` VARCHAR(20) NULL,
  `email` VARCHAR(45) NULL,
  `caminho_foto` MEDIUMBLOB NULL,
  `numero_residencia` VARCHAR(6) NULL,
  `complemento_residencia` VARCHAR(100) NULL,
  `status_pessoa` CHAR NOT NULL DEFAULT '1',
  `id_do_usuario` INT NULL,
  `id_do_endereco` INT NULL,
  PRIMARY KEY (`id_pessoa`),
  INDEX `fk_pessoa_usuario1_idx` (`id_do_usuario` ASC),
  INDEX `fk_pessoa_endereco1_idx` (`id_do_endereco` ASC),
  CONSTRAINT `fk_pessoa_usuario1`
    FOREIGN KEY (`id_do_usuario`)
    REFERENCES `Event_Viewer`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_pessoa_endereco1`
    FOREIGN KEY (`id_do_endereco`)
    REFERENCES `Event_Viewer`.`endereco` (`id_endereco`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`tipo_de_grupo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`tipo_de_grupo` (
  `id_tipo_de_grupo` INT NOT NULL AUTO_INCREMENT,
  `nome_tipo_de_grupo` VARCHAR(45) NOT NULL,
  `descricao_tipo_de_grupo` VARCHAR(255) NULL,
  `status_tipo_de_grupo` CHAR NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_tipo_de_grupo`),
  UNIQUE INDEX `nome_UNIQUE` (`nome_tipo_de_grupo` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`grupo_de_participante`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`grupo_de_participante` (
  `id_grupo_de_participante` INT NOT NULL AUTO_INCREMENT,
  `nome_grupo_de_participante` VARCHAR(45) NOT NULL,
  `descricao_grupo_de_participante` VARCHAR(255) NULL,
  `status_grupo_de_participante` CHAR NOT NULL DEFAULT '1',
  `id_do_tipo_de_grupo` INT NULL,
  PRIMARY KEY (`id_grupo_de_participante`),
  INDEX `fk_grupo_de_participante_tipo_de_grupo1_idx` (`id_do_tipo_de_grupo` ASC),
  CONSTRAINT `fk_grupo_de_participante_tipo_de_grupo1`
    FOREIGN KEY (`id_do_tipo_de_grupo`)
    REFERENCES `Event_Viewer`.`tipo_de_grupo` (`id_tipo_de_grupo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`tipo_de_convidado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`tipo_de_convidado` (
  `id_tipo_de_convidado` INT NOT NULL AUTO_INCREMENT,
  `nome_tipo_convidado` VARCHAR(45) NOT NULL,
  `descricao_tipo_convidado` VARCHAR(255) NULL,
  `status_tipo_convidado` CHAR NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_tipo_de_convidado`),
  UNIQUE INDEX `nome_UNIQUE` (`nome_tipo_convidado` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`convidado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`convidado` (
  `id_convidado` INT NOT NULL AUTO_INCREMENT,
  `nome_convidado` VARCHAR(45) NOT NULL,
  `descricao_convidado` VARCHAR(255) NULL,
  `status_convidado` CHAR NOT NULL DEFAULT '1',
  `id_do_tipo_de_convidado` INT NULL,
  PRIMARY KEY (`id_convidado`),
  INDEX `fk_convidado_tipo_de_convidado1_idx` (`id_do_tipo_de_convidado` ASC),
  CONSTRAINT `fk_convidado_tipo_de_convidado1`
    FOREIGN KEY (`id_do_tipo_de_convidado`)
    REFERENCES `Event_Viewer`.`tipo_de_convidado` (`id_tipo_de_convidado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`grupos_do_evento_agendado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`grupos_do_evento_agendado` (
  `id_do_evento_agendado` INT NOT NULL,
  `id_do_grupo` INT NOT NULL,
  PRIMARY KEY (`id_do_evento_agendado`, `id_do_grupo`),
  INDEX `fk_evento_agendado_has_grupo_de_participante_grupo_de_parti_idx` (`id_do_grupo` ASC),
  INDEX `fk_evento_agendado_has_grupo_de_participante_evento_agendad_idx` (`id_do_evento_agendado` ASC),
  CONSTRAINT `fk_evento_agendado_has_grupo_de_participante_evento_agendado`
    FOREIGN KEY (`id_do_evento_agendado`)
    REFERENCES `Event_Viewer`.`evento_agendado` (`id_evento_agendado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_evento_agendado_has_grupo_de_participante_grupo_de_partici1`
    FOREIGN KEY (`id_do_grupo`)
    REFERENCES `Event_Viewer`.`grupo_de_participante` (`id_grupo_de_participante`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`convidados_do_evento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`convidados_do_evento` (
  `id_do_evento_agendado` INT NOT NULL,
  `id_do_convidado` INT NOT NULL,
  PRIMARY KEY (`id_do_evento_agendado`, `id_do_convidado`),
  INDEX `fk_evento_agendado_has_convidado_convidado1_idx` (`id_do_convidado` ASC),
  INDEX `fk_evento_agendado_has_convidado_evento_agendado1_idx` (`id_do_evento_agendado` ASC),
  CONSTRAINT `fk_evento_agendado_has_convidado_evento_agendado1`
    FOREIGN KEY (`id_do_evento_agendado`)
    REFERENCES `Event_Viewer`.`evento_agendado` (`id_evento_agendado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_evento_agendado_has_convidado_convidado1`
    FOREIGN KEY (`id_do_convidado`)
    REFERENCES `Event_Viewer`.`convidado` (`id_convidado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event_Viewer`.`pessoas_do_grupo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`pessoas_do_grupo` (
  `id_do_grupo_de_participante` INT NOT NULL,
  `id_da_pessoa` INT NOT NULL,
  `contato_no_evento` VARCHAR(100) NULL,
  PRIMARY KEY (`id_do_grupo_de_participante`, `id_da_pessoa`),
  INDEX `fk_grupo_de_participante_has_pessoa_pessoa1_idx` (`id_da_pessoa` ASC),
  INDEX `fk_grupo_de_participante_has_pessoa_grupo_de_participante1_idx` (`id_do_grupo_de_participante` ASC),
  CONSTRAINT `fk_grupo_de_participante_has_pessoa_grupo_de_participante1`
    FOREIGN KEY (`id_do_grupo_de_participante`)
    REFERENCES `Event_Viewer`.`grupo_de_participante` (`id_grupo_de_participante`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_grupo_de_participante_has_pessoa_pessoa1`
    FOREIGN KEY (`id_da_pessoa`)
    REFERENCES `Event_Viewer`.`pessoa` (`id_pessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `Event_Viewer` ;

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_endereco`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_endereco` (`id_endereco` INT, `cep` INT, `logradouro` INT, `status_endereco` INT, `id_do_bairro` INT, `id_bairro` INT, `nome_bairro` INT, `status_bairro` INT, `id_do_municipio` INT, `id_municipio` INT, `nome_municipio` INT, `status_municipio` INT, `id_do_estado` INT, `id_estado` INT, `nome_estado` INT, `status_estado` INT, `id_do_pais` INT, `id_pais` INT, `nome_pais` INT, `status_pais` INT);

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_grupo_participante`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_grupo_participante` (`id_grupo_de_participante` INT, `nome_grupo_de_participante` INT, `descricao_grupo_de_participante` INT, `status_grupo_de_participante` INT, `id_do_tipo_de_grupo` INT, `id_tipo_de_grupo` INT, `nome_tipo_de_grupo` INT, `descricao_tipo_de_grupo` INT, `status_tipo_de_grupo` INT);

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_participantes_do_grupo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_participantes_do_grupo` (`id_do_grupo_de_participante` INT, `id_da_pessoa` INT, `contato_no_evento` INT, `id_pessoa` INT, `nome_pessoa` INT, `identidade` INT, `cpf` INT, `telefone_residencial` INT, `telefone_movel` INT, `email` INT, `caminho_foto` INT, `numero_residencia` INT, `complemento_residencia` INT, `status_pessoa` INT, `id_do_usuario` INT, `id_do_endereco` INT);

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_convidado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_convidado` (`id_convidado` INT, `nome_convidado` INT, `descricao_convidado` INT, `status_convidado` INT, `id_do_tipo_de_convidado` INT, `id_tipo_de_convidado` INT, `nome_tipo_convidado` INT, `descricao_tipo_convidado` INT, `status_tipo_convidado` INT);

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_convidados_do_evento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_convidados_do_evento` (`id_do_evento_agendado` INT, `id_do_convidado` INT);

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_Participante_Completo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_Participante_Completo` (`id_pessoa` INT, `nome_pessoa` INT, `identidade` INT, `cpf` INT, `telefone_residencial` INT, `telefone_movel` INT, `email` INT, `caminho_foto` INT, `numero_residencia` INT, `complemento_residencia` INT, `status_pessoa` INT, `id_do_usuario` INT, `id_do_endereco` INT, `id_endereco` INT, `cep` INT, `logradouro` INT, `status_endereco` INT, `id_do_bairro` INT, `id_bairro` INT, `nome_bairro` INT, `status_bairro` INT, `id_do_municipio` INT, `id_municipio` INT, `nome_municipio` INT, `status_municipio` INT, `id_do_estado` INT, `id_estado` INT, `nome_estado` INT, `status_estado` INT, `id_do_pais` INT, `id_pais` INT, `nome_pais` INT, `status_pais` INT);

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_local_de_evento_completo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_local_de_evento_completo` (`id_local_de_evento` INT, `nome_do_local` INT, `numero_local` INT, `endereco_virtual` INT, `complemento_do_local` INT, `status_local_evento` INT, `id_do_endereco` INT, `id_endereco` INT, `cep` INT, `logradouro` INT, `status_endereco` INT, `id_do_bairro` INT, `id_bairro` INT, `nome_bairro` INT, `status_bairro` INT, `id_do_municipio` INT, `id_municipio` INT, `nome_municipio` INT, `status_municipio` INT, `id_do_estado` INT, `id_estado` INT, `nome_estado` INT, `status_estado` INT, `id_do_pais` INT, `id_pais` INT, `nome_pais` INT, `status_pais` INT);

-- -----------------------------------------------------
-- Placeholder table for view `Event_Viewer`.`view_evento_agendado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Event_Viewer`.`view_evento_agendado` (`id_evento_agendado` INT, `descricao_evento_agendado` INT, `capacidade_participante` INT, `data` INT, `horario_encontro` INT, `horario_inicio` INT, `horario_termino` INT, `data_publicacao` INT, `status_evento_agendado` INT, `id_do_evento` INT, `id_do_local_de_evento` INT, `id_do_usuario_organizador` INT, `id_evento` INT, `nome_evento` INT, `descricao_evento` INT, `logotipo_evento` INT, `status_evento` INT, `id_local_de_evento` INT, `nome_do_local` INT, `numero_local` INT, `endereco_virtual` INT, `complemento_do_local` INT, `status_local_evento` INT, `id_do_endereco` INT, `id_endereco` INT, `cep` INT, `logradouro` INT, `status_endereco` INT, `id_do_bairro` INT, `id_bairro` INT, `nome_bairro` INT, `status_bairro` INT, `id_do_municipio` INT, `id_municipio` INT, `nome_municipio` INT, `status_municipio` INT, `id_do_estado` INT, `id_estado` INT, `nome_estado` INT, `status_estado` INT, `id_do_pais` INT, `id_pais` INT, `nome_pais` INT, `status_pais` INT, `id_usuario` INT, `usuario_login` INT, `senha` INT, `status_usuario` INT, `nome_usuario` INT, `tipo_usuario` INT, `id_do_evento_agendado` INT, `id_do_grupo` INT, `id_grupo_de_participante` INT, `nome_grupo_de_participante` INT, `descricao_grupo_de_participante` INT, `status_grupo_de_participante` INT, `id_do_tipo_de_grupo` INT, `id_convidado` INT, `nome_convidado` INT, `descricao_convidado` INT, `status_convidado` INT, `id_do_tipo_de_convidado` INT);

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_endereco`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_endereco`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `Event_Viewer`.`view_endereco` AS 
select 
    *
from
    endereco
         join
    bairro ON endereco.id_do_bairro = bairro.id_bairro
		 join
	municipio on bairro.id_do_municipio = municipio.id_municipio
		join
	estado on municipio.id_do_estado = estado.id_estado
		join
	pais on estado.id_do_pais = pais.id_pais
;

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_grupo_participante`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_grupo_participante`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `Event_Viewer`.`view_grupo_participante` AS 
select * from grupo_de_participante gp
left join
tipo_de_grupo tg
on
gp.id_do_tipo_de_grupo = tg.id_tipo_de_grupo
;

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_participantes_do_grupo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_participantes_do_grupo`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `Event_Viewer`.`view_participantes_do_grupo` AS
select * from pessoas_do_grupo pg
left join 
pessoa p
on
pg.id_da_pessoa = p.id_pessoa;

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_convidado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_convidado`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `Event_Viewer`.`view_convidado` AS
select 
    *
from
    convidado c
        left join
    tipo_de_convidado tc ON c.id_do_tipo_de_convidado = tc.id_tipo_de_convidado;

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_convidados_do_evento`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_convidados_do_evento`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `Event_Viewer`.`view_convidados_do_evento` AS
select * from convidados_do_evento ce
join
view_convidado vc
on
ce.id_do_convidado = vc.id_convidado;

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_Participante_Completo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_Participante_Completo`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `Event_Viewer`.`view_Participante_Completo` AS


select 
    *
from
	pessoa
		 left join
    endereco ON pessoa.id_do_endereco = endereco.id_endereco
         left join
    bairro ON endereco.id_do_bairro = bairro.id_bairro
		 left join
	municipio on bairro.id_do_municipio = municipio.id_municipio
		left join
	estado on municipio.id_do_estado = estado.id_estado
		left join
	pais on estado.id_do_pais = pais.id_pais

;

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_local_de_evento_completo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_local_de_evento_completo`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `view_local_de_evento_completo` AS
select 
    *
from
	local_de_evento
		 left join
    endereco ON local_de_evento.id_do_endereco = endereco.id_endereco
         left join
    bairro ON endereco.id_do_bairro = bairro.id_bairro
		 left join
	municipio on bairro.id_do_municipio = municipio.id_municipio
		left join
	estado on municipio.id_do_estado = estado.id_estado
		left join
	pais on estado.id_do_pais = pais.id_pais

;

-- -----------------------------------------------------
-- View `Event_Viewer`.`view_evento_agendado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Event_Viewer`.`view_evento_agendado`;
USE `Event_Viewer`;
CREATE  OR REPLACE VIEW `Event_Viewer`.`view_evento_agendado` AS
       SELECT 
        `evento_agendado`.`id_evento_agendado` AS `id_evento_agendado`,
        `evento_agendado`.`descricao_evento_agendado` AS `descricao_evento_agendado`,
        `evento_agendado`.`capacidade_participante` AS `capacidade_participante`,
        `evento_agendado`.`data` AS `data`,
        `evento_agendado`.`horario_encontro` AS `horario_encontro`,
        `evento_agendado`.`horario_inicio` AS `horario_inicio`,
        `evento_agendado`.`horario_termino` AS `horario_termino`,
        `evento_agendado`.`data_publicacao` AS `data_publicacao`,
        `evento_agendado`.`status_evento_agendado` AS `status_evento_agendado`,
        `evento_agendado`.`id_do_evento` AS `id_do_evento`,
        `evento_agendado`.`id_do_local_de_evento` AS `id_do_local_de_evento`,
        `evento_agendado`.`id_do_usuario_organizador` AS `id_do_usuario_organizador`,
        `evento`.`id_evento` AS `id_evento`,
        `evento`.`nome_evento` AS `nome_evento`,
        `evento`.`descricao_evento` AS `descricao_evento`,
        `evento`.`logotipo_evento` AS `logotipo_evento`,
        `evento`.`status_evento` AS `status_evento`,
        `local_de_evento`.`id_local_de_evento` AS `id_local_de_evento`,
        `local_de_evento`.`nome_do_local` AS `nome_do_local`,
        `local_de_evento`.`numero_local` AS `numero_local`,
        `local_de_evento`.`endereco_virtual` AS `endereco_virtual`,
        `local_de_evento`.`complemento_do_local` AS `complemento_do_local`,
        `local_de_evento`.`status_local_evento` AS `status_local_evento`,
        `local_de_evento`.`id_do_endereco` AS `id_do_endereco`,
        `endereco`.`id_endereco` AS `id_endereco`,
        `endereco`.`cep` AS `cep`,
        `endereco`.`logradouro` AS `logradouro`,
        `endereco`.`status_endereco` AS `status_endereco`,
        `endereco`.`id_do_bairro` AS `id_do_bairro`,
        `bairro`.`id_bairro` AS `id_bairro`,
        `bairro`.`nome_bairro` AS `nome_bairro`,
        `bairro`.`status_bairro` AS `status_bairro`,
        `bairro`.`id_do_municipio` AS `id_do_municipio`,
        `municipio`.`id_municipio` AS `id_municipio`,
        `municipio`.`nome_municipio` AS `nome_municipio`,
        `municipio`.`status_municipio` AS `status_municipio`,
        `municipio`.`id_do_estado` AS `id_do_estado`,
        `estado`.`id_estado` AS `id_estado`,
        `estado`.`nome_estado` AS `nome_estado`,
        `estado`.`status_estado` AS `status_estado`,
        `estado`.`id_do_pais` AS `id_do_pais`,
        `pais`.`id_pais` AS `id_pais`,
        `pais`.`nome_pais` AS `nome_pais`,
        `pais`.`status_pais` AS `status_pais`,
        `usuario`.`id_usuario` AS `id_usuario`,
        `usuario`.`usuario_login` AS `usuario_login`,
        `usuario`.`senha` AS `senha`,
        `usuario`.`status_usuario` AS `status_usuario`,
        `usuario`.`nome_usuario` AS `nome_usuario`,
        `usuario`.`tipo_usuario` AS `tipo_usuario`,
        `grupos_do_evento_agendado`.`id_do_evento_agendado` AS `id_do_evento_agendado`,
        `grupos_do_evento_agendado`.`id_do_grupo` AS `id_do_grupo`,
        `grupo_de_participante`.`id_grupo_de_participante` AS `id_grupo_de_participante`,
        `grupo_de_participante`.`nome_grupo_de_participante` AS `nome_grupo_de_participante`,
        `grupo_de_participante`.`descricao_grupo_de_participante` AS `descricao_grupo_de_participante`,
        `grupo_de_participante`.`status_grupo_de_participante` AS `status_grupo_de_participante`,
        `grupo_de_participante`.`id_do_tipo_de_grupo` AS `id_do_tipo_de_grupo`,
        `convidado`.`id_convidado` AS `id_convidado`,
        `convidado`.`nome_convidado` AS `nome_convidado`,
        `convidado`.`descricao_convidado` AS `descricao_convidado`,
        `convidado`.`status_convidado` AS `status_convidado`,
        `convidado`.`id_do_tipo_de_convidado` AS `id_do_tipo_de_convidado`
    FROM
        ((((((((((((`evento_agendado`
        JOIN `evento` ON ((`evento_agendado`.`id_do_evento` = `evento`.`id_evento`)))
        JOIN `local_de_evento` ON ((`evento_agendado`.`id_do_local_de_evento` = `local_de_evento`.`id_local_de_evento`)))
        LEFT JOIN `endereco` ON ((`local_de_evento`.`id_do_endereco` = `endereco`.`id_endereco`)))
        LEFT JOIN `bairro` ON ((`endereco`.`id_do_bairro` = `bairro`.`id_bairro`)))
        LEFT JOIN `municipio` ON ((`bairro`.`id_do_municipio` = `municipio`.`id_municipio`)))
        LEFT JOIN `estado` ON ((`municipio`.`id_do_estado` = `estado`.`id_estado`)))
        LEFT JOIN `pais` ON ((`estado`.`id_do_pais` = `pais`.`id_pais`)))
        LEFT JOIN `usuario` ON ((`evento_agendado`.`id_do_usuario_organizador` = `usuario`.`id_usuario`)))
        LEFT JOIN `grupos_do_evento_agendado` ON ((`grupos_do_evento_agendado`.`id_do_evento_agendado` = `evento_agendado`.`id_evento_agendado`)))
        LEFT JOIN `grupo_de_participante` ON ((`grupos_do_evento_agendado`.`id_do_grupo` = `grupo_de_participante`.`id_grupo_de_participante`)))
        LEFT JOIN `convidados_do_evento` ON ((`convidados_do_evento`.`id_do_evento_agendado` = `evento_agendado`.`id_evento_agendado`)))
        LEFT JOIN `convidado` ON ((`convidados_do_evento`.`id_do_convidado` = `convidado`.`id_convidado`)));

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `Event_Viewer`.`usuario`
-- -----------------------------------------------------
START TRANSACTION;
USE `Event_Viewer`;
INSERT INTO `Event_Viewer`.`usuario` (`id_usuario`, `usuario_login`, `senha`, `status_usuario`, `nome_usuario`, `tipo_usuario`) VALUES (1, 'ADMIN', 'ADMIN', '1', 'ADMIN', '1');

COMMIT;

