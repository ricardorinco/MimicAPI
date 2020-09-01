> "Restaurarei o exausto e saciarei o enfraquecido". **Jeremias 31:25 NVI**

# Mimic API

Projeto para um futuro app de mimica.
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/ricardorinco/mimicapi/master?label=LAST%20COMMIT%20&style=for-the-badge)

## :rocket: Tecnologias

Projeto desenvolvido com as seguintes tecnologias:
  
- [.Net Core](https://dotnet.microsoft.com/)
- [SQLite](https://www.sqlite.org/index.html)
- [Docker](https://www.docker.com/)

## :construction_worker: Como rodar

### Visual Studio  

``` bash

# Clonar repositório
$ git clone https://github.com/ricardorinco/mimicapi.git

# Rodar o migration
$ Update-Database

```

### Docker

``` bash

# Criando a imagem
$ docker-compose build

# Subindo um container
docker run -p 32789:80 -p 32788:443 -e Kestrel__Certificates__Default__Path=/app/Infrastructure/Certificate/certificate.pfx -e Kestrel__Certificates__Default__Password={PASSWORD} -e "ASPNETCORE_URLS=https://+;http://+" -v {CERTIFICATE PATH} mimicapi:latest

``

## :memo: License

Esse projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE.md) para mais detalhes.