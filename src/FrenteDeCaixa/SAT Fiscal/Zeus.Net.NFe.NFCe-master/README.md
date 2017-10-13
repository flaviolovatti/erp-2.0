Zeus.Net.NFe.NFCe
=================

Biblioteca gratuita para Geração de NFe 2.0 e 3.10 e NFCe 3.10 e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx

A biblioteca foi desenvolvida com o Visual Studio Community 2013 e é compatível com o Visual Studio Community 2015 e 2015 Update 1.
Está licenciada sobre a LGPL.

**Instruções para compilar a solução**
- No visual studio, abra o arquivo "Zeus NFe.sln", defina o "NFe.AppTeste" como projeto de inicialização, compile e execute.

**Projetos na Solução**
- NFe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Classes: Biblioteca com todas as classes para montagem da NFe/NFCe, de acordo com os manuais vigentes até 14/04/2015;
- NFe.Impressao: Biblioteca que implementa a impressão da NFe/NFCe; 
- NFe.Servicos: Biblioteca que implementa o consumo e retorno dos serviços da NFe/NFCe;
- NFe.Utils: Biblioteca com classes de apoio e extensão para todas as demais bibliotecas;
- NFe.Wsdl: Biblioteca com as classes de serviço wsdl. 

**DANFE**
- Foi implementado em 09/09/2015 a impressão do NFCe em Fast Reports (https://www.fast-report.com/pt/product/fast-report-net/);
- Os recursos implementados na biblioteca de impressão foram: Visualização e impressão direta, além dos recursos de exportação para pdf, xls, doc, etc. do próprio Fast Reports;
- A impressão segue rigorosamente o Manual de Especificacoes Tecnicas do DANFE NFC-e QRCode Versao 3.2);
- Obs: Visando abranger o maior número possível de impressoras térmicas, a impressão é feita via spooler do windows. A impressão térmica via spooler, dependendo da impressora, pode sair com má qualidade. Para sanar isso, no relatório são utlizadas duas fontes condensadas que possuem boa legibilidade em tamanho pequeno, a saber a OpenSans e UbuntuCondensed, ambas de uso livre podendo ser obtidas em https://www.google.com/fonts;
- As fontes estão anexadas ao projeto em NFe.Impressao\NFCe\Fontes;
- Instale as fontes informadas no PC que for imprimir o DANFE da NFCe;
- Impressão testada e funcionando 100% nas impressoras Bematech MP-4200, Daruma DR700 e Epson TM-81 e TM-20.

Exemplo de impressão do DANFE da NFCe utilizando a bilbioteca:

```cs
var proc = new nfeProc().CarregarDeArquivoXml(Caminho_do_arquivo_XML);
var danfe = new DanfeFrNfce(proc, new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, "00001", "XXXXXXXXXXXXXXXXXXXXXXXXXX", null/*Logomarca em byte[]*/));
danfe.Visualizar();
//danfe.Imprimir();
//danfe.ExibirDesign();

```

**TODO:**
- [x] Implementar consumo do serviço NfeDownloadNF;
- [ ] Implementar envio síncrono na versão 3.10;
- [ ] Implementar envio de nfe compactada para a versão 3.10;
- [x] Implementar impressão do DANFE de NFCe Mini. Concluído em 09/09/2015;
- [ ] Implementar impressão do DANFE de NFCe A4;
- [ ] Implementar impressão do DANFE de NFe;
- [ ] Implementar envio de emails;
- [ ] Implementar consumo do serviço NFeDistribuicaoDFe;
- [ ] Implementar consumo do serviço NfeConsultaDest;
- [ ] Implementar Evento de Pedido de Prorrogação da Suspensão do ICMS na Remessa para Industrialização (NT2015/001);
- [x] Implementar "diversas atualizações e melhorias no Sistema da NF-e" (NT2015/002) entre 01/10/15 e 03/11/15(entrada em produção);
- [x] Implementar "Cobrança do ICMS na Operação Interestadual" (NT2015/003) entre 01/10/15 e 03/11/15(entrada em produção);
- [ ] Implementar Serviço Administração do CSC para NFCe;
- [ ] Implementar possíveis mudanças no Manual de Padrões Padrões Técnicos do DANFE-NFC-e e QR Code, versão 3.3 que será obrigatório a partir de  01/09/2016;
- [ ] Alterações no DANFE de NFCe (Adicionar opção para definir o tamanho da logomarca);
- [x] Adicionar opção para determinar se o desconto por item deve ser impresso.
 
**Atenção:**
Quaisquer dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github!

**Telas do aplicativo de demonstração de Uso da biblioteca:**

![](http://www.zeusautomacao.com.br/imagens/git/01.png)
![](http://www.zeusautomacao.com.br/imagens/git/02.png)
![](http://www.zeusautomacao.com.br/imagens/git/03.png)
![](http://www.zeusautomacao.com.br/imagens/git/04.png)
![](http://www.zeusautomacao.com.br/imagens/git/05.png)
![](http://www.zeusautomacao.com.br/imagens/git/06.png)
![](http://www.zeusautomacao.com.br/imagens/git/08.png)
![](http://www.zeusautomacao.com.br/imagens/git/09.png)
![](http://www.zeusautomacao.com.br/imagens/git/10.png)
![](http://www.zeusautomacao.com.br/imagens/git/11.jpg)

