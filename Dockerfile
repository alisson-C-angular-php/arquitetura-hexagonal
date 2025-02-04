# Usando a imagem oficial do MySQL
FROM mysql:8.0

# Definindo a variável de ambiente para a senha do root
ENV MYSQL_ROOT_PASSWORD=123456

# Expondo a porta padrão do MySQL
EXPOSE 3306

# Comando para iniciar o MySQL
CMD ["mysqld"]
