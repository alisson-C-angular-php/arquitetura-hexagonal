# Usando a imagem oficial do MySQL
FROM mysql:8.0

# Definindo a variável de ambiente para a senha do root
ENV MYSQL_ROOT_PASSWORD=123456

# Expondo a porta padrão do MySQL
EXPOSE 3306

# Copiando o arquivo create_database.sql para o diretório de inicialização do MySQL
COPY create_database.sql /docker-entrypoint-initdb.d/

# Comando para iniciar o MySQL
CMD ["mysqld"]
