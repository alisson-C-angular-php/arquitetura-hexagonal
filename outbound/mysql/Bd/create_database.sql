CREATE DATABASE IF NOT EXISTS curso;

use curso;

CREATE TABLE aluno (
    id INT DEFAULT NULL,
    nome VARCHAR(200) DEFAULT NULL,
    idade INT DEFAULT NULL,
    curso VARCHAR(200) DEFAULT NULL,
    ra VARCHAR(200) DEFAULT NULL
);






DELIMITER $$

CREATE PROCEDURE sp_select_aluno()
BEGIN
    SELECT id, nome, idade, curso, ra
    FROM aluno;
END$$

DELIMITER ;



DELIMITER $$



CREATE PROCEDURE sp_insert_aluno(     IN p_codigo INT,      IN p_nome VARCHAR(200),     IN p_idade INT,      IN p_curso VARCHAR(200),
  IN p_ra VARCHAR(200) ) BEGIN     INSERT INTO aluno (id, nome, idade, curso, ra)      VALUES (p_codigo, p_nome, p_idade, p_curso, p_ra); END$$

DELIMITER ;







DELIMITER $$


CREATE PROCEDURE sp_update_aluno(     IN p_codigo INT,      IN p_nome VARCHAR(200),     IN p_idade INT,      IN p_curso VARCHAR(200),
  IN p_ra VARCHAR(200) ) BEGIN     update aluno set nome = p_nome, idade = p_idade, curso = p_curso, ra = p_ra     where id = p_codigo; END$$ 



 DELIMITER ;






DELIMITER $$



CREATE PROCEDURE sp_delete_aluno(   
	  IN p_codigo INT 
) 
BEGIN
 DELETE FROM aluno WHERE id = p_codigo; 

END$$

 DELIMITER ;