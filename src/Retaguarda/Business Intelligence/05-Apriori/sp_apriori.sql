/*
SQLyog Enterprise - MySQL GUI v8.05 
MySQL - 5.0.51a-community-nt : Database - sispdv
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`sispdv` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `sispdv`;

/*Table structure for table `regras` */

DROP TABLE IF EXISTS `regras`;

CREATE TABLE `regras` (
  `x` varchar(13) NOT NULL,
  `y` varchar(13) NOT NULL,
  `freq_x` float default NULL,
  `freq_xy` float default NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `regras` */

insert  into `regras`(`x`,`y`,`freq_x`,`freq_xy`) values ('0024051000000','0024128000002',42.5,30),('0024128000002','0024051000000',40,30);

/* Procedure structure for procedure `sp_apriori` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_apriori` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_apriori`(IN sup_min INTEGER,IN conf INTEGER, in mes varchar(2), in ano varchar(4))
BEGIN
    DROP TABLE IF EXISTS `1_item`;
    DELETE FROM regras;
    SET @total := (SELECT COUNT(DISTINCT c_venda_codigo  ) AS total 
	FROM d_venda
	 INNER JOIN c_venda ON c_venda.codigo =  d_venda.c_venda_codigo
	WHERE MONTH(c_venda.dataVenda) = mes
	AND YEAR(c_venda.dataVenda) = ano
     );
  
    -- criacao da tabela temporaria para um item 
    CREATE TEMPORARY TABLE 1_item(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    prdno VARCHAR(13) NOT NULL,
    freq FLOAT NOT NULL);
    
   -- inserindo na tabela temporaria os 1 itens de acordo com o suporte minimo definido no parametro de entrada 
    INSERT INTO 1_item(prdno,freq) SELECT produto_codigo,ROUND(100*COUNT(produto_codigo)/@total,2) AS freq
    FROM d_venda    
    INNER JOIN c_venda ON c_venda.codigo =  d_venda.c_venda_codigo
    WHERE MONTH(c_venda.dataVenda) = mes
	AND YEAR(c_venda.dataVenda) = ano
    GROUP BY produto_codigo
    HAVING freq >= sup_min;
   
    SET  @total_aux:= (SELECT COUNT(*) FROM 1_item);
    SET @i:=1;
   
   /* nesse while verifico as associacoes dos 1 itens classificados acima */ 
    WHILE @i <= @total_aux DO
      SET @cod:= (SELECT prdno FROM 1_item WHERE id=@i);
      SET @freqx:= (SELECT freq FROM 1_item WHERE id=@i);
      INSERT INTO regras
	SELECT @cod,produto_codigo,@freqx,ROUND(100*COUNT(produto_codigo)/@total,2) AS freq
	FROM d_venda
		INNER JOIN c_venda ON c_venda.codigo =  d_venda.c_venda_codigo
	WHERE c_venda_codigo  IN(SELECT c_venda_codigo FROM d_venda  WHERE produto_codigo=@cod)      
-- abaixo filtro os produtos que sairam com o outro produto
	AND produto_codigo <> @cod 
	AND MONTH(c_venda.dataVenda) = mes
	AND YEAR(c_venda.dataVenda) = ano
	GROUP BY produto_codigo HAVING freq >=sup_min; 
	SET @i:=@i+1;
    END WHILE;
    
    -- seleciona as regras que estao dentro da confianca passada como parametro, ou seja, deleta aquelas que nao interessam 
    DELETE FROM regras WHERE (freq_xy/freq_x) < (conf/100);
   
 	SELECT p1.descricao AS produto1,
	       p2.descricao AS produto2,
	       freq_x, 
	       freq_xy,
	       SUBSTRING(((freq_xy / freq_x) * 100 ),1,5) AS porcetagem
 	    FROM regras
		LEFT JOIN produto AS p1 ON p1.codigo = regras.x
		LEFT JOIN produto AS p2 ON p2.codigo = regras.y
	;
  END */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
