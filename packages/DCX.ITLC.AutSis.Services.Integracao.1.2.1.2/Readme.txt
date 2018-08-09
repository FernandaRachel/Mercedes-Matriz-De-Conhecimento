AutSis
============================================================================================

Essa biblioteca do AutSis é utilizada para obter os dados cadastrados no novo site.

Ambientes:
------------
Quando o pacote é baixado, automaticamente as configurações necessárias para o funcionamento da DLL são inseridas no arquivo de configuração da aplicação.
Para aplicações Web, as informações são colocadas no Web.config, e para aplicações Desktop no app.config.

- EnderecoWebApi
    - Ambiente de desenvolvimento
  
- EnderecoWebApiHomologacao
    - Ambiente de homologação
    
- EnderecoWebApiProducao
    - Ambiente de produção

Para utilizar o ambiente desejado, basta remover o nome do ambiente, após a descrição EnderecoWebApi,
remover as tags de comentário, comentar os outros ambientes e compilar a aplicação.
Para visualizar o log da aplicação basta acessar a pasta C:\ProgramData\MBBRas\AutSis\autsis.log.
Para que o log funcione corretamente é necessário o arquivo log4net.config (na pasta build da biblioteca)
estar na pasta de build da aplicação.
No caso de aplicações Web, quando publicada é necessário colocar este arquivo na pasta de publicação. 

Dúvidas:
-----------
Entre em contato com wendel.estrada@t-systems.com.br, ramal: 625436.