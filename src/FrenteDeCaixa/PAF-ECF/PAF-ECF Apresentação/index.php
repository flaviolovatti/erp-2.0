<?php
// pega o endere�o do diret�rio
$diretorio = getcwd(); 
// abre o diret�rio
$ponteiro  = opendir($diretorio);
// monta os vetores com os itens encontrados na pasta
while ($nome_itens = readdir($ponteiro)) {
    $itens[] = $nome_itens;
}
// ordena o vetor de itens
sort($itens);
// percorre o vetor para fazer a separacao entre arquivos e pastas 
foreach ($itens as $listar) {
// retira "./" e "../" para que retorne apenas pastas e arquivos
   if ($listar!="." && $listar!=".."){ 

// checa se o tipo de arquivo encontrado � uma pasta
   		if (is_dir($listar)) { 
// caso VERDADEIRO adiciona o item � vari�vel de pastas
			$pastas[]=$listar; 
		} else{ 
// caso FALSO adiciona o item � vari�vel de arquivos
			$arquivos[]=$listar;
		}
   }
}// lista as pastas se houverem
if ($pastas != "" ) { 
foreach($pastas as $listar){
   print "Pasta: <a href='$listar'>$listar</a><br>";}
   }
// lista os arquivos se houverem
if ($arquivos != "") {
foreach($arquivos as $listar){
   print " Arquivo: <a href='$listar'>$listar</a><br>";}
   }
?>
