### Preparando ambiente

1) Instale o Docker
<https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe>

2) Instale o Azure Data Studio
<https://go.microsoft.com/fwlink/?linkid=2204567>

<br/>

### Preparando banco de dados

1) Instale o SqlServer
* Abra o terminal e digite o comando abaixo
> docker run --name=SqlServer2022 --restart=always --network=bridge -p 1433:1433 -e "ACCEPT_EULA=Y" -e TZ=America/Sao_Paulo -e "SA_PASSWORD=Adm#n#str@dor" -v /custom/mount:/var/lib/sqlserver2022/data -d mcr.microsoft.com/mssql/server:2022-latest

2) Crie o banco de dados
* Abra o Azure Data Studio
* Connect no banco de dados SqlServer2022
> Server: localhost <br/>
> User name: sa <br/>
> Password: Adm#n#str@dor

* Abra o arquivo [DataBase.sql](https://github.com/thiagoautran/Desafio.Sorteio/blob/main/DataBase.sql) na raiz do repositorio e execute o script

<br/>

### Criando as imagens dos projetos

1) Baixe o repositorio do projeto

2) Abra o terminal do windows na pasta do repositorio do projeto

3) Execute o comando para criar a imagem da api no docker
> docker build Back -t door.prize.api

4) Execute o comando para criar a imagem da app mvc no docker
> docker build Front -t door.prize.mvc

<br/>

### Publicando as api's no docker

1) Execute o comando para criar o container da api no docker
> docker run -p 8003:80 --network=bridge -e ASPNETCORE_ENVIRONMENT=Docker -e TZ=America/Sao_Paulo --restart=always --name door.prize.api -d door.prize.api

2) Execute o comando para criar o container da app mvc no docker
> docker run -p 8004:80 --network=bridge -e ASPNETCORE_ENVIRONMENT=Development -e TZ=America/Sao_Paulo --restart=always --name door.prize.mvc -d door.prize.mvc

<br/>

### Importando arquivo csv
1) Faça o download do arquivo csv [lista_pessoas.csv](https://github.com/thiagoautran/Desafio.Sorteio/blob/main/lista_pessoas.csv) na raiz do repositorio

2) Importe o arquivo pela rota
> curl --location --request POST 'https://localhost:8003/door.prize/v1/participant' --form 'arquivo=@"/C:/lista_pessoas.csv"'

<br/>

### Acessando projetos publicados

1) Api [www.localhost:8003](http://www.localhost:8003/index.html)

2) MVC [www.localhost:8004](http://www.localhost:8004)
